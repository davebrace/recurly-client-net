using System.Xml.Linq;

namespace Recurly.Notifications
{
    public class AccountNotification : NotificationBase
    {
        public AccountNotificationType NotificationType { get; private set; }

        public AccountNotification(XDocument document, AccountNotificationType notificationType) : base(document)
        {
            NotificationType = notificationType;
        }
    }
}
