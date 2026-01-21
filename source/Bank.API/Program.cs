using AutoMapper;
using Bank.API.Token;
using Bank.API.ViewModel;
using Bank.Domain.Entities;
using Bank.Infrastructure.Context;
using Bank.Infrastructure.Interfaces;
using Bank.Infrastructure.Repositories;
using Bank.Services.DTO;
using Bank.Services.interfaces;
using Bank.Services.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddControllers();

#region JWT

var secretKey = builder.Configuration["Jwt:Key"];

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey)),
        ValidateIssuer = false,
        ValidateAudience = false,
    };
});

#endregion



builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();

//builder.Services.AddSwaggerGen();

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
    cfg.CreateMap<UpdateUserViewModel, UserDTO>().ReverseMap();
});

#endregion

builder.Services.AddDbContext<BankContext>(options => options.UseSqlServer(builder.Configuration["ConnectionStrings:BANK"]), ServiceLifetime.Transient);
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITokenGenerator, TokenGenerator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    //app.UseSwagger();
    app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "bank api"));
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

// app.MapGet("/weatherforecast", () =>
// {
//     var forecast =  Enumerable.Range(1, 5).Select(index =>
//         new WeatherForecast
//         (
//             DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//             Random.Shared.Next(-20, 55),
//             summaries[Random.Shared.Next(summaries.Length)]
//         ))
//         .ToArray();
//     return forecast;
// })
// .WithName("GetWeatherForecast");

//app.MapGet("/user", () => new User());
app.Run();

// record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
// {
//     public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
// }
