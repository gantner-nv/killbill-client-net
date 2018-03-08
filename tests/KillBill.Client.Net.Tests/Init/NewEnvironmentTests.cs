using System;
using System.Configuration;
using KillBill.Client.Net.Model;
using NUnit.Framework;

namespace KillBill.Client.Net.Tests.Init
{
    [TestFixture]
    public class NewEnvironmentTests : BaseTestFixture
    {
        /// <summary>
        /// Run this test and then save the TenantId in BaseTestFixture
        /// </summary>
        [Test]
        [Ignore("This test should only be run manually to create a new tenant.")]
        public void Create_Tenant()
        {
            // arrange
            var apiKey = Configuration.ApiKey;
            var externalKey = Guid.NewGuid().ToString();
            var tenant = new Tenant
            {
                ApiKey = apiKey,
                ApiSecret = Configuration.ApiSecret,
                ExternalKey = externalKey
            };

            // act
            var newTenant = Client.CreateTenant(tenant, RequestOptions);

            // assert
            Assert.That(newTenant, Is.Not.Null);
            Assert.That(newTenant.TenantId, Is.Not.EqualTo(Guid.Empty));
            Assert.That(newTenant.ApiKey, Is.EqualTo(apiKey));
            Console.WriteLine("TENANTID: " + newTenant.TenantId);
        }

        /// <summary>
        /// Run this test and then save the AccountId in BaseTestFixture
        /// </summary>
        [Test]
        [Ignore("This test should only be run manually to create a new account.")]
        public void Create_Account()
        {
            // arrange
            var account = new Account()
            {
                ExternalKey = Guid.NewGuid().ToString(),
                Name = "Test",
                Email = "test@test.com",
                Currency = "EUR",
                Address1 = "Test Address 1",
                Address2 = "Test Address 2"
            };

            // arrange
            var createdAccount = Client.CreateAccount(account, RequestOptions);

            // assert
            Assert.That(createdAccount, Is.Not.Null);
            Assert.That(createdAccount.ExternalKey, Is.EqualTo(account.ExternalKey));
            Console.WriteLine("ACCOUNNTID: " + createdAccount.AccountId);
        }
    }
}
