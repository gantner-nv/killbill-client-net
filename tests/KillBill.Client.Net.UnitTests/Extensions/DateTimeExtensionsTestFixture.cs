using System;
using KillBill.Client.Net.Extensions;
using NUnit.Framework;

namespace KillBill.Client.Net.UnitTests.Extensions
{
    [TestFixture]
    public class DateTimeExtensionsTestFixture
    {
        [Test]
        public void When_ConvertingToDateString_Then_StringIsReturnedCorrectly()
        {
            // arrange
            var dateTime = new DateTime(2018, 11, 22, 3, 44, 55);

            // act
            var result = dateTime.ToDateString();

            // assert
            Assert.That(result, Is.EqualTo("2018-11-22"));
        }

        [Test]
        public void When_ConvertingToDateStringISO_Then_StringIsReturnedCorrectly()
        {
            // arrange
            var dateTime = new DateTime(2018, 11, 22, 3, 44, 55);

            // act
            var result = dateTime.ToDateStringISO();

            // assert
            Assert.That(result, Is.EqualTo("2018-11-22 03:44:55"));
        }
    }
}