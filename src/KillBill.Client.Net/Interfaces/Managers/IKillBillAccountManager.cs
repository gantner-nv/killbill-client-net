using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KillBill.Client.Net.Data;
using KillBill.Client.Net.Model;

namespace KillBill.Client.Net.Interfaces.Managers
{
    public interface IKillBillAccountManager
    {
        // ACCOUNT
        Task<Account> GetAccount(Guid accountId, RequestOptions inputOptions, bool withBalance = false, bool withCba = false);

        Task<Account> GetAccount(string externalKey, RequestOptions inputOptions, bool withBalance = false, bool withCba = false);

        Task<Account> CreateAccount(Account account, RequestOptions inputOptions);

        Task<Account> UpdateAccount(Account account, RequestOptions inputOptions);

        Task<Account> UpdateAccount(Account account, bool treatNullAsReset, RequestOptions inputOptions);

        Task BlockAccount(Guid accountId, BlockingState blockingState, RequestOptions inputOptions, DateTime? requestedDate = null, Dictionary<string, string> pluginProperties = null);

        // ACCOUNTS
        Task<Accounts> GetAccounts(RequestOptions requestOptions);

        Task<Accounts> GetAccounts(long offset, long limit, RequestOptions inputOptions, AuditLevel auditLevel = AuditLevel.NONE);

        // ACCOUNT EMAILS
        Task<AccountEmails> GetEmailsForAccount(Guid accountId, RequestOptions inputOptions);

        Task AddEmailToAccount(AccountEmail email, RequestOptions inputOptions);

        Task RemoveEmailFromAccount(AccountEmail email, RequestOptions inputOptions);

        // ACCOUNT TIMELINE
        Task<AccountTimeline> GetAccountTimeline(Guid accountId, RequestOptions inputOptions, AuditLevel auditLevel = AuditLevel.NONE);

        // ACCOUNT BUNDLES
        Task<Bundles> GetAccountBundles(Guid accountId, RequestOptions inputOptions);

        Task<Bundles> GetAccountBundles(Guid accountId, string externalKey, RequestOptions inputOptions);
    }
}