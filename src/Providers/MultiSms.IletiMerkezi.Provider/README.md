# Ileti Merkezi Provider [🔗](https://www.iletimerkezi.com/panel/auth/signup)

## Example 1

```csharp
var _smsFactory = MultiSmsServiceFactory.Instance
    .UseOptions(options =>
    {
        options.DefaultOrginator = "test_orginator";
        options.DefaultProvider = IletiMerkeziProvider.Name;
    })
    .UseIletiMerkezi("key", "hash", "orginator")
    .Create();

var message = MessageBody.Compose()
    .To("5325321221")
    .WithContent("test message")
    .Build();

var result = await _smsFactory.SendAsync(message, cancellationToken);
```

## Example 2

```csharp
var message = MessageBody.Compose()
    .To("+5325321221")
    .WithContent("test message")
    .Build();

var result = await _iletiMerkeziProvider.SendAsync(message, cancellationToken);
```

## Example 3

```csharp
var message = MessageBody.Compose()
    .To("+5325321221")
    .WithContent("test message")
    .Build();

var result = await _smsService.SendAsync(message, cancellationToken);
```