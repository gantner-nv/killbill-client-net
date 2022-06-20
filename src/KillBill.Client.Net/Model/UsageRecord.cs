using System;
using System.Collections.Generic;
using KillBill.Client.Net.JSON;
using Newtonsoft.Json;

namespace KillBill.Client.Net.Model
{
    public class UsageRecord
    {
        public Guid SubscriptionId { get; set; }

        [JsonConverter(typeof(ShortDateTimeConverter))]
        public DateTime StartDate { get; set; }
        
        [JsonConverter(typeof(ShortDateTimeConverter))]
        public DateTime EndDate { get; set; }

        public List<RolledUpUnit> RolledUpUnits { get; set; }
    }
}