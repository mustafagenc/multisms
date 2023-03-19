using System.Text.Json.Serialization;

namespace MultiSms.IletiMerkezi.Provider.Models;

public class IletiMerkeziMessageResponse
{
    [JsonPropertyName("response")]
    public Response response { get; set; }
}

public class Status
{
    [JsonPropertyName("code")]
    public string code { get; set; }

    [JsonPropertyName("message")]
    public string message { get; set; }
}

public class Response
{
    [JsonPropertyName("status")]
    public Status status { get; set; }
}