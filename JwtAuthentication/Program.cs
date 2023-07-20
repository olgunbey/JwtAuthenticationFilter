using JwtAuthentication.Context;
using JwtAuthentication.Filter;
using JwtAuthentication.JwtService;
using JwtAuthentication.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer("Data Source=OLGUNBEY\\OLGUNBEYSQL; Initial Catalog=JwtAuthentication;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"));
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IDbService, DbService>();
builder.Services.AddScoped<IService, Service>();
builder.Services.AddScoped<LoginDto>(opt => new LoginDto());



builder.Services.AddScoped<KontrolModels>(opt => new KontrolModels() { Kontol = false });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
