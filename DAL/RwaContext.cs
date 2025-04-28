using DevExtremeVSTemplateMVC.Models;
using DevExtremeVSTemplateMVC.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace DevExtremeVSTemplateMVC.DAL
{
    public class RwaContext : DbContext
    {
        private readonly IHttpContextAccessor _accessor;
        //private readonly IMemoryCache _cache;

        const double CACHE_TIMEOUT_SECONDS = 10 * 60; // 10 minutes

        public DbSet<EmployeeTask> Tasks { get; set; }
        public DbSet<FilteredTask> FilteredTasks { get; set; }

        public RwaContext(IHttpContextAccessor accessor, IMemoryCache cache) : base(CreateInMemoryOptions(accessor)) {
            _accessor = accessor;
            //_cache = cache;
            SeedFromMasterIfNeeded().Wait();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.EnableSensitiveDataLogging();
        }

        public RwaContext(DbContextOptions<RwaContext> options)
            : base(options) { }

        private RwaContext(DbContextOptions<RwaContext> options, object _)
            : base(options) { }

        private static DbContextOptions<RwaContext> CreateInMemoryOptions(IHttpContextAccessor accessor) {
            var sessionId = accessor.HttpContext?.Session?.Id
                ?? throw new InvalidOperationException("Session not available");

            return new DbContextOptionsBuilder<RwaContext>()
                .UseInMemoryDatabase($"session_{sessionId}")
                .Options;
        }

        private async Task SeedFromMasterIfNeeded() {
            var session = _accessor.HttpContext?.Session
                ?? throw new InvalidOperationException("Session not available");

            if (!session.Keys.Contains("Used")) {
                //var cacheKey = $"seeded_{session.Id}";
                //if (_cache.TryGetValue(cacheKey, out _))
                //    return;

                var masterOptions = new DbContextOptionsBuilder<RwaContext>()
                    .UseSqlite($"Data Source={Constants.DatabasePath}")
                    .Options;

                using var master = new RwaContext(masterOptions, null!); // only used for data access
                var tasks = master.Tasks.AsNoTracking().ToList();
                var filteredTasks = master.FilteredTasks.AsNoTracking().ToList();

                Tasks.AddRange(tasks);
                FilteredTasks.AddRange(filteredTasks);
                // force session to initialize - https://github.com/dotnet/aspnetcore/issues/3228
                session.SetString("Used", "true");
                await SaveChangesAsync();
            }
            //_cache.Set(cacheKey, true, new MemoryCacheEntryOptions {
            //    SlidingExpiration = TimeSpan.FromSeconds(CACHE_TIMEOUT_SECONDS)
            //});
        }
    }
}
