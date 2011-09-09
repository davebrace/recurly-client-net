using System.Xml.Linq;
using NUnit.Framework;
using Recurly.Notifications;
using Recurly.Test.Mock;

namespace Recurly.Test.Notifications
{
    [TestFixture]
    public class NotificationFactoryTests
    {
        #region Setup/Teardown

        [SetUp]
        public void Init()
        {
            _documents = new MockNotificationDocuments();
            _notificationFactory = new NotificationFactory();
        }

        #endregion

        private MockNotificationDocuments _documents;
        private NotificationFactory _notificationFactory;

        private void TestPaymentFactoryResult(string element, PaymentNotificationType type)
        {
            NotificationBase result = _notificationFactory.GetTypedNotification(_documents.GetEmptyDocument(element));
            Assert.IsInstanceOf<PaymentNotification>(result);
            var paymentNotification = (PaymentNotification) result;
            Assert.AreEqual(type, paymentNotification.PaymentNotificationType);
        }

        private void TestAccountFactoryResult(string element, AccountNotificationType type)
        {
            XDocument testDoc = _documents.GetEmptyDocument(element);
            NotificationBase result = _notificationFactory.GetTypedNotification(testDoc);
            Assert.IsInstanceOf<AccountNotification>(result);
            var account = (AccountNotification) result;
            Assert.AreEqual(type, account.AccountNotificationType);
        }

        private void TestSubscriptionFactoryResult(string element, SubscriptionNotificationType type)
        {
            XDocument testDoc = _documents.GetEmptyDocument(element);
            NotificationBase result = _notificationFactory.GetTypedNotification(testDoc);
            Assert.IsInstanceOf<SubscriptionNotification>(result);
            var subscriptionNotification = (SubscriptionNotification) result;
            Assert.AreEqual(type, subscriptionNotification.SubscriptionNotificationType);
        }

        [Test]
        public void TestGetAccountNotifications()
        {
            TestAccountFactoryResult(NotificationFactory.NewAccountNotification, AccountNotificationType.New);
            TestAccountFactoryResult(NotificationFactory.CanceledAccountNotification, AccountNotificationType.Canceled);
            TestAccountFactoryResult(NotificationFactory.BillingInfoUpdatedNotification,
                                     AccountNotificationType.BillingInfoUpdated);
        }

        [Test]
        public void TestGetPaymentNotifications()
        {
            TestPaymentFactoryResult(NotificationFactory.SuccessfulPaymentNotification, PaymentNotificationType.Success);
            TestPaymentFactoryResult(NotificationFactory.FailedPaymentNotification, PaymentNotificationType.Failed);
            TestPaymentFactoryResult(NotificationFactory.SuccessfulRefundNotification, PaymentNotificationType.Refund);
            TestPaymentFactoryResult(NotificationFactory.VoidPaymentNotification, PaymentNotificationType.Void);
        }

        [Test]
        public void TestGetSubscriptionNotification()
        {
            TestSubscriptionFactoryResult(NotificationFactory.CanceledSubscriptionNotification,
                                          SubscriptionNotificationType.Canceled);
            TestSubscriptionFactoryResult(NotificationFactory.ExpiredSubscriptionNotification,
                                          SubscriptionNotificationType.Expired);
            TestSubscriptionFactoryResult(NotificationFactory.NewSubscriptionNotification,
                                          SubscriptionNotificationType.New);
            TestSubscriptionFactoryResult(NotificationFactory.ReactivatedSubscriptionNotification,
                                          SubscriptionNotificationType.Reactivated);
            TestSubscriptionFactoryResult(NotificationFactory.RenewedSubscriptionNotification,
                                          SubscriptionNotificationType.Renewed);
            TestSubscriptionFactoryResult(NotificationFactory.UpdatedSubscriptionNotification,
                                          SubscriptionNotificationType.Updated);
        }
    }
}