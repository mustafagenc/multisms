# Mas Gsm

- [Masgsm Panel](https://panel.masgsm.com.tr/auth/login)
- [Api Dökümantasyonu](https://www.masgsm.com.tr/files/api-dokumantasyonu.pdf)


## Örnek Kullanım
```csharp
private readonly IMasGsmProvider _masGsmProvider;

public MasGsmController(IMasGsmProvider masGsmProvider)
{
    _masGsmProvider = masGsmProvider;
}

[HttpGet]
public async Task<SendingResult> Send(CancellationToken cancellationToken)
{
    var message = MessageBody.Compose()
        .To("+905325321221")
        .WithContent("test message")
        .Build();

    return await _masGsmProvider.SendAsync(message, cancellationToken);
}
```