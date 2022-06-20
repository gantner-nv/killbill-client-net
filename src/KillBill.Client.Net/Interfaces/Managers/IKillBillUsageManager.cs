using System;
using System.Threading.Tasks;
using KillBill.Client.Net.Data;
using KillBill.Client.Net.Model;

namespace KillBill.Client.Net.Interfaces.Managers
{
    public interface IKillBillUsageManager
    {
        Task<UsageRecordRequest> RecordUsage(UsageRecordRequest usage, RequestOptions inputOptions);

        Task<UsageRecord> GetRecordedUsage(Guid subscritionId, RequestOptions inputOptions);
    }
}