using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Taller.API.Data;
using Taller.API.Datos;
using Taller.API.Datos.Entidades;
using Taller.API.Helpers;
using Vehicles.API.Helpers;

var builder = WebApplication.CreateBuilder(args);

//Condiciones para el inicio de sesion
builder.Services.AddIdentity<User, IdentityRole>(x =>
{
    x.User.RequireUniqueEmail = true;
    x.User.RequireUniqueEmail = true;
    x.Password.RequireDigit = false;
    x.Password.RequiredUniqueChars = 0;
    x.Password.RequireLowercase = false;
    x.Password.RequireNonAlphanumeric = false;
    x.Password.RequireUppercase = false;
}).AddEntityFrameworkStores<DataContext>();

//Esto reemplaza el codigo de Conexion de la version 5 para la conexion
var con = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(con));


//
builder.Services.AddTransient<SeedDb>();
builder.Services.AddScoped<IUserHelper, UserHelper>();
builder.Services.AddScoped<ICombosHelper, CombosHelper>();


// Add services to the container.
builder.Services.AddControllersWithViews();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
