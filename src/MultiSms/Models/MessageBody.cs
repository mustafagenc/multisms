using MultiSms.Factories;

namespace MultiSms.Models;

/// <summary>
///
/// </summary>
public class MessageBody {
  /// <summary>
  ///
  /// </summary>
  /// <param name="content"></param>
  /// <param name="originator"></param>
  /// <param name="to"></param>
  /// <exception cref="ArgumentNullException"></exception>
  public MessageBody(string content, string originator, PhoneNumber to,
                     ICollection<ProviderData> providerData) {
    if (to is null) {
      throw new ArgumentNullException(nameof(to));
    }

    Content = content ?? string.Empty;

    Originator = originator;
    To = to;

    ProviderData = providerData ?? new HashSet<ProviderData>();
  }

  /// <summary>
  /// Gonderilecek mesaj
  /// </summary>
  public string Content { get; set; }

  /// <summary>
  /// Gonderen bilgisi
  /// </summary>
  public string Originator { get; set; }

  /// <summary>
  /// Gonderilecek numara
  /// </summary>
  public PhoneNumber To { get; }

  /// <summary>
  ///
  /// </summary>
  public ICollection<ProviderData> ProviderData { get; set; }

  public void SetFrom(string originator) => Originator = originator;

  public static Composer Compose() => new();
}