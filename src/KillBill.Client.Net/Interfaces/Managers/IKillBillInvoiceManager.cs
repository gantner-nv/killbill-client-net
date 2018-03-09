using System;
using System.Collections.Generic;
using KillBill.Client.Net.Data;
using KillBill.Client.Net.Model;

namespace KillBill.Client.Net.Interfaces.Managers
{
    public interface IKillBillInvoiceManager
    {
        // INVOICE
        /// <summary>
        /// Triggers an invoice RUN!
        /// </summary>
        /// <remarks>Don't be fooled by the method name... this SHOULD NOT be used to create invoices. Invoices are created as a byproduct of other actions like 'Creating Credits', 'External Charges'</remarks>
        Invoice CreateInvoice(Guid accountId, DateTime futureDate, RequestOptions inputOptions);

        // TODO implement CreateDryRunInvoice
        // Invoice CreateDryRunInvoice(Guid accountId, DateTime? futureDate, InvoiceDryRun dryRunInfo, RequestOptions requestOptions);
        Invoice GetInvoice(int invoiceNumber, RequestOptions inputOptions, bool withItems = false, bool withChildrenItems = false, AuditLevel auditLevel = AuditLevel.NONE);

        Invoice GetInvoice(string invoiceIdOrNumber, RequestOptions inputOptions, bool withItems = false, bool withChildrenItems = false, AuditLevel auditLevel = AuditLevel.NONE);

        // INVOICES
        Invoices GetInvoices(RequestOptions inputOptions);

        Invoices GetInvoices(bool withItems, long offset, long limit, RequestOptions inputOptions, AuditLevel auditLevel = AuditLevel.NONE);

        Invoices GetInvoicesForAccount(Guid accountId, RequestOptions inputOptions, bool withItems = false, bool unpaidOnly = false, bool includeMigrationInvoices = false, AuditLevel auditLevel = AuditLevel.NONE);

        Invoices SearchInvoices(string key, RequestOptions inputOptions);

        Invoices SearchInvoices(string key, long offset, long limit, RequestOptions inputOptions);

        // INVOICE ITEM
        /// <summary>
        /// Executes an 'external charge' action... note if no InvoiceId is provided on each charge then the server will create a new invoice for the batch.
        /// </summary>
        /// <remarks>The currency on each charges needs to be the same as the currency on the referenced account.</remarks>
        /// <returns>List of processed charges with invoice references.</returns>
        List<InvoiceItem> CreateExternalCharges(IEnumerable<InvoiceItem> externalCharges, DateTime? requestedDate, bool autoPay, bool autoCommit, RequestOptions inputOptions);

        List<InvoiceItem> CreateExternalCharges(IEnumerable<InvoiceItem> externalCharges, DateTime? requestedDate, bool autoPay, bool autoCommit, string paymentExternalKey, string transactionExternalKey, RequestOptions inputOptions);

        // CREDIT
        /// <summary>
        /// Creates a credit against an account. This also creates a new invoice.
        /// </summary>
        Credit CreateCredit(Credit credit, bool autoCommit, RequestOptions inputOptions);

        Credit GetCredit(Guid creditId, RequestOptions inputOptions, AuditLevel auditLevel = AuditLevel.NONE);
    }
}