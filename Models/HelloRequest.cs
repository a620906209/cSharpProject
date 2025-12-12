namespace SimpleApi.Models;

// 這個模型用於示範「資料模型 / 專案內部模型」的放置方式。
// 目前專案的 API Request 主要使用 Dtos/Requests 底下的 DTO，
// 此檔案僅用來對照：Model 與 DTO 的職責與資料夾結構通常會分開。
public class HelloRequest
{
    public string Name { get; set; } = string.Empty;

    public int Age { get; set; }
}
