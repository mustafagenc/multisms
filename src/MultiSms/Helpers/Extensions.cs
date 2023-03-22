using System.Xml.Serialization;
using MultiSms.Models;

namespace MultiSms.Helpers;

public static class Extensions {
  public static bool IsValid(this string value) {
    return !(string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value));
  }

  public static ProviderData GetData(this IEnumerable<ProviderData> data,
                                     string key) {
    return data.FirstOrDefault(e => e.Key == key);
  }

  public static string Serialize(this object dataToSerialize) {
    if (dataToSerialize == null)
      return null;

    using StringWriter stringwriter = new();
    var serializer = new XmlSerializer(dataToSerialize.GetType());
    serializer.Serialize(stringwriter, dataToSerialize);
    return stringwriter.ToString();
  }
}
