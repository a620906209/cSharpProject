# SimpleApi

範例 ASP.NET Core Web API，示範：
- Swagger 文件（`/swagger`）
- 多種 route/parameter 綁定（Route、Query、Body）
- 透過 EF Core + Pomelo 連 Laragon 的 MySQL

## 本機環境需求
1. .NET 6 SDK
2. Laragon 開啟 MySQL（預設 `127.0.0.1:3306`，root 無密碼）
3. `dotnet-ef` 工具（`dotnet tool install --global dotnet-ef`）

## 設定與啟動
1. 複製 `appsettings.json` 中的連線字串 (DefaultConnection) 對應你的 MySQL 帳密。預設指向 `SimpleApiDb`。
2. 還原套件並建置：
   ```bash
   dotnet restore
   dotnet build
   ```
3. 建立 migration + 套用資料庫
   ```bash
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```
4. 啟動 API
   ```bash
   dotnet watch run
   ```
5. 開啟 Swagger UI：`http://localhost:5000/swagger`。

## 目前可用的 endpoint
| Method | Route | 說明 |
| --- | --- | --- |
| GET | `/hello` | 回傳簡單訊息 |
| GET | `/hello/{name}` | 路由參數 |
| GET | `/hello/greet?name=&age=` | Query 參數 |
| GET | `/hello/time` | 回傳 server 時間 |
| POST | `/hello/create` | JSON body → `HelloRequest` |
| POST | `/hello/from-body` | JSON body，只示範來源 |
| GET | `/hello/from-route/{name}` | 顯式 `[FromRoute]` |
| GET | `/hello/from-query` | 顯式 `[FromQuery]` |
| GET | `/hello/messages` | 讀取 `HelloMessages` 資料表 |
| POST | `/hello/messages` | 寫入 MySQL（`HelloRequest`） |

> `HelloRequest` 模型：
> ```json
> {
>   "name": "string",
>   "age": 0
> }
> ```

## Postman / Swagger 測試
1. 進入 Swagger UI，點 `POST /hello/messages`，填入範例 JSON，按 `Execute`。
2. 再用 `GET /hello/messages` 確認資料已存入 MySQL。若用 Postman：
   - URL：`http://localhost:5000/hello/messages`
   - Header：`Content-Type: application/json`
   - Body（raw JSON）：
     ```json
     {
       "name": "小明",
       "age": 25
     }
     ```

## 資料庫變更流程
1. 修改 `HelloMessage` 或新資料模型。
2. `dotnet ef migrations add <描述性名稱>`。
3. `dotnet ef database update`。
4. 若需要回退 migration，用 `dotnet ef migrations remove`。

需要我幫你寫 `.env`、資料表 seed 或 CI 指令也可以再說。
