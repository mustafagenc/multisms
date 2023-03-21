using System.Xml.Serialization;

namespace MultiSms.Mutlucell.Provider.Models;

[XmlRoot(ElementName = "mesaj")]
public class Mesaj
{
    [XmlElement(ElementName = "metin")]
    public string Metin { get; set; }

    [XmlElement(ElementName = "nums")]
    public string Nums { get; set; }
}

[XmlRoot(ElementName = "smspack")]
public class MutlucellMessage
{
    [XmlElement(ElementName = "mesaj")]
    public List<Mesaj> Mesaj { get; set; }

    [XmlAttribute(AttributeName = "ka")]
    public string Ka { get; set; }

    [XmlAttribute(AttributeName = "pwd")]
    public string Pwd { get; set; }

    [XmlAttribute(AttributeName = "org")]
    public string Org { get; set; }

    [XmlText]
    public string Text { get; set; }

    [XmlAttribute(AttributeName = "charset")]
    public string Charset { get; set; }
}