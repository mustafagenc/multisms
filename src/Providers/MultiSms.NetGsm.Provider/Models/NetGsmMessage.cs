using System.Xml.Serialization;

namespace MultiSms.NetGsm.Provider.Models;

[XmlRoot(ElementName = "mainbody")]
public class NetGsmMessage
{
    [XmlElement(ElementName = "header")]
    public Header Header { get; set; }

    [XmlElement(ElementName = "body")]
    public Body Body { get; set; }
}

[XmlRoot(ElementName = "company")]
public class Company
{
    [XmlAttribute(AttributeName = "dil")]
    public string Dil { get; set; }

    [XmlText]
    public string Text { get; set; }
}

[XmlRoot(ElementName = "header")]
public class Header
{
    [XmlElement(ElementName = "company")]
    public Company Company { get; set; }

    [XmlElement(ElementName = "usercode")]
    public string Usercode { get; set; }

    [XmlElement(ElementName = "password")]
    public string Password { get; set; }

    [XmlElement(ElementName = "type")]
    public string Type { get; set; }

    [XmlElement(ElementName = "msgheader")]
    public string Msgheader { get; set; }
}

[XmlRoot(ElementName = "body")]
public class Body
{
    [XmlElement(ElementName = "msg")]
    public string Msg { get; set; }

    [XmlElement(ElementName = "no")]
    public string No { get; set; }
}