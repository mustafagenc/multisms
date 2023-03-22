using Microsoft.AspNetCore.Mvc;
using MultiSms.Factories;
using MultiSms.IletiMerkezi.Provider;
using MultiSms.IletiMerkezi.Provider.Extensions;
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
    [HttpGet("Send1")]
    public async Task<SendingResult> Send1(CancellationToken cancellationToken)
    {
        var message = MessageBody.Compose()
                      .To("+905325321221")
                      .WithContent("test message")
                      .Build();

        return await _smsService.SendAsync(message, cancellationToken);
    }

    [HttpGet("Send2")]
    public async Task<SendingResult> Send2(CancellationToken cancellationToken)
    {
        var message = MessageBody.Compose()
                      .To("+905325321221")
                      .WithContent("test message")
                      .Build();

        return await _iletiMerkeziProvider.SendAsync(message, cancellationToken);
    }

    [HttpGet("Send3")]
    public async Task<SendingResult> Send3(CancellationToken cancellationToken)
    {
        var _smsFactory = MultiSmsServiceFactory.Instance
                          .UseOptions(options =>
        {
            options.DefaultOrginator = "test_orginator";
            options.DefaultProvider = IletiMerkeziProvider.Name;
        })
        .UseIletiMerkezi("test_username", "test_password", "test_orginator")
        .Create();

        var message = MessageBody.Compose()
                      .To("+905325321221")
                      .WithContent("test message")
                      .Build();

        var result = await _smsFactory.SendAsync(message, cancellationToken);

        return result;
    }
}