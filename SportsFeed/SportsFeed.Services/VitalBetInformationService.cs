using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

using SportsFeed.Models;
using SportsFeed.Services.Contracts;

namespace SportsFeed.Services
{
    public class VitalBetInformationService : IBetInformationService
    {
        public IEnumerable<Sport> GetData()
        {
            var text = File.ReadAllText("D:\\SampleData.xml");
            var serializer = new XmlSerializer(typeof(XmlSports));
            var memStream = new MemoryStream(Encoding.UTF8.GetBytes(text));
            var result = (XmlSports)serializer.Deserialize(memStream);

            return result.Sports;
        }
    }
}
