using System.Threading.Tasks;
using NUnit.Framework;

namespace KillBill.Client.Net.IntegrationTests.Implementations
{
    [TestFixture]
    public class TenantTestFixture : BaseTestFixture
    {
        [Test]
        public async Task When_GettingTenantById_Then_TheCorrectTenantIsReturned()
        {
            // arrange
            var tenant = await Client.GetTenant(TenantId, RequestOptions);

            // assert
            if (tenant == null)
                Assert.Inconclusive("Tenant not found.");

            Assert.That(tenant, Is.Not.Null);
            Assert.That(tenant.ApiKey, Is.EqualTo(ApiKey));
            Assert.That(tenant.TenantId, Is.EqualTo(TenantId));
        }

        [Test]
        public async Task When_GettingTenantByApiKey_Then_TheCorrectTenantIsReturned()
        {
            // arrange
            var tenant = await Client.GetTenant(ApiKey, RequestOptions);

            // assert
            if (tenant == null)
                Assert.Inconclusive("Tenant not found.");

            Assert.That(tenant, Is.Not.Null);
            Assert.That(tenant.ApiKey, Is.EqualTo(ApiKey));
            Assert.That(tenant.TenantId, Is.EqualTo(TenantId));
        }
    }
}