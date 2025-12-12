using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace SimpleApi.Data;

// 這個靜態類別負責在應用程式啟動時處理資料庫初始化工作（套用 Migration 與建立預設資料）
public static class DbInitializer
{
    // 透過 DI 取得 AppDbContext，執行 Migrate() 確保資料庫結構最新，
    // 並在 Users 資料表為空時插入預設的使用者資料，避免系統啟動後完全沒有資料可用
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        db.Database.Migrate();

        if (!db.Users.Any())
        {
            db.Users.AddRange(
                new User
                {
                    Name = "預設用戶",
                    Phone = "0900000000",
                    Birthday = new DateTime(1990, 1, 1),
                    UpdatedAt = DateTime.UtcNow
                },
                new User
                {
                    Name = "客服人員",
                    Phone = "0911222333",
                    Birthday = new DateTime(1985, 12, 31),
                    UpdatedAt = DateTime.UtcNow
                });

            db.SaveChanges();
        }
    }
}
