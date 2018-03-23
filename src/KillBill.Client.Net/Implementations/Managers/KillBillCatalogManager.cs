using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KillBill.Client.Net.Data;
using KillBill.Client.Net.Extensions;
using KillBill.Client.Net.Infrastructure;
using KillBill.Client.Net.Interfaces;
using KillBill.Client.Net.Interfaces.Managers;
using KillBill.Client.Net.Model;

namespace KillBill.Client.Net.Implementations.Managers
{
    public class KillBillCatalogManager : KillBillBaseManager, IKillBillCatalogManager
    {
        private readonly IKbHttpClient _client;

        public KillBillCatalogManager(IKbHttpClient client)
            : base(client.Configuration)
        {
            _client = client;
        }

        // CATALOG
        public async Task<List<Catalog>> GetCatalogJson(RequestOptions inputOptions, DateTime? requestedDate = null)
        {
            var uri = Configuration.CATALOG_PATH;
            var queryParams = new MultiMap<string>().Create(inputOptions.QueryParams);
            if (requestedDate.HasValue)
            {
                queryParams.Add(Configuration.QUERY_REQUESTED_DT, requestedDate.Value.ToDateString());
            }

            var requestOptions = inputOptions.Extend().WithQueryParams(queryParams).Build();
            return await _client.Get<List<Catalog>>(uri, requestOptions);
        }

        public async Task UploadCatalogXml(string catalogXml, RequestOptions inputOptions)
        {
            var uri = Configuration.CATALOG_PATH;
            var requestOptions = inputOptions.Extend().WithContentType(ContentType.Xml).Build();
            await _client.Post(uri, catalogXml, requestOptions);
        }

        // PLAN
        public async Task<List<PlanDetail>> GetBasePlans(RequestOptions inputOptions)
        {
            var uri = Configuration.CATALOG_PATH + "/" + Configuration.AVAILABLEBASEPLANS;
            return await _client.Get<List<PlanDetail>>(uri, inputOptions);
        }

        public async Task<List<PlanDetail>> GetAvailableAddons(string baseProductName, RequestOptions inputOptions)
        {
            var uri = Configuration.CATALOG_PATH + "/" + Configuration.AVAILABLEADDONS;

            var queryParams = new MultiMap<string>().Create(inputOptions.QueryParams);
            queryParams.Add("baseProductName", baseProductName);

            var requestOptions = inputOptions.Extend().WithQueryParams(queryParams).Build();

            return await _client.Get<List<PlanDetail>>(uri, requestOptions);
        }
        
        public async Task<Plan> GetPlanFromSubscription(Guid subscriptionId, RequestOptions inputOptions, DateTime? requestedDate = null)
        {
            var uri = Configuration.CATALOG_PATH + "/" + Configuration.PLAN;

            var queryParams = new MultiMap<string>().Create(inputOptions.QueryParams);
            queryParams.Add(Configuration.QUERY_SUBSCRIPTION_ID, subscriptionId.ToString());
            if (requestedDate.HasValue)
            {
                queryParams.Add(Configuration.QUERY_REQUESTED_DT, requestedDate.Value.ToDateString());
            }

            var requestOptions = inputOptions.Extend().WithQueryParams(queryParams).Build();
            return await _client.Get<Plan>(uri, requestOptions);
        }

        // PRICE LIST
        public async Task<PriceList> GetPriceListFromSubscription(Guid subscriptionId, RequestOptions inputOptions, DateTime? requestedDate = null)
        {
            var uri = Configuration.CATALOG_PATH + "/" + Configuration.PRICELIST;

            var queryParams = new MultiMap<string>().Create(inputOptions.QueryParams);
            queryParams.Add(Configuration.QUERY_SUBSCRIPTION_ID, subscriptionId.ToString());
            if (requestedDate.HasValue)
            {
                queryParams.Add(Configuration.QUERY_REQUESTED_DT, requestedDate.Value.ToDateString());
            }

            var requestOptions = inputOptions.Extend().WithQueryParams(queryParams).Build();
            return await _client.Get<PriceList>(uri, requestOptions);
        }

        // PRODUCT
        public async Task<Product> GetProductFromSubscription(Guid subscriptionId, RequestOptions inputOptions, DateTime? requestedDate = null)
        {
            var uri = Configuration.CATALOG_PATH + "/" + Configuration.PRODUCT;

            var queryParams = new MultiMap<string>().Create(inputOptions.QueryParams);
            queryParams.Add(Configuration.QUERY_SUBSCRIPTION_ID, subscriptionId.ToString());
            if (requestedDate.HasValue)
            {
                queryParams.Add(Configuration.QUERY_REQUESTED_DT, requestedDate.Value.ToDateString());
            }

            var requestOptions = inputOptions.Extend().WithQueryParams(queryParams).Build();
            return await _client.Get<Product>(uri, requestOptions);
        }
    }
}