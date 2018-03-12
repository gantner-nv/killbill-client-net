using System;
using System.Collections.Generic;
using KillBill.Client.Net.Data;
using KillBill.Client.Net.Model;

namespace KillBill.Client.Net.Interfaces.Managers
{
    public interface IKillBillBundleManager
    {
        // BUNDLE
        Bundle GetBundle(Guid bundleId, RequestOptions inputOptions);

        Bundle GetBundle(string externalKey, RequestOptions inputOptions);

        Bundle TransferBundle(Bundle bundle, RequestOptions inputOptions);

        void BlockBundle(Guid bundleId, BlockingState blockingState, RequestOptions inputOptions, DateTime? requestedDate = null, Dictionary<string, string> pluginProperties = null);

        void PauseBundle(Guid bundleId, RequestOptions inputOptions, DateTime? requestedDate = null, Dictionary<string, string> pluginProperties = null);

        void ResumeBundle(Guid bundleId, RequestOptions inputOptions, DateTime? requestedDate = null, Dictionary<string, string> pluginProperties = null);

        // BUNDLES
        Bundles GetAccountBundles(Guid accountId, RequestOptions inputOptions);

        Bundles GetAccountBundles(Guid accountId, string externalKey, RequestOptions inputOptions);

        Bundles GetBundles(RequestOptions inputOptions, long offset = 0L, long limit = 100L, AuditLevel auditLevel = AuditLevel.NONE);

        Bundles SearchBundles(string key, RequestOptions inputOptions, long offset = 0L, long limit = 100L, AuditLevel auditLevel = AuditLevel.NONE);
    }
}