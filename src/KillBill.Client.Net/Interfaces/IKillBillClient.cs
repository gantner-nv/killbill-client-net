using System;
using System.Collections.Generic;
using KillBill.Client.Net.Data;
using KillBill.Client.Net.Interfaces.Managers;
using KillBill.Client.Net.Model;

namespace KillBill.Client.Net.Interfaces
{
    public interface IKillBillClient : IKillBillTenantManager,
                                       IKillBillAccountManager,
                                       IKillBillCatalogManager,
                                       IKillBillSubscriptionManager,
                                       IKillBillBundleManager,
                                       IKillBillPlanManager,
                                       IKillBillInvoiceManager,
                                       IKillBillPaymentManager,
                                       IKillBillNotificationManager
    {
        // REQUEST OPTIONS
        RequestOptions BaseOptions(string createdBy = null, string requestId = null, string reason = null, string comment = null);
    }
}