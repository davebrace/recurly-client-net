using System;
using NUnit.Framework;
using Recurly.Notifications;
using Recurly.Test.Mock;

namespace Recurly.Test.Notifications
{
    [TestFixture]
    public class PaymentNotificationTests
    {
        #region Setup/Teardown

        [SetUp]
        public void Init()
        {
            _documents = new MockNotificationDocuments();
        }

        #endregion

        private MockNotificationDocuments _documents;

        [Test]
        public void TestPaymentNotificationConstructor()
        {
            var paymentNotification = new PaymentNotification(_documents.GetSuccessfulPaymentNotificationDocument(),
                                                              PaymentNotificationType.Success);

            Assert.AreEqual(PaymentNotificationType.Success, paymentNotification.PaymentNotificationType);

            Assert.IsNotNull(paymentNotification.Account);

            Assert.AreEqual("verena@test.com", paymentNotification.Account.AccountCode);
            Assert.AreEqual("verena", paymentNotification.Account.Username);
            Assert.AreEqual("verena@test.com", paymentNotification.Account.Email);
            Assert.AreEqual("Verena", paymentNotification.Account.FirstName);
            Assert.AreEqual("Test", paymentNotification.Account.LastName);
            Assert.AreEqual("Company, Inc.", paymentNotification.Account.CompanyName);

            Assert.IsNotNull(paymentNotification.Transaction);
            Assert.AreEqual("a5143c1d3a6f4a8287d0e2cc1d4c0427", paymentNotification.Transaction.Id);
            Assert.AreEqual("", paymentNotification.Transaction.InvoiceId);
            Assert.AreEqual("purchase", paymentNotification.Transaction.Action);
            Assert.AreEqual(DateTime.Parse("2009-11-22T13:10:38-08:00"), paymentNotification.Transaction.Date);
            Assert.AreEqual(1000, paymentNotification.Transaction.AmountInCents);
            Assert.AreEqual("Success", paymentNotification.Transaction.Status);
            Assert.AreEqual("Bogus Gateway: Forced success", paymentNotification.Transaction.Message);
            Assert.AreEqual(string.Empty, paymentNotification.Transaction.Reference);
            Assert.AreEqual(string.Empty, paymentNotification.Transaction.CvvResult);
            Assert.AreEqual(string.Empty, paymentNotification.Transaction.AvsResult);
            Assert.AreEqual(string.Empty, paymentNotification.Transaction.AvsResultStreet);
            Assert.AreEqual(string.Empty, paymentNotification.Transaction.AvsResultPostal);
            Assert.IsTrue(paymentNotification.Transaction.Test);
            Assert.IsTrue(paymentNotification.Transaction.Voidable);
            Assert.IsTrue(paymentNotification.Transaction.Refundable);
        }
    }
}