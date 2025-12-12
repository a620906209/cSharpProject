using System.ComponentModel.DataAnnotations;

namespace SimpleApi.Dtos.Requests;

// 這個 DTO 用於建立使用者（POST /Users）的 Request Body 綁定。
// 目的：讓 API 對外的輸入格式（DTO）與資料庫的 Entity（Data/User）分離，
// 之後即使資料庫欄位調整，也能透過 mapping 控制 API 是否要跟著改。
public class CreateUserRequestDto
{
    [Required(ErrorMessage = "Name 必填")]
    [StringLength(100, ErrorMessage = "Name 長度不可超過 100 字")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Phone 必填")]
    [Phone(ErrorMessage = "Phone 格式不正確")]
    [StringLength(30, ErrorMessage = "Phone 長度不可超過 30 字")]
    public string Phone { get; set; } = string.Empty;

    public DateTime? Birthday { get; set; }
}
