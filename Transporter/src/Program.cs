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

DotNetEnv.Env.Load();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();

builder.Services.AddSwaggerGen();

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


