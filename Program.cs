using DevExtremeVSTemplateMVC.DAL;
using DevExtremeVSTemplateMVC.Middleware;
using DevExtremeVSTemplateMVC.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var viewBuilder = builder.Services.AddControllersWithViews();
#if DEBUG
builder.Services.AddSassCompiler();
#endif
viewBuilder.AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

if (builder.Environment.IsDevelopment()) {
    viewBuilder.AddRazorRuntimeCompilation();
}

builder.Services.AddHttpClient();

builder.Services.AddMemoryCache();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IDbConnectionAccessor, DbConnectionAccessor>();
builder.Services.AddScoped<DemoDbContext>(provider => {
    var conn = provider.GetRequiredService<IDbConnectionAccessor>().GetConnection();
    var options = new DbContextOptionsBuilder<DemoDbContext>()
        .UseSqlite(conn)
        .Options;

    return new DemoDbContext(options);
});
builder.Services.AddScoped<IDataSeeder, DataSeeder>();
builder.Services.AddSingleton<DataLoadingMonitor>();
builder.Services.AddSession(options => {
    options.IdleTimeout = SessionDbContextMiddleware.CACHE_IDLE_TIMEOUT;
});

var app = builder.Build();

app.Lifetime.ApplicationStarted.Register(async () => {
    using var scope = app.Services.CreateScope();
    var httpClient = scope.ServiceProvider.GetRequiredService<HttpClient>();
    var monitor = scope.ServiceProvider.GetRequiredService<DataLoadingMonitor>();

    await DemoDataFetcher.Download(httpClient, builder.Configuration);
    monitor.SetLoaded();
});

if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.UseSession();

app.UseMiddleware<SessionDbContextMiddleware>();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
