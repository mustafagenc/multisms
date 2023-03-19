using MultiSms.Models;
using System.Xml.Serialization;

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

    using (StringWriter stringwriter = new System.IO.StringWriter()) {
      var serializer = new XmlSerializer(dataToSerialize.GetType());
      serializer.Serialize(stringwriter, dataToSerialize);
      return stringwriter.ToString();
    }
  }

  public static T Deserialize<T>(this string xmlText) {
    if (String.IsNullOrWhiteSpace(xmlText))
      return default(T);

    using (StringReader stringReader = new System.IO.StringReader(xmlText)) {
      var serializer = new XmlSerializer(typeof(T));
      return (T)serializer.Deserialize(stringReader);
    }
  }
}
