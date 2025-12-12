using System.ComponentModel.DataAnnotations;

namespace SimpleApi.Dtos.Requests;

// 這個 DTO 用於 API 的 Request Body 綁定（例如 [FromBody]）。
// 與 Data 底下的 Entity（資料庫模型）或 Models（專案內部模型）區隔，
// 讓 API 對外契約與內部資料結構可以獨立演進。
public class HelloRequestDto
{
    [Required(ErrorMessage = "Name 必填")]
    [StringLength(100, ErrorMessage = "Name 長度不可超過 100 字")]
    public string Name { get; set; } = string.Empty;

    [Range(0, 150, ErrorMessage = "Age 必須介於 0 到 150")]
    public int Age { get; set; }
}
