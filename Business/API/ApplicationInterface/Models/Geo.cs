using System.Text.Json.Serialization;

namespace Business.API.ApplicationInterface.Models;

public class Geo
{
    [JsonPropertyName("lat")]
    public string? Lat { get; set; }

    [JsonPropertyName("lng")]
    public string? Lng { get; set; }
}