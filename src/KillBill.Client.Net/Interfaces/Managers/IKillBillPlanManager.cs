using System.Collections.Generic;
using KillBill.Client.Net.Data;
using KillBill.Client.Net.Model;

namespace KillBill.Client.Net.Interfaces.Managers
{
    public interface IKillBillPlanManager
    {
        // PLAN DETAIL
        List<PlanDetail> GetBasePlans(RequestOptions inputOptions);

        List<PlanDetail> GetAvailableAddons(string baseProductName, RequestOptions inputOptions);
    }
}