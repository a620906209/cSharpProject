namespace SimpleApi.Dtos.Requests;

// 這個 DTO 用於 API 的 Request Body 綁定（例如 [FromBody]）。
// 與 Data 底下的 Entity（資料庫模型）或 Models（專案內部模型）區隔，
// 讓 API 對外契約與內部資料結構可以獨立演進。
public class HelloRequestDto
{
    public string Name { get; set; } = string.Empty;

    public int Age { get; set; }
}
