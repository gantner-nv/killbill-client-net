using System;
using System.Collections.Generic;
using KillBill.Client.Net.Data;
using KillBill.Client.Net.Model;

namespace KillBill.Client.Net.Interfaces.Managers
{
    public interface IKillBillCatalogManager
    {
        // CATALOG
        List<Catalog> GetCatalogJson(RequestOptions inputOptions, DateTime? requestedDate = null);

        void UploadCatalogXml(string catalogXml, RequestOptions inputOptions);

        // PRODUCT
        Product GetProductFromSubscription(Guid subscriptionId, RequestOptions inputOptions, DateTime? requestedDate = null);
    }
}