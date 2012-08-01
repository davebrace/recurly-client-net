using NUnit.Framework;

namespace Recurly.Test
{
    [TestFixture]
    public class SubscriptionTest
    {
        [Test]
        public void ReactivateSubscription_Should_Reactivate_A_Cancelled_Or_Expired_Subscription()
        {
            RecurlySubscription.ReactivateSubscription("1026");
        }
    }
}