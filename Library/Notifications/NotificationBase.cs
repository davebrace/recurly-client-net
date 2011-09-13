using System.Xml.Linq;

namespace Recurly.Notifications
{
    public abstract class NotificationBase
    {
        protected NotificationBase(Account account)
        {
            Account = account;
        }

        protected NotificationBase(XDocument document)
        {
            Account = new Account(document);
        }

        public Account Account { get; private set; }
    }
}