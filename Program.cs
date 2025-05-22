using DevExtremeVSTemplateMVC.DAL;
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
builder.Services.AddSession(options => {
    options.IdleTimeout = RwaContext.CACHE_IDLE_TIMEOUT;
});
builder.Services.AddScoped<RwaContext>();

var app = builder.Build();



app.Lifetime.ApplicationStarted.Register(async () => {
    using var scope = app.Services.CreateScope();
    var httpClient = scope.ServiceProvider.GetRequiredService<HttpClient>();
    await DatabaseFromRemoteService.Download(httpClient);
});

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
