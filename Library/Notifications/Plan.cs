using System.Linq;
using System.Xml.Linq;

namespace Recurly.Notifications
{
    public class Plan
    {
        public Plan(string code, string name)
        {
            Code = code;
            Name = name;
        }

        public Plan(XElement element)
        {
            XElement plan = element.Descendants("plan").FirstOrDefault();
            if (plan == null) return;

            Code = plan.Element("plan_code").Value;
            Name = plan.Element("name").Value;
        }

        public string Code { get; private set; }
        public string Name { get; private set; }
    }
}