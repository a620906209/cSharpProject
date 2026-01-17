using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
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

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();

        var problemDetails = new ProblemDetails
        {
            Title = "伺服器發生未預期錯誤",
            Status = StatusCodes.Status500InternalServerError,
            Instance = context.Request.Path
        };

        problemDetails.Extensions["traceId"] = context.TraceIdentifier;
        if (exceptionHandlerFeature?.Error is not null)
        {
            problemDetails.Extensions["error"] = exceptionHandlerFeature.Error.Message;
        }

        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = "application/problem+json";
        await context.Response.WriteAsJsonAsync(problemDetails);
    });
});

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

