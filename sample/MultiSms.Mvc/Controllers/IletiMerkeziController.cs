using Microsoft.AspNetCore.Mvc;
using MultiSms.IletiMerkezi.Provider;
using MultiSms.Interfaces;
using MultiSms.Models;

namespace MultiSms.Mvc.Controllers;

[ApiController]
[Route("[controller]")]
public class IletiMerkeziController : ControllerBase
{
    private readonly ILogger<IletiMerkeziController> _logger;
    private readonly ISmsService _smsService;
    private readonly IIletiMerkeziProvider _iletiMerkeziProvider;

    public IletiMerkeziController(ILogger<IletiMerkeziController> logger, IIletiMerkeziProvider iletiMerkeziProvider, ISmsService smsService)
    {
        _logger = logger;
        _smsService = smsService;
        _iletiMerkeziProvider = iletiMerkeziProvider;
    }

    /// <summary>
    /// Default Provider
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<SendingResult> Send1(CancellationToken cancellationToken)
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
        var message = MessageBody.Compose()
            .To("+905325321221")
            .WithContent("test message")
            .Build();

        return await _iletiMerkeziProvider.SendAsync(message, cancellationToken);
    }

}