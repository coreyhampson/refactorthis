using RefactorThis.Persistence.Models;
using System;

namespace RefactorThis.Domain.Services
{
    public class InvoicePaymentProcessor
    {
        private readonly InvoiceService _invoiceDomainService;

        public InvoicePaymentProcessor(InvoiceService invoiceDomainService)
        {
            _invoiceDomainService = invoiceDomainService;
        }

        public string ProcessPayment(Payment payment)
        {
            var inv = _invoiceDomainService.GetInvoice(payment.Reference);

            if (inv == null)
            {
                // TODO [Logging] - Add logging here (or in an exception logging handler)
                // TODO - Implement an identifier into the invoice so we can include it in the error message / logging
                throw new InvalidOperationException("There is no invoice matching this payment");
            }

            var dueAmount = inv.OrderTotal - inv.PaidAmount;
            var responseMessage = "";

            // // TODO - Add conditions for negative values - these would likely either be an error or a refund (if applicable) so we should handle accordingly
            // if(inv.PaidAmount < 0 || inv.OrderTotal < 0 || dueAmount < 0)
            // {
            //     throw new InvalidOperationException("The invoice is in an invalid state, it has a negative amount.");
            // }

            // If no payment exists against the invoice
            if (inv.PaidAmount == 0)
            {
                if (inv.OrderTotal == 0)
                {
                    responseMessage = "No payment needed";
                }
                else
                {
                    if (payment.Amount > inv.OrderTotal)
                    {
                        responseMessage = "The payment is greater than the invoice amount";
                    }
                    else if (payment.Amount < inv.OrderTotal)
                    {
                        inv.Payments.Add(payment);
                        responseMessage = "Invoice is now partially paid";
                    }
                    else if (dueAmount == 0)
                    {
                        inv.Payments.Add(payment);
                        responseMessage = "Invoice is now fully paid";
                    }
                }
            }
            else // If a payment exists against the invoice
            {
                if (inv.OrderTotal == 0)
                {
                    // TODO [Logging] - Add logging here (or in an exception logging handler) + improve messaging
                    // TODO - Implement an identifier into the invoice so we can include it in the error message / logging
                    throw new InvalidOperationException("The invoice is in an invalid state, it has an amount of 0 and it has payments.");
                }

                if (dueAmount == 0)
                {
                    responseMessage = "Invoice was already fully paid";
                }
                else if (payment.Amount > dueAmount)
                {
                    responseMessage = "The payment is greater than the partial amount remaining";
                }
                else if (payment.Amount < dueAmount)
                {
                    inv.Payments.Add(payment);
                    responseMessage = "Another partial payment received, still not fully paid";
                }
                else if (payment.Amount == dueAmount)
                {
                    inv.Payments.Add(payment);
                    responseMessage = "Final partial payment received, invoice is now fully paid";
                }
            }
            _invoiceDomainService.SaveInvoice(inv);

            return responseMessage;
        }
    }
}