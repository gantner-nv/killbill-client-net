using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;

namespace KillBill.Client.Net.IntegrationTests.SafeTests
{
    [TestFixture]
    public class InvoiceTests : BaseTestFixture
    {
        [TestCase("5149bcae-f0de-4bc8-9b32-a6e1bc5f83ed")]
        public async Task Get_Invoice_By_Id(string invoiceId)
        {
            // act
            var invoice = await Client.GetInvoice(invoiceId, RequestOptions);

            // assert
            Assert.That(invoice, Is.Not.Null);
            Assert.That(invoice.InvoiceId, Is.EqualTo(invoiceId));
            Assert.That(invoice.Balance, Is.GreaterThan(0));
        }

        [TestCase(1)]
        public async Task Get_Invoice_By_Number(int invoiceNumber)
        {
            // act
            var invoice = await Client.GetInvoice(invoiceNumber, RequestOptions);

            // assert
            Assert.That(invoice, Is.Not.Null);
            Assert.That(invoice.InvoiceNumber, Is.EqualTo(invoiceNumber));
            Assert.That(invoice.Balance, Is.GreaterThan(0));
        }

        [TestCase("5149bcae-f0de-4bc8-9b32-a6e1bc5f83ed")]
        public async Task Search_Invoices_By_InvoiceId(string searchTerm)
        {
            // act
            var invoices = await Client.SearchInvoices(searchTerm, RequestOptions);

            // assert
            Assert.That(invoices, Is.Not.Null);
            Assert.That(invoices, Is.Not.Empty);

            Assert.That(invoices.Any(x => x.InvoiceId.ToString() == searchTerm), Is.True);
        }

        [TestCase("5")]
        public async Task Search_Invoices_By_InvoiceNumber(string searchTerm)
        {
            // act
            var searchResults = await Client.SearchInvoices(searchTerm, RequestOptions);

            // assert
            Assert.That(searchResults, Is.Not.Null);
            Assert.That(searchResults, Is.Not.Empty);

            Assert.That(searchResults.Any(x => x.InvoiceNumber.ToString() == searchTerm), Is.True);
        }
    }
}