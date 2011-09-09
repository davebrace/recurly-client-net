using System;
using System.Linq;
using System.Xml.Linq;

namespace Recurly.Notifications
{
    public class NotificationFactory
    {
        public NotificationBase GetTypedNotification(XDocument document)
        {
            XElement firstElement = document.Elements().First();

            switch (firstElement.Name.LocalName)
            {
                case "failed_payment_notification":
                    return new PaymentNotification(document, PaymentNotificationType.Failed);
                case "successful_payment_notification":
                    return new PaymentNotification(document, PaymentNotificationType.Success);
                case "successful_refund_notification":
                    return new PaymentNotification(document, PaymentNotificationType.Refund);
                case "void_payment_notification":
                    return new PaymentNotification(document, PaymentNotificationType.Void);

                case "new_account_notification":
                    return new AccountNotification(document, AccountNotificationType.New);
                case "canceled_account_notification":
                    return new AccountNotification(document, AccountNotificationType.Canceled);
                case "billing_info_updated_notification":
                    return new AccountNotification(document, AccountNotificationType.BillingInfoUpdated);

                case "new_subscription_notification":
                    return new SubscriptionNotification(document, SubscriptionNotificationType.New);
                case "updated_subscription_notification":
                    return new SubscriptionNotification(document, SubscriptionNotificationType.Updated);
                case "expired_subscription_notification":
                    return new SubscriptionNotification(document, SubscriptionNotificationType.Expired);
                case "canceled_subscription_notification":
                    return new SubscriptionNotification(document, SubscriptionNotificationType.Canceled);
                case "renewed_subscription_notification":
                    return new SubscriptionNotification(document, SubscriptionNotificationType.Renewed);
                case "reactivated_subscription_notification":
                    return new SubscriptionNotification(document, SubscriptionNotificationType.Reactivated);
            }

            throw new ArgumentException("Unrecognized notificiation xml.");
        }
    }
}