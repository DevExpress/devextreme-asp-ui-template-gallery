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
builder.Services.AddSession();
builder.Services.AddScoped<RwaContext>();
//builder.Services.AddScoped<RwaContext>(provider => {
//    var accessor = provider.GetRequiredService<IHttpContextAccessor>();
//    var sessionId = accessor.HttpContext?.Session?.Id
//        ?? throw new InvalidOperationException("Session not available");
//    var options = new DbContextOptionsBuilder<RwaContext>()
//        .UseInMemoryDatabase($"session_{sessionId}")
//        .Options;
    
//    RwaContext context = new RwaContext(options);
//    //context.SeedFromMasterIfNeeded(accessor);
//    return context;
//});

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
//app.UseMiddleware<SeedMiddleware>();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
