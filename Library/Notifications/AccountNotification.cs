using System.Xml.Linq;

namespace Recurly.Notifications
{
    public class AccountNotification : NotificationBase
    {
        public AccountNotification(XDocument document, AccountNotificationType notificationType) : base(document)
        {
            AccountNotificationType = notificationType;
        }

        public AccountNotification(Account account, AccountNotificationType notificationType) : base(account)
        {
            AccountNotificationType = notificationType;
        }

        public AccountNotificationType AccountNotificationType { get; private set; }
    }
}