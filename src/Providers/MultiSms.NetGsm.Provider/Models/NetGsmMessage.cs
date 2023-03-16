using System;
using Newtonsoft.Json;

namespace MultiSms.NetGsm.Provider.Models;

public partial class NetGsmMessage
{

    [JsonProperty("no")]
    public string Phone { get; set; }

    [JsonProperty("msg")]
    public string Message { get; set; }

    [JsonProperty("msgheader")]
    public string Orginator { get; set; }

}