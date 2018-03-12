using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        private readonly KillBillPlanManager _planManager;
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
            _planManager = new KillBillPlanManager(client);
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
        public Account GetAccount(Guid accountId, RequestOptions inputOptions, bool withBalance = false, bool withCba = false)
        {
            return _accountManager.GetAccount(accountId, inputOptions, withBalance, withCba);
        }

        public Account GetAccount(string externalKey, RequestOptions inputOptions, bool withBalance = false, bool withCba = false)
        {
            return _accountManager.GetAccount(externalKey, inputOptions, withBalance, withCba);
        }

        public Account CreateAccount(Account account, RequestOptions inputOptions)
        {
            return _accountManager.CreateAccount(account, inputOptions);
        }

        public Account UpdateAccount(Account account, RequestOptions inputOptions)
        {
            return _accountManager.UpdateAccount(account, inputOptions);
        }

        public Account UpdateAccount(Account account, bool treatNullAsReset, RequestOptions inputOptions)
        {
            return _accountManager.UpdateAccount(account, treatNullAsReset, inputOptions);
        }

        public void BlockAccount(Guid accountId, BlockingState blockingState, RequestOptions inputOptions, DateTime? requestedDate = null, Dictionary<string, string> pluginProperties = null)
        {
            _accountManager.BlockAccount(accountId, blockingState, inputOptions, requestedDate, pluginProperties);
        }

        // ACCOUNTS
        public Accounts GetAccounts(RequestOptions inputOptions)
        {
            return _accountManager.GetAccounts(inputOptions);
        }

        public Accounts GetAccounts(long offset, long limit, RequestOptions inputOptions, AuditLevel auditLevel = AuditLevel.NONE)
        {
            return _accountManager.GetAccounts(offset, limit, inputOptions, auditLevel);
        }

        // ACCOUNT EMAILS
        public AccountEmails GetEmailsForAccount(Guid accountId, RequestOptions inputOptions)
        {
            return _accountManager.GetEmailsForAccount(accountId, inputOptions);
        }

        public void AddEmailToAccount(AccountEmail email, RequestOptions inputOptions)
        {
            _accountManager.AddEmailToAccount(email, inputOptions);
        }

        public void RemoveEmailFromAccount(AccountEmail email, RequestOptions inputOptions)
        {
            _accountManager.RemoveEmailFromAccount(email, inputOptions);
        }

        // ACCOUNT TIMELINE
        public AccountTimeline GetAccountTimeline(Guid accountId, RequestOptions inputOptions, AuditLevel auditLevel = DefaultAuditLevel)
        {
            return _accountManager.GetAccountTimeline(accountId, inputOptions, auditLevel);
        }

        // BUNDLE
        public Bundle GetBundle(Guid bundleId, RequestOptions inputOptions)
        {
            return _bundleManager.GetBundle(bundleId, inputOptions);
        }

        public Bundle GetBundle(string externalKey, RequestOptions inputOptions)
        {
            return _bundleManager.GetBundle(externalKey, inputOptions);
        }

        public Bundle TransferBundle(Bundle bundle, RequestOptions inputOptions)
        {
            return _bundleManager.TransferBundle(bundle, inputOptions);
        }

        public void BlockBundle(Guid bundleId, BlockingState blockingState, RequestOptions inputOptions, DateTime? requestedDate = null, Dictionary<string, string> pluginProperties = null)
        {
            _bundleManager.BlockBundle(bundleId, blockingState, inputOptions, requestedDate, pluginProperties);
        }

        public void PauseBundle(Guid bundleId, RequestOptions inputOptions, DateTime? requestedDate = null, Dictionary<string, string> pluginProperties = null)
        {
            _bundleManager.PauseBundle(bundleId, inputOptions, requestedDate, pluginProperties);
        }

        public void ResumeBundle(Guid bundleId, RequestOptions inputOptions, DateTime? requestedDate = null, Dictionary<string, string> pluginProperties = null)
        {
            _bundleManager.ResumeBundle(bundleId, inputOptions, requestedDate, pluginProperties);
        }

        // BUNDLES
        public Bundles GetAccountBundles(Guid accountId, RequestOptions inputOptions)
        {
            return _bundleManager.GetAccountBundles(accountId, inputOptions);
        }

        public Bundles GetAccountBundles(Guid accountId, string externalKey, RequestOptions inputOptions)
        {
            return _bundleManager.GetAccountBundles(accountId, externalKey, inputOptions);
        }

        public Bundles GetBundles(RequestOptions inputOptions, long offset = 0L, long limit = 100L, AuditLevel auditLevel = DefaultAuditLevel)
        {
            return _bundleManager.GetBundles(inputOptions, offset, limit, auditLevel);
        }

        public Bundles SearchBundles(string key, RequestOptions inputOptions, long offset = 0L, long limit = 100L, AuditLevel auditLevel = DefaultAuditLevel)
        {
            return _bundleManager.SearchBundles(key, inputOptions, offset, limit, auditLevel);
        }

        // CATALOG
        public List<Catalog> GetCatalogJson(RequestOptions inputOptions, DateTime? requestedDate = null)
        {
            return _catalogManager.GetCatalogJson(inputOptions, requestedDate);
        }

        public void UploadCatalogXml(string catalogXml, RequestOptions inputOptions)
        {
            _catalogManager.UploadCatalogXml(catalogXml, inputOptions);
        }

        // INVOICE
        public Invoice CreateInvoice(Guid accountId, DateTime futureDate, RequestOptions inputOptions)
        {
            return _invoiceManager.CreateInvoice(accountId, futureDate, inputOptions);
        }

        public Invoice GetInvoice(int invoiceNumber, RequestOptions inputOptions, bool withItems = false, bool withChildrenItems = false, AuditLevel auditLevel = DefaultAuditLevel)
        {
            return _invoiceManager.GetInvoice(invoiceNumber, inputOptions, withItems, withChildrenItems, auditLevel);
        }

        public Invoice GetInvoice(string invoiceIdOrNumber, RequestOptions inputOptions, bool withItems = false, bool withChildrenItems = false, AuditLevel auditLevel = DefaultAuditLevel)
        {
            return _invoiceManager.GetInvoice(invoiceIdOrNumber, inputOptions, withItems, withChildrenItems, auditLevel);
        }

        // INVOICES
        public Invoices GetInvoices(RequestOptions inputOptions)
        {
            return _invoiceManager.GetInvoices(inputOptions);
        }

        public Invoices GetInvoices(bool withItems, long offset, long limit, RequestOptions inputOptions, AuditLevel auditLevel = DefaultAuditLevel)
        {
            return _invoiceManager.GetInvoices(withItems, offset, limit, inputOptions, auditLevel);
        }

        public Invoices GetInvoicesForAccount(Guid accountId, RequestOptions inputOptions, bool withItems = false, bool unpaidOnly = false, bool includeMigrationInvoices = false, AuditLevel auditLevel = DefaultAuditLevel)
        {
            return _invoiceManager.GetInvoicesForAccount(accountId, inputOptions, withItems, unpaidOnly, includeMigrationInvoices, auditLevel);
        }

        public Invoices SearchInvoices(string key, RequestOptions inputOptions)
        {
            return _invoiceManager.SearchInvoices(key, inputOptions);
        }

        public Invoices SearchInvoices(string key, long offset, long limit, RequestOptions inputOptions)
        {
            return _invoiceManager.SearchInvoices(key, offset, limit, inputOptions);
        }

        // INVOICE ITEM
        public List<InvoiceItem> CreateExternalCharges(IEnumerable<InvoiceItem> externalCharges, DateTime? requestedDate, bool autoPay, bool autoCommit, RequestOptions inputOptions)
        {
            return _invoiceManager.CreateExternalCharges(externalCharges, requestedDate, autoPay, autoCommit, inputOptions);
        }

        public List<InvoiceItem> CreateExternalCharges(IEnumerable<InvoiceItem> externalCharges, DateTime? requestedDate, bool autoPay, bool autoCommit, string paymentExternalKey, string transactionExternalKey, RequestOptions inputOptions)
        {
            return _invoiceManager.CreateExternalCharges(externalCharges, requestedDate, autoPay, autoCommit, paymentExternalKey, transactionExternalKey, inputOptions);
        }

        // CREDIT
        public Credit CreateCredit(Credit credit, bool autoCommit, RequestOptions inputOptions)
        {
            return _invoiceManager.CreateCredit(credit, autoCommit, inputOptions);
        }

        public Credit GetCredit(Guid creditId, RequestOptions inputOptions, AuditLevel auditLevel = DefaultAuditLevel)
        {
            return _invoiceManager.GetCredit(creditId, inputOptions, auditLevel);
        }

        // INVOICE EMAIL
        public InvoiceEmail GetEmailNotificationsForAccount(Guid accountId, RequestOptions inputOptions)
        {
            return _notificationManager.GetEmailNotificationsForAccount(accountId, inputOptions);
        }

        public void UpdateEmailNotificationsForAccount(InvoiceEmail invoiceEmail, RequestOptions inputOptions)
        {
            _notificationManager.UpdateEmailNotificationsForAccount(invoiceEmail, inputOptions);
        }

        // PAYMENT
        public Payment CreatePayment(Guid accountId, PaymentTransaction paymentTransaction, RequestOptions inputOptions)
        {
            return _paymentManager.CreatePayment(accountId, paymentTransaction, inputOptions);
        }

        public Payment CreatePayment(Guid accountId, PaymentTransaction paymentTransaction, Dictionary<string, string> pluginProperties, RequestOptions inputOptions)
        {
            return _paymentManager.CreatePayment(accountId, paymentTransaction, pluginProperties, inputOptions);
        }

        public Payment CreatePayment(Guid accountId, Guid? paymentMethodId, PaymentTransaction paymentTransaction, RequestOptions inputOptions)
        {
            return _paymentManager.CreatePayment(accountId, paymentMethodId, paymentTransaction, inputOptions);
        }

        public Payment CreatePayment(Guid accountId, Guid? paymentMethodId, PaymentTransaction paymentTransaction, Dictionary<string, string> pluginProperties, RequestOptions inputOptions)
        {
            return _paymentManager.CreatePayment(accountId, paymentMethodId, paymentTransaction, pluginProperties, inputOptions);
        }

        public Payment CreatePayment(Guid accountId, Guid? paymentMethodId, PaymentTransaction paymentTransaction, List<string> controlPluginNames, Dictionary<string, string> pluginProperties, RequestOptions inputOptions)
        {
            return _paymentManager.CreatePayment(accountId, paymentMethodId, paymentTransaction, controlPluginNames, pluginProperties, inputOptions);
        }

        // PAYMENTS
        public Payments GetPaymentsForAccount(Guid accountId, RequestOptions inputOptions, AuditLevel auditLevel = DefaultAuditLevel)
        {
            return _paymentManager.GetPaymentsForAccount(accountId, inputOptions, auditLevel);
        }

        public InvoicePayments GetInvoicePaymentsForAccount(Guid accountId, RequestOptions inputOptions, AuditLevel auditLevel = DefaultAuditLevel)
        {
            return _paymentManager.GetInvoicePaymentsForAccount(accountId, inputOptions, auditLevel);
        }

        // PAYMENT METHOD
        public PaymentMethod GetPaymentMethod(Guid paymentMethodId, RequestOptions inputOptions, bool withPluginInfo = false, AuditLevel auditLevel = DefaultAuditLevel)
        {
            return _paymentManager.GetPaymentMethod(paymentMethodId, inputOptions, withPluginInfo, auditLevel);
        }

        public PaymentMethods GetPaymentMethodsForAccount(Guid accountId, RequestOptions inputOptions, Dictionary<string, string> pluginProperties = null, bool withPluginInfo = false, AuditLevel auditLevel = DefaultAuditLevel)
        {
            return _paymentManager.GetPaymentMethodsForAccount(accountId, inputOptions, pluginProperties, withPluginInfo, auditLevel);
        }

        public PaymentMethod CreatePaymentMethod(PaymentMethod paymentMethod, RequestOptions inputOptions)
        {
            return _paymentManager.CreatePaymentMethod(paymentMethod, inputOptions);
        }

        public void DeletePaymentMethod(Guid paymentMethodId, RequestOptions inputOptions, bool deleteDefault = false, bool forceDeleteDefault = false)
        {
            _paymentManager.DeletePaymentMethod(paymentMethodId, inputOptions, deleteDefault, forceDeleteDefault);
        }

        public void UpdateDefaultPaymentMethod(Guid accountId, Guid paymentMethodId, RequestOptions inputOptions)
        {
            _paymentManager.UpdateDefaultPaymentMethod(accountId, paymentMethodId, inputOptions);
        }

        // PLAN DETAIL
        public List<PlanDetail> GetBasePlans(RequestOptions inputOptions)
        {
            return _planManager.GetBasePlans(inputOptions);
        }

        public List<PlanDetail> GetAvailableAddons(string baseProductName, RequestOptions inputOptions)
        {
            return _planManager.GetAvailableAddons(baseProductName, inputOptions);
        }

        // SUBSCRIPTION
        public Subscription GetSubscription(Guid subscriptionId, RequestOptions inputOptions)
        {
            return _subscriptionManager.GetSubscription(subscriptionId, inputOptions);
        }

        public Subscription CreateSubscription(Subscription subscription, RequestOptions inputOptions, DateTime? requestedDate = null, bool? isMigrated = null)
        {
            return _subscriptionManager.CreateSubscription(subscription, inputOptions, requestedDate, isMigrated);
        }

        public Subscription UpdateSubscription(Subscription subscription, RequestOptions inputOptions, BillingActionPolicy? billingPolicy = null, DateTime? requestedDate = null, bool? isMigrated = null)
        {
            return _subscriptionManager.UpdateSubscription(subscription, inputOptions, billingPolicy, requestedDate, isMigrated);
        }

        public void CancelSubscription(Guid subscriptionId, RequestOptions inputOptions, DateTime? requestedDate = null, bool? useRequestedDateForBilling = null, EntitlementActionPolicy? entitlementPolicy = null, BillingActionPolicy? billingPolicy = null)
        {
            _subscriptionManager.CancelSubscription(subscriptionId, inputOptions, requestedDate, useRequestedDateForBilling, entitlementPolicy, billingPolicy);
        }

        public void UncancelSubscription(Guid subscriptionId, RequestOptions inputOptions, Dictionary<string, string> pluginProperties = null)
        {
            _subscriptionManager.UncancelSubscription(subscriptionId, inputOptions, pluginProperties);
        }

        public Bundle CreateSubscriptionWithAddOns(Subscription subscription, RequestOptions inputOptions, DateTime? requestedDate = null, int? timeoutSec = null)
        {
            return _subscriptionManager.CreateSubscriptionWithAddOns(subscription, inputOptions, requestedDate, timeoutSec);
        }

        public Bundle CreateSubscriptionsWithAddOns(IEnumerable<Subscription> subscriptions, RequestOptions inputOptions, DateTime? requestedDate = null, int? timeoutSec = null)
        {
            return _subscriptionManager.CreateSubscriptionsWithAddOns(subscriptions, inputOptions, requestedDate, timeoutSec);
        }

        public void BlockSubscription(Guid subscriptionId, BlockingState blockingState, RequestOptions inputOptions, DateTime? requestedDate = null, Dictionary<string, string> pluginProperties = null)
        {
            _subscriptionManager.BlockSubscription(subscriptionId, blockingState, inputOptions, requestedDate, pluginProperties);
        }

        // TENANT
        public Tenant CreateTenant(Tenant tenant, RequestOptions inputOptions, bool useGlobalDefault = true)
        {
            return _tenantManager.CreateTenant(tenant, inputOptions, useGlobalDefault);
        }

        public Tenant GetTenant(Guid tenantId, RequestOptions inputOptions)
        {
            return _tenantManager.GetTenant(tenantId, inputOptions);
        }

        public Tenant GetTenant(string apiKey, RequestOptions inputOptions)
        {
            return _tenantManager.GetTenant(apiKey, inputOptions);
        }

        public void UnregisterCallbackNotificationForTenant(Guid tenantId, RequestOptions inputOptions)
        {
            _tenantManager.UnregisterCallbackNotificationForTenant(tenantId, inputOptions);
        }

        // TENANT KEY
        public TenantKey RegisterCallBackNotificationForTenant(string callback, RequestOptions inputOptions)
        {
            return _tenantManager.RegisterCallBackNotificationForTenant(callback, inputOptions);
        }

        public TenantKey GetCallbackNotificationForTenant(RequestOptions inputOptions)
        {
            return _tenantManager.GetCallbackNotificationForTenant(inputOptions);
        }
    }
}