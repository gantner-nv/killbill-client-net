using System;
using System.Linq;
using NUnit.Framework;

namespace KillBill.Client.Net.Tests.SafeTests
{
    [TestFixture]
    public class AccountTests : BaseTestFixture
    {
        [Test]
        public void When_GettingAccount_Then_TheCorrectAccountIsReturned()
        {
            // arrange
            var account = Client.GetAccount(AccountId, RequestOptions);

            // assert
            if (account == null)
                Assert.Inconclusive("Account not found.");

            Assert.That(account, Is.Not.Null);
            Assert.That(account.AccountId, Is.EqualTo(AccountId));
        }

        [Test]
        public void When_GettingAccounts_Then_OurAccountIsIncludedInTheResult()
        {
            // act
            var accounts = Client.GetAccounts(RequestOptions);

            // assert
            if (!accounts.Any())
                Assert.Inconclusive("No accounts found.");

            Assert.That(accounts, Is.Not.Null);
            Assert.That(accounts, Is.Not.Empty);
            Assert.That(accounts.Any(a => a.AccountId == AccountId), Is.True);
        }

        [Test]
        [TestCase("lister-bundle-a053363e-933e-4d16-91ab-a65c41111bf8")]
        public void Get_Bundle(string bundleKey)
        {
            // act
            var bundle = Client.GetBundle(bundleKey, RequestOptions);

            // assert 
            Assert.That(bundle, Is.Not.Null);
        }

        [Test]
        public void Get_Bundles()
        {
            // act
            var bundles = Client.GetAccountBundles(AccountId, RequestOptions);

            // assert
            if (!bundles.Any())
                Assert.Inconclusive("No bundles found for account.");

            Console.WriteLine($"Found {bundles.Count} bundles for account");
            var firstBundle = bundles.First();
            Console.WriteLine($"Testing first bundle with key - {firstBundle.ExternalKey}");
            Assert.That(firstBundle.AccountId, Is.EqualTo(AccountId));
            Assert.That(firstBundle.ExternalKey, Is.Not.Null);
            Assert.That(firstBundle.ExternalKey, Is.Not.Empty);
        }

        [Test]
        public void Get_Invoices()
        {
            // act
            var invoices = Client.GetInvoicesForAccount(AccountId, RequestOptions);

            // assert
            if (!invoices.Any())
                Assert.Inconclusive("No invoices found for account.");

            Console.WriteLine($"Found {invoices.Count} invoices for account {AccountId}");
            var invoice = invoices.First();
            Assert.That(invoice.AccountId, Is.EqualTo(AccountId));
            Assert.That(invoice.InvoiceId, Is.Not.EqualTo(Guid.Empty));
        }

        [Test]
        public void Get_Account_Timeline()
        {
            // act 
            var timeline = Client.GetAccountTimeline(AccountId, RequestOptions);

            // assert
            Assert.That(timeline, Is.Not.Null);
            Assert.That(timeline.Account, Is.Not.Null);
            Assert.That(timeline.Account.AccountId, Is.EqualTo(AccountId));
        }

        [Test]
        public void Get_Emails_For_Account()
        {
            // act
            var emails = Client.GetEmailsForAccount(AccountId, RequestOptions);

            // assert
            if (!emails.Any())
                Assert.Inconclusive("No emails found for account.");

            Assert.That(emails.First().AccountId, Is.EqualTo(AccountId));
        }

        [Test]
        public void Get_Payments_For_Account()
        {
            // act
            var payments = Client.GetPaymentsForAccount(AccountId, RequestOptions);

            // assert
            if (!payments.Any())
                Assert.Inconclusive("No payments found for account.");

            Assert.That(payments.First().AccountId, Is.EqualTo(AccountId));
        }

        [Test]
        public void Get_InvoicePayments_For_Account()
        {
            // act
            var invoicePayments = Client.GetInvoicePaymentsForAccount(AccountId, RequestOptions);

            // assert
            if (!invoicePayments.Any())
                Assert.Inconclusive("No invoice payments found for account.");

            Assert.That(invoicePayments.First().AccountId, Is.EqualTo(AccountId));
        }
    }
}