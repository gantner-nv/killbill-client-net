using System;
using KillBill.Client.Net.Data;
using KillBill.Client.Net.Interfaces;
using KillBill.Client.Net.Interfaces.Managers;
using KillBill.Client.Net.Model;

namespace KillBill.Client.Net.Implementations.Managers
{
    public class KillBillNotificationManager : KillBillBaseManager, IKillBillNotificationManager
    {
        private readonly IKbHttpClient _client;

        public KillBillNotificationManager(IKbHttpClient client)
            : base(client.Configuration)
        {
            _client = client;
        }

        // INVOICE EMAIL
        public InvoiceEmail GetEmailNotificationsForAccount(Guid accountId, RequestOptions inputOptions)
        {
            var uri = Configuration.ACCOUNTS_PATH + "/" + accountId + "/" + Configuration.EMAIL_NOTIFICATIONS;
            return _client.Get<InvoiceEmail>(uri, inputOptions);
        }

        public void UpdateEmailNotificationsForAccount(InvoiceEmail invoiceEmail, RequestOptions inputOptions)
        {
            if (invoiceEmail.AccountId.Equals(Guid.Empty))
                throw new ArgumentException("invoiceEmail#AccountId can not be empty");

            var uri = Configuration.ACCOUNTS_PATH + "/" + invoiceEmail.AccountId + "/" + Configuration.EMAIL_NOTIFICATIONS;
            _client.Put(uri, invoiceEmail, inputOptions);
        }
    }
}