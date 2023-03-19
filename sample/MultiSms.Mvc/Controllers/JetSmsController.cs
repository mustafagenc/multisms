using Microsoft.AspNetCore.Mvc;
using MultiSms.Interfaces;
using MultiSms.JetSms.Provider;
using MultiSms.Models;

namespace MultiSms.Mvc.Controllers;

[ApiController]
[Route("[controller]")]
public class JetSmsController : ControllerBase
{
    private readonly ILogger<JetSmsController> _logger;
    private readonly ISmsService _smsService;
    private readonly IJetSmsProvider _jetSmsProvider;

    public JetSmsController(ILogger<JetSmsController> logger, IJetSmsProvider jetSmsProvider, ISmsService smsService)
    {
        _logger = logger;
        _smsService = smsService;
        _jetSmsProvider = jetSmsProvider;
    }

    [HttpGet]
    public async Task<SendingResult> Send(CancellationToken cancellationToken)
    {
        var message = MessageBody.Compose()
            .To("+905325321221")
            .WithContent("test message")
            .Build();

        return await _jetSmsProvider.SendAsync(message, cancellationToken);
    }
}