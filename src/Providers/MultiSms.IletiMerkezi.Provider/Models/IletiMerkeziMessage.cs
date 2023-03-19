using System.Text.Json.Serialization;

namespace MultiSms.IletiMerkezi.Provider.Models;

public class IletiMerkeziMessage
{
    [JsonPropertyName("request")]
    public Request request {
        get;
        set;
    }
}

public class Authentication
{
    [JsonPropertyName("username")]
    public string username {
        get;
        set;
    }

    [JsonPropertyName("password")]
    public string password {
        get;
        set;
    }
}

public class Message
{
    [JsonPropertyName("text")]
    public string text {
        get;
        set;
    }

    [JsonPropertyName("receipts")]
    public Receipts receipts {
        get;
        set;
    }
}

public class Order
{
    [JsonPropertyName("sender")]
    public string sender {
        get;
        set;
    }

    [JsonPropertyName("sendDateTime")]
    public List<string> sendDateTime {
        get;
        set;
    }

    [JsonPropertyName("message")]
    public Message message {
        get;
        set;
    }
}

public class Receipts
{
    [JsonPropertyName("number")]
    public List<string> number {
        get;
        set;
    }
}

public class Request
{
    [JsonPropertyName("authentication")]
    public Authentication authentication {
        get;
        set;
    }

    [JsonPropertyName("order")]
    public Order order {
        get;
        set;
    }
}