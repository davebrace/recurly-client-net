using System.Xml.Linq;

namespace Recurly.Notifications
{
    public class PaymentNotification : NotificationBase
    {
        public PaymentNotification(XDocument document, PaymentNotificationType type) :
            base(document)
        {
            PaymentNotificationType = type;
            Transaction = new Transaction(document);
        }

        public PaymentNotificationType PaymentNotificationType { get; private set; }
        public Transaction Transaction { get; private set; }
    }
}