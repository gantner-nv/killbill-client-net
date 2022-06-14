using System;
using System.Threading.Tasks;
using KillBill.Client.Net.Data;
using KillBill.Client.Net.Infrastructure;
using KillBill.Client.Net.Interfaces;
using KillBill.Client.Net.Model;

namespace KillBill.Client.Net.Implementations.Managers
{
    public class KillBillUsageManager : KillBillBaseManager
    {
        private readonly IKbHttpClient _client;
        
        public KillBillUsageManager(IKbHttpClient client) 
                : base(client.Configuration)
        {
            _client = client;
        }
        
        public async Task<UsageRecordRequest> RecordUsage(UsageRecordRequest usage, RequestOptions inputOptions)
        {
            if (usage == null)
                throw new ArgumentNullException(nameof(usage));

            var requestOptions = inputOptions.Extend().Build();
            return await _client.Post<UsageRecordRequest>(Configuration.USAGES_PATH, usage, requestOptions);
        }
        
        public async Task<UsageRecord> GetRecordedUsage(Guid subscritionId, RequestOptions inputOptions)
        {
            var uri = $"{Configuration.USAGES_PATH}/{subscritionId}";
            var queryParams = new MultiMap<string>().Create(inputOptions.QueryParams);
            var requestOptions = inputOptions.Extend().WithQueryParams(queryParams).Build();
            return await _client.Get<UsageRecord>(uri, requestOptions);
        }
    }
}