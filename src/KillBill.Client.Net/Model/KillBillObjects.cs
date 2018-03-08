using System;
using System.Collections;
using System.Collections.Generic;
using KillBill.Client.Net.Data;
using KillBill.Client.Net.Infrastructure;
using KillBill.Client.Net.Interfaces;
using Newtonsoft.Json;

namespace KillBill.Client.Net.Model
{
    public class KillBillObjects<T> : List<T>, IKillBillObjects
    {
        public int PaginationCurrentOffset { get; set; }

        public int PaginationNextOffset { get; set; }

        public int PaginationTotalNbRecords { get; set; }

        public int PaginationMaxNbRecords { get; set; }

        public string PaginationNextPageUri { get; set; }

        public IKbHttpClient KillBillHttpClient { get; set; }

        // TODO: revisit this once the java client is updated to use requestOptions
        public KillBillObjects<T> GetNext(RequestOptions requestOptions)
        {
            if (KillBillHttpClient == null || PaginationNextPageUri == null)
                return null;

            return KillBillHttpClient.Get<KillBillObjects<T>>(PaginationNextPageUri, requestOptions);
        }
    }
}