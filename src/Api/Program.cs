using Api.Extensions;
using Api.Interfaces;
using Api.Services;
using Application;
using Identity;
using Identity.Data;
using Infrastructure;
using Persistence;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddPersistenceServices(builder.Configuration);

builder.Services.AddAuthentication(builder.Configuration);
builder.Services.AddAuthorization();

builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddApplicationServices();
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddInfrastructureServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await IdentitySeeder.SeedAsync(app.Services);

app.Run();
