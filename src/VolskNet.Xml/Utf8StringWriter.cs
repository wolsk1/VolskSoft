namespace VolskSoft.Bibliotheca.Xml
{
    using System.IO;
    using System.Text;

    public class Utf8StringWriter : StringWriter
    {
        public override Encoding Encoding { get { return Encoding.UTF8; } }
    }
}