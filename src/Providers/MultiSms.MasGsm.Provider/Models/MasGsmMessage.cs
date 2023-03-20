using System.Text.Json.Serialization;

namespace MultiSms.MasGsm.Provider.Models;

public class MasGsmMessage
{

    [JsonPropertyName("originator")]
    public string Originator { get; set; }

    [JsonPropertyName("message")]
    public string Message { get; set; }

    [JsonPropertyName("encoding")]
    public string Encoding { get; set; }

    [JsonPropertyName("to")]
    public string To { get; set; }

}

