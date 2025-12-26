using AutoMapper;
using Bank.API.ViewModel;
using Bank.Domain.Entities;
using Bank.Infrastructure.Context;
using Bank.Infrastructure.Interfaces;
using Bank.Infrastructure.Repositories;
using Bank.Services.DTO;
using Bank.Services.interfaces;
using Bank.Services.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

#region AutoMapper
// var autoMapperConfig = new AutoMapper.MapperConfiguration(cfg =>
// {
//     cfg.CreateMap<User, UserDTO>().ReverseMap();
//     cfg.CreateMap<CreateUserViewModel, UserDTO>().ReverseMap();
// });

// builder.Services.AddSingleton(autoMapperConfig.CreateMapper());

builder.Services.AddAutoMapper(cfg =>
{
    cfg.CreateMap<User, UserDTO>().ReverseMap();
    cfg.CreateMap<CreateUserViewModel, UserDTO>().ReverseMap();
});

#endregion

builder.Services.AddDbContext<BankContext>(options => options.UseSqlServer(builder.Configuration["ConnectionStrings:BANK"]), ServiceLifetime.Transient);
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

//app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
