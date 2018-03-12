using NUnit.Framework;

namespace KillBill.Client.Net.Tests.Implementations
{
    [TestFixture]
    public class TenantTestFixture : BaseTestFixture
    {
        [Test]
        public void When_GettingTenantById_Then_TheCorrectTenantIsReturned()
        {
            // arrange
            var tenant = Client.GetTenant(TenantId, RequestOptions);

            // assert
            if (tenant == null)
                Assert.Inconclusive("Tenant not found.");

            Assert.That(tenant, Is.Not.Null);
            Assert.That(tenant.ApiKey, Is.EqualTo(ApiKey));
            Assert.That(tenant.TenantId, Is.EqualTo(TenantId));
        }

        [Test]
        public void When_GettingTenantByApiKey_Then_TheCorrectTenantIsReturned()
        {
            // arrange
            var tenant = Client.GetTenant(ApiKey, RequestOptions);

            // assert
            if (tenant == null)
                Assert.Inconclusive("Tenant not found.");

            Assert.That(tenant, Is.Not.Null);
            Assert.That(tenant.ApiKey, Is.EqualTo(ApiKey));
            Assert.That(tenant.TenantId, Is.EqualTo(TenantId));
        }
    }
}