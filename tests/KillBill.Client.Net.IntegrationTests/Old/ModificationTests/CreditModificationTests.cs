using KillBill.Client.Net.Model;
using NUnit.Framework;

namespace KillBill.Client.Net.IntegrationTests.ModificationTests
{
    [TestFixture]
    public class CreditModificationTests : BaseTestFixture
    {
        [Test]
        public void When_AddingCreditToAccount_Then_TheCreditIsReturnedCorrectly()
        {
            // arrange
            var credit = new Credit()
            {
                AccountId = AccountId,
                CreditAmount = Random.Next(100, 200)
            };

            // act
            var processed = Client.CreateCredit(credit, true, RequestOptions);

            // assert
            Assert.That(processed, Is.Not.Null);
            Assert.That(processed.AccountId, Is.EqualTo(AccountId));
            Assert.That(processed.CreditAmount, Is.InRange(100, 200));
        }
    }
}