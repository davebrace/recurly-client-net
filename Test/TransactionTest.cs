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

            RecurlyTransaction transaction = RecurlyTransaction.CreateAccountTransaction(account.AccountCode, 100,
                                                                                         "Test one dollar transaction.");

            Assert.AreEqual(100, transaction.AmountInCents);
            Assert.IsFalse(string.IsNullOrEmpty(transaction.Id));
        }
    }
}