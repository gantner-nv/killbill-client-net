using System;
using System.Collections.Generic;

namespace KillBill.Client.Net.Model
{
    public class UsageRecord
    {
        public Guid SubscriptionId { get; set; }

        public DateTime StartDate { get; set; }
        
        public DateTime EndDate { get; set; }

        public List<RolledUpUnit> RolledUpUnits { get; set; }
    }
}