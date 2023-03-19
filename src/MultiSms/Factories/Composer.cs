using MultiSms.Models;

namespace MultiSms.Factories;

public class Composer {
  private PhoneNumber _to;
  private string _orginator;
  private string _bodyContent;
  private readonly HashSet<ProviderData> _providerData;

  public Composer() { _providerData = new HashSet<ProviderData>(); }

  public Composer WithContent(string messageContent) {
    _bodyContent = messageContent;
    return this;
  }

  public Composer Orginator(string orginator) {
    if (orginator is null)
      throw new ArgumentNullException(nameof(orginator));

    _orginator = orginator;
    return this;
  }

  public Composer
  To(string phoneNumber) => To(new PhoneNumber(phoneNumber.Trim()));

  public Composer To(PhoneNumber phoneNumber) {
    if (phoneNumber is null)
      throw new ArgumentNullException(nameof(phoneNumber));

    _to = phoneNumber;
    return this;
  }

  public Composer PassProviderData(string key, object value) =>
      PassProviderData(new ProviderData(key, value));

  public Composer PassProviderData(params ProviderData[] data) {
    foreach (var item in data)
      _providerData.Add(item);

    return this;
  }

  public MessageBody Build() => new MessageBody(_bodyContent, _orginator, _to,
                                                _providerData);
}
