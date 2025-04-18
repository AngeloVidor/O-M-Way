using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using src.API.Middlewares;
using src.Application.Mappers;
using src.Application.Security;
using src.Application.UseCases.AddProduct.Implementations;
using src.Application.UseCases.AddProduct.Interfaces;
using src.Application.UseCases.CheckZipCodeValidity.Implementations;
using src.Application.UseCases.CheckZipCodeValidity.Interfaces;
using src.Application.UseCases.CreateLoad.Implementations;
using src.Application.UseCases.CreateLoad.Interfaces;
using src.Application.UseCases.DriverSnapshots.Implementations;
using src.Application.UseCases.DriverSnapshots.Interfaces;
using src.Application.UseCases.ValidateCPF.Implementation;
using src.Application.UseCases.ValidateCPF.Interface;
using src.Infrastructure.Broker.Background;
using src.Infrastructure.Broker.Events.Subscriber.DriverCreated.Implementations;
using src.Infrastructure.Broker.Events.Subscriber.DriverCreated.Interfaces;
using src.Infrastructure.Data;
using src.Infrastructure.Repositories.Implementations.DriverSnapshots;
using src.Infrastructure.Repositories.Implementations.Items;
using src.Infrastructure.Repositories.Implementations.Loading;
using src.Infrastructure.Repositories.Implementations.Loading.Management;
using src.Infrastructure.Repositories.Interfaces.DriverSnapshots;
using src.Infrastructure.Repositories.Interfaces.Loading;
using src.Infrastructure.Repositories.Interfaces.Loading.Management;


DotNetEnv.Env.Load();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    c.AddSecurityDefinition(
        "Bearer",
        new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Enter JWT token in format: Bearer {your_token}",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "Bearer"
        }
    );

    c.AddSecurityRequirement(
        new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] { }
            }
        }
    );
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<ILoadRepository, LoadRepository>();
builder.Services.AddScoped<ILoadService, LoadService>();
builder.Services.AddSingleton<IValidateCpfService, ValidateCpfService>();

builder.Services.AddSingleton<IHostedService, ServiceBackground>();
builder.Services.AddScoped<IDriverEventConsumer, DriverEventConsumer>();
builder.Services.AddScoped<IDriverSnapshotRepository, DriverSnapshotRepository>();
builder.Services.AddScoped<IDriverSnapshotService, DriverSnapshotService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ILoadManagementRepository, LoadManagementRepository>();
builder.Services.AddSingleton<IZipCodeValidityCheckerService, ZipCodeValidityCheckerService>();


builder.Services.AddSingleton<HttpClient>();



builder.Services.AddAutoMapper(typeof(MappingProfile));


var jwtDurationStr = Environment.GetEnvironmentVariable("JWT_DURATION");
var jwtDuration = string.IsNullOrEmpty(jwtDurationStr) ? 30 : int.Parse(jwtDurationStr);

var jwtConfig = new JwtModel
{
    JWT_KEY = Environment.GetEnvironmentVariable("JWT_KEY"),
    JWT_ISSUER = Environment.GetEnvironmentVariable("JWT_ISSUER"),
    JWT_AUDIENCE = Environment.GetEnvironmentVariable("JWT_AUDIENCE"),
    JWT_DurationInMinutes = jwtDuration
};
builder.Services.AddSingleton(jwtConfig);

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtConfig.JWT_ISSUER,
            ValidAudience = jwtConfig.JWT_AUDIENCE,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.JWT_KEY))
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<JwtAuthorizationMiddleware>();

app.UseHttpsRedirection();

app.MapControllers();


app.Run();
