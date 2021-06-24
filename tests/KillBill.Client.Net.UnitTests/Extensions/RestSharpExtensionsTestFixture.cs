using KillBill.Client.Net.Extensions;
using NUnit.Framework;
using RestSharp;

namespace KillBill.Client.Net.UnitTests.Extensions
{
    [TestFixture]
    public class RestSharpExtensionsTestFixture
    {
        [Test]
        [System.Obsolete]
        public void When_GetValue_Then_TheValueIsReturnedCorrectly()
        {
            // arrange
            var response = new RestResponse();
            response.Headers.Add(new Parameter("headerA", "A", ParameterType.HttpHeader));
            response.Headers.Add(new Parameter("headerB", "B", ParameterType.HttpHeader));

            // act
            var result = response.GetValue("headerB");

            // assert
            Assert.That(result, Is.EqualTo("B"));
        }

        [Test]
        [System.Obsolete]
        public void Given_HeaderDoesNotExist_When_GetValue_Then_NullIsReturned()
        {
            // arrange
            var response = new RestResponse();
            response.Headers.Add(new Parameter("headerA", "A", ParameterType.HttpHeader));
            response.Headers.Add(new Parameter("headerB", "B", ParameterType.HttpHeader));

            // act
            var result = response.GetValue("headerC");

            // assert
            Assert.That(result, Is.Null);
        }
    }
}