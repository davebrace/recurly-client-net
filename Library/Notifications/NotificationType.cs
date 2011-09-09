namespace Recurly.Notifications
{
    public enum AccountNotificationType
    {
        New,
        Canceled,
        BillingInfoUpdated
    }

    public enum SubscriptionNotificationType
    {
        New,
        Updated,
        Expired,
        Canceled,
        Renewed,
        Reactivated
    }

    public enum PaymentNotificationType
    {
        Success,
        Failed,
        Refund,
        Void
    }
}