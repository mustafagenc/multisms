using Microsoft.AspNetCore.Mvc;
using MultiSms.Interfaces;
using MultiSms.Models;

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
    public SendingResult Send()
    {
        var message = MessageBody.Compose()
            .To("+905325321221")
            .WithContent("test message")
            .Build();

        return _smsService.Send(message);
    }
}