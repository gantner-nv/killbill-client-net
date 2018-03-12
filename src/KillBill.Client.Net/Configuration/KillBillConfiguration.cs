namespace KillBill.Client.Net.Configuration
{
    public class KillBillConfiguration
    {
        // Configurable items
        public string ServerUrl { get; set; }

        public string ApiKey { get; set; }

        public string ApiSecret { get; set; }

        public string ApiProxy { get; set; }

        public string HttpUser { get; set; }

        public string HttpPassword { get; set; }

        // General settings
        public string API_PREFIX { get; set; } = string.Empty;

        public string API_VERSION { get; set; } = "/1.0";

        public string API_POSTFIX { get; set; } = "/kb";

        public string AUDIT_OPTION_CREATED_BY { get; set; } = "__AUDIT_OPTION_CREATED_BY";

        public string AUDIT_OPTION_REASON { get; set; } = "__AUDIT_OPTION_REASON";

        public string AUDIT_OPTION_COMMENT { get; set; } = "__AUDIT_OPTION_COMMENT";

        public string TENANT_OPTION_API_KEY { get; set; } = "__TENANT_OPTION_API_KEY";

        public string TENANT_OPTION_API_SECRET { get; set; } = "__TENANT_OPTION_API_SECRET";

        public string CONTROL_PLUGIN_NAME { get; set; } = "controlPluginName";

        public string PREFIX
        {
            get { return API_PREFIX + API_VERSION + API_POSTFIX; }
        }

        public int DEFAULT_HTTP_TIMEOUT_SEC { get; set; } = 10;

        // Resource paths        
        public string ACCOUNTS { get; set; } = "accounts";

        public string ACCOUNTS_PATH
        {
            get { return PREFIX + "/" + ACCOUNTS; }
        }

        public string BLOCK { get; set; } = "block";

        public string BUNDLES { get; set; } = "bundles";

        public string BUNDLES_PATH
        {
            get { return PREFIX + "/" + BUNDLES; }
        }

        public string CATALOG { get; set; } = "catalog";

        public string CATALOG_PATH
        {
            get { return PREFIX + "/" + CATALOG; }
        }

        public string CHARGEBACKS { get; set; } = "chargebacks";

        public string CHARGES { get; set; } = "charges";

        public string CREATEENTITLEMENT_WITHADDONS { get; set; } = "createEntitlementWithAddOns";

        public string CREATEENTITLEMENTS_WITHADDONS { get; set; } = "createEntitlementsWithAddOns";

        public string CREDITS { get; set; } = "credits";

        public string CREDITS_PATH
        {
            get { return PREFIX + "/" + CREDITS; }
        }

        public string CUSTOM_FIELDS { get; set; } = "customFields";

        public string CUSTOM_FIELDS_PATH
        {
            get { return PREFIX + "/" + CUSTOM_FIELDS; }
        }

        public string EMAILS { get; set; } = "emails";

        public string EMAIL_NOTIFICATIONS { get; set; } = "emailNotifications";

        public string FORM { get; set; } = "form";

        public string HOSTED { get; set; } = "hosted";

        public string INVOICES { get; set; } = "invoices";

        public string INVOICES_PATH
        {
            get { return PREFIX + "/" + INVOICES; }
        }

        public string DRY_RUN { get; set; } = "dryRun";

        public string INVOICE_PAYMENTS { get; set; } = "invoicePayments";

        public string INVOICE_PAYMENTS_PATH
        {
            get { return PREFIX + "/" + INVOICE_PAYMENTS; }
        }

        public string NOTIFICATION { get; set; } = "notification";

        public string OVERDUE { get; set; } = "overdue";

        public string PAGINATION { get; set; } = "pagination";

        public string PAUSE { get; set; } = "pause";

        public string PAYMENTS { get; set; } = "payments";

        public string PAYMENTS_PATH
        {
            get { return PREFIX + "/" + PAYMENTS; }
        }

        public string PAYMENT_GATEWAYS { get; set; } = "paymentGateways";

        public string PAYMENT_GATEWAYS_PATH
        {
            get { return PREFIX + "/" + PAYMENT_GATEWAYS; }
        }

        public string PAYMENT_METHODS { get; set; } = "paymentMethods";

        public string PAYMENT_METHODS_DEFAULT_PATH_POSTFIX { get; set; } = "setDefault";

        public string PAYMENT_METHODS_PATH
        {
            get { return PREFIX + "/" + PAYMENT_METHODS; }
        }

        public string PLUGINS { get; set; } = "plugins";

        public string PLUGINS_PATH
        {
            get { return "/" + PLUGINS; }
        }

        public string REFUNDS { get; set; } = "refunds";

        public string REGISTER_NOTIFICATION_CALLBACK { get; set; } = "registerNotificationCallback";

        public string LEGACY_REGISTER_NOTIFICATION_CALLBACK { get; set; } = "REGISTER_NOTIFICATION_CALLBACK";

        public string RESUME { get; set; } = "resume";

        public string SEARCH { get; set; } = "search";

        public string SECURITY { get; set; } = "security";

        public string SECURITY_PATH
        {
            get { return PREFIX + "/" + SECURITY; }
        }

        public string SUBSCRIPTIONS { get; set; } = "subscriptions";

        public string SUBSCRIPTIONS_PATH
        {
            get { return PREFIX + "/" + SUBSCRIPTIONS; }
        }

        public string TAGS { get; set; } = "tags";

        public string TAGS_PATH
        {
            get { return PREFIX + "/" + TAGS; }
        }

        public string TAG_DEFINITIONS { get; set; } = "tagDefinitions";

        public string TAG_DEFINITIONS_PATH
        {
            get { return PREFIX + "/" + TAG_DEFINITIONS; }
        }

        public string TENANTS { get; set; } = "tenants";

        public string TENANTS_PATH
        {
            get { return PREFIX + "/" + TENANTS; }
        }

        public string TIMELINE { get; set; } = "timeline";

        public string UNCANCEL { get; set; } = "uncancel";

        // Query parameters
        public string QUERY_ACCOUNT_ID { get; set; } = "accountId";

        public string QUERY_ACCOUNT_WITH_BALANCE { get; set; } = "accountWithBalance";

        public string QUERY_ACCOUNT_WITH_BALANCE_AND_CBA { get; set; } = "accountWithBalanceAndCBA";

        public string QUERY_ACCOUNT_TREAT_NULL_AS_RESET { get; set; } = "treatNullAsReset";

        public string QUERY_API_KEY { get; set; } = "apiKey";

        public string QUERY_AUDIT { get; set; } = "audit";

        public string QUERY_BILLING_POLICY { get; set; } = "billingPolicy";

        public string QUERY_USE_REQUESTED_DATE_FOR_BILLING { get; set; } = "useRequestedDateForBilling";

        public string QUERY_CALL_COMPLETION { get; set; } = "callCompletion";

        public string QUERY_CALL_TIMEOUT { get; set; } = "callTimeoutSec";

        public string QUERY_CUSTOM_FIELDS { get; set; } = "customFieldList";

        public string QUERY_DELETE_DEFAULT_PM_WITH_AUTO_PAY_OFF { get; set; } = "deleteDefaultPmWithAutoPayOff";

        public string QUERY_FORCE_DEFAULT_PM_DELETION { get; set; } = "forceDefaultPmDeletion";

        public string QUERY_DRY_RUN { get; set; } = "dryRun";

        public string QUERY_ENTITLEMENT_POLICY { get; set; } = "entitlementPolicy";

        public string QUERY_EXTERNAL_KEY { get; set; } = "externalKey";

        public string QUERY_INVOICE_WITH_ITEMS { get; set; } = "withItems";

        public string QUERY_INVOICE_WITH_CHILDREN_ITEMS { get; set; } = "withChildrenItems";

        public string QUERY_NOTIFICATION_CALLBACK { get; set; } = "cb";

        public string QUERY_PAYMENT_EXTERNAL { get; set; } = "externalPayment";

        public string QUERY_PAYMENT_METHOD_IS_DEFAULT { get; set; } = "isDefault";

        public string QUERY_PAYMENT_PLUGIN_NAME { get; set; } = "pluginName";

        public string QUERY_PAY_INVOICE { get; set; } = "payInvoice";

        public string QUERY_AUTO_COMMIT { get; set; } = "autoCommit";

        public string QUERY_PLUGIN_PROPERTY { get; set; } = "pluginProperty";

        public string QUERY_REQUESTED_DT { get; set; } = "requestedDate";

        public string QUERY_PAYMENT_EXT_KEY { get; set; } = "paymentExternalKey";

        public string QUERY_TRANSACTION_EXT_KEY { get; set; } = "transactionExternalKey";

        public string QUERY_SEARCH_LIMIT { get; set; } = "limit";

        public string QUERY_SEARCH_OFFSET { get; set; } = "offset";

        public string QUERY_TAGS { get; set; } = "tagList";

        public string QUERY_TARGET_DATE { get; set; } = "targetDate";

        public string QUERY_UNPAID_INVOICES_ONLY { get; set; } = "unpaidInvoicesOnly";

        public string QUERY_WITH_MIGRATION_INVOICES { get; set; } = "withMigrationInvoices";

        public string QUERY_PAYMENT_METHOD_PLUGIN_NAME { get; set; } = "pluginName";

        public string QUERY_WITH_PLUGIN_INFO { get; set; } = "withPluginInfo";

        public string QUERY_TENANT_USE_GLOBAL_DEFAULT { get; set; } = "useGlobalDefault";

        public string QUERY_MIGRATED { get; set; } = "migrated";

        // Metadata Additional headers
        public string HDR_API_KEY { get; set; } = "X-Killbill-ApiKey";

        public string HDR_API_SECRET { get; set; } = "X-Killbill-ApiSecret";

        public string HDR_CREATED_BY { get; set; } = "X-Killbill-CreatedBy";

        public string HDR_REASON { get; set; } = "X-Killbill-Reason";

        public string HDR_COMMENT { get; set; } = "X-Killbill-Comment";

        public string HDR_PAGINATION_CURRENT_OFFSET { get; set; } = "X-Killbill-Pagination-CurrentOffset";

        public string HDR_PAGINATION_NEXT_OFFSET { get; set; } = "X-Killbill-Pagination-NextOffset";

        public string HDR_PAGINATION_TOTAL_NB_RECORDS { get; set; } = "X-Killbill-Pagination-TotalNbRecords";

        public string HDR_PAGINATION_MAX_NB_RECORDS { get; set; } = "X-Killbill-Pagination-MaxNbRecords";

        public string HDR_PAGINATION_NEXT_PAGE_URI { get; set; } = "X-Killbill-Pagination-NextPageUri";
    }
}