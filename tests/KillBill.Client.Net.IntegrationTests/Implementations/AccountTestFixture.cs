using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;

namespace KillBill.Client.Net.IntegrationTests.Implementations
{
    [TestFixture]
    public class AccountTestFixture : BaseTestFixture
    {
        [Test]
        public async Task When_GettingAccount_Then_TheCorrectAccountIsReturned()
        {
            // arrange
            var account = await Client.GetAccount(AccountId, RequestOptions);

            // assert
            if (account == null)
                Assert.Inconclusive("Account not found.");

            Assert.That(account, Is.Not.Null);
            Assert.That(account.AccountId, Is.EqualTo(AccountId));
        }

        [Test]
        public async Task When_GettingAccounts_Then_OurAccountIsIncludedInTheResult()
        {
            // act
            var accounts = await Client.GetAccounts(RequestOptions);

            // assert
            if (!accounts.Any())
                Assert.Inconclusive("No accounts found.");

            Assert.That(accounts, Is.Not.Null);
            Assert.That(accounts, Is.Not.Empty);
            Assert.That(accounts.Any(a => a.AccountId == AccountId), Is.True);
        }
    }
}