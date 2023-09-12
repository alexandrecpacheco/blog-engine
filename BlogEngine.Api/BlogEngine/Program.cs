using BlogEngine;
using BlogEngine.Domain;
using BlogEngine.Domain.Intefaces;
using BlogEngine.IoC;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCustomSwagger();


builder.Services.RegisterDependencyInjection();

builder.Services.AddCustomCors();
var apiSettings = new ApiSettings();
builder.Configuration.Bind("BlogEngine", apiSettings);
builder.Services.AddCustomAuthorization();
builder.Services.AddSingleton(apiSettings);
builder.Services.AddCustomAuthentication(apiSettings);

builder.Services.AddLogging();

var app = builder.Build();

app.Services.GetRequiredService<IDatabase>().UpgradeIfNecessary();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseCors(o =>
    {
        o.AllowAnyHeader();
        o.AllowAnyMethod();
        o.AllowAnyOrigin();
    });

    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

