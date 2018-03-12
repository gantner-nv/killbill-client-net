using System;
using NUnit.Framework;

namespace KillBill.Client.Net.IntegrationTests.Implementations
{
    [TestFixture]
    public class CatalogTextFixture : BaseTestFixture
    {
        [Test]
        [Ignore("This test should only be run manually to upload a new catalog XML. The 'effectiveDate' is unique and 2 catalogs with the same date can not be uploaded.")]
        public void When_UploadingCatalogXml_Then_ItIsSavedCorrectly()
        {
            // arrange
            var catalogXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?><catalog xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xsi:noNamespaceSchemaLocation=\"CatalogSchema.xsd\"><effectiveDate>2014-04-07T00:00:00Z</effectiveDate><catalogName>Enviso</catalogName><recurringBillingMode>IN_ADVANCE</recurringBillingMode><currencies><currency>USD</currency><currency>GBP</currency><currency>EUR</currency><currency>JPY</currency><currency>BTC</currency></currencies><units><unit name=\"tickets\"/></units><products><product name=\"Trade_Venue_Access\"><category>ADD_ON</category></product><product name=\"Trade_Reseller_Access\"><category>ADD_ON</category></product><product name=\"Forms_Access\"><category>ADD_ON</category></product><product name=\"Cloud_Access\"><category>ADD_ON</category></product><product name=\"Trade_Venue_Standard\"><category>BASE</category><included><addonProduct>Trade_Venue_Access</addonProduct></included><available></available><limits/></product><product name=\"Trade_Reseller_Standard\"><category>BASE</category><included><addonProduct>Trade_Reseller_Access</addonProduct></included><available></available><limits/></product><product name=\"Forms_Standard\"><category>BASE</category><included><addonProduct>Forms_Access</addonProduct></included><available></available><limits/></product><product name=\"Cloud_Standard\"><category>BASE</category><included><addonProduct>Cloud_Access</addonProduct></included><available></available><limits/></product></products><rules><changePolicy><changePolicyCase><policy>IMMEDIATE</policy></changePolicyCase></changePolicy><cancelPolicy><cancelPolicyCase><policy>IMMEDIATE</policy></cancelPolicyCase></cancelPolicy></rules><plans><plan name=\"trade_venue_standard\"><product>Trade_Venue_Standard</product><finalPhase type=\"EVERGREEN\"><duration><unit>UNLIMITED</unit><number>-1</number></duration><fixed type=\"ONE_TIME\"><fixedPrice><price><currency>EUR</currency><value>0</value></price></fixedPrice></fixed></finalPhase><plansAllowedInBundle>-1</plansAllowedInBundle></plan><plan name=\"trade_reseller_standard\"><product>Trade_Reseller_Standard</product><finalPhase type=\"EVERGREEN\"><duration><unit>UNLIMITED</unit><number>-1</number></duration><fixed type=\"ONE_TIME\"><fixedPrice><price><currency>EUR</currency><value>0</value></price></fixedPrice></fixed></finalPhase><plansAllowedInBundle>-1</plansAllowedInBundle></plan><plan name=\"forms_standard\"><product>Forms_Standard</product><finalPhase type=\"EVERGREEN\"><duration><unit>UNLIMITED</unit><number>-1</number></duration><fixed type=\"ONE_TIME\"><fixedPrice><price><currency>EUR</currency><value>0</value></price></fixedPrice></fixed><usages/></finalPhase><plansAllowedInBundle>-1</plansAllowedInBundle></plan><plan name=\"cloud_standard\"><product>Cloud_Standard</product><finalPhase type=\"EVERGREEN\"><duration><unit>UNLIMITED</unit><number>-1</number></duration><fixed type=\"ONE_TIME\"><fixedPrice><price><currency>EUR</currency><value>0</value></price></fixedPrice></fixed></finalPhase><plansAllowedInBundle>-1</plansAllowedInBundle></plan></plans><priceLists><defaultPriceList name=\"DEFAULT\"><plans><plan>trade_venue_standard</plan><plan>trade_reseller_standard</plan><plan>forms_standard</plan><plan>cloud_standard</plan></plans></defaultPriceList></priceLists></catalog>";

            // act
            Client.UploadCatalogXml(catalogXml, RequestOptions);
        }

        [Test]
        public void When_GettingCatalogJson_Then_ItIsReturnedCorrectly()
        {
            // arrange / act
            var result = Client.GetCatalogJson(RequestOptions);

            // assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void Given_Date_When_GettingCatalogJson_Then_ItIsReturnedCorrectly()
        {
            // arrange 
            var date = new DateTime(2018, 01, 01);

            // act
            var result = Client.GetCatalogJson(RequestOptions, date);

            // assert
            Assert.That(result, Is.Not.Null);
        }
    }
}