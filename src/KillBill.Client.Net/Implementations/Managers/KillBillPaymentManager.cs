using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KillBill.Client.Net.Data;
using KillBill.Client.Net.Infrastructure;
using KillBill.Client.Net.Interfaces;
using KillBill.Client.Net.Interfaces.Managers;
using KillBill.Client.Net.Model;

namespace KillBill.Client.Net.Implementations.Managers
{
    public class KillBillPaymentManager : KillBillBaseManager, IKillBillPaymentManager
    {
        private readonly IKbHttpClient _client;

        public KillBillPaymentManager(IKbHttpClient client)
            : base(client.Configuration)
        {
            _client = client;
        }

        // PAYMENT
        public async Task<Payment> CreatePayment(Guid accountId, PaymentTransaction paymentTransaction, RequestOptions inputOptions)
        {
            return await CreatePayment(accountId, null, paymentTransaction, null, new Dictionary<string, string>(), inputOptions);
        }

        public async Task<Payment> CreatePayment(Guid accountId, PaymentTransaction paymentTransaction, Dictionary<string, string> pluginProperties, RequestOptions inputOptions)
        {
            return await CreatePayment(accountId, null, paymentTransaction, null, pluginProperties, inputOptions);
        }

        public async Task<Payment> CreatePayment(Guid accountId, Guid? paymentMethodId, PaymentTransaction paymentTransaction, RequestOptions inputOptions)
        {
            return await CreatePayment(accountId, paymentMethodId, paymentTransaction, null, new Dictionary<string, string>(), inputOptions);
        }

        public async Task<Payment> CreatePayment(Guid accountId, Guid? paymentMethodId, PaymentTransaction paymentTransaction, Dictionary<string, string> pluginProperties, RequestOptions inputOptions)
        {
            return await CreatePayment(accountId, paymentMethodId, paymentTransaction, null, pluginProperties, inputOptions);
        }

        public async Task<Payment> CreatePayment(Guid accountId, Guid? paymentMethodId, PaymentTransaction paymentTransaction, List<string> controlPluginNames, Dictionary<string, string> pluginProperties, RequestOptions inputOptions)
        {
            var allowedTransactionTypes = new[] { "AUTHORIZE", "CREDIT", "PURCHASE" };
            if (accountId.Equals(Guid.Empty))
                throw new ArgumentException("createPayment#accountId must not be empty");

            if (paymentTransaction == null)
                throw new ArgumentNullException(nameof(paymentTransaction));

            if (!allowedTransactionTypes.Contains(paymentTransaction.TransactionType))
                throw new ArgumentException("Invalid paymentTransaction type " + paymentTransaction.TransactionType);

            if (paymentTransaction.Amount <= 0)
                throw new ArgumentException("PaymentTransaction#amount cannot be 0 or less");

            if (paymentTransaction.Currency == null)
                throw new ArgumentException("PaymentTransaction#currency cannot be null");

            var uri = Configuration.ACCOUNTS_PATH + "/" + accountId + "/" + Configuration.PAYMENTS;

            var param = new MultiMap<string>().Create(inputOptions.QueryParams);

            if (paymentMethodId.HasValue)
                param.Add("paymentMethodId", paymentMethodId.ToString());

            if (controlPluginNames != null && controlPluginNames.Count > 0)
            {
                param.PutAll(Configuration.CONTROL_PLUGIN_NAME, controlPluginNames);
            }

            StorePluginPropertiesAsParams(pluginProperties, ref param);
            var followLocation = inputOptions.FollowLocation ?? true;
            var requestOptions = inputOptions.Extend().WithQueryParams(param).WithFollowLocation(followLocation).Build();

            return await _client.Post<Payment>(uri, paymentTransaction, requestOptions);
        }

        // PAYMENTS
        public async Task<Payments> GetPaymentsForAccount(Guid accountId, RequestOptions inputOptions, AuditLevel auditLevel = DefaultAuditLevel)
        {
            var uri = Configuration.ACCOUNTS_PATH + "/" + accountId + "/" + Configuration.PAYMENTS;

            var queryParams = new MultiMap<string>().Create(inputOptions.QueryParams);
            queryParams.Add(Configuration.QUERY_AUDIT, auditLevel.ToString());
            var requestOptions = inputOptions.Extend().WithQueryParams(queryParams).Build();

            return await _client.Get<Payments>(uri, requestOptions);
        }

        public async Task<InvoicePayments> GetInvoicePaymentsForAccount(Guid accountId, RequestOptions inputOptions, AuditLevel auditLevel = DefaultAuditLevel)
        {
            var uri = Configuration.ACCOUNTS_PATH + "/" + accountId + "/" + Configuration.INVOICE_PAYMENTS;

            var queryParams = new MultiMap<string>().Create(inputOptions.QueryParams);
            queryParams.Add(Configuration.QUERY_AUDIT, auditLevel.ToString());
            var requestOptions = inputOptions.Extend().WithQueryParams(queryParams).Build();

            return await _client.Get<InvoicePayments>(uri, requestOptions);
        }

        // PAYMENT METHODS
        public async Task<PaymentMethod> GetPaymentMethod(Guid paymentMethodId, RequestOptions inputOptions, bool withPluginInfo = false, AuditLevel auditLevel = DefaultAuditLevel)
        {
            var uri = Configuration.PAYMENT_METHODS_PATH + "/" + paymentMethodId;

            var queryParams = new MultiMap<string>().Create(inputOptions.QueryParams);
            queryParams.Add(Configuration.QUERY_WITH_PLUGIN_INFO, withPluginInfo.ToString());
            queryParams.Add(Configuration.QUERY_AUDIT, auditLevel.ToString());
            var requestOptions = inputOptions.Extend().WithQueryParams(queryParams).Build();

            return await _client.Get<PaymentMethod>(uri, requestOptions);
        }

        public async Task<PaymentMethods> GetPaymentMethodsForAccount(Guid accountId, RequestOptions inputOptions, Dictionary<string, string> pluginProperties = null, bool withPluginInfo = false, AuditLevel auditLevel = DefaultAuditLevel)
        {
            var uri = Configuration.ACCOUNTS_PATH + "/" + accountId + "/" + Configuration.PAYMENT_METHODS;

            var queryParams = new MultiMap<string>().Create(inputOptions.QueryParams);
            queryParams.Add(Configuration.QUERY_WITH_PLUGIN_INFO, withPluginInfo.ToString());
            queryParams.Add(Configuration.QUERY_AUDIT, auditLevel.ToString());
            StorePluginPropertiesAsParams(pluginProperties, ref queryParams);
            var requestOptions = inputOptions.Extend().WithQueryParams(queryParams).Build();

            return await _client.Get<PaymentMethods>(uri, requestOptions);
        }

        public async Task<PaymentMethod> CreatePaymentMethod(PaymentMethod paymentMethod, RequestOptions inputOptions)
        {
            if (paymentMethod == null)
                throw new ArgumentNullException(nameof(paymentMethod));

            if (paymentMethod.AccountId.Equals(Guid.Empty) || string.IsNullOrEmpty(paymentMethod.PluginName))
                throw new ArgumentException("paymentMethod#accountId and paymentMethod#pluginName must not be empty");

            var uri = Configuration.ACCOUNTS_PATH + "/" + paymentMethod.AccountId + "/" + Configuration.PAYMENT_METHODS;
            var followLocation = inputOptions.FollowLocation ?? true;
            var queryParams = new MultiMap<string>().Create(inputOptions.QueryParams);
            queryParams.Add(Configuration.QUERY_PAYMENT_METHOD_IS_DEFAULT, paymentMethod.IsDefault ? "true" : "false");

            var requestOptions = inputOptions.Extend().WithFollowLocation(followLocation).WithQueryParams(queryParams).Build();

            return await _client.Post<PaymentMethod>(uri, paymentMethod, requestOptions);
        }

        public async Task DeletePaymentMethod(Guid paymentMethodId, RequestOptions inputOptions, bool deleteDefault = false, bool forceDeleteDefault = false)
        {
            var uri = Configuration.PAYMENT_METHODS_PATH + "/" + paymentMethodId;
            var queryParams = new MultiMap<string>().Create(inputOptions.QueryParams);
            queryParams.Add(Configuration.QUERY_DELETE_DEFAULT_PM_WITH_AUTO_PAY_OFF, deleteDefault.ToString());
            queryParams.Add(Configuration.QUERY_FORCE_DEFAULT_PM_DELETION, forceDeleteDefault.ToString());
            var requestOptions = inputOptions.Extend().WithQueryParams(queryParams).Build();
            await _client.Delete(uri, requestOptions);
        }

        public async Task UpdateDefaultPaymentMethod(Guid accountId, Guid paymentMethodId, RequestOptions inputOptions)
        {
            var uri = Configuration.ACCOUNTS_PATH + "/" + accountId + "/" + Configuration.PAYMENT_METHODS + "/" + paymentMethodId + "/" + Configuration.PAYMENT_METHODS_DEFAULT_PATH_POSTFIX;
            await _client.Put(uri, null, inputOptions);
        }
    }
}