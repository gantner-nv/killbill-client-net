using System;
using System.Collections.Generic;
using KillBill.Client.Net.Data;
using KillBill.Client.Net.Model;

namespace KillBill.Client.Net.Interfaces.Managers
{
    public interface IKillBillSubscriptionManager
    {
        // SUBSCRIPTION
        Subscription GetSubscription(Guid subscriptionId, RequestOptions inputOptions);

        Subscription CreateSubscription(Subscription subscription, RequestOptions inputOptions, DateTime? requestedDate = null, bool? isMigrated = null);

        Subscription UpdateSubscription(Subscription subscription, RequestOptions inputOptions, BillingActionPolicy? billingPolicy = null, DateTime? requestedDate = null, bool? isMigrated = null);

        void CancelSubscription(Guid subscriptionId, RequestOptions inputOptions, DateTime? requestedDate = null, bool? useRequestedDateForBilling = null, EntitlementActionPolicy? entitlementPolicy = null, BillingActionPolicy? billingPolicy = null);

        void UncancelSubscription(Guid subscriptionId, RequestOptions inputOptions, Dictionary<string, string> pluginProperties = null);

        Bundle CreateSubscriptionWithAddOns(Subscription subscription, RequestOptions inputOptions, DateTime? requestedDate = null, int? timeoutSec = null);

        Bundle CreateSubscriptionsWithAddOns(IEnumerable<Subscription> subscriptions, RequestOptions inputOptions, DateTime? requestedDate = null, int? timeoutSec = null);

        void BlockSubscription(Guid subscriptionId, BlockingState blockingState, RequestOptions inputOptions, DateTime? requestedDate = null, Dictionary<string, string> pluginProperties = null);
    }
}