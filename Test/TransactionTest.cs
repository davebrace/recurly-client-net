using NUnit.Framework;

namespace Recurly.Test
{
    [TestFixture]
    internal class TransactionTest
    {
        [Test]
        public void CreateTestTransaction()
        {
            var account = RecurlyAccount.Get("TestAccount");
            account.FirstName = "Dave";
            account.LastName = "Brace";
            account.Email = "dbrace@test.com";

            account.BillingInfo = new RecurlyBillingInfo(account)
            {
                FirstName = account.FirstName,
                LastName = account.LastName,
                Address1 = "test",
                City = "Chicago",
                State = "IL",
                PostalCode = "60661",
                PhoneNumber = "5555555555",
                Country = "US",
                IpAddress = "192.168.4.134",
                CreditCard =
                {
                    Number = "4111-1111-1111-1111",
                    ExpirationMonth = 12,
                    ExpirationYear = 2012,
                    VerificationValue = "123"
                }
            };

            account.BillingInfo.Update();

            //RecurlyTransaction transaction = RecurlyTransaction.CreateAccountTransaction(account.AccountCode, 100,
            //                                                                             "Test one dollar transaction.");

            //Assert.AreEqual(100, transaction.AmountInCents);
            //Assert.IsFalse(string.IsNullOrEmpty(transaction.Id));
        }
    }
}