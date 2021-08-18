using RefactorThis.Persistence.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace RefactorThis.Persistence.Models
{
	/// <summary> Invoice object </summary>
	/// <remarks> <i>Persistence Layer</i> </remarks>
    public class Invoice
	{
		/// <summary> Total amount against this invoice </summary>
		public decimal OrderTotal { get; set; }
		/// <summary> List collection of <see cref="Payment"/> objects against this invoice </summary>
		public List<Payment> Payments { get; set; }
		/// <summary> Paid amount based on sum of all payments against this invoice </summary>
		public decimal PaidAmount => (Payments != null && Payments.Any()) ? Payments.Sum(x => x.Amount) : 0;

		// // TODO - Add Invoice Number property as an identifier
		// /// <summary> Invoice reference number </summary>
		// public string InvoiceNumber { get; set; }
	}
}