using System;
using KillBill.Client.Net.Configuration;
using KillBill.Client.Net.Implementations;
using NUnit.Framework;

namespace KillBill.Client.Net.IntegrationTests.Implementations
{
    [TestFixture]
    public class SubscriptionTestFixture
    {
        [Test]
        public void When_Then()
        {
            // arrange
            var config = new KillBillConfiguration("https://alb.development.syx-route66.site:9090", "SyxAutomations", "SyxAutomations", "admin", "password");
            var client = new KillBillClient(config);

            // create account
            var account = new KillBill.Client.Net.Model.Account
            {
                Name = "subscriptiontest2",
                ExternalKey = "subscriptiontestkey2=="
            };

            var createdAccount = client.CreateAccount(account, client.BaseOptions("enviso"));

            // create subscription
            var subscription = new KillBill.Client.Net.Model.Subscription
            {
                AccountId = createdAccount.AccountId,
                PlanName = "forms_standard",
                StartDate = DateTime.Now
            };

            var options = client.BaseOptions("enviso").Extend().WithFollowLocation(false).Build();

            // act
            var result = client.CreateSubscription(subscription, options);

            // assert
        }
    }
}