using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DotNetReact.Model;

public class Error
{
    [Required]
    [JsonPropertyName("code")]
    public string Code { get; set; } = "";

    [Required]
    [JsonPropertyName("message")]
    public string Message { get; set; } = "";
}