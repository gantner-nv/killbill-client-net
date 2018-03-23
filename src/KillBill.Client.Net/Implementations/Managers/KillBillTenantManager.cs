using System;
using System.Threading.Tasks;
using KillBill.Client.Net.Data;
using KillBill.Client.Net.Infrastructure;
using KillBill.Client.Net.Interfaces;
using KillBill.Client.Net.Interfaces.Managers;
using KillBill.Client.Net.Model;

namespace KillBill.Client.Net.Implementations.Managers
{
    public class KillBillTenantManager : KillBillBaseManager, IKillBillTenantManager
    {
        private readonly IKbHttpClient _client;

        public KillBillTenantManager(IKbHttpClient client)
            : base(client.Configuration)
        {
            _client = client;
        }

        // TENANT    
        public async Task<Tenant> CreateTenant(Tenant tenant, RequestOptions inputOptions, bool useGlobalDefault = true)
        {
            if (tenant == null)
                throw new ArgumentNullException(nameof(tenant));

            if (string.IsNullOrEmpty(tenant.ApiKey) || string.IsNullOrEmpty(tenant.ApiSecret))
                throw new ArgumentException("tenant#apiKey and tenant#apiSecret must not be empty");

            var followLocation = inputOptions.FollowLocation ?? true;
            var queryParams = new MultiMap<string>().Create(inputOptions.QueryParams);
            queryParams.Add(Configuration.QUERY_TENANT_USE_GLOBAL_DEFAULT, useGlobalDefault.ToString());
            var requestOptions = inputOptions.Extend().WithFollowLocation(followLocation).WithQueryParams(queryParams).Build();
            return await _client.Post<Tenant>(Configuration.TENANTS_PATH, tenant, requestOptions);
        }

        public async Task<Tenant> GetTenant(Guid tenantId, RequestOptions inputOptions)
        {
            var uri = Configuration.TENANTS_PATH + "/" + tenantId;
            return await _client.Get<Tenant>(uri, inputOptions);
        }

        public async Task<Tenant> GetTenant(string apiKey, RequestOptions inputOptions)
        {
            var uri = Configuration.TENANTS_PATH;
            var queryParams = new MultiMap<string>().Create(inputOptions.QueryParams);
            queryParams.Add(Configuration.QUERY_API_KEY, apiKey);
            var requestOptions = inputOptions.Extend().WithQueryParams(queryParams).Build();
            return await _client.Get<Tenant>(uri, requestOptions);
        }

        // TENANT KEY
        public async Task<TenantKey> RegisterCallBackNotificationForTenant(string callback, RequestOptions inputOptions)
        {
            var uri = Configuration.TENANTS_PATH + "/" + Configuration.REGISTER_NOTIFICATION_CALLBACK;
            var followLocation = inputOptions.FollowLocation ?? true;
            var queryParams = new MultiMap<string>().Create(inputOptions.QueryParams);
            queryParams.Add(Configuration.QUERY_NOTIFICATION_CALLBACK, callback);
            var requestOptions = inputOptions.Extend().WithFollowLocation(followLocation).WithQueryParams(queryParams).Build();
            return await _client.Post<TenantKey>(uri, null, requestOptions);
        }

        public async Task UnregisterCallbackNotificationForTenant(Guid tenantId, RequestOptions inputOptions)
        {
            var uri = Configuration.TENANTS_PATH + "/" + Configuration.LEGACY_REGISTER_NOTIFICATION_CALLBACK;
            await _client.Delete(uri, inputOptions);
        }

        public async Task<TenantKey> GetCallbackNotificationForTenant(RequestOptions inputOptions)
        {
            var uri = Configuration.TENANTS_PATH + "/" + Configuration.REGISTER_NOTIFICATION_CALLBACK;
            return await _client.Get<TenantKey>(uri, inputOptions);
        }
    }
}