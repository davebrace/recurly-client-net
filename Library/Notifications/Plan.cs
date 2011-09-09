using System.Linq;
using System.Xml.Linq;

namespace Recurly.Notifications
{
    public class Plan
    {
        public Plan(XElement element)
        {
            XElement plan = element.Descendants("plan").FirstOrDefault();
            if (plan == null) return;

            Code = element.Element("plan_code").Value;
            Name = element.Element("name").Value;
        }

        public string Code { get; private set; }
        public string Name { get; private set; }
    }
}