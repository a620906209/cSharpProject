using Microsoft.Extensions.Configuration;

namespace SimpleApi.Extensions;

// 這個靜態類別用來擴充 IConfiguration，集中處理「如何組合預設的資料庫連線字串」的邏輯
public static class ConfigurationExtensions
{
    // 從組態 (appsettings + 環境變數) 取得預設連線字串，並用 DB_USER / DB_PASSWORD 兩個環境變數補上帳號密碼
    // 讓整個專案只要呼叫這個方法，就能取得一致規則下產生的資料庫連線字串
    public static string BuildDefaultConnectionString(this IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("缺少 DefaultConnection 連線字串");

        var dbUser = Environment.GetEnvironmentVariable("DB_USER");
        var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");

        if (!string.IsNullOrEmpty(dbUser))
        {
            connectionString += $";User={dbUser}";
        }

        if (dbPassword is not null)
        {
            connectionString += $";Password={dbPassword}";
        }

        return connectionString;
    }
}
