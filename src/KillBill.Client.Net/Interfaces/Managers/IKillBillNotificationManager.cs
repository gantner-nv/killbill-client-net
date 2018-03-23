using System;
using System.Threading.Tasks;
using KillBill.Client.Net.Data;
using KillBill.Client.Net.Model;

namespace KillBill.Client.Net.Interfaces.Managers
{
    public interface IKillBillNotificationManager
    {
        // INVOICE EMAIL
        Task<InvoiceEmail> GetEmailNotificationsForAccount(Guid accountId, RequestOptions inputOptions);

        Task UpdateEmailNotificationsForAccount(InvoiceEmail invoiceEmail, RequestOptions inputOptions);
    }
}