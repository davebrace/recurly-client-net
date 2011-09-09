using System;
using System.Linq;
using System.Xml.Linq;

namespace Recurly.Notifications
{
    public class NotificationFactory
    {
        internal const string FailedPaymentNotification = "failed_payment_notification";
        internal const string SuccessfulPaymentNotification = "successful_payment_notification";
        internal const string SuccessfulRefundNotification = "successful_refund_notification";
        internal const string VoidPaymentNotification = "void_payment_notification";
        internal const string NewAccountNotification = "new_account_notification";
        internal const string CanceledAccountNotification = "canceled_account_notification";
        internal const string BillingInfoUpdatedNotification = "billing_info_updated_notification";
        internal const string NewSubscriptionNotification = "new_subscription_notification";
        internal const string UpdatedSubscriptionNotification = "updated_subscription_notification";
        internal const string ExpiredSubscriptionNotification = "expired_subscription_notification";
        internal const string CanceledSubscriptionNotification = "canceled_subscription_notification";
        internal const string RenewedSubscriptionNotification = "renewed_subscription_notification";
        internal const string ReactivatedSubscriptionNotification = "reactivated_subscription_notification";

        public NotificationBase GetTypedNotification(XDocument document)
        {
            XElement firstElement = document.Elements().First();

            switch (firstElement.Name.LocalName)
            {
                case FailedPaymentNotification:
                    return new PaymentNotification(document, PaymentNotificationType.Failed);
                case SuccessfulPaymentNotification:
                    return new PaymentNotification(document, PaymentNotificationType.Success);
                case SuccessfulRefundNotification:
                    return new PaymentNotification(document, PaymentNotificationType.Refund);
                case VoidPaymentNotification:
                    return new PaymentNotification(document, PaymentNotificationType.Void);

                case NewAccountNotification:
                    return new AccountNotification(document, AccountNotificationType.New);
                case CanceledAccountNotification:
                    return new AccountNotification(document, AccountNotificationType.Canceled);
                case BillingInfoUpdatedNotification:
                    return new AccountNotification(document, AccountNotificationType.BillingInfoUpdated);

                case NewSubscriptionNotification:
                    return new SubscriptionNotification(document, SubscriptionNotificationType.New);
                case UpdatedSubscriptionNotification:
                    return new SubscriptionNotification(document, SubscriptionNotificationType.Updated);
                case ExpiredSubscriptionNotification:
                    return new SubscriptionNotification(document, SubscriptionNotificationType.Expired);
                case CanceledSubscriptionNotification:
                    return new SubscriptionNotification(document, SubscriptionNotificationType.Canceled);
                case RenewedSubscriptionNotification:
                    return new SubscriptionNotification(document, SubscriptionNotificationType.Renewed);
                case ReactivatedSubscriptionNotification:
                    return new SubscriptionNotification(document, SubscriptionNotificationType.Reactivated);
            }

            throw new ArgumentException("Unrecognized notificiation xml.");
        }
    }
}