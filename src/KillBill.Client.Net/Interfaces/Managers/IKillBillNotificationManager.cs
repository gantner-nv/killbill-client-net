using System;
using KillBill.Client.Net.Data;
using KillBill.Client.Net.Model;

namespace KillBill.Client.Net.Interfaces.Managers
{
    public interface IKillBillNotificationManager
    {
        // INVOICE EMAIL
        InvoiceEmail GetEmailNotificationsForAccount(Guid accountId, RequestOptions inputOptions);

        void UpdateEmailNotificationsForAccount(InvoiceEmail invoiceEmail, RequestOptions inputOptions);
    }
}