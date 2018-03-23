using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KillBill.Client.Net.Model;
using NUnit.Framework;

namespace KillBill.Client.Net.IntegrationTests.ModificationTests
{
    [TestFixture]
    public class InvoiceModificationTests : BaseTestFixture
    {
        [Test]
        public async Task When_CreatingExternalCharges_TheyAreCreatedCorrectly()
        {
            // arrange
            var externalCharges = new List<InvoiceItem>
            {
                new InvoiceItem
                {
                    AccountId = AccountId,
                    Amount = 100,
                    Currency = "EUR",
                    Description = "LINE ITEM 1"
                },
                new InvoiceItem
                {
                    AccountId = AccountId,
                    Amount = 200,
                    Currency = "EUR",
                    Description = "LINE ITEM 2"
                },
            };

            // act
            var invoiceItems = await Client.CreateExternalCharges(externalCharges, DateTime.Now, false, false, RequestOptions);

            // assert
            Assert.That(invoiceItems, Is.Not.Null);
            Assert.That(invoiceItems, Is.Not.Empty);
            Assert.That(invoiceItems.Count, Is.EqualTo(2));
        }
    }
}