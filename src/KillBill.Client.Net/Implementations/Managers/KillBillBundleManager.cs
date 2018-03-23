using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using KillBill.Client.Net.Configuration;
using KillBill.Client.Net.Data;
using KillBill.Client.Net.Extensions;
using KillBill.Client.Net.Infrastructure;
using KillBill.Client.Net.Interfaces;
using KillBill.Client.Net.Interfaces.Managers;
using KillBill.Client.Net.Model;

namespace KillBill.Client.Net.Implementations.Managers
{
    public class KillBillBundleManager : KillBillBaseManager, IKillBillBundleManager
    {
        private readonly IKbHttpClient _client;

        public KillBillBundleManager(IKbHttpClient client)
            : base(client.Configuration)
        {
            _client = client;
        }

        // BUNDLE
        public async Task<Bundle> GetBundle(Guid bundleId, RequestOptions inputOptions)
        {
            var uri = Configuration.BUNDLES_PATH + "/" + bundleId;
            return await _client.Get<Bundle>(uri, inputOptions);
        }

        public async Task<Bundle> GetBundle(string externalKey, RequestOptions inputOptions)
        {
            var uri = Configuration.BUNDLES_PATH;

            var queryParams = new MultiMap<string>().Create(inputOptions.QueryParams);
            queryParams.Add(Configuration.QUERY_EXTERNAL_KEY, externalKey);

            var requestOptions = inputOptions.Extend().WithQueryParams(queryParams).Build();

            var bundles = await _client.Get<Bundles>(uri, requestOptions);

            return bundles?.First();
        }

        public async Task<Bundle> TransferBundle(Bundle bundle, RequestOptions inputOptions)
        {
            if (bundle == null)
                throw new ArgumentNullException(nameof(bundle));

            if (bundle.AccountId.Equals(Guid.Empty))
                throw new ArgumentException("AccountEmail#accountId cannot be empty");

            if (bundle.BundleId.Equals(Guid.Empty))
                throw new ArgumentException("AccountEmail#bundleId cannot be empty");

            var uri = Configuration.BUNDLES_PATH + "/" + bundle.BundleId;

            var followLocation = inputOptions.FollowLocation ?? true;
            var requestOptions = inputOptions.Extend().WithFollowLocation(followLocation).Build();

            return await _client.Put<Bundle>(uri, bundle, requestOptions);
        }

        public async Task BlockBundle(Guid bundleId, BlockingState blockingState, RequestOptions inputOptions, DateTime? requestedDate = null, Dictionary<string, string> pluginProperties = null)
        {
            if (bundleId == Guid.Empty) throw new ArgumentNullException(nameof(bundleId));

            var uri = Configuration.BUNDLES_PATH + "/" + bundleId + "/" + Configuration.BLOCK;

            var queryParams = new MultiMap<string>().Create(inputOptions.QueryParams);
            if (requestedDate.HasValue) queryParams.Add(Configuration.QUERY_REQUESTED_DT, requestedDate.Value.ToDateString());
            StorePluginPropertiesAsParams(pluginProperties, ref queryParams);
            var requestOptions = inputOptions.Extend().WithQueryParams(queryParams).Build();

            await _client.Put(uri, blockingState, requestOptions);
        }

        public async Task PauseBundle(Guid bundleId, RequestOptions inputOptions, DateTime? requestedDate = null, Dictionary<string, string> pluginProperties = null)
        {
            if (bundleId == Guid.Empty) throw new ArgumentNullException(nameof(bundleId));

            var uri = Configuration.BUNDLES_PATH + "/" + bundleId + "/" + Configuration.PAUSE;

            var queryParams = new MultiMap<string>().Create(inputOptions.QueryParams);
            if (requestedDate.HasValue) queryParams.Add(Configuration.QUERY_REQUESTED_DT, requestedDate.Value.ToDateString());
            StorePluginPropertiesAsParams(pluginProperties, ref queryParams);
            var requestOptions = inputOptions.Extend().WithQueryParams(queryParams).Build();

            await _client.Put(uri, null, requestOptions);
        }

        public async Task ResumeBundle(Guid bundleId, RequestOptions inputOptions, DateTime? requestedDate = null, Dictionary<string, string> pluginProperties = null)
        {
            if (bundleId == Guid.Empty) throw new ArgumentNullException(nameof(bundleId));

            var uri = Configuration.BUNDLES_PATH + "/" + bundleId + "/" + Configuration.RESUME;

            var queryParams = new MultiMap<string>().Create(inputOptions.QueryParams);
            if (requestedDate.HasValue) queryParams.Add(Configuration.QUERY_REQUESTED_DT, requestedDate.Value.ToDateString());
            StorePluginPropertiesAsParams(pluginProperties, ref queryParams);
            var requestOptions = inputOptions.Extend().WithQueryParams(queryParams).Build();

            await _client.Put(uri, null, requestOptions);
        }

        // BUNDLES
        public async Task<Bundles> GetBundles(RequestOptions inputOptions, long offset = 0L, long limit = 100L, AuditLevel auditLevel = DefaultAuditLevel)
        {
            var uri = Configuration.BUNDLES_PATH + "/" + Configuration.PAGINATION;
            var queryParams = new MultiMap<string>().Create(inputOptions.QueryParams);
            queryParams.Add(Configuration.QUERY_SEARCH_OFFSET, offset.ToString());
            queryParams.Add(Configuration.QUERY_SEARCH_LIMIT, limit.ToString());
            queryParams.Add(Configuration.QUERY_AUDIT, auditLevel.ToString());
            var requestOptions = inputOptions.Extend().WithQueryParams(queryParams).Build();
            return await _client.Get<Bundles>(uri, requestOptions);
        }

        public async Task<Bundles> SearchBundles(string key, RequestOptions inputOptions, long offset = 0L, long limit = 100L, AuditLevel auditLevel = DefaultAuditLevel)
        {
            var uri = Configuration.BUNDLES_PATH + "/" + Configuration.SEARCH + "/" + HttpUtility.UrlEncode(key);

            var queryParams = new MultiMap<string>().Create(inputOptions.QueryParams);
            queryParams.Add(Configuration.QUERY_SEARCH_OFFSET, offset.ToString());
            queryParams.Add(Configuration.QUERY_SEARCH_LIMIT, limit.ToString());
            queryParams.Add(Configuration.QUERY_AUDIT, auditLevel.ToString());

            var requestOptions = inputOptions.Extend().WithQueryParams(queryParams).Build();

            return await _client.Get<Bundles>(uri, requestOptions);
        }
    }
}