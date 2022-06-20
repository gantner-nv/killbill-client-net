using System;
using System.Collections.Generic;
using KillBill.Client.Net.JSON;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace KillBill.Client.Net.Model
{
    public class UnitUsageRecord
    {
        public string UnitType { get; set; }

        public List<UsageItem> UsageRecords { get; set; }
    }

    public class UsageItem
    {
        [JsonConverter(typeof(ShortDateTimeConverter))]
        public DateTime RecordDate { get; set; }

        public int Amount { get; set; }
    }
}