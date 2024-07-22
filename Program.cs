using FlagX0.Web.Application.Interface.UseCases;
using FlagX0.Web.Application.UseCases.Flags;
using FlagX0.Web.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios al contenedor.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                       ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false; // Cambia a 'true' si requieres confirmación de cuenta
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
})
.AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.User.RequireUniqueEmail = true;
});

builder.Services.AddControllersWithViews();

// Agregar proveedor de logs para ver los errores en consola
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Registro de servicios para casos de uso
builder.Services.AddScoped<ICreateFlagApplication, CreateFlagApplication>();
builder.Services.AddScoped<IGetFlagApplication, GetFlagApplication>();
builder.Services.AddScoped<IFlagUserDetails, FlagUserDetails>();
builder.Services.AddScoped<IUpdateFlagApplication, UpdateFlagApplication>();
builder.Services.AddScoped<IDeleteFlagApplication, DeleteFlagApplication>();
builder.Services.AddScoped<IGetPaginatedFlagApplication, GetPaginatedFlagApplication>();
builder.Services.AddScoped<FlagsApplication>();

builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("token", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        In = ParameterLocation.Header,
        Name = HeaderNames.Authorization,
        Scheme = "Bearer"
    });
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "FlagX0 API", Version = "v1" });
});

builder.Services.AddAuthorization();
builder.Services.AddAuthentication().AddCookie("Identity.Bearer");

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();

    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "FlagX0 API V1");
    });
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Método para crear un usuario de prueba
async Task CreateTestUser(IServiceProvider serviceProvider)
{
    var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
    var user = new IdentityUser { UserName = "testuser@example.com", Email = "testuser@example.com" };
    var result = await userManager.CreateAsync(user, "Test@1234");

    if (result.Succeeded)
    {
        // Opcionalmente confirmar el correo del usuario si se requiere confirmación
        // var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
        // await userManager.ConfirmEmailAsync(user, token);
    }
    else
    {
        // Manejar errores
        foreach (var error in result.Errors)
        {
            Console.WriteLine(error.Description);
        }
    }
}

// Asegurar que la base de datos esté actualizada siempre
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    context.Database.Migrate();

    // Crear un usuario de prueba
    await CreateTestUser(scope.ServiceProvider);
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

// Asegúrate de mapear las rutas del controlador personalizado
app.MapControllers();

app.MapGroup("/account").MapIdentityApi<IdentityUser>();

app.Run();
