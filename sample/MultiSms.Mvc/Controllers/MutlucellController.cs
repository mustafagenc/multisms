using Microsoft.AspNetCore.Mvc;
using MultiSms.Interfaces;
using MultiSms.Models;
using MultiSms.Mutlucell.Provider;

namespace MultiSms.Mvc.Controllers;

[ApiController]
[Route("[controller]")]
public class MutlucellController : ControllerBase
{
    private readonly ILogger<MutlucellController> _logger;
    private readonly ISmsService _smsService;
    private readonly IMutlucellProvider _mutlucellProvider;

    public MutlucellController(ILogger<MutlucellController> logger, IMutlucellProvider mutlucellProvider, ISmsService smsService)
    {
        _logger = logger;
        _smsService = smsService;
        _mutlucellProvider = mutlucellProvider;
    }

    [HttpGet]
    public async Task<SendingResult> Send(CancellationToken cancellationToken)
    {
        var message = MessageBody.Compose()
            .To("+905325321221")
            .WithContent("test message")
            .Build();

        return await _mutlucellProvider.SendAsync(message, cancellationToken);
    }
}
