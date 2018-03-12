using System;
using NUnit.Framework;

namespace KillBill.Client.Net.IntegrationTests.ModificationTests
{
    [TestFixture]
    [Ignore("This test was disabled as we are not using notifications yet.")]
    public class TenantModificationTests : BaseTestFixture
    {
        [Test]
        public void Register_Notification_Callback()
        {
            // arrange
            var callback = "http://localhost:8080/notsurewhatiexpecttosee";

            // act
            var tenantKey = Client.RegisterCallBackNotificationForTenant(callback, RequestOptions);

            // assert
            Assert.That(tenantKey, Is.Not.Null);
        }

        [TestCase("d632f46a-15cf-409a-83c1-34390b983a12")]
        public void Unregister_Notification_Callback(string tenantIdString)
        {
            // arrange
            var tenantId = Guid.Parse(tenantIdString);

            // act
            Client.UnregisterCallbackNotificationForTenant(tenantId, RequestOptions);
        }

        [Test]
        public void Retrieve_Notification_Callbacks()
        {
            // act
            var tenantKey = Client.GetCallbackNotificationForTenant(RequestOptions);

            // assert
            Assert.That(tenantKey, Is.Not.Null); // Because in the above test we registered a new one
            Assert.That(tenantKey.Values, Is.Not.Empty); // Because it should atleast have the callback in the above test
        }
    }
}