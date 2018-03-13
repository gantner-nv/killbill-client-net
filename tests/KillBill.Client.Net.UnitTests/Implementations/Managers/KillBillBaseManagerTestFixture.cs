using System.Collections.Generic;
using System.Linq;
using KillBill.Client.Net.Configuration;
using KillBill.Client.Net.Implementations.Managers;
using KillBill.Client.Net.Infrastructure;
using NUnit.Framework;

namespace KillBill.Client.Net.UnitTests.Implementations.Managers
{
    [TestFixture]
    public class KillBillBaseManagerTestFixture
    {
        [Test]
        public void When_StoringPluginPropertiesAsParams_Then_TheyAreAddedCorrectly()
        {
            // arrange
            var queryParams = new MultiMap<string>();
            var pluginProperties = new Dictionary<string, string>
            {
                { "Test1", "whatever" },
                { "Test2", "rules" }
            };
            var configuration = new KillBillConfiguration(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);

            var killBillBaseManager = new KillBillBaseManager(configuration);

            // act
            killBillBaseManager.StorePluginPropertiesAsParams(pluginProperties, ref queryParams);

            // assert
            Assert.That(queryParams.Dictionary.Count, Is.EqualTo(1));
            var pluginPropertiesParam = queryParams.Dictionary.First(q => q.Key == configuration.QUERY_PLUGIN_PROPERTY);
            Assert.That(pluginPropertiesParam, Is.Not.Null);
        }
    }
}