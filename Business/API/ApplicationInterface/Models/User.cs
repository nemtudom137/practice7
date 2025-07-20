using System.Text.Json.Serialization;

namespace Business.API.ApplicationInterface.Models;

public class User
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("username")]
    public string? UserName { get; set; }

    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [JsonPropertyName("address")]
    public Adderss? Adderss { get; set; }

    [JsonPropertyName("phone")]
    public string? Phone { get; set; }

    [JsonPropertyName("website")]
    public string? Website { get; set; }

    [JsonPropertyName("company")]
    public Company? Company { get; set; }

    public bool IsValid() =>
        !(Id == 0 || string.IsNullOrEmpty(Name) ||
        string.IsNullOrEmpty(UserName) ||
        string.IsNullOrEmpty(Email) ||
        Adderss == null ||
        string.IsNullOrEmpty(Phone) ||
        string.IsNullOrEmpty(Website) ||
        Company == null);
}
