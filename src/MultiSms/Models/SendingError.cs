namespace MultiSms.Models;

public class SendingError
{
    public SendingError(Exception exception)
    {
        Code = "error";
        Message = "belirlenemeyen hata...";
        Exception = exception ?? throw new ArgumentNullException(nameof(exception));
    }

    public SendingError(string code, string message)
    {
        Code = code ?? throw new ArgumentNullException(nameof(code));
        Message = message ?? throw new ArgumentNullException(nameof(message));
    }

    public SendingError(string code, string message, Exception exception)
    {
        Code = code ?? throw new ArgumentNullException(nameof(code));
        Message = message ?? throw new ArgumentNullException(nameof(message));
        Exception = exception ?? throw new ArgumentNullException(nameof(exception));
    }

    public string Code { get; }

    public string Message { get; }

    public Exception Exception { get; }

    public override string ToString() => $"{Code} | {Message}";
}

