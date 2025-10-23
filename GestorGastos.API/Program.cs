using GestorGastos.API;
using GestorGastos.API.Servicios;
using Microsoft.Extensions.DependencyInjection;
using NLog.Extensions.Logging;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var config = builder.Configuration;

var cadenaConexionSql = new ConexionBaseDatos(config.GetConnectionString("SQL")); //obtenemos la cadena de conexion con nuestra clase personalizada
builder.Services.AddSingleton(cadenaConexionSql);

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

//configuracion de swagger manual
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(cfg => { }, typeof(Program).Assembly); //servicio AutoMapper

builder.Services.AddSingleton<IServicioTipoCuenta, ServicioTipoCuenta>(); //servicio TipoCuenta
builder.Services.AddSingleton<IServicioCuenta, ServicioCuenta>();
builder.Services.AddSingleton<IServicioCategoria, ServicioCategoria>();

builder.Host.ConfigureLogging((hostingContext, logging) =>  //Servicio de Log
{
    logging.AddNLog();
});

//configuracion CORS
var origenesPermitidos = config.GetValue<string>("origenesPermitidos")!.Split(',');
builder.Services.AddCors(opciones =>
{
    opciones.AddDefaultPolicy(opcionesCORS =>
    {
        opcionesCORS.WithOrigins(origenesPermitidos).AllowAnyMethod().AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //configuracion manual
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
