using Microsoft.AspNetCore.Localization;
using System.Globalization;
using GestionProductos.Web.Hubs; // 🔹 Importante para SignalR

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddViewLocalization()
    .AddDataAnnotationsLocalization();

// Configuración de SignalR (WebSockets)
builder.Services.AddSignalR(); // 🔹 Servicio WebSockets

// Configuración de i18n
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

// Configuración requerida para la función Stateful del Carrito (Sesiones HTTP)
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Idiomas soportados (Español e Inglés)
var supportedCultures = new[] { new CultureInfo("es"), new CultureInfo("en") };

// 🔹 Forzar el punto '.' como separador decimal universal en todas las culturas
foreach (var culture in supportedCultures)
{
    culture.NumberFormat.NumberDecimalSeparator = ".";
    culture.NumberFormat.CurrencyDecimalSeparator = ".";
}

app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("es"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
});

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

// Habilitar el middleware de sesión
app.UseSession();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Producto}/{action=Index}/{id?}")
    .WithStaticAssets();

// 🔹 Mapeo de la ruta del WebSocket de SignalR
app.MapHub<ProductoHub>("/productoHub");

app.Run();