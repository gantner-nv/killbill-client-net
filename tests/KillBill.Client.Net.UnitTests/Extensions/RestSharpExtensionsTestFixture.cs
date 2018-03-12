using System.Collections.Generic;
using KillBill.Client.Net.Extensions;
using NUnit.Framework;
using RestSharp;

namespace KillBill.Client.Net.UnitTests.Extensions
{
    [TestFixture]
    public class RestSharpExtensionsTestFixture
    {
        [Test]
        public void When_GetValue_Then_TheValueIsReturnedCorrectly()
        {
            // arrange
            var headers = new List<Parameter>
            {
                new Parameter { Type = ParameterType.HttpHeader, Name = "headerA", Value = "A" },
                new Parameter { Type = ParameterType.HttpHeader, Name = "headerB", Value = "B" }
            };

            // act
            var result = headers.GetValue("headerB");

            // assert
            Assert.That(result, Is.EqualTo("B"));
        }

        [Test]
        public void Given_HeaderDoesNotExist_When_GetValue_Then_NullIsReturned()
        {
            // arrange
            var headers = new List<Parameter>
            {
                new Parameter { Type = ParameterType.HttpHeader, Name = "headerA", Value = "A" },
                new Parameter { Type = ParameterType.HttpHeader, Name = "headerB", Value = "B" }
            };

            // act
            var result = headers.GetValue("headerC");

            // assert
            Assert.That(result, Is.Null);
        }
    }
}