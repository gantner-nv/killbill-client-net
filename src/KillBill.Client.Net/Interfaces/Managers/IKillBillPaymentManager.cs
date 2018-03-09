using System;
using System.Collections.Generic;
using KillBill.Client.Net.Data;
using KillBill.Client.Net.Model;

namespace KillBill.Client.Net.Interfaces.Managers
{
    public interface IKillBillPaymentManager
    {
        // PAYMENT
        Payment CreatePayment(Guid accountId, PaymentTransaction paymentTransaction, RequestOptions inputOptions);

        Payment CreatePayment(Guid accountId, PaymentTransaction paymentTransaction, Dictionary<string, string> pluginProperties, RequestOptions inputOptions);

        Payment CreatePayment(Guid accountId, Guid? paymentMethodId, PaymentTransaction paymentTransaction, RequestOptions inputOptions);

        Payment CreatePayment(Guid accountId, Guid? paymentMethodId, PaymentTransaction paymentTransaction, Dictionary<string, string> pluginProperties, RequestOptions inputOptions);

        Payment CreatePayment(Guid accountId, Guid? paymentMethodId, PaymentTransaction paymentTransaction, List<string> controlPluginNames, Dictionary<string, string> pluginProperties, RequestOptions inputOptions);

        // PAYMENTS
        Payments GetPaymentsForAccount(Guid accountId, RequestOptions inputOptions, AuditLevel auditLevel = AuditLevel.NONE);

        InvoicePayments GetInvoicePaymentsForAccount(Guid accountId, RequestOptions inputOptions, AuditLevel auditLevel = AuditLevel.NONE);

        // PAYMENT METHOD
        PaymentMethod GetPaymentMethod(Guid paymentMethodId, RequestOptions inputOptions, bool withPluginInfo = false, AuditLevel auditLevel = AuditLevel.NONE);

        PaymentMethods GetPaymentMethodsForAccount(Guid accountId, RequestOptions inputOptions, Dictionary<string, string> pluginProperties = null, bool withPluginInfo = false, AuditLevel auditLevel = AuditLevel.NONE);

        PaymentMethod CreatePaymentMethod(PaymentMethod paymentMethod, RequestOptions inputOptions);

        void DeletePaymentMethod(Guid paymentMethodId, RequestOptions inputOptions, bool deleteDefault = false, bool forceDeleteDefault = false);

        void UpdateDefaultPaymentMethod(Guid accountId, Guid paymentMethodId, RequestOptions inputOptions);
    }
}