using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KillBill.Client.Net.Data;
using KillBill.Client.Net.Model;

namespace KillBill.Client.Net.Interfaces.Managers
{
    public interface IKillBillCatalogManager
    {
        // CATALOG
        Task<List<Catalog>> GetCatalogJson(RequestOptions inputOptions, DateTime? requestedDate = null);

        Task UploadCatalogXml(string catalogXml, RequestOptions inputOptions);

        // PLAN
        Task<List<PlanDetail>> GetBasePlans(RequestOptions inputOptions);

        Task<List<PlanDetail>> GetAvailableAddons(string baseProductName, RequestOptions inputOptions);

        Task<Plan> GetPlanFromSubscription(Guid subscriptionId, RequestOptions inputOptions, DateTime? requestedDate = null);

        // PRICE LIST
        Task<PriceList> GetPriceListFromSubscription(Guid subscriptionId, RequestOptions inputOptions, DateTime? requestedDate = null);

        // PRODUCT
        Task<Product> GetProductFromSubscription(Guid subscriptionId, RequestOptions inputOptions, DateTime? requestedDate = null);
    }
}