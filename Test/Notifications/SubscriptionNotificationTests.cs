using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Recurly.Notifications;
using Recurly.Test.Mock;

namespace Recurly.Test.Notifications
{
    [TestFixture]
    public class SubscriptionNotificationTests
    {
        [SetUp]
        public void Init()
        {
            _documents = new MockNotificationDocuments();
        }

        private MockNotificationDocuments _documents;

        [Test]
        public void TestSubscriptionNotificationConstructor()
        {
            var sub = new SubscriptionNotification(_documents.GetExpiredSubscriptionNotificationDocument(),
                                                   SubscriptionNotificationType.Expired);

            Assert.IsNotNull(sub);
        
            Assert.IsNotNull(sub.Account);

            Assert.AreEqual("jane.doe@gmail.com", sub.Account.AccountCode);
            Assert.AreEqual(string.Empty, sub.Account.Username);
            Assert.AreEqual("janedoe@gmail.com", sub.Account.Email);
            Assert.AreEqual("Jane", sub.Account.FirstName);
            Assert.AreEqual("Doe", sub.Account.LastName);
            Assert.AreEqual(string.Empty, sub.Account.CompanyName);

            Assert.IsNotNull(sub.Subscription);
            
            Assert.IsNotNull(sub.Subscription.Plan);
            Assert.AreEqual("1dpt", sub.Subscription.Plan.Code);
            Assert.AreEqual("Subscription One", sub.Subscription.Plan.Name);

            Assert.AreEqual("expired", sub.Subscription.State);
            Assert.AreEqual(1, sub.Subscription.Quantity);
            Assert.AreEqual(200, sub.Subscription.AmountInCents);
            Assert.AreEqual(DateTime.Parse("2010-09-23T22:05:03Z"), sub.Subscription.ActivatedAt);
            Assert.AreEqual(DateTime.Parse("2010-09-23T22:05:43Z"), sub.Subscription.CanceledAt);
            Assert.AreEqual(DateTime.Parse("2010-09-24T22:05:03Z"), sub.Subscription.ExpiresAt);
            Assert.AreEqual(DateTime.Parse("2010-09-23T22:05:03Z"), sub.Subscription.CurrentPeriodStartedAt);
            Assert.AreEqual(DateTime.Parse("2010-09-24T22:05:03Z"), sub.Subscription.CurrentPeriodEndsAt);
            Assert.AreEqual(null, sub.Subscription.TrialStartedAt);
            Assert.AreEqual(null, sub.Subscription.TrialEndsAt);
        }

        [Test]
        public void TestNewSubscriptionNotificationParse()
        {
            var sub = new SubscriptionNotification(_documents.GetNewSubscriptionNotificationDocument(),
                                                   SubscriptionNotificationType.New);

            Assert.IsNotNull(sub);
        }
    }
}
