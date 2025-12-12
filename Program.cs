using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SimpleApi.Data;
using SimpleApi.Extensions;
using SimpleApi.Services;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;
using DotNetEnv;

// 讀取同層的 .env，讓開發時的環境變數也能注入到 Configuration
DotNetEnv.Env.Load();
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();



// 設定 HTTP 請求管線
if (app.Environment.IsDevelopment())
{
    DbInitializer.Initialize(app.Services);
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

