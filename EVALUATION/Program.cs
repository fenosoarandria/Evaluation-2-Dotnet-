using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


// Liaison base de données
builder.Services.AddDbContext<ApplicationDbContext>
    (options =>options.UseNpgsql(builder.Configuration.GetConnectionString("ApplicationDbContext")));

//----------------------------------------------
// Récupérer l'assembly de l'application
var assembly = Assembly.GetExecutingAssembly();

// Récupérer tous les types de classe non abstraits dans l'assembly
var serviceTypes = assembly.GetTypes()
    .Where(type => type.IsClass && !type.IsAbstract && !type.IsGenericTypeDefinition);

// Enregistrer tous les services trouvés dans le conteneur DI
foreach (var serviceType in serviceTypes)
{
    // Ici, vous pouvez spécifier la portée du service selon vos besoins (Scoped, Transient, Singleton)
    builder.Services.AddScoped(serviceType);
}


builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10000); // Réglez la durée d'expiration de la session selon vos besoins.
});

// Configure the HTTP request pipeline.
builder.Services.AddControllersWithViews();

var app = builder.Build();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
// Middleware pour gérer les cookies et la session
app.UseCookiePolicy();
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Connexion}/{action=LoginEquipe}/{id?}");

app.Run();
