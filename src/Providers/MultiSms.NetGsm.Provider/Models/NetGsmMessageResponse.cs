using MultiSms.Models;
using System.Xml.Linq;

namespace MultiSms.NetGsm.Provider.Models;

public class NetGsmMessageResponse
{
    public static string Result(string key)
    {
        switch (key)
        {
        case "20":
            return "Mesaj metninde ki problemden dolayı gönderilemediğini veya standart maksimum mesaj karakter sayısını geçti.";
        case "30":
            return "Geçersiz kullanıcı adı , şifre veya kullanıcınızın API erişim iznininiz bulunmamakta.";
        case "40":
            return "Mesaj başlığınızın (gönderici adınızın) sistemde tanımlı değil.";
        case "70":
            return "Hatalı sorgulama. Gönderdiğiniz parametrelerden birisi hatalı veya zorunlu alanlardan birinin eksik.";
        case "80":
            return "Bilinmeyen bir hata oluştu";
        default:
            return "";
        }
    }
}