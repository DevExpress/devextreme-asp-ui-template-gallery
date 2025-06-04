using DevExtremeVSTemplateMVC.DAL;
using DevExtremeVSTemplateMVC.Services;
using DevExtremeVSTemplateMVC.Utils;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace DevExtremeVSTemplateMVC.Middleware
{
    public class SessionDbContextMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IMemoryCache _cache;
        private readonly IServiceProvider _provider;
        private readonly IConfiguration _config;

        public static TimeSpan CACHE_IDLE_TIMEOUT = TimeSpan.FromSeconds(20 * 60); // 20 minutes
        public static TimeSpan CACHE_ABSOLUTE_TIMEOUT = TimeSpan.FromSeconds(120 * 60); // 2 hours
        const string SESSION_KEEP_FLAG = "keep";

        public SessionDbContextMiddleware(RequestDelegate next, IMemoryCache cache, IServiceProvider provider, IConfiguration config) {
            _next = next;
            _cache = cache;
            _provider = provider;
            _config = config;
        }

        public async Task InvokeAsync(HttpContext context) {
            await DemoDataFetcher.DownloadTask;

            var sessionId = context.Session.Id;

            var connection = await _cache.GetOrCreateAsync(sessionId, async entry => {
                var conn = new SqliteConnection("Data Source=:memory:");
                await conn.OpenAsync();

                var options = new DbContextOptionsBuilder<DemoDbContext>()
                .UseSqlite(conn)
                    .Options;

                using var tempContext = new DemoDbContext(options);
                await tempContext.Database.EnsureCreatedAsync();

                using var scope = _provider.CreateScope();
                var seeder = scope.ServiceProvider.GetRequiredService<IDataSeeder>();
                context.Session.SetString(SESSION_KEEP_FLAG, "true");

                string databasePath = string.Format("{0}/{1}",
                        _config.GetValue<string>(ConfigKeys.DatabasePathDirectoryKey),
                        _config.GetValue<string>(ConfigKeys.DatabaseFileNameKey));
                await seeder.SeedFromFileDbAsync(tempContext, databasePath);

                entry.SlidingExpiration = CACHE_IDLE_TIMEOUT;
                entry.AbsoluteExpirationRelativeToNow = CACHE_ABSOLUTE_TIMEOUT;
                entry.RegisterPostEvictionCallback((key, value, reason, state) => {
                    ((SqliteConnection)value).Close();
                });

                return conn;
            });

            context.Items["SqliteConnection"] = connection;

            await _next(context);
        }
    }
}
