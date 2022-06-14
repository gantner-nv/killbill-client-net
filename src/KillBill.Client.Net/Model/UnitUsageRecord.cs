using System;
using System.Collections.Generic;

namespace KillBill.Client.Net.Model
{
    public class UnitUsageRecord
    {
        public string UnitType { get; set; }

        public List<UsageItem> UsageRecords { get; set; }
    }

    public class UsageItem
    {
        public DateTime RecordDate { get; set; }

        public int Amount { get; set; }
    }
}