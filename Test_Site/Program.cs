



using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test_Site_1.Persistence.Contexts;
using Test_Site_1.Application.Application.Services.Users.Queries.GetRoles;
using Test_Site_1.Application.Interfaces.Contexts;
using Test_Site_1.Application.Services.Users.Commands.EditUser;
using Test_Site_1.Application.Services.Users.Commands.RemoveUser;
using Test_Site_1.Application.Services.Users.Commands.RgegisterUser;
using Test_Site_1.Application.Services.Users.Commands.UserLogin;
using Test_Site_1.Application.Services.Users.Commands.UserSatusChange;
using Test_Site_1.Application.Services.Users.Queries.GetUsers;
using Test_Site_1.Application.Services.Products.FacadPattern;
using Test_Site_1.Application.Interfaces.FacadPatterns;
using Microsoft.CodeAnalysis.Options;
using Microsoft.AspNetCore.Authentication.Cookies;
using Test_Site_1.Application.Services.Common.Queries.GetMenuItem;
using Test_Site_1.Application.Services.Common.Queries.GetCategory;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(Option =>
{
    Option.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    Option.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    Option.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(Option =>
{
    Option.LoginPath = new PathString("/");
    Option.ExpireTimeSpan = TimeSpan.FromMinutes(5.0);
});



// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddScoped<IDataBaseContext, DataBaseContext>();
builder.Services.AddScoped<IGetUsersService, GetUsersService>();
builder.Services.AddScoped<IGetRolesService, GetRolesService>();
builder.Services.AddScoped<IRegisterUserService, RegisterUserService>();
builder.Services.AddScoped<IUserSatusChangeService, UserSatusChangeService>();
builder.Services.AddScoped<IRemoveUserService, RemoveUserService>();
builder.Services.AddScoped<IUserLoginService, UserLoginService>();
builder.Services.AddScoped<IEditUserService, EditUserService>();

builder.Services.AddScoped<IProductFacad, ProductFacad>();


builder.Services.AddScoped<IGetMenuItemService, GetMenuItemService>();
builder.Services.AddScoped<IGetCategoryService, GetCategoryService>();



var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<IDataBaseContext, DataBaseContext>(options => options.UseSqlServer(connectionString));



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

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
);
app.Run();
