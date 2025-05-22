using DevExtremeVSTemplateMVC.Models;
using DevExtremeVSTemplateMVC.Utils;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace DevExtremeVSTemplateMVC.DAL
{
    public class RwaContext : DbContext
    {
        private readonly IMemoryCache cache;

        public static TimeSpan CACHE_IDLE_TIMEOUT = TimeSpan.FromSeconds(20 * 60); // 20 minutes
        public static TimeSpan CACHE_ABSOLUTE_TIMEOUT = TimeSpan.FromSeconds(120 * 60); // 2 hours
        const string SESSION_KEEP_FLAG = "keep";

        public DbSet<EmployeeTask> Tasks { get; set; }

        public RwaContext(IHttpContextAccessor accessor, IMemoryCache cache) : base(CreateInMemoryOptions(accessor, cache)) {
            this.cache = cache;
            this.Database.EnsureCreated();
            SeedFromMasterIfNeeded(accessor).Wait();
        }

        public RwaContext(DbContextOptions<RwaContext> options)
            : base(options) { }

        private RwaContext(DbContextOptions<RwaContext> options, object _)
            : base(options) { }

        private static DbContextOptions<RwaContext> CreateInMemoryOptions(IHttpContextAccessor accessor, IMemoryCache cache) {
            var sessionId = accessor.HttpContext?.Session?.Id
                ?? throw new InvalidOperationException("Session not available");
            System.Diagnostics.Debug.WriteLine("CREATE: " + sessionId);
            var cacheKey = $"seeded_{sessionId}";
            if (!cache.TryGetValue(cacheKey, out CacheEntry cacheEntry)) {
                SqliteConnection connection = new SqliteConnection("DataSource=:memory:");
                connection.Open();
                var memoryCacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(CACHE_IDLE_TIMEOUT)
                    .SetAbsoluteExpiration(CACHE_ABSOLUTE_TIMEOUT)
                    .RegisterPostEvictionCallback(CacheEntryEvictionCallback, cache);
                cacheEntry = new CacheEntry() { Connection = connection };
                cache.Set(cacheKey, cacheEntry, memoryCacheEntryOptions);
            }
            return new DbContextOptionsBuilder<RwaContext>().UseSqlite(cacheEntry.Connection).Options;
        }

        public async Task SeedFromMasterIfNeeded(IHttpContextAccessor _accessor) {
            var session = _accessor.HttpContext?.Session
                ?? throw new InvalidOperationException("Session is not available");
            var cacheKey = $"seeded_{session.Id}";
            CacheEntry cacheEntry = cache.Get<CacheEntry>(cacheKey);
            if (!cacheEntry.DataPopulated) {
                // force session to initialize - https://github.com/dotnet/aspnetcore/issues/3228
                // or https://learn.microsoft.com/en-us/aspnet/core/fundamentals/app-state?view=aspnetcore-9.0#:~:text=Empty%20sessions%20aren%27t%20retained
                session.SetString(SESSION_KEEP_FLAG, "true");
                var masterOptions = new DbContextOptionsBuilder<RwaContext>().UseSqlite($"Data Source={Constants.DatabasePath}").Options;
                using var master = new RwaContext(masterOptions, null!); // only used for data access
                var tasks = master.Tasks.AsNoTracking().ToList();
                Tasks.AddRange(tasks);
                cacheEntry.DataPopulated = true;
                await SaveChangesAsync();
            }
        }

        private static void CacheEntryEvictionCallback(object cacheKey, object cacheValue, EvictionReason evictionReason, object state) {
            ((CacheEntry)cacheValue).Connection.Close();
        }
    }

    public class CacheEntry {
        public SqliteConnection Connection;
        public bool DataPopulated = false;
    }
}
