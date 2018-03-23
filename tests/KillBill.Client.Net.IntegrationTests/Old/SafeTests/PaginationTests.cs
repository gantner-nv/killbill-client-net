using System.Threading.Tasks;
using NUnit.Framework;

namespace KillBill.Client.Net.IntegrationTests.SafeTests
{
    [TestFixture]
    public class PaginationTests : BaseTestFixture
    {
        [Test]
        public async Task Next_Uri_Link_Is_Correct()
        {
            // assert
            const int limit = 1;

            // act
            var accounts = await Client.GetAccounts(RequestOptions);

            // assert
            Assert.That(accounts, Is.Not.Null); // Because even in empty situations we return a blank Accounts object
            Assert.That(accounts.Count, Is.LessThanOrEqualTo(limit));
            Assert.That(accounts.PaginationMaxNbRecords, Is.GreaterThan(1)); // Because we should have more than 1 account as test data
            Assert.That(accounts.PaginationNextPageUri, Is.Not.Empty); // Because with a limit of 1 there should be more data to trigger paging

            var secondPage = await accounts.GetNext(RequestOptions);
            Assert.That(secondPage, Is.Not.Null);
        }
    }
}
