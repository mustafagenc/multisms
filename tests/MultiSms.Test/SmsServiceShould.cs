using System.Threading.Channels;
using Moq;
using MultiSms.Exceptions;
using MultiSms.Interfaces;
using MultiSms.Models;
using Xunit;

namespace MultiSms.Test;

public class SmsServiceShould
{
    private const string _provider_name_1 = "mock1_provider";
    private const string _provider_name_2 = "mock2_provider";
    private readonly ISmsProvider _provider1;
    private readonly ISmsProvider _provider2;

    public SmsServiceShould()
    {
        var provider1_mock = new Mock<ISmsProvider>();
        provider1_mock.Setup(e => e.Name).Returns(_provider_name_1);
        provider1_mock.Setup(e => e.Send(It.IsAny<MessageBody>())).Returns(SendingResult.Success(_provider_name_1));
        _provider1 = provider1_mock.Object;

        var provider2_mock = new Mock<ISmsProvider>();
        provider2_mock.Setup(e => e.Name).Returns(_provider_name_2);
        provider2_mock.Setup(e => e.Send(It.IsAny<MessageBody>())).Returns(SendingResult.Success(_provider_name_2));
        _provider2 = provider2_mock.Object;
    }

    [Fact]
    public void ThrowIfProviderNull()
    {
        var options = new MultiSmsServiceOptions();

        Assert.Throws<ArgumentNullException>(() =>
        {
            _ = new SmsService(null, options);
        });
    }

    [Fact]
    public void ThrowIfProviderEmpty()
    {
        var options = new MultiSmsServiceOptions();
        var provider = Array.Empty<ISmsProvider>();

        Assert.Throws<ArgumentException>(() =>
        {
            _ = new SmsService(provider, options);
        });
    }

    [Fact]
    public void ThrowIfOptionsAreNull()
    {
        MultiSmsServiceOptions options = null;

        Assert.Throws<ArgumentNullException>(() =>
        {
            _ = new SmsService(new[] { _provider1 }, options);
        });
    }

    [Fact]
    public void ThrowIfOptionsAreNotValid()
    {
        var options = new MultiSmsServiceOptions();

        Assert.Throws<RequiredOptionException<MultiSmsServiceOptions>>(() =>
        {
            _ = new SmsService(new[] { _provider1 }, options);
        });
    }



}

