# Multi Sms Provider for .NET

[![Publish](https://github.com/mustafagenc/multisms/actions/workflows/publish.yml/badge.svg)](https://github.com/mustafagenc/multisms/actions/workflows/publish.yml) [![Version](https://img.shields.io/nuget/v/MultiSms?label=Nuget)](https://www.nuget.org/packages/MultiSms) [![MultiSms on fuget.org](https://www.fuget.org/packages/MultiSms/badge.svg)](https://www.fuget.org/packages/MultiSms) [![Download](https://img.shields.io/nuget/dt/MultiSms?label=Downloads&color=green&logo=nuget)](https://www.nuget.org/packages/MultiSms) [![License](https://img.shields.io/github/license/mustafagenc/multisms?label=License)](https://github.com/mustafagenc/multisms/blob/main/LICENSE) 

Multi Sms Providers for .NET is a C# package designed to simplify the process of integrating multiple SMS service providers into your .NET application.
The package is easy to use and includes support for various SMS providers such as Twilio, NetGSM, İleti Merkezi, JetSms, Mutlucell, Mas GSM and SmsVitrini.

## 📞 Providers

- [x] [NetGSM](https://www.nuget.org/packages/MultiSms.NetGsm.Provider/)
- [x] [İleti Merkezi](https://www.nuget.org/packages/MultiSms.IletiMerkezi.Provider/)
- [x] [JetSms](https://www.nuget.org/packages/MultiSms.JetSms.Provider/)
- [x] [Twilio](https://www.nuget.org/packages/MultiSms.Twilio.Provider/)
- [ ] [Mutlucell](https://www.nuget.org/packages/MultiSms.Mutlucell.Provider/)
- [ ] [Mas GSM](https://www.nuget.org/packages/MultiSms.MasGsm.Provider/)
- [ ] [SmsVitrini](https://www.nuget.org/packages/MultiSms.SmsVitrini.Provider/)
- [ ] [Vodafone](#)
- [ ] [Get Sms](#)

## 🔗 Links

- [İleti Yönetim Sistemi](https://iys.org.tr)

## Install
Install ```NuGet\Install-Package MultiSms``` package using [NuGet](https://www.nuget.org/packages/MultiSms) to get started.

## Usage

### Sms Service
```csharp
  var _smsFactory = MultiSmsServiceFactory.Instance.UseOptions(options =>
  {
      options.DefaultOrginator = "test_orginator";
      options.DefaultProvider = NetGsmProvider.Name;
  })
  .UseNetGsm("test_username", "test_password", "test_orginator")
  .Create();

  var message = MessageBody.Compose()
      .To("+905325321221")
      .WithContent("test message")
      .Build();

  var result = await _smsFactory.SendAsync(message);
```

### Dependency Injection
```csharp
builder.Services.AddMultiSms(options =>
{
    options.DefaultProvider = NetGsmProvider.Name;
    options.DefaultOrginator = "test_orginator";
})
.UseNetGsm(
    username: "test_username"
    password: "test_password",
    orginator: "test_orginator"
);
```

```csharp
public class NetGsmController : ControllerBase
{
    private readonly ILogger<NetGsmController> _logger;
    private readonly ISmsService _smsService;

    public NetGsmController(ILogger<NetGsmController> logger, ISmsService smsService)
    {
        _logger = logger;
        _smsService = smsService;
    }

    [HttpGet]
    public async Task<SendingResult> Send(CancellationToken cancellationToken)
    {
        var message = MessageBody.Compose()
            .To("+905325321221")
            .WithContent("test message")
            .Build();

        return await _smsService.SendAsync(message, cancellationToken);
    }
}
```
