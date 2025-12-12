using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using SimpleApi.Data;
using SimpleApi.Repositories;
using SimpleApi.Services;

namespace SimpleApi.Extensions;

// 這個靜態類別用來擴充 IServiceCollection，集中註冊本專案會用到的服務（MVC、Swagger、DbContext、自訂 Service、Repository 等）
public static class ServiceCollectionExtensions
{
    // 將應用程式需要的所有服務一次性註冊進 DI 容器，讓 Program.cs 啟動程式碼更精簡
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        // MVC 與 Swagger
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "SimpleApi 練習 API",
                Version = "v1",
                Description = "這是一個用來練習 ASP.NET Core Web API 與 Swagger 的簡單範例專案。"
            });
        });

        // 連線字串與 DB
        var connectionString = configuration.BuildDefaultConnectionString();
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        });

        // 自訂 service
        services.AddScoped<IUserGreetingService, UserGreetingService>();
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}
