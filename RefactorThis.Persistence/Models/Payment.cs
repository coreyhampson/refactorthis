namespace RefactorThis.Persistence.Models
{
	/// <summary> Payment object </summary>
	/// <remarks> <i>Persistence Layer</i> </remarks>
	public class Payment
	{
		/// <summary> Payment Amount </summary>
		public decimal Amount { get; set; }
		/// <summary> Reference Number <i>(used as an identifier)</i> </summary>
		public string Reference { get; set; }
	}
}