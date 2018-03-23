using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace KillBill.Client.Net.IntegrationTests.SafeTests
{
    [TestFixture]
    public class PaymentMethodTests : BaseTestFixture
    {
        [TestCase("035ccf7d-8015-4296-97a5-91571321ba1c")]
        public async Task Get_PaymentMethod(string paymentMethodString)
        {
            // arrange
            var paymentMethodId = Guid.Parse(paymentMethodString);

            // act
            var paymentMethod = await Client.GetPaymentMethod(paymentMethodId, RequestOptions);

            // assert
            Assert.That(paymentMethod, Is.Not.Null);
            Assert.That(paymentMethod.PaymentMethodId, Is.EqualTo(paymentMethodId));
        }

        [Test]
        public async Task Get_PaymentMethodsForAccount()
        {
            // act
            var paymentMethods = await Client.GetPaymentMethodsForAccount(AccountId, RequestOptions);

            // assert
            Assert.That(paymentMethods, Is.Not.Null);
            Assert.That(paymentMethods, Is.Not.Empty);
        }
    }
}