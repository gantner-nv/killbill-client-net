using KillBill.Client.Net.Interfaces;

namespace KillBill.Client.Net.Model
{
    public interface IKillBillObjects
    {
        int PaginationCurrentOffset { get; set; }

        int PaginationNextOffset { get; set; }

        int PaginationTotalNbRecords { get; set; }

        int PaginationMaxNbRecords { get; set; }

        string PaginationNextPageUri { get; set; }

        IKbHttpClient KillBillHttpClient { get; set; }
    }
}