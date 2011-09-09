using NUnit.Framework;
using Recurly.Notifications;
using Recurly.Test.Mock;

namespace Recurly.Test.Notifications
{
    [TestFixture]
    public class AccountNotificationTests
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
        public void TestAccountNotificationConstructor()
        {
            var accountNotification = new AccountNotification(_documents.GetCanceledAccountNotificationDocument(),
                                                              AccountNotificationType.Canceled);

            Assert.IsNotNull(accountNotification);
            Assert.AreEqual(AccountNotificationType.Canceled, accountNotification.AccountNotificationType);

            Assert.IsNotNull(accountNotification.Account);
            Assert.AreEqual("verena@test.com", accountNotification.Account.AccountCode);
            Assert.AreEqual(string.Empty, accountNotification.Account.Username);
            Assert.AreEqual("verena@test.com", accountNotification.Account.Email);
            Assert.AreEqual("Verena", accountNotification.Account.FirstName);
            Assert.AreEqual("Test", accountNotification.Account.LastName);
            Assert.AreEqual(string.Empty, accountNotification.Account.CompanyName);
        }
    }
}