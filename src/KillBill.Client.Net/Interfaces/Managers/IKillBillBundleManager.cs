using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KillBill.Client.Net.Data;
using KillBill.Client.Net.Model;

namespace KillBill.Client.Net.Interfaces.Managers
{
    public interface IKillBillBundleManager
    {
        // BUNDLE
        Task<Bundle> GetBundle(Guid bundleId, RequestOptions inputOptions);

        Task<Bundle> GetBundle(string externalKey, RequestOptions inputOptions);

        Task<Bundle> TransferBundle(Bundle bundle, RequestOptions inputOptions);

        Task BlockBundle(Guid bundleId, BlockingState blockingState, RequestOptions inputOptions, DateTime? requestedDate = null, Dictionary<string, string> pluginProperties = null);

        Task PauseBundle(Guid bundleId, RequestOptions inputOptions, DateTime? requestedDate = null, Dictionary<string, string> pluginProperties = null);

        Task ResumeBundle(Guid bundleId, RequestOptions inputOptions, DateTime? requestedDate = null, Dictionary<string, string> pluginProperties = null);

        // BUNDLES
        Task<Bundles> GetBundles(RequestOptions inputOptions, long offset = 0L, long limit = 100L, AuditLevel auditLevel = AuditLevel.NONE);

        Task<Bundles> SearchBundles(string key, RequestOptions inputOptions, long offset = 0L, long limit = 100L, AuditLevel auditLevel = AuditLevel.NONE);
    }
}