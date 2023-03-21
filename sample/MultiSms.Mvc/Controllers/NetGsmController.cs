using Microsoft.AspNetCore.Mvc;
using MultiSms.Factories;
using MultiSms.Interfaces;
using MultiSms.Models;
using MultiSms.NetGsm.Provider;
using MultiSms.NetGsm.Provider.Extensions;

namespace MultiSms.Mvc.Controllers;

[ApiController]
[Route("[controller]")]
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

    [HttpGet]
    public async Task<SendingResult> Send2(CancellationToken cancellationToken)
    {
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

        var result = await _smsFactory.SendAsync(message, cancellationToken);

        return result;
    }

}
