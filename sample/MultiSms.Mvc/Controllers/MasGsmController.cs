using Microsoft.AspNetCore.Mvc;
using MultiSms.Interfaces;
using MultiSms.MasGsm.Provider;
using MultiSms.Models;

namespace MultiSms.Mvc.Controllers;

[ApiController]
[Route("[controller]")]
public class MasGsmController : ControllerBase
{
    private readonly ILogger<MasGsmController> _logger;
    private readonly ISmsService _smsService;
    private readonly IMasGsmProvider _masGsmProvider;

    public MasGsmController(ILogger<MasGsmController> logger, IMasGsmProvider masGsmProvider, ISmsService smsService)
    {
        _logger = logger;
        _smsService = smsService;
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

}

