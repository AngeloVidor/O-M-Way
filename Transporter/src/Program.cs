using Microsoft.EntityFrameworkCore;
using src.Application.Mappers;
using src.Application.UseCases.CreateTransporter.Implementations;
using src.Application.UseCases.CreateTransporter.Interfaces;
using src.Application.UseCases.GenerateVerificationCode.Implementations;
using src.Application.UseCases.GenerateVerificationCode.Interfaces;
using src.Application.UseCases.SendVerificationCodeToEmail.Implementations;
using src.Application.UseCases.SendVerificationCodeToEmail.Interfaces;
using src.Infrastructure.Data;
using src.Infrastructure.Repositories.Implementations;
using src.Infrastructure.Repositories.Implementations.VerificationCode;
using src.Infrastructure.Repositories.Interfaces;
using src.Infrastructure.Repositories.Interfaces.VerificationCode;
using DotNetEnv;
using src.Application.UseCases.CheckZipCodeValidity.Implementations;
using src.Application.UseCases.CheckZipCodeValidity.Interfaces;
using src.Application.UseCases.ConsultCNPJ.Interfaces;
using src.Application.UseCases.ConsultCNPJ.Imlementations;
using src.Infrastructure.Repositories.Interfaces.TemporaryData;
using src.Infrastructure.Repositories.Implementations.TemporaryData;
using src.Application.UseCases.AuthenticateTransporter.Interfaces;
using src.Application.UseCases.AuthenticateTransporter.Implementations;
using src.Infrastructure.Repositories.Interfaces.Utility;
using src.Infrastructure.Repositories.Implementations.Utility;
using src.Application.Security.Interface;
using src.Application.Security.Implementation;
using src.Application.Security.Model;
using Microsoft.OpenApi.Models;
using src.Application.UseCases.Utility.Interfaces;
using src.Application.UseCases.Utility.Implementations;

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


builder.Services.AddScoped<ITransporterRepository, TransporterRepository>();
builder.Services.AddScoped<ITransporterService, TransporterService>();
builder.Services.AddScoped<IVerificationCodeRepository, VerificationCodeRepository>();
builder.Services.AddScoped<IVerificationCodeHandler, VerificationCodeHandler>();
builder.Services.AddScoped<ISendVerificationCodeToEmailService, SendVerificationCodeToEmailService>();
builder.Services.AddScoped<MimeKit.MimeMessage>();
builder.Services.AddScoped<IZipCodeValidityCheckerService, ZipCodeValidityCheckerService>();
builder.Services.AddScoped<IConsultCnpjService, ConsultCnpjService>();
builder.Services.AddScoped<ITransporterTemporaryDataRepository, TransporterTemporaryDataRepository>();
builder.Services.AddScoped<IAuthenticateTransporterService, AuthenticateTransporterService>();
builder.Services.AddScoped<IUtilityRepository, UtilityRepository>();
builder.Services.AddScoped<IUtilityService, UtilityService>();
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();

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
builder.Services.AddSingleton<HttpClient>();
builder.Services.AddAutoMapper(typeof(MappingProfile));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapControllers();

app.Run();


