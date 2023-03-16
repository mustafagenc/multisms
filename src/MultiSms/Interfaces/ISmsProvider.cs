using MultiSms.Models;

namespace MultiSms.Interfaces;

public interface ISmsProvider
{
    string Name { get; }

    SendingResult Send(MessageBody message);

    Task<SendingResult> SendAsync(MessageBody message, CancellationToken cancellationToken = default);
}