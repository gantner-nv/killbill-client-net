using System;
using System.Collections.Generic;

namespace KillBill.Client.Net.Model
{
    public class UsageRecordRequest
    {
        public Guid SubscriptionId { get; set; }
        
        public Guid TrackingId { get; set; }

        public List<UnitUsageRecord> UnitUsageRecords { get; set; }
    }
}