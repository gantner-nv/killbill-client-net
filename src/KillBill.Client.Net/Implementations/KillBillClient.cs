using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using KillBill.Client.Net.Configuration;
using KillBill.Client.Net.Data;
using KillBill.Client.Net.Extensions;
using KillBill.Client.Net.Implementations.Managers;
using KillBill.Client.Net.Infrastructure;
using KillBill.Client.Net.Interfaces;
using KillBill.Client.Net.Model;

namespace KillBill.Client.Net.Implementations
{
    public class KillBillClient : IKillBillClient
    {
        private const AuditLevel DefaultAuditLevel = AuditLevel.NONE;
        private readonly KillBillConfiguration _config;
        private readonly KillBillAccountManager _accountManager;
        private readonly KillBillBundleManager _bundleManager;
        private readonly KillBillCatalogManager _catalogManager;
        private readonly KillBillInvoiceManager _invoiceManager;
        private readonly KillBillNotificationManager _notificationManager;
        private readonly KillBillPaymentManager _paymentManager;
        private readonly KillBillSubscriptionManager _subscriptionManager;
        private readonly KillBillTenantManager _tenantManager;

        public KillBillClient(KillBillConfiguration configuration)
        {
            var client = new KillBillHttpClient(configuration);
            _config = client.Configuration;

            _accountManager = new KillBillAccountManager(client);
            _bundleManager = new KillBillBundleManager(client);
            _catalogManager = new KillBillCatalogManager(client);
            _invoiceManager = new KillBillInvoiceManager(client);
            _notificationManager = new KillBillNotificationManager(client);
            _paymentManager = new KillBillPaymentManager(client);
            _subscriptionManager = new KillBillSubscriptionManager(client);
            _tenantManager = new KillBillTenantManager(client);
        }

        public RequestOptions BaseOptions(string createdBy = null, string requestId = null, string reason = null, string comment = null)
        {
            return RequestOptions.Default(_config)
                                 .Extend()
                                 .WithCreatedBy(createdBy)
                                 .WithReason(reason)
                                 .WithComment(comment)
                                 .Build();
        }

        // ACCOUNT
        public async Task<Account> GetAccount(Guid accountId, RequestOptions inputOptions, bool withBalance = false, bool withCba = false)
        {
            return await _accountManager.GetAccount(accountId, inputOptions, withBalance, withCba);
        }

        public async Task<Account> GetAccount(string externalKey, RequestOptions inputOptions, bool withBalance = false, bool withCba = false)
        {
            return await _accountManager.GetAccount(externalKey, inputOptions, withBalance, withCba);
        }

        public async Task<Account> CreateAccount(Account account, RequestOptions inputOptions)
        {
            return await _accountManager.CreateAccount(account, inputOptions);
        }

        public async Task<Account> UpdateAccount(Account account, RequestOptions inputOptions)
        {
            return await _accountManager.UpdateAccount(account, inputOptions);
        }

        public async Task<Account> UpdateAccount(Account account, bool treatNullAsReset, RequestOptions inputOptions)
        {
            return await _accountManager.UpdateAccount(account, treatNullAsReset, inputOptions);
        }

        public async Task BlockAccount(Guid accountId, BlockingState blockingState, RequestOptions inputOptions, DateTime? requestedDate = null, Dictionary<string, string> pluginProperties = null)
        {
            await _accountManager.BlockAccount(accountId, blockingState, inputOptions, requestedDate, pluginProperties);
        }

        // ACCOUNTS
        public async Task<Accounts> GetAccounts(RequestOptions inputOptions)
        {
            return await _accountManager.GetAccounts(inputOptions);
        }

        public async Task<Accounts> GetAccounts(long offset, long limit, RequestOptions inputOptions, AuditLevel auditLevel = AuditLevel.NONE)
        {
            return await _accountManager.GetAccounts(offset, limit, inputOptions, auditLevel);
        }

        // ACCOUNT EMAILS
        public async Task<AccountEmails> GetEmailsForAccount(Guid accountId, RequestOptions inputOptions)
        {
            return await _accountManager.GetEmailsForAccount(accountId, inputOptions);
        }

        public async Task AddEmailToAccount(AccountEmail email, RequestOptions inputOptions)
        {
            await _accountManager.AddEmailToAccount(email, inputOptions);
        }

        public async Task RemoveEmailFromAccount(AccountEmail email, RequestOptions inputOptions)
        {
            await _accountManager.RemoveEmailFromAccount(email, inputOptions);
        }

        // ACCOUNT TIMELINE
        public async Task<AccountTimeline> GetAccountTimeline(Guid accountId, RequestOptions inputOptions, AuditLevel auditLevel = DefaultAuditLevel)
        {
            return await _accountManager.GetAccountTimeline(accountId, inputOptions, auditLevel);
        }

        // ACCOUNT BUNDLES
        public async Task<Bundles> GetAccountBundles(Guid accountId, RequestOptions inputOptions)
        {
            return await _accountManager.GetAccountBundles(accountId, inputOptions);
        }

        public async Task<Bundles> GetAccountBundles(Guid accountId, string externalKey, RequestOptions inputOptions)
        {
            return await _accountManager.GetAccountBundles(accountId, externalKey, inputOptions);
        }

        // BUNDLE
        public async Task<Bundle> GetBundle(Guid bundleId, RequestOptions inputOptions)
        {
            return await _bundleManager.GetBundle(bundleId, inputOptions);
        }

        public async Task<Bundle> GetBundle(string externalKey, RequestOptions inputOptions)
        {
            return await _bundleManager.GetBundle(externalKey, inputOptions);
        }

        public async Task<Bundle> TransferBundle(Bundle bundle, RequestOptions inputOptions)
        {
            return await _bundleManager.TransferBundle(bundle, inputOptions);
        }

        public async Task BlockBundle(Guid bundleId, BlockingState blockingState, RequestOptions inputOptions, DateTime? requestedDate = null, Dictionary<string, string> pluginProperties = null)
        {
            await _bundleManager.BlockBundle(bundleId, blockingState, inputOptions, requestedDate, pluginProperties);
        }

        public async Task PauseBundle(Guid bundleId, RequestOptions inputOptions, DateTime? requestedDate = null, Dictionary<string, string> pluginProperties = null)
        {
            await _bundleManager.PauseBundle(bundleId, inputOptions, requestedDate, pluginProperties);
        }

        public async Task ResumeBundle(Guid bundleId, RequestOptions inputOptions, DateTime? requestedDate = null, Dictionary<string, string> pluginProperties = null)
        {
            await _bundleManager.ResumeBundle(bundleId, inputOptions, requestedDate, pluginProperties);
        }

        // BUNDLES
        public async Task<Bundles> GetBundles(RequestOptions inputOptions, long offset = 0L, long limit = 100L, AuditLevel auditLevel = DefaultAuditLevel)
        {
            return await _bundleManager.GetBundles(inputOptions, offset, limit, auditLevel);
        }

        public async Task<Bundles> SearchBundles(string key, RequestOptions inputOptions, long offset = 0L, long limit = 100L, AuditLevel auditLevel = DefaultAuditLevel)
        {
            return await _bundleManager.SearchBundles(key, inputOptions, offset, limit, auditLevel);
        }

        // CATALOG
        public async Task<List<Catalog>> GetCatalogJson(RequestOptions inputOptions, DateTime? requestedDate = null)
        {
            return await _catalogManager.GetCatalogJson(inputOptions, requestedDate);
        }

        public async Task UploadCatalogXml(string catalogXml, RequestOptions inputOptions)
        {
            await _catalogManager.UploadCatalogXml(catalogXml, inputOptions);
        }

        public async Task<Plan> GetPlanFromSubscription(Guid subscriptionId, RequestOptions inputOptions, DateTime? requestedDate = null)
        {
            return await _catalogManager.GetPlanFromSubscription(subscriptionId, inputOptions, requestedDate);
        }

        // PRICE LIST
        public async Task<PriceList> GetPriceListFromSubscription(Guid subscriptionId, RequestOptions inputOptions, DateTime? requestedDate = null)
        {
            return await _catalogManager.GetPriceListFromSubscription(subscriptionId, inputOptions, requestedDate);
        }

        // PRODUCT
        public async Task<Product> GetProductFromSubscription(Guid subscriptionId, RequestOptions inputOptions, DateTime? requestedDate = null)
        {
            return await _catalogManager.GetProductFromSubscription(subscriptionId, inputOptions, requestedDate);
        }

        // INVOICE
        public async Task<Invoice> CreateInvoice(Guid accountId, DateTime futureDate, RequestOptions inputOptions)
        {
            return await _invoiceManager.CreateInvoice(accountId, futureDate, inputOptions);
        }

        public async Task<Invoice> GetInvoice(int invoiceNumber, RequestOptions inputOptions, bool withItems = false, bool withChildrenItems = false, AuditLevel auditLevel = DefaultAuditLevel)
        {
            return await _invoiceManager.GetInvoice(invoiceNumber, inputOptions, withItems, withChildrenItems, auditLevel);
        }

        public async Task<Invoice> GetInvoice(string invoiceIdOrNumber, RequestOptions inputOptions, bool withItems = false, bool withChildrenItems = false, AuditLevel auditLevel = DefaultAuditLevel)
        {
            return await _invoiceManager.GetInvoice(invoiceIdOrNumber, inputOptions, withItems, withChildrenItems, auditLevel);
        }

        // INVOICES
        public async Task<Invoices> GetInvoices(RequestOptions inputOptions)
        {
            return await _invoiceManager.GetInvoices(inputOptions);
        }

        public async Task<Invoices> GetInvoices(bool withItems, long offset, long limit, RequestOptions inputOptions, AuditLevel auditLevel = DefaultAuditLevel)
        {
            return await _invoiceManager.GetInvoices(withItems, offset, limit, inputOptions, auditLevel);
        }

        public async Task<Invoices> GetInvoicesForAccount(Guid accountId, RequestOptions inputOptions, bool withItems = false, bool unpaidOnly = false, bool includeMigrationInvoices = false, AuditLevel auditLevel = DefaultAuditLevel)
        {
            return await _invoiceManager.GetInvoicesForAccount(accountId, inputOptions, withItems, unpaidOnly, includeMigrationInvoices, auditLevel);
        }

        public async Task<Invoices> SearchInvoices(string key, RequestOptions inputOptions)
        {
            return await _invoiceManager.SearchInvoices(key, inputOptions);
        }

        public async Task<Invoices> SearchInvoices(string key, long offset, long limit, RequestOptions inputOptions)
        {
            return await _invoiceManager.SearchInvoices(key, offset, limit, inputOptions);
        }

        // INVOICE ITEM
        public async Task<List<InvoiceItem>> CreateExternalCharges(IEnumerable<InvoiceItem> externalCharges, DateTime? requestedDate, bool autoPay, bool autoCommit, RequestOptions inputOptions)
        {
            return await _invoiceManager.CreateExternalCharges(externalCharges, requestedDate, autoPay, autoCommit, inputOptions);
        }

        public async Task<List<InvoiceItem>> CreateExternalCharges(IEnumerable<InvoiceItem> externalCharges, DateTime? requestedDate, bool autoPay, bool autoCommit, string paymentExternalKey, string transactionExternalKey, RequestOptions inputOptions)
        {
            return await _invoiceManager.CreateExternalCharges(externalCharges, requestedDate, autoPay, autoCommit, paymentExternalKey, transactionExternalKey, inputOptions);
        }

        // CREDIT
        public async Task<Credit> CreateCredit(Credit credit, bool autoCommit, RequestOptions inputOptions)
        {
            return await _invoiceManager.CreateCredit(credit, autoCommit, inputOptions);
        }

        public async Task<Credit> GetCredit(Guid creditId, RequestOptions inputOptions, AuditLevel auditLevel = DefaultAuditLevel)
        {
            return await _invoiceManager.GetCredit(creditId, inputOptions, auditLevel);
        }

        // INVOICE EMAIL
        public async Task<InvoiceEmail> GetEmailNotificationsForAccount(Guid accountId, RequestOptions inputOptions)
        {
            return await _notificationManager.GetEmailNotificationsForAccount(accountId, inputOptions);
        }

        public async Task UpdateEmailNotificationsForAccount(InvoiceEmail invoiceEmail, RequestOptions inputOptions)
        {
            await _notificationManager.UpdateEmailNotificationsForAccount(invoiceEmail, inputOptions);
        }

        // PAYMENT
        public async Task<Payment> CreatePayment(Guid accountId, PaymentTransaction paymentTransaction, RequestOptions inputOptions)
        {
            return await _paymentManager.CreatePayment(accountId, paymentTransaction, inputOptions);
        }

        public async Task<Payment> CreatePayment(Guid accountId, PaymentTransaction paymentTransaction, Dictionary<string, string> pluginProperties, RequestOptions inputOptions)
        {
            return await _paymentManager.CreatePayment(accountId, paymentTransaction, pluginProperties, inputOptions);
        }

        public async Task<Payment> CreatePayment(Guid accountId, Guid? paymentMethodId, PaymentTransaction paymentTransaction, RequestOptions inputOptions)
        {
            return await _paymentManager.CreatePayment(accountId, paymentMethodId, paymentTransaction, inputOptions);
        }

        public async Task<Payment> CreatePayment(Guid accountId, Guid? paymentMethodId, PaymentTransaction paymentTransaction, Dictionary<string, string> pluginProperties, RequestOptions inputOptions)
        {
            return await _paymentManager.CreatePayment(accountId, paymentMethodId, paymentTransaction, pluginProperties, inputOptions);
        }

        public async Task<Payment> CreatePayment(Guid accountId, Guid? paymentMethodId, PaymentTransaction paymentTransaction, List<string> controlPluginNames, Dictionary<string, string> pluginProperties, RequestOptions inputOptions)
        {
            return await _paymentManager.CreatePayment(accountId, paymentMethodId, paymentTransaction, controlPluginNames, pluginProperties, inputOptions);
        }

        // PAYMENTS
        public async Task<Payments> GetPaymentsForAccount(Guid accountId, RequestOptions inputOptions, AuditLevel auditLevel = DefaultAuditLevel)
        {
            return await _paymentManager.GetPaymentsForAccount(accountId, inputOptions, auditLevel);
        }

        public async Task<InvoicePayments> GetInvoicePaymentsForAccount(Guid accountId, RequestOptions inputOptions, AuditLevel auditLevel = DefaultAuditLevel)
        {
            return await _paymentManager.GetInvoicePaymentsForAccount(accountId, inputOptions, auditLevel);
        }

        // PAYMENT METHOD
        public async Task<PaymentMethod> GetPaymentMethod(Guid paymentMethodId, RequestOptions inputOptions, bool withPluginInfo = false, AuditLevel auditLevel = DefaultAuditLevel)
        {
            return await _paymentManager.GetPaymentMethod(paymentMethodId, inputOptions, withPluginInfo, auditLevel);
        }

        public async Task<PaymentMethods> GetPaymentMethodsForAccount(Guid accountId, RequestOptions inputOptions, Dictionary<string, string> pluginProperties = null, bool withPluginInfo = false, AuditLevel auditLevel = DefaultAuditLevel)
        {
            return await _paymentManager.GetPaymentMethodsForAccount(accountId, inputOptions, pluginProperties, withPluginInfo, auditLevel);
        }

        public async Task<PaymentMethod> CreatePaymentMethod(PaymentMethod paymentMethod, RequestOptions inputOptions)
        {
            return await _paymentManager.CreatePaymentMethod(paymentMethod, inputOptions);
        }

        public async Task DeletePaymentMethod(Guid paymentMethodId, RequestOptions inputOptions, bool deleteDefault = false, bool forceDeleteDefault = false)
        {
            await _paymentManager.DeletePaymentMethod(paymentMethodId, inputOptions, deleteDefault, forceDeleteDefault);
        }

        public async Task UpdateDefaultPaymentMethod(Guid accountId, Guid paymentMethodId, RequestOptions inputOptions)
        {
            await _paymentManager.UpdateDefaultPaymentMethod(accountId, paymentMethodId, inputOptions);
        }

        // PLAN DETAIL
        public async Task<List<PlanDetail>> GetBasePlans(RequestOptions inputOptions)
        {
            return await _catalogManager.GetBasePlans(inputOptions);
        }

        public async Task<List<PlanDetail>> GetAvailableAddons(string baseProductName, RequestOptions inputOptions)
        {
            return await _catalogManager.GetAvailableAddons(baseProductName, inputOptions);
        }

        // SUBSCRIPTION
        public async Task<Subscription> GetSubscription(Guid subscriptionId, RequestOptions inputOptions)
        {
            return await _subscriptionManager.GetSubscription(subscriptionId, inputOptions);
        }

        public async Task<Subscription> CreateSubscription(Subscription subscription, RequestOptions inputOptions, DateTime? requestedDate = null, bool? isMigrated = null)
        {
            return await _subscriptionManager.CreateSubscription(subscription, inputOptions, requestedDate, isMigrated);
        }

        public async Task<Subscription> UpdateSubscription(Subscription subscription, RequestOptions inputOptions, BillingActionPolicy? billingPolicy = null, DateTime? requestedDate = null, bool? isMigrated = null)
        {
            return await _subscriptionManager.UpdateSubscription(subscription, inputOptions, billingPolicy, requestedDate, isMigrated);
        }

        public async Task CancelSubscription(Guid subscriptionId, RequestOptions inputOptions, DateTime? requestedDate = null, bool? useRequestedDateForBilling = null, EntitlementActionPolicy? entitlementPolicy = null, BillingActionPolicy? billingPolicy = null)
        {
            await _subscriptionManager.CancelSubscription(subscriptionId, inputOptions, requestedDate, useRequestedDateForBilling, entitlementPolicy, billingPolicy);
        }

        public async Task UncancelSubscription(Guid subscriptionId, RequestOptions inputOptions, Dictionary<string, string> pluginProperties = null)
        {
            await _subscriptionManager.UncancelSubscription(subscriptionId, inputOptions, pluginProperties);
        }

        public async Task<Bundle> CreateSubscriptionWithAddOns(Subscription subscription, RequestOptions inputOptions, DateTime? requestedDate = null, int? timeoutSec = null)
        {
            return await _subscriptionManager.CreateSubscriptionWithAddOns(subscription, inputOptions, requestedDate, timeoutSec);
        }

        public async Task<Bundle> CreateSubscriptionsWithAddOns(IEnumerable<Subscription> subscriptions, RequestOptions inputOptions, DateTime? requestedDate = null, int? timeoutSec = null)
        {
            return await _subscriptionManager.CreateSubscriptionsWithAddOns(subscriptions, inputOptions, requestedDate, timeoutSec);
        }

        public async Task BlockSubscription(Guid subscriptionId, BlockingState blockingState, RequestOptions inputOptions, DateTime? requestedDate = null, Dictionary<string, string> pluginProperties = null)
        {
            await _subscriptionManager.BlockSubscription(subscriptionId, blockingState, inputOptions, requestedDate, pluginProperties);
        }

        // TENANT
        public async Task<Tenant> CreateTenant(Tenant tenant, RequestOptions inputOptions, bool useGlobalDefault = true)
        {
            return await _tenantManager.CreateTenant(tenant, inputOptions, useGlobalDefault);
        }

        public async Task<Tenant> GetTenant(Guid tenantId, RequestOptions inputOptions)
        {
            return await _tenantManager.GetTenant(tenantId, inputOptions);
        }

        public async Task<Tenant> GetTenant(string apiKey, RequestOptions inputOptions)
        {
            return await _tenantManager.GetTenant(apiKey, inputOptions);
        }

        public async Task UnregisterCallbackNotificationForTenant(Guid tenantId, RequestOptions inputOptions)
        {
            await _tenantManager.UnregisterCallbackNotificationForTenant(tenantId, inputOptions);
        }

        // TENANT KEY
        public async Task<TenantKey> RegisterCallBackNotificationForTenant(string callback, RequestOptions inputOptions)
        {
            return await _tenantManager.RegisterCallBackNotificationForTenant(callback, inputOptions);
        }

        public async Task<TenantKey> GetCallbackNotificationForTenant(RequestOptions inputOptions)
        {
            return await _tenantManager.GetCallbackNotificationForTenant(inputOptions);
        }
    }
}