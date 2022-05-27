using AngularAPI.Dtos.Helpers;
using AngularAPI.Repository;
using AngularAPI.Services;
using AngularProject.Data;
using AngularProject.Models;
using DotNetWebAPI.Middlewares;
using DotNetWebAPI.Services;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGeneration.Design;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "";
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// adding db context service to container
builder.Services.AddDbContext<ShoppingDbContext>( options => options.UseSqlServer(builder.Configuration.GetConnectionString("ShopDbConn")));

// adding usermanager service to container
builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<ShoppingDbContext>();

// add jwt authentication schema services
builder.Services.AddAuthentication( options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer( options => {
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new() {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"])),
    };
});


// adding repository services to the container
builder.Services.AddScoped<IGenericRepository<User>, GenericRepositoryT<User> >();
builder.Services.AddScoped<IProductRepository, ProductService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IWishListRepository, WishListRepository>();



// adding db context service
builder.Services.AddDbContext<ShoppingDbContext>(
    options => options.UseSqlServer(
               builder.Configuration.GetConnectionString("ShopDbConn")
    ));
// Auto Mapper DTO
builder.Services.AddAutoMapper(typeof(MappingProfiles));

// Reference Loop Handling
builder.Services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling =
Newtonsoft.Json.ReferenceLoopHandling.Ignore);

builder.Services.AddScoped<AuthMiddleware>();


// Enable Cores
builder.Services.AddCors(
    options =>
    {
        options.AddPolicy(MyAllowSpecificOrigins,
            builder =>
            {
                builder.AllowAnyOrigin();
                builder.AllowAnyMethod();
                builder.AllowAnyHeader();
            }
            );
    }
    );

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var loggerFactory = scope.ServiceProvider.GetRequiredService<ILoggerFactory>();

    try
    {

        var db = scope.ServiceProvider.GetRequiredService<ShoppingDbContext>();
       // await db.Database.MigrateAsync();
        await ShoppingContextSeed.SeedAsync(db, loggerFactory);

    }
    catch (Exception ex)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex, "An Error occured during migration");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthentication();

app.UseAuthorization();

//  middleware that reads the token and gets current user
app.UseMiddleware<AuthMiddleware>();

app.MapControllers();

app.Run();
