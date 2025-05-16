using DevExtremeVSTemplateMVC.Models;
using DevExtremeVSTemplateMVC.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace DevExtremeVSTemplateMVC.DAL
{
    public class RwaContext : DbContext
    {
        private readonly IHttpContextAccessor _accessor;
        //private readonly IMemoryCache _cache;

        const double CACHE_TIMEOUT_SECONDS = 10 * 60; // 10 minutes
        const string SESSION_USAGE_NAME = "RESERVED";

        public DbSet<EmployeeTask> Tasks { get; set; }
        public DbSet<FilteredTask> FilteredTasks { get; set; }

        public RwaContext(IHttpContextAccessor accessor, IMemoryCache cache) : base(CreateInMemoryOptions(accessor)) {
            _accessor = accessor;
            //_cache = cache;
            //this.Database.EnsureDeleted();
            System.Diagnostics.Debug.WriteLine("CREATED: " + this.GetHashCode());
            SeedFromMasterIfNeeded(accessor).Wait();
        }

        public RwaContext(DbContextOptions<RwaContext> options)
            : base(options) {

            System.Diagnostics.Debug.WriteLine("CREATED: " + this.GetHashCode());
        }

        private RwaContext(DbContextOptions<RwaContext> options, object _)
            : base(options) {

            System.Diagnostics.Debug.WriteLine("CREATED: " + this.GetHashCode());
        }

        private static DbContextOptions<RwaContext> CreateInMemoryOptions(IHttpContextAccessor accessor) {
            var sessionId = accessor.HttpContext?.Session?.Id
                ?? throw new InvalidOperationException("Session not available");

            return new DbContextOptionsBuilder<RwaContext>()
                .UseInMemoryDatabase($"session_{sessionId}")
                .Options;
        }

        public async Task SeedFromMasterIfNeeded(IHttpContextAccessor _accessor) {
            var session = _accessor.HttpContext?.Session
                ?? throw new InvalidOperationException("Session not available");

            if (!session.Keys.Contains(SESSION_USAGE_NAME)) {
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
                session.SetString(SESSION_USAGE_NAME, "true");
                await SaveChangesAsync();
            }
            //_cache.Set(cacheKey, true, new MemoryCacheEntryOptions {
            //    SlidingExpiration = TimeSpan.FromSeconds(CACHE_TIMEOUT_SECONDS)
            //});
        }
        public override void Dispose() {
            System.Diagnostics.Debug.WriteLine("DISPOSED: " + this.GetHashCode());
            base.Dispose();
        }
        public override ValueTask DisposeAsync() {
            System.Diagnostics.Debug.WriteLine("DISPOSED: " + this.GetHashCode());
            return base.DisposeAsync();
        }
    }
}
