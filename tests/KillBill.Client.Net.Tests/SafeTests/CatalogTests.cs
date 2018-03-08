using System.Linq;
using NUnit.Framework;

namespace KillBill.Client.Net.Tests.SafeTests
{
    [TestFixture]
    public class CatalogTests : BaseTestFixture
    {
        [Test]
        public void Get_Catalog_Json()
        {
            // act
            var catalogs = Client.GetCatalogJson(RequestOptions);

            // assert
            if (catalogs == null)
                Assert.Inconclusive("Catalogs not found.");

            Assert.That(catalogs, Is.Not.Null);
            Assert.That(catalogs, Is.Not.Empty);
            var catalog = catalogs.First();
            Assert.That(catalog.Currencies, Is.Not.Null);
            Assert.That(catalog.Currencies, Is.Not.Empty);
            Assert.That(catalog.Name, Is.Not.Null);
            Assert.That(catalog.Name, Is.Not.Empty);
            Assert.That(catalog.PriceLists, Is.Not.Null);
            Assert.That(catalog.PriceLists, Is.Not.Empty);

            var priceList = catalog.PriceLists.First();
            Assert.That(priceList.Name, Is.Not.Null);
            Assert.That(priceList.Name, Is.Not.Empty);
            Assert.That(priceList.Plans, Is.Not.Null);
            Assert.That(priceList.Plans, Is.Not.Empty);
        }

        [Test]
        public void Get_Base_Plans()
        {
            // act
            var plans = Client.GetBasePlans(RequestOptions);

            // assert
            if (plans == null)
                Assert.Inconclusive("No base plans found");

            Assert.That(plans, Is.Not.Null);
            Assert.That(plans, Is.Not.Empty);

            var plan = plans.First();
            Assert.That(plan.Product, Is.Not.Null);
            Assert.That(plan.Product, Is.Not.Empty);
            Assert.That(plan.PriceList, Is.Not.Null);
            Assert.That(plan.PriceList, Is.Not.Empty);
            Assert.That(plan.Plan, Is.Not.Null);
            Assert.That(plan.Plan, Is.Not.Empty);
            Assert.That(plan.FinalPhaseRecurringPrice, Is.Not.Null);
            Assert.That(plan.FinalPhaseRecurringPrice, Is.Not.Empty);

            var price = plan.FinalPhaseRecurringPrice.First();
            Assert.That(price.Currency, Is.Not.Null);
            Assert.That(price.Currency, Is.Not.Empty);
        }

        [Test]
        public void Get_Available_Addons()
        {
            // act
            var addons = Client.GetAvailableAddons("system-connect", RequestOptions);

            // assert
            if (addons == null)
                Assert.Inconclusive("No addons found");

            Assert.That(addons, Is.Not.Null);
            Assert.That(addons, Is.Not.Empty);
        }
    }
}