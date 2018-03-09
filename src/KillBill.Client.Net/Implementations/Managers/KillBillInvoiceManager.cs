using System;
using System.Collections.Generic;
using System.Text;
using KillBill.Client.Net.Data;
using KillBill.Client.Net.Extensions;
using KillBill.Client.Net.Infrastructure;
using KillBill.Client.Net.Interfaces;
using KillBill.Client.Net.Interfaces.Managers;
using KillBill.Client.Net.Model;

namespace KillBill.Client.Net.Implementations.Managers
{
    public class KillBillInvoiceManager : KillBillBaseManager, IKillBillInvoiceManager
    {
        private readonly IKbHttpClient _client;

        public KillBillInvoiceManager(IKbHttpClient client)
            : base(client.Configuration)
        {
            _client = client;
        }

        // INVOICE
        public Invoice CreateInvoice(Guid accountId, DateTime futureDate, RequestOptions inputOptions)
        {
            var followLocation = inputOptions.FollowLocation ?? true;

            var queryParams = new MultiMap<string>().Create(inputOptions.QueryParams);
            queryParams.Add(Configuration.QUERY_ACCOUNT_ID, accountId.ToString());
            queryParams.Add(Configuration.QUERY_TARGET_DATE, futureDate.ToDateString());

            var requestOptions = inputOptions.Extend().WithFollowLocation(followLocation).WithQueryParams(queryParams).Build();

            return _client.Post<Invoice>(Configuration.INVOICES_PATH, null, requestOptions);
        }

        public Invoice GetInvoice(int invoiceNumber, RequestOptions inputOptions, bool withItems = false, bool withChildrenItems = false, AuditLevel auditLevel = DefaultAuditLevel)
        {
            return GetInvoiceByIdOrNumber(invoiceNumber.ToString(), inputOptions, withItems, withChildrenItems, auditLevel);
        }

        public Invoice GetInvoice(string invoiceIdOrNumber, RequestOptions inputOptions, bool withItems = false, bool withChildrenItems = false, AuditLevel auditLevel = DefaultAuditLevel)
        {
            return GetInvoiceByIdOrNumber(invoiceIdOrNumber, inputOptions, withItems, withChildrenItems, auditLevel);
        }

        // INVOICES
        public Invoices GetInvoices(RequestOptions inputOptions)
        {
            return GetInvoices(true, 0L, 100L, inputOptions);
        }

        public Invoices GetInvoices(bool withItems, long offset, long limit, RequestOptions inputOptions, AuditLevel auditLevel = AuditLevel.NONE)
        {
            var uri = Configuration.INVOICES_PATH + "/" + Configuration.PAGINATION;

            var queryParams = new MultiMap<string>().Create(inputOptions.QueryParams);
            queryParams.Add(Configuration.QUERY_SEARCH_OFFSET, offset.ToString());
            queryParams.Add(Configuration.QUERY_SEARCH_LIMIT, limit.ToString());
            queryParams.Add(Configuration.QUERY_INVOICE_WITH_ITEMS, withItems.ToString());
            queryParams.Add(Configuration.QUERY_AUDIT, auditLevel.ToString());

            var requestOptions = inputOptions.Extend().WithQueryParams(queryParams).Build();

            return _client.Get<Invoices>(uri, requestOptions);
        }

        public Invoices GetInvoicesForAccount(Guid accountId, RequestOptions inputOptions, bool withItems = false, bool unpaidOnly = false, bool includeMigrationInvoices = false, AuditLevel auditLevel = DefaultAuditLevel)
        {
            var uri = Configuration.ACCOUNTS_PATH + "/" + accountId + "/" + Configuration.INVOICES;

            var queryParams = new MultiMap<string>().Create(inputOptions.QueryParams);
            queryParams.Add(Configuration.QUERY_INVOICE_WITH_ITEMS, withItems.ToString());
            queryParams.Add(Configuration.QUERY_UNPAID_INVOICES_ONLY, unpaidOnly.ToString());
            queryParams.Add(Configuration.QUERY_WITH_MIGRATION_INVOICES, includeMigrationInvoices.ToString());
            queryParams.Add(Configuration.QUERY_AUDIT, auditLevel.ToString());

            var requestOptions = inputOptions.Extend().WithQueryParams(queryParams).Build();

            return _client.Get<Invoices>(uri, requestOptions);
        }

        public Invoices SearchInvoices(string key, RequestOptions inputOptions)
        {
            return SearchInvoices(key, 0L, 100L, DefaultAuditLevel, inputOptions);
        }

        public Invoices SearchInvoices(string key, long offset, long limit, RequestOptions inputOptions)
        {
            return SearchInvoices(key, offset, limit, DefaultAuditLevel, inputOptions);
        }

        // INVOICE ITEM
        public List<InvoiceItem> CreateExternalCharges(IEnumerable<InvoiceItem> externalCharges, DateTime? requestedDate, bool autoPay, bool autoCommit, RequestOptions inputOptions)
        {
            return CreateExternalCharges(externalCharges, requestedDate, autoPay, autoCommit, null, null, inputOptions);
        }

        public List<InvoiceItem> CreateExternalCharges(IEnumerable<InvoiceItem> externalCharges, DateTime? requestedDate, bool autoPay, bool autoCommit, string paymentExternalKey, string transactionExternalKey, RequestOptions inputOptions)
        {
            var externalChargesPerAccount = new Dictionary<Guid, List<InvoiceItem>>();

            foreach (var externalCharge in externalCharges)
            {
                if (externalCharge.AccountId == Guid.Empty)
                    throw new ArgumentException("InvoiceItem#accountId cannot be empty");

                if (string.IsNullOrEmpty(externalCharge.Currency))
                    throw new ArgumentException("InvoiceItem#currency cannot be empty");

                if (!externalChargesPerAccount.ContainsKey(externalCharge.AccountId))
                    externalChargesPerAccount.Add(externalCharge.AccountId, new List<InvoiceItem>());

                externalChargesPerAccount[externalCharge.AccountId].Add(externalCharge);
            }

            var createdExternalCharges = new List<InvoiceItem>();
            foreach (var accountId in externalChargesPerAccount.Keys)
            {
                var invoiceItems = CreateExternalCharges(accountId, externalChargesPerAccount[accountId], requestedDate, autoPay, autoCommit, paymentExternalKey, paymentExternalKey, inputOptions);
                createdExternalCharges.AddRange(invoiceItems);
            }

            return createdExternalCharges;
        }

        // CREDIT
        public Credit CreateCredit(Credit credit, bool autoCommit, RequestOptions inputOptions)
        {
            if (credit == null)
                throw new ArgumentNullException(nameof(credit));
            if (credit.AccountId.Equals(Guid.Empty))
                throw new ArgumentException("Credit#accountId cannot be null");
            if (credit.CreditAmount <= 0)
                throw new ArgumentException("Credit#CreditAmount must be greater than 0");

            var followLocation = inputOptions.FollowLocation ?? true;
            var queryParams = new MultiMap<string>().Create(inputOptions.QueryParams);
            queryParams.Add(Configuration.QUERY_AUTO_COMMIT, autoCommit.ToString());

            var requestOptions = inputOptions.Extend().WithFollowLocation(followLocation).WithQueryParams(queryParams).Build();

            return _client.Post<Credit>(Configuration.CREDITS_PATH, credit, requestOptions);
        }

        public Credit GetCredit(Guid creditId, RequestOptions inputOptions, AuditLevel auditLevel = DefaultAuditLevel)
        {
            var uri = Configuration.CREDITS_PATH + "/" + creditId;
            var queryParams = new MultiMap<string>().Create(inputOptions.QueryParams);
            queryParams.Add(Configuration.QUERY_AUDIT, auditLevel.ToString());
            var requestOptions = inputOptions.Extend().WithQueryParams(queryParams).Build();
            return _client.Get<Credit>(uri, requestOptions);
        }

        private Invoices SearchInvoices(string key, long offset, long limit, AuditLevel auditLevel, RequestOptions inputOptions)
        {
            var utf = Encoding.UTF8.GetBytes(key);
            var uri = Configuration.INVOICES_PATH + "/" + Configuration.SEARCH + "/" + Encoding.UTF8.GetString(utf);

            var queryParams = new MultiMap<string>().Create(inputOptions.QueryParams);
            queryParams.Add(Configuration.QUERY_SEARCH_OFFSET, offset.ToString());
            queryParams.Add(Configuration.QUERY_SEARCH_LIMIT, limit.ToString());
            queryParams.Add(Configuration.QUERY_AUDIT, auditLevel.ToString());

            var requestOptions = inputOptions.Extend().WithQueryParams(queryParams).Build();
            return _client.Get<Invoices>(uri, requestOptions);
        }

        private Invoice GetInvoiceByIdOrNumber(string invoiceIdOrNumber, RequestOptions inputOptions, bool withItems = false, bool withChildrenItems = false, AuditLevel auditLevel = DefaultAuditLevel)
        {
            var uri = Configuration.INVOICES_PATH + "/" + invoiceIdOrNumber;

            var queryParams = new MultiMap<string>().Create(inputOptions.QueryParams);
            queryParams.Add(Configuration.QUERY_INVOICE_WITH_ITEMS, withItems.ToString());
            queryParams.Add(Configuration.QUERY_INVOICE_WITH_CHILDREN_ITEMS, withChildrenItems.ToString());
            queryParams.Add(Configuration.QUERY_AUDIT, auditLevel.ToString());

            var requestOptions = inputOptions.Extend().WithQueryParams(queryParams).Build();

            return _client.Get<Invoice>(uri, requestOptions);
        }

        private IEnumerable<InvoiceItem> CreateExternalCharges(Guid accountId, IEnumerable<InvoiceItem> externalCharges, DateTime? requestedDate, bool autoPay, bool autoCommit, string paymentExternalKey, string transactionExternalKey, RequestOptions inputOptions)
        {
            var uri = Configuration.INVOICES_PATH + "/" + Configuration.CHARGES + "/" + accountId;

            var queryParams = new MultiMap<string>();

            if (requestedDate.HasValue)
            {
                queryParams.Add(Configuration.QUERY_REQUESTED_DT, requestedDate.Value.ToDateString());
            }

            queryParams.Add(Configuration.QUERY_PAY_INVOICE, autoPay.ToString());
            queryParams.Add(Configuration.QUERY_AUTO_COMMIT, autoCommit.ToString());

            if (!string.IsNullOrEmpty(paymentExternalKey))
            {
                queryParams.Add(Configuration.QUERY_PAYMENT_EXT_KEY, paymentExternalKey);
            }

            if (!string.IsNullOrEmpty(transactionExternalKey))
            {
                queryParams.Add(Configuration.QUERY_TRANSACTION_EXT_KEY, transactionExternalKey);
            }

            var requestOptions = inputOptions.Extend().WithQueryParams(queryParams).Build();

            return _client.Post<List<InvoiceItem>>(uri, externalCharges, requestOptions);
        }
    }
}