using System;
using NUnit.Framework;

namespace KillBill.Client.Net.Tests.SafeTests
{
    [TestFixture]
    public class SubscriptionTests : BaseTestFixture
    {
        [TestCase("fab1f507-d43c-4f9d-8ec3-070414512c45")]
        public void Get_Subscription(string strId)
        {
            // arrange
            var subscriptionId = Guid.Parse(strId);

            // act
            var subscription = Client.GetSubscription(subscriptionId, RequestOptions);

            // assert
            if (subscription == null)
                Assert.Inconclusive("Could not find subscription in KBill");

            Assert.That(subscription.SubscriptionId, Is.EqualTo(subscriptionId));
        }
    }
}