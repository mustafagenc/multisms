using System.Runtime.Serialization;

namespace MultiSms.Exceptions;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TOptions"></typeparam>
[Serializable]
public class RequiredOptionException<TOptions> : Exception
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="optionsName"></param>
    /// <param name="message"></param>
    public RequiredOptionException(string optionsName, string message) : base(message)
    {
        OptionsName = optionsName;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected RequiredOptionException(SerializationInfo info, StreamingContext context) : base(info, context)
    {

    }

    /// <summary>
    /// 
    /// </summary>
    public string OptionsName { get; }

    /// <summary>
    /// 
    /// </summary>
    public Type OptionsType => typeof(TOptions);

}

