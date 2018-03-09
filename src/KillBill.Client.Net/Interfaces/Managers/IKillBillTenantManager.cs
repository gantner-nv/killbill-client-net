using System;
using KillBill.Client.Net.Data;
using KillBill.Client.Net.Model;

namespace KillBill.Client.Net.Interfaces.Managers
{
    public interface IKillBillTenantManager
    {
        // TENANT
        Tenant CreateTenant(Tenant tenant, RequestOptions inputOptions, bool useGlobalDefault = true);

        void UnregisterCallbackNotificationForTenant(Guid tenantId, RequestOptions inputOptions);

        // TENANT KEY
        TenantKey RegisterCallBackNotificationForTenant(string callback, RequestOptions inputOptions);

        TenantKey GetCallbackNotificationForTenant(RequestOptions inputOptions);
    }
}