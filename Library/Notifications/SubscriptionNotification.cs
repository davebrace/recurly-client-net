using System.Xml.Linq;

namespace Recurly.Notifications
{
    public class SubscriptionNotification : NotificationBase
    {
        public SubscriptionNotification(Account account, Subscription subscription,
                                        SubscriptionNotificationType notificationType)
            : base(account)
        {
            Subscription = subscription;
            SubscriptionNotificationType = notificationType;
        }

        public SubscriptionNotification(XDocument document, SubscriptionNotificationType notificationType)
            : base(document)
        {
            Subscription = new Subscription(document);
            SubscriptionNotificationType = notificationType;
        }

        public SubscriptionNotificationType SubscriptionNotificationType { get; private set; }

        public Subscription Subscription { get; private set; }
    }
}