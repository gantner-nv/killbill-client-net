using System;
using System.Collections.Generic;
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
    public class KillBillAccountManager : KillBillBaseManager, IKillBillAccountManager
    {
        private IKbHttpClient _client;

        public KillBillAccountManager(IKbHttpClient client)
            : base(client.Configuration)
        {
            _client = client;
        }

        public async Task<Account> GetAccount(Guid accountId, RequestOptions inputOptions, bool withBalance = false, bool withCba = false)
        {
            var uri = Configuration.ACCOUNTS_PATH + "/" + accountId;

            var queryParams = new MultiMap<string>().Create(inputOptions.QueryParams);
            queryParams.Add(Configuration.QUERY_ACCOUNT_WITH_BALANCE, withBalance ? "true" : "false");
            queryParams.Add(Configuration.QUERY_ACCOUNT_WITH_BALANCE_AND_CBA, withCba ? "true" : "false");

            var requestOptions = inputOptions.Extend().WithQueryParams(queryParams).Build();
            return await _client.Get<Account>(uri, requestOptions);
        }

        public async Task<Account> GetAccount(string externalKey, RequestOptions inputOptions, bool withBalance = false, bool withCba = false)
        {
            var uri = Configuration.ACCOUNTS_PATH;
            var queryParams = new MultiMap<string>().Create(inputOptions.QueryParams);
            queryParams.Add(Configuration.QUERY_EXTERNAL_KEY, externalKey);
            queryParams.Add(Configuration.QUERY_ACCOUNT_WITH_BALANCE, withBalance ? "true" : "false");
            queryParams.Add(Configuration.QUERY_ACCOUNT_WITH_BALANCE_AND_CBA, withCba ? "true" : "false");
            var requestOptions = inputOptions.Extend().WithQueryParams(queryParams).Build();
            return await _client.Get<Account>(uri, requestOptions);
        }

        public async Task<Account> CreateAccount(Account account, RequestOptions inputOptions)
        {
            var followLocation = inputOptions.FollowLocation ?? true;
            var requestOptions = inputOptions.Extend().WithFollowLocation(followLocation).Build();
            return await _client.Post<Account>(Configuration.ACCOUNTS_PATH, account, requestOptions);
        }

        public async Task<Account> UpdateAccount(Account account, RequestOptions inputOptions)
        {
            return await UpdateAccount(account, false, inputOptions);
        }

        public async Task<Account> UpdateAccount(Account account, bool treatNullAsReset, RequestOptions inputOptions)
        {
            var uri = Configuration.ACCOUNTS_PATH + "/" + account.AccountId;
            var queryParams = new MultiMap<string>().Create(inputOptions.QueryParams);
            queryParams.Add(Configuration.QUERY_ACCOUNT_TREAT_NULL_AS_RESET, treatNullAsReset ? "true" : "false");
            var requestOptions = inputOptions.Extend().WithQueryParams(queryParams).Build();
            return await _client.Put<Account>(uri, account, requestOptions);
        }

        public async Task BlockAccount(Guid accountId, BlockingState blockingState, RequestOptions inputOptions, DateTime? requestedDate = null, Dictionary<string, string> pluginProperties = null)
        {
            if (accountId == Guid.Empty) throw new ArgumentNullException(nameof(accountId));

            var uri = Configuration.ACCOUNTS_PATH + "/" + accountId + "/" + Configuration.BLOCK;

            var queryParams = new MultiMap<string>().Create(inputOptions.QueryParams);
            if (requestedDate.HasValue) queryParams.Add(Configuration.QUERY_REQUESTED_DT, requestedDate.Value.ToDateString());
            StorePluginPropertiesAsParams(pluginProperties, ref queryParams);
            var requestOptions = inputOptions.Extend().WithQueryParams(queryParams).Build();

            await _client.Put(uri, blockingState, requestOptions);
        }

        // ACCOUNTS
        public async Task<Accounts> GetAccounts(RequestOptions inputOptions)
        {
            return await GetAccounts(0L, 100L, inputOptions);
        }

        public async Task<Accounts> GetAccounts(long offset, long limit, RequestOptions inputOptions, AuditLevel auditLevel = AuditLevel.NONE)
        {
            var uri = Configuration.ACCOUNTS_PATH + "/" + Configuration.PAGINATION;

            var queryParams = new MultiMap<string>().Create(inputOptions.QueryParams);
            queryParams.Add(Configuration.QUERY_SEARCH_OFFSET, offset.ToString());
            queryParams.Add(Configuration.QUERY_SEARCH_LIMIT, limit.ToString());
            queryParams.Add(Configuration.QUERY_AUDIT, auditLevel.ToString());

            var requestOptions = inputOptions.Extend().WithQueryParams(queryParams).Build();

            return await _client.Get<Accounts>(uri, requestOptions);
        }

        // ACCOUNT EMAILS
        public async Task<AccountEmails> GetEmailsForAccount(Guid accountId, RequestOptions inputOptions)
        {
            var uri = Configuration.ACCOUNTS_PATH + "/" + accountId + "/" + Configuration.EMAILS;
            return await _client.Get<AccountEmails>(uri, inputOptions);
        }

        public async Task AddEmailToAccount(AccountEmail email, RequestOptions inputOptions)
        {
            if (email == null)
                throw new ArgumentNullException(nameof(email));

            if (email.AccountId.Equals(Guid.Empty))
                throw new ArgumentException("AccountEmail#accountId cannot be empty");

            var uri = Configuration.ACCOUNTS_PATH + "/" + email.AccountId + "/" + Configuration.EMAILS;

            await _client.Post(uri, email, inputOptions);
        }

        public async Task RemoveEmailFromAccount(AccountEmail email, RequestOptions inputOptions)
        {
            if (email == null)
                throw new ArgumentNullException(nameof(email));

            if (email.AccountId.Equals(Guid.Empty))
                throw new ArgumentException("AccountEmail#accountId cannot be empty");

            var uri = Configuration.ACCOUNTS_PATH + "/" + email.AccountId + "/" + Configuration.EMAILS + "/" + HttpUtility.UrlEncode(email.Email);

            await _client.Delete(uri, inputOptions);
        }

        // ACCOUNT TIMELINE
        public async Task<AccountTimeline> GetAccountTimeline(Guid accountId, RequestOptions inputOptions, AuditLevel auditLevel = DefaultAuditLevel)
        {
            var uri = Configuration.ACCOUNTS_PATH + "/" + accountId + "/" + Configuration.TIMELINE;

            var queryParams = new MultiMap<string>().Create(inputOptions.QueryParams);
            queryParams.Add(Configuration.QUERY_AUDIT, auditLevel.ToString());
            var requestOptions = inputOptions.Extend().WithQueryParams(queryParams).Build();

            return await _client.Get<AccountTimeline>(uri, requestOptions);
        }

        // ACCOUNT BUNDLES
        public async Task<Bundles> GetAccountBundles(Guid accountId, RequestOptions inputOptions)
        {
            var uri = Configuration.ACCOUNTS_PATH + "/" + accountId + "/" + Configuration.BUNDLES;
            return await _client.Get<Bundles>(uri, inputOptions);
        }

        public async Task<Bundles> GetAccountBundles(Guid accountId, string externalKey, RequestOptions inputOptions)
        {
            var uri = Configuration.ACCOUNTS_PATH + "/" + accountId + "/" + Configuration.BUNDLES;

            var queryParams = new MultiMap<string>().Create(inputOptions.QueryParams);
            queryParams.Add(Configuration.QUERY_EXTERNAL_KEY, externalKey);
            var requestOptions = inputOptions.Extend().WithQueryParams(queryParams).Build();

            return await _client.Get<Bundles>(uri, requestOptions);
        }
    }
}