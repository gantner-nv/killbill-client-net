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
    public class KillBillSubscriptionManager : KillBillBaseManager, IKillBillSubscriptionManager
    {
        private readonly IKbHttpClient _client;

        public KillBillSubscriptionManager(IKbHttpClient client)
            : base(client.Configuration)
        {
            _client = client;
        }

        // SUBSCRIPTION
        public async Task<Subscription> GetSubscription(Guid subscriptionId, RequestOptions inputOptions)
        {
            var uri = Configuration.SUBSCRIPTIONS_PATH + "/" + subscriptionId;
            return await _client.Get<Subscription>(uri, inputOptions);
        }

        public async Task<Subscription> CreateSubscription(Subscription subscription, RequestOptions inputOptions, DateTime? requestedDate = null, bool? isMigrated = null)
        {
            ValidateSubscription(subscription);

            // var httpTimeout = Configuration.DEFAULT_HTTP_TIMEOUT_SEC;
            var followLocation = inputOptions.FollowLocation ?? true;

            var queryParams = new MultiMap<string>().Create(inputOptions.QueryParams);

            // if (timeoutSec.HasValue)
            // {
            //     queryParams.Add(Configuration.QUERY_CALL_COMPLETION, timeoutSec.Value > 0 ? "true" : "false");
            //     queryParams.Add(Configuration.QUERY_CALL_TIMEOUT, timeoutSec.Value.ToString());
            //     timeoutSec = timeoutSec.Value;
            // }
            if (requestedDate.HasValue)
            {
                queryParams.Add(Configuration.QUERY_REQUESTED_DT, requestedDate.Value.ToShortDateString());
            }

            if (isMigrated.HasValue)
            {
                queryParams.Add(Configuration.QUERY_MIGRATED, isMigrated.ToString());
            }

            var requestOptions = inputOptions.Extend().WithQueryParams(queryParams).WithFollowLocation(followLocation).Build();

            // return client.Post<Subscription>(Configuration.SUBSCRIPTIONS_PATH, subscription, requestOptions, httpTimeout);
            return await _client.Post<Subscription>(Configuration.SUBSCRIPTIONS_PATH, subscription, requestOptions);
        }

        public async Task<Subscription> UpdateSubscription(Subscription subscription, RequestOptions inputOptions, BillingActionPolicy? billingPolicy = null, DateTime? requestedDate = null, bool? isMigrated = null)
        {
            if (subscription.SubscriptionId.Equals(Guid.Empty))
                throw new ArgumentException("Subscription#subscriptionId cannot be empty");

            ValidateSubscription(subscription, false);

            var uri = Configuration.SUBSCRIPTIONS_PATH + "/" + subscription.SubscriptionId;
            var queryParams = new MultiMap<string>().Create(inputOptions.QueryParams);

            // if (timeoutSec.HasValue)
            // {
            //     queryParams.Add(Configuration.QUERY_CALL_COMPLETION, timeoutSec.Value > 0 ? "true" : "false");
            //     queryParams.Add(Configuration.QUERY_CALL_TIMEOUT, timeoutSec.Value.ToString());
            //     timeoutSec = timeoutSec.Value;
            // }
            if (requestedDate.HasValue)
            {
                queryParams.Add(Configuration.QUERY_REQUESTED_DT, requestedDate.Value.ToShortDateString());
            }

            if (billingPolicy.HasValue)
            {
                queryParams.Add(Configuration.QUERY_BILLING_POLICY, billingPolicy.ToString());
            }

            var requestOptions = inputOptions.Extend().WithQueryParams(queryParams).Build();
            return await _client.Put<Subscription>(uri, subscription, requestOptions);
        }

        public async Task CancelSubscription(Guid subscriptionId, RequestOptions inputOptions, DateTime? requestedDate = null, bool? useRequestedDateForBilling = null, EntitlementActionPolicy? entitlementPolicy = null, BillingActionPolicy? billingPolicy = null)
        {
            var uri = Configuration.SUBSCRIPTIONS_PATH + "/" + subscriptionId;
            var queryParams = new MultiMap<string>().Create(inputOptions.QueryParams);

            // if (timeoutSec.HasValue)
            // {
            //     queryParams.Add(Configuration.QUERY_CALL_COMPLETION, timeoutSec.Value > 0 ? "true" : "false");
            //     queryParams.Add(Configuration.QUERY_CALL_TIMEOUT, timeoutSec.Value.ToString());
            //     timeoutSec = timeoutSec.Value;
            // }
            if (requestedDate.HasValue)
            {
                queryParams.Add(Configuration.QUERY_REQUESTED_DT, requestedDate.Value.ToShortDateString());
            }

            if (entitlementPolicy.HasValue)
            {
                queryParams.Add(Configuration.QUERY_ENTITLEMENT_POLICY, entitlementPolicy.ToString());
            }

            if (billingPolicy.HasValue)
            {
                queryParams.Add(Configuration.QUERY_BILLING_POLICY, billingPolicy.ToString());
            }

            if (useRequestedDateForBilling.HasValue)
            {
                queryParams.Add(Configuration.QUERY_USE_REQUESTED_DATE_FOR_BILLING, useRequestedDateForBilling.Value ? "true" : "false");
            }

            var requestOptions = inputOptions.Extend().WithQueryParams(queryParams).Build();
            await _client.Delete(uri, requestOptions);
        }

        public async Task UncancelSubscription(Guid subscriptionId, RequestOptions inputOptions, Dictionary<string, string> pluginProperties = null)
        {
            if (subscriptionId == Guid.Empty) throw new ArgumentNullException(nameof(subscriptionId));

            var uri = Configuration.SUBSCRIPTIONS_PATH + "/" + subscriptionId + "/" + Configuration.UNCANCEL;

            var queryParams = new MultiMap<string>().Create(inputOptions.QueryParams);
            StorePluginPropertiesAsParams(pluginProperties, ref queryParams);
            var requestOptions = inputOptions.Extend().WithQueryParams(queryParams).Build();

            await _client.Put(uri, null, requestOptions);
        }

        public async Task<Bundle> CreateSubscriptionWithAddOns(Subscription subscription, RequestOptions inputOptions, DateTime? requestedDate = null, int? timeoutSec = null)
        {
            ValidateSubscription(subscription);

            var queryParams = new MultiMap<string>().Create(inputOptions.QueryParams);
            if (timeoutSec.HasValue && timeoutSec.Value > 0)
            {
                queryParams.Add(Configuration.QUERY_CALL_COMPLETION, "true");
                queryParams.Add(Configuration.QUERY_CALL_TIMEOUT, timeoutSec.Value.ToString());
            }

            if (requestedDate.HasValue)
                queryParams.Add(Configuration.QUERY_REQUESTED_DT, requestedDate.Value.ToDateString());

            // var httpTimeout = Math.Max(Configuration.DEFAULT_HTTP_TIMEOUT_SEC, timeoutSec ?? 0);
            var uri = Configuration.SUBSCRIPTIONS_PATH + "/" + Configuration.CREATEENTITLEMENT_WITHADDONS;
            var followLocation = inputOptions.FollowLocation ?? true;
            var requestOptions = inputOptions.Extend().WithQueryParams(queryParams).WithFollowLocation(followLocation).Build();

            return await _client.Post<Bundle>(uri, subscription, requestOptions);
        }

        public async Task<Bundle> CreateSubscriptionsWithAddOns(IEnumerable<Subscription> subscriptions, RequestOptions inputOptions, DateTime? requestedDate = null, int? timeoutSec = null)
        {
            foreach (var subscription in subscriptions)
            {
                ValidateSubscription(subscription);
            }

            var queryParams = new MultiMap<string>().Create(inputOptions.QueryParams);
            if (timeoutSec.HasValue && timeoutSec.Value > 0)
            {
                queryParams.Add(Configuration.QUERY_CALL_COMPLETION, "true");
                queryParams.Add(Configuration.QUERY_CALL_TIMEOUT, timeoutSec.Value.ToString());
            }

            if (requestedDate.HasValue)
                queryParams.Add(Configuration.QUERY_REQUESTED_DT, requestedDate.Value.ToDateString());

            // var httpTimeout = Math.Max(Configuration.DEFAULT_HTTP_TIMEOUT_SEC, timeoutSec ?? 0);
            var uri = Configuration.SUBSCRIPTIONS_PATH + "/" + Configuration.CREATEENTITLEMENTS_WITHADDONS;
            var followLocation = inputOptions.FollowLocation ?? true;
            var requestOptions = inputOptions.Extend().WithQueryParams(queryParams).WithFollowLocation(followLocation).Build();

            return await _client.Post<Bundle>(uri, subscriptions, requestOptions);
        }

        public async Task BlockSubscription(Guid subscriptionId, BlockingState blockingState, RequestOptions inputOptions, DateTime? requestedDate = null, Dictionary<string, string> pluginProperties = null)
        {
            if (subscriptionId == Guid.Empty) throw new ArgumentNullException(nameof(subscriptionId));

            var uri = Configuration.SUBSCRIPTIONS_PATH + "/" + subscriptionId + "/" + Configuration.BLOCK;

            var queryParams = new MultiMap<string>().Create(inputOptions.QueryParams);
            if (requestedDate.HasValue) queryParams.Add(Configuration.QUERY_REQUESTED_DT, requestedDate.Value.ToDateString());
            StorePluginPropertiesAsParams(pluginProperties, ref queryParams);
            var requestOptions = inputOptions.Extend().WithQueryParams(queryParams).Build();

            await _client.Put(uri, blockingState, requestOptions);
        }

        private void ValidateSubscription(Subscription subscription, bool validateAccount = true)
        {
            if (!string.IsNullOrEmpty(subscription.PlanName)) return;

            if (validateAccount && subscription.ProductCategory == "BASE" && subscription.AccountId.Equals(Guid.Empty))
                throw new ArgumentException("Account#accountId cannot be empty for base subscription");

            if (string.IsNullOrEmpty(subscription.ProductName))
                throw new ArgumentException("Subscription#productName cannot be null");

            if (string.IsNullOrEmpty(subscription.ProductCategory))
                throw new ArgumentException("Subscription#productCategory cannot be null");

            if (string.IsNullOrEmpty(subscription.BillingPeriod))
                throw new ArgumentException("Subscription#billingPeriod cannot be null");

            if (string.IsNullOrEmpty(subscription.PriceList))
                throw new ArgumentException("Subscription#priceList cannot be null");
        }
    }
}