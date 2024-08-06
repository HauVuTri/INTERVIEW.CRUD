using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using INTERVIEW.CRUD.Models;
using INTERVIEW.CRUD.Configs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
var services = builder.Services;


#region register servies
services.Configure<DbConfig>(builder.Configuration.GetSection("DbSetting"));
// services.AddDbContext<BankingContext>(options =>{
//     var connString = builder.Configuration.GetSection("DbSetting");
//     options.UseSqlServer(connString);
// });
services.AddDbContext<BankingContext>(options=>{
    options.UseSqlServer(builder.Configuration.GetConnectionString("BankingDB"));
});
//add swagger Gen
services.AddSwaggerGen();
//add jwt authentication
services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "yourissuer",
        ValidAudience = "youraudience",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your_secret_key"))
    };
});

//add authorization with role-based policy
services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
});

#endregion


var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    // options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    // options.RoutePrefix = string.Empty;
});

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();