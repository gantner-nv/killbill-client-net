using System.Collections.Generic;
using KillBill.Client.Net.Data;
using KillBill.Client.Net.Infrastructure;
using KillBill.Client.Net.Interfaces;
using KillBill.Client.Net.Interfaces.Managers;
using KillBill.Client.Net.Model;

namespace KillBill.Client.Net.Implementations.Managers
{
    public class KillBillPlanManager : KillBillBaseManager, IKillBillPlanManager
    {
        private readonly IKbHttpClient _client;

        public KillBillPlanManager(IKbHttpClient client)
            : base(client.Configuration)
        {
            _client = client;
        }

        // PLAN DETAIL
        public List<PlanDetail> GetBasePlans(RequestOptions inputOptions)
        {
            var uri = Configuration.CATALOG_PATH + "/availableBasePlans";
            return _client.Get<List<PlanDetail>>(uri, inputOptions);
        }

        public List<PlanDetail> GetAvailableAddons(string baseProductName, RequestOptions inputOptions)
        {
            var uri = Configuration.CATALOG_PATH + "/availableAddons";

            var queryParams = new MultiMap<string>().Create(inputOptions.QueryParams);
            queryParams.Add("baseProductName", baseProductName);

            var requestOptions = inputOptions.Extend().WithQueryParams(queryParams).Build();

            return _client.Get<List<PlanDetail>>(uri, requestOptions);
        }
    }
}