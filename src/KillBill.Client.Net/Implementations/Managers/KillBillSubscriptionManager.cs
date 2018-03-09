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
    public class KillBillSubscriptionManager : KillBillBaseManager, IKillBillSubscriptionManager
    {
        private readonly IKbHttpClient _client;

        public KillBillSubscriptionManager(IKbHttpClient client)
            : base(client.Configuration)
        {
            _client = client;
        }

        // SUBSCRIPTION
        public Subscription GetSubscription(Guid subscriptionId, RequestOptions inputOptions)
        {
            var uri = Configuration.SUBSCRIPTIONS_PATH + "/" + subscriptionId;
            return _client.Get<Subscription>(uri, inputOptions);
        }

        public Subscription CreateSubscription(Subscription subscription, RequestOptions inputOptions, DateTime? requestedDate = null, bool? isMigrated = null)
        {
            // var httpTimeout = Configuration.DEFAULT_HTTP_TIMEOUT_SEC;
            var followLocation = inputOptions.FollowLocation ?? true;

            if (subscription.PlanName == null)
            {
                if (subscription.AccountId.Equals(Guid.Empty))
                    throw new ArgumentException("Subscription#accountId cannot be empty");

                if (string.IsNullOrEmpty(subscription.ProductName))
                    throw new ArgumentException("Subscription#productName cannot be null");

                if (string.IsNullOrEmpty(subscription.ProductCategory))
                    throw new ArgumentException("Subscription#productCategory cannot be null");

                if (string.IsNullOrEmpty(subscription.BillingPeriod))
                    throw new ArgumentException("Subscription#billingPeriod cannot be null");

                if (string.IsNullOrEmpty(subscription.PriceList))
                    throw new ArgumentException("Subscription#priceList cannot be null");
            }

            if (subscription.ProductCategory == "BASE")
            {
                if (subscription.AccountId.Equals(Guid.Empty))
                    throw new ArgumentException("Subscription#accountId cannot be empty");
            }

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
            return _client.Post<Subscription>(Configuration.SUBSCRIPTIONS_PATH, subscription, requestOptions);
        }

        public Subscription UpdateSubscription(Subscription subscription, RequestOptions inputOptions, BillingActionPolicy? billingPolicy = null, DateTime? requestedDate = null, bool? isMigrated = null)
        {
            if (subscription.SubscriptionId.Equals(Guid.Empty))
                throw new ArgumentException("Subscription#subscriptionId cannot be empty");

            if (subscription.PlanName == null)
            {
                if (string.IsNullOrEmpty(subscription.ProductName))
                    throw new ArgumentException("Subscription#productName cannot be null");

                if (string.IsNullOrEmpty(subscription.ProductCategory))
                    throw new ArgumentException("Subscription#productCategory cannot be null");

                if (string.IsNullOrEmpty(subscription.BillingPeriod))
                    throw new ArgumentException("Subscription#billingPeriod cannot be null");

                if (string.IsNullOrEmpty(subscription.PriceList))
                    throw new ArgumentException("Subscription#priceList cannot be null");
            }

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
            return _client.Put<Subscription>(uri, subscription, requestOptions);
        }

        public void CancelSubscription(Guid subscriptionId, RequestOptions inputOptions, DateTime? requestedDate = null, bool? useRequestedDateForBilling = null, EntitlementActionPolicy? entitlementPolicy = null, BillingActionPolicy? billingPolicy = null)
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
            _client.Delete(uri, requestOptions);
        }

        public Bundle CreateSubscriptionWithAddOns(IEnumerable<Subscription> subscriptions, RequestOptions inputOptions, DateTime? requestedDate = null, int? timeoutSec = null)
        {
            foreach (var subscription in subscriptions)
            {
                if (string.IsNullOrEmpty(subscription.PlanName))
                {
                    if (string.IsNullOrEmpty(subscription.ProductName))
                        throw new ArgumentException("Subscription#productName cannot be null");

                    if (string.IsNullOrEmpty(subscription.ProductCategory))
                        throw new ArgumentException("Subscription#productCategory cannot be null");

                    if (string.IsNullOrEmpty(subscription.ProductCategory))
                        throw new ArgumentException("Subscription#billingPeriod cannot be null");

                    if (string.IsNullOrEmpty(subscription.PriceList))
                        throw new ArgumentException("Subscription#priceList cannot be null");

                    if (subscription.ProductCategory == "BASE" && subscription.AccountId.Equals(Guid.Empty))
                    {
                        throw new ArgumentException("Account#accountId cannot be null for base subscription");
                    }
                }
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
            var uri = Configuration.SUBSCRIPTIONS_PATH + "/createEntitlementWithAddOns";
            var followLocation = inputOptions.FollowLocation ?? true;
            var requestOptions =
                inputOptions.Extend().WithQueryParams(queryParams).WithFollowLocation(followLocation).Build();

            return _client.Post<Bundle>(uri, subscriptions, requestOptions);
        }
    }
}