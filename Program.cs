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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
