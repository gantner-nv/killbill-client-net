using System;
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

        // BUNDLES
        Bundles GetAccountBundles(Guid accountId, RequestOptions inputOptions);

        Bundles GetBundles(RequestOptions inputOptions, long offset = 0L, long limit = 100L, AuditLevel auditLevel = AuditLevel.NONE);

        Bundles SearchBundles(string key, RequestOptions inputOptions, long offset = 0L, long limit = 100L, AuditLevel auditLevel = AuditLevel.NONE);
    }
}