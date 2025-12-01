using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OpLawyers.Components;
using OpLawyers.Components.Account;
using OpLawyers.DAL;
using OpLawyers.Models;
using OpLawyers.Services;
using OpLawyers.Components;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

var connectionString = builder.Configuration.GetConnectionString("SqlConStr") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddScoped<ClientesService>();
builder.Services.AddScoped<CasosService>();
builder.Services.AddScoped<CitasService>();
builder.Services.AddScoped<HorariosService>();

builder.Services.AddDbContext<Contexto>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<Usuario, IdentityRole>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequiredLength = 6;
    })
.AddEntityFrameworkStores<Contexto>()
.AddDefaultTokenProviders();

builder.Services.AddAuthorizationBuilder();

builder.Services.AddSingleton<IEmailSender<Usuario>, IdentityNoOpEmailSender>();

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();



app.Use(async (context, next) =>
{
    if (context.User.Identity?.IsAuthenticated == true)
    {
        var userManager = context.RequestServices.GetRequiredService<UserManager<Usuario>>();
        var clientesService = context.RequestServices.GetRequiredService<ClientesService>();

        var user = await userManager.GetUserAsync(context.User);
        if (user != null)
        {
            var cliente = await clientesService.ObtenerClientePorUsuarioIdAsync(user.Id);

            if (cliente != null && cliente.Estado == "bloqueado")
            {
                var signInManager = context.RequestServices.GetRequiredService<SignInManager<Usuario>>();
                await signInManager.SignOutAsync();
                context.Response.Redirect("/Account/Login");
                return;
            }
        }
    }

    await next();
});

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

app.Run();
