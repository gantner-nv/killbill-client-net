using System;
using KillBill.Client.Net.Model;
using NUnit.Framework;

namespace KillBill.Client.Net.IntegrationTests.ModificationTests
{
    [TestFixture]
    public class PaymentModificationTests : BaseTestFixture
    {
        [TestCase("1524e2d3-cd26-4714-a105-e3983dcfded6")]
        [Ignore("This test was disabled as we are not using payments yet.")]
        public void When_CreatingPaymentMethod_Then_PaymentMethodIsCreatedCorrectly(string paymentMethodString)
        {
            // arrange
            var paymentMethodId = Guid.Parse(paymentMethodString);
            var paymentTransaction = new PaymentTransaction
            {
                Amount = 550,
                Currency = "AUD",
                EffectiveDate = DateTime.UtcNow,
                TransactionType = "AUTHORIZE",
                TransactionExternalKey = Guid.NewGuid().ToString()
            };

            // act
            var payment = Client.CreatePayment(AccountId, paymentMethodId, paymentTransaction, RequestOptions);

            // assert
            Assert.That(payment, Is.Not.Null);
        }
    }
}