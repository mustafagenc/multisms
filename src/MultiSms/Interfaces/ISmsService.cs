using MultiSms.Models;

namespace MultiSms.Interfaces;

public interface ISmsService {
  SendingResult Send(MessageBody message);

  SendingResult Send(MessageBody message, string providerName);

  SendingResult Send(MessageBody message, ISmsProvider provider);

  Task<SendingResult> SendAsync(MessageBody message,
                                CancellationToken cancellationToken);

  Task<SendingResult> SendAsync(MessageBody message, string providerName,
                                CancellationToken cancellationToken);

  Task<SendingResult> SendAsync(MessageBody message, ISmsProvider provider,
                                CancellationToken cancellationToken);
}
