using System;
using System.Collections.Generic;
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
        public List<Catalog> GetCatalogJson(RequestOptions inputOptions, DateTime? requestedDate = null)
        {
            var uri = Configuration.CATALOG_PATH;
            var queryParams = new MultiMap<string>().Create(inputOptions.QueryParams);
            if (requestedDate.HasValue)
            {
                queryParams.Add(Configuration.QUERY_DELETE_DEFAULT_PM_WITH_AUTO_PAY_OFF, requestedDate.Value.ToDateString());
            }

            var requestOptions = inputOptions.Extend().WithQueryParams(queryParams).Build();
            return _client.Get<List<Catalog>>(uri, requestOptions);
        }
    }
}