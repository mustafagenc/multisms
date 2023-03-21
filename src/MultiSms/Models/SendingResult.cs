using System.Text;

namespace MultiSms.Models;

public partial class SendingResult
{
    public bool IsSuccess { get; }

    public string ProviderName { get; }

    public IDictionary<string, object> MetaData { get; }

    public IEnumerable<SendingError> Errors => _errors;

    private readonly HashSet<SendingError> _errors;

    public SendingResult(bool isSuccess, string providerName, params SendingError[] errors)
    {
        IsSuccess = isSuccess;
        MetaData = new Dictionary<string, object>();
        ProviderName = providerName ?? throw new ArgumentNullException(nameof(providerName));
        _errors = new HashSet<SendingError>(errors);
    }

    public override string ToString()
    {
        var stringbuilder = new StringBuilder($"{ProviderName} -> sending: ");

        if (IsSuccess)
        {
            stringbuilder.Append("Succeeded");
        }
        else
        {
            stringbuilder.Append("Failed");
        }

        if (!(_errors is null) && _errors.Count != 0)
        {
            stringbuilder.Append($" | {_errors.Count} errors");
        }

        if (MetaData.Count != 0)
        {
            stringbuilder.Append($" | {MetaData.Count} meta-data");
        }

        return stringbuilder.ToString();
    }

    public SendingResult AddError(SendingError error)
    {
        if (error is null)
        {
            throw new ArgumentNullException(nameof(error));
        }

        _errors.Add(error);
        return this;
    }

    public SendingResult AddError(Exception exception)
    {
        return AddError(new SendingError(exception));
    }

    public SendingResult AddMetaData(string key, object value)
    {
        MetaData.Add(key, value);
        return this;
    }

    public TValue GetMetaData<TValue>(string key, TValue defaultValue = default)
    {
        if (MetaData.TryGetValue(key, out object value))
        {
            return (TValue)value;
        }

        return defaultValue;
    }

    public static SendingResult Success(string providerName)
    {
        return new SendingResult(true, providerName);
    }

    public static SendingResult Failure(string providerName, params SendingError[] errors)
    {
        return new(false, providerName);
    }
}