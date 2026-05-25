using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.
using AutoMapper;
using System;
using agenda_telefonica_back.Data;
using agenda_telefonica_back.Models;
using agenda_telefonica_back.Repositories;
using agenda_telefonica_back.Services;
using agenda_telefonica_back.Repositories.Interfaces;
using agenda_telefonica_back.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

//para o erro de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
        .AllowAnyHeader().AllowAnyMethod();
    });
});

// Configuração de Logging
builder.Logging.AddConsole();

// Adicionar DbContext ao container de DI
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configuração centralizada via IOptions<T>
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

// Registrar Services e Repositories com diferentes lifetimes
builder.Services.AddScoped<IContatoRepository, ContatoRepository>();
builder.Services.AddScoped<IContatoService, ContatoService>();

// Configurar AutoMapper para mapeamento de Entities em DTOs
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Adicionar Controllers
builder.Services.AddControllers();

// Swagger/OpenAPI para documentação da API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//para o erro de CORS
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
