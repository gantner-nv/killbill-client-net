using System;
using System.Collections.Generic;
using KillBill.Client.Net.Model;
using NUnit.Framework;

namespace KillBill.Client.Net.IntegrationTests.ModificationTests
{
    [TestFixture]
    public class PaymentMethodModificationTests : BaseTestFixture
    {
        [TestCase("tok_154JgSC746zpV9sQFB1h4ACJ")]
        [Ignore("This test was disabled as we are not using payments yet.")]
        public void Create_PaymentMethod_Stripe(string stripeToken)
        {
            // arrange
            var stripePaymentMethod = new PaymentMethod
            {
                AccountId = AccountId,
                PluginName = "killbill-stripe",
                PluginInfo = new PaymentMethodPluginDetail
                {
                    Properties = new List<PluginProperty>
                    {
                        new PluginProperty
                        {
                            Key = "token",
                            Value = stripeToken
                        }
                    }
                }
            };

            // act
            var paymentMethod = Client.CreatePaymentMethod(stripePaymentMethod, RequestOptions);

            // assert
            Assert.That(paymentMethod, Is.Not.Null);
            Assert.That(paymentMethod.AccountId, Is.EqualTo(AccountId));
            Assert.That(paymentMethod.PaymentMethodId.HasValue, Is.True);
            Assert.That(paymentMethod.PaymentMethodId, Is.Not.EqualTo(Guid.Empty));
        }
    }
}