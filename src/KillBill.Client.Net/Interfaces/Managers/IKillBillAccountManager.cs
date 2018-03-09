using System;
using System.Collections.Generic;
using KillBill.Client.Net.Data;
using KillBill.Client.Net.Model;

namespace KillBill.Client.Net.Interfaces.Managers
{
    public interface IKillBillAccountManager
    {
        // ACCOUNT
        Account GetAccount(Guid accountId, RequestOptions inputOptions, bool withBalance = false, bool withCba = false);

        Account GetAccount(string externalKey, RequestOptions inputOptions, bool withBalance = false, bool withCba = false);

        Account CreateAccount(Account account, RequestOptions inputOptions);

        Account UpdateAccount(Account account, RequestOptions inputOptions);

        Account UpdateAccount(Account account, bool treatNullAsReset, RequestOptions inputOptions);

        void BlockAccount(Guid accountId, BlockingState blockingState, RequestOptions inputOptions, DateTime? requestedDate = null, Dictionary<string, string> pluginProperties = null);

        // ACCOUNTS
        Accounts GetAccounts(RequestOptions requestOptions);

        Accounts GetAccounts(long offset, long limit, RequestOptions inputOptions, AuditLevel auditLevel = AuditLevel.NONE);

        // ACCOUNT EMAILS
        AccountEmails GetEmailsForAccount(Guid accountId, RequestOptions inputOptions);

        void AddEmailToAccount(AccountEmail email, RequestOptions inputOptions);

        void RemoveEmailFromAccount(AccountEmail email, RequestOptions inputOptions);

        // ACCOUNT TIMELINE
        AccountTimeline GetAccountTimeline(Guid accountId, RequestOptions inputOptions, AuditLevel auditLevel = AuditLevel.NONE);
    }
}