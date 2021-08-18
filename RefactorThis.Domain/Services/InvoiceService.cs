using RefactorThis.Persistence.Models;
using RefactorThis.Persistence.Repositories;

namespace RefactorThis.Domain.Services
{
    public class InvoiceService
    {
        private readonly InvoiceRepository _invoiceRepository;
        public InvoiceService(InvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        /// <summary>
        /// Retrieves an <see cref="Invoice"/> by reference
        /// </summary>
        /// <param name="reference">Reference number to retrieve the associated invoice</param>
        public Invoice GetInvoice(string reference)
        {
            var invoice = _invoiceRepository.GetInvoice(reference);
            return invoice;
        }
        /// <summary>
        /// Saves the <see cref="Invoice"/>
        /// </summary>
        /// <param name="invoice">The invoice to save</param>
        public void SaveInvoice(Invoice invoice)
        {
            _invoiceRepository.SaveInvoice(invoice);
        }
        /// <summary>
        /// Adds the <see cref="Invoice"/> to the <seealso cref="InvoiceRepository"/>
        /// </summary>
        /// <param name="invoice">The invoice to add to the repository</param>
        public void Add(Invoice invoice)
        {
            _invoiceRepository.Add(invoice);
        }
    }
}