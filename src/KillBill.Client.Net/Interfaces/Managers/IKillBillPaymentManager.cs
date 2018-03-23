using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KillBill.Client.Net.Data;
using KillBill.Client.Net.Model;

namespace KillBill.Client.Net.Interfaces.Managers
{
    public interface IKillBillPaymentManager
    {
        // PAYMENT
        Task<Payment> CreatePayment(Guid accountId, PaymentTransaction paymentTransaction, RequestOptions inputOptions);

        Task<Payment> CreatePayment(Guid accountId, PaymentTransaction paymentTransaction, Dictionary<string, string> pluginProperties, RequestOptions inputOptions);

        Task<Payment> CreatePayment(Guid accountId, Guid? paymentMethodId, PaymentTransaction paymentTransaction, RequestOptions inputOptions);

        Task<Payment> CreatePayment(Guid accountId, Guid? paymentMethodId, PaymentTransaction paymentTransaction, Dictionary<string, string> pluginProperties, RequestOptions inputOptions);

        Task<Payment> CreatePayment(Guid accountId, Guid? paymentMethodId, PaymentTransaction paymentTransaction, List<string> controlPluginNames, Dictionary<string, string> pluginProperties, RequestOptions inputOptions);

        // PAYMENTS
        Task<Payments> GetPaymentsForAccount(Guid accountId, RequestOptions inputOptions, AuditLevel auditLevel = AuditLevel.NONE);

        Task<InvoicePayments> GetInvoicePaymentsForAccount(Guid accountId, RequestOptions inputOptions, AuditLevel auditLevel = AuditLevel.NONE);

        // PAYMENT METHOD
        Task<PaymentMethod> GetPaymentMethod(Guid paymentMethodId, RequestOptions inputOptions, bool withPluginInfo = false, AuditLevel auditLevel = AuditLevel.NONE);

        Task<PaymentMethods> GetPaymentMethodsForAccount(Guid accountId, RequestOptions inputOptions, Dictionary<string, string> pluginProperties = null, bool withPluginInfo = false, AuditLevel auditLevel = AuditLevel.NONE);

        Task<PaymentMethod> CreatePaymentMethod(PaymentMethod paymentMethod, RequestOptions inputOptions);

        Task DeletePaymentMethod(Guid paymentMethodId, RequestOptions inputOptions, bool deleteDefault = false, bool forceDeleteDefault = false);

        Task UpdateDefaultPaymentMethod(Guid accountId, Guid paymentMethodId, RequestOptions inputOptions);
    }
}