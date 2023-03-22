using MultiSms.Models;
using System.Xml.Linq;

namespace MultiSms.Mutlucell.Provider.Models;

public class MutlucellMessageResponse {
  public static string Result(string key) {
    switch (key) {
    case "20":
      return "Xml eksik veya hatalı.";
    case "21":
      return "Kullanılan originatöre sahip değilsiniz.";
    case "22":
      return "Kontörünüz yetersiz.";
    case "23":
      return "Kullanıcı adı ya da parolanız hatalı.";
    case "24":
      return "Şu anda size ait başka bir işlem aktif.";
    case "25":
      return "SMSC Stopped (Bu hatayı alırsanız, işlemi 1-2 dk sonra tekrar deneyin)";
    case "30":
      return "Hesap Aktivasyonu sağlanmamış";
    default:
      return "";
    }
  }
}