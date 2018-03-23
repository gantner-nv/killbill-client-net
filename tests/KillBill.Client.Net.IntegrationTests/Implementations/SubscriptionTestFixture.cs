using System;
using System.Threading.Tasks;
using KillBill.Client.Net.Configuration;
using KillBill.Client.Net.Implementations;
using NUnit.Framework;

namespace KillBill.Client.Net.IntegrationTests.Implementations
{
    [TestFixture]
    public class SubscriptionTestFixture
    {
        [Test]
        public async Task Given_FollowLocationFalse_When_CreatingSubscription_Then_NoResultIsReturned()
        {
            // arrange
            var config = new KillBillConfiguration("https://alb.development.syx-route66.site:9090", "SyxAutomations", "SyxAutomations", "admin", "password");
            var client = new KillBillClient(config);

            // create account
            var account = new KillBill.Client.Net.Model.Account
            {
                Name = "subscriptiontest6", // provide non existing account name
                ExternalKey = "subscriptiontestkey6==" // provide non existing account name
            };

            var createdAccount = await client.CreateAccount(account, client.BaseOptions("enviso"));

            // create subscription
            var subscription = new KillBill.Client.Net.Model.Subscription
            {
                AccountId = createdAccount.AccountId,
                PlanName = "admin_standard",
                StartDate = DateTime.Now
            };

            var options = client.BaseOptions("enviso").Extend().WithFollowLocation(false).Build();

            // act
            var result = await client.CreateSubscription(subscription, options);

            // assert
            Assert.That(result, Is.Null);
        }
    }
}