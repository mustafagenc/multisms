namespace MultiSms.JetSms.Provider.Models;

public class JetSmsMessageResponse {
  public static string Result(string key) {
    switch (key) {
    case "-5":
      return "Login hatası: Username, Password, Orginator uyumsuzluğu.";
    case "-6":
      return "Girilen bir kısım veride hata oluştu.";
    case "-7":
      return "SendDate bugünden büyük ve geçerli bir tarih olmalıdır.";
    case "-8":
      return "En azından bir Msisdn bilgisi verilmelidir.";
    case "-9":
      return "En azından bir Message değeri verilmelidir.";
    case "-10":
      return "Birden fazla Msisdn e farklı mesaj gönderimi için, Msisdn ve Message sayıları aynı olmadır.";
    case "-15":
      return "Sistem hatası";
    case "-99":
      return "Bilinmeyen Hata";
    default:
      return "";
    }
  }
}
