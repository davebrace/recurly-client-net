using System.Xml.Linq;

namespace Recurly.Notifications
{
    public class AccountNotification : NotificationBase
    {
        public AccountNotificationType AccountNotificationType { get; private set; }

        public AccountNotification(XDocument document, AccountNotificationType notificationType) : base(document)
        {
            AccountNotificationType = notificationType;
        }
    }
}
