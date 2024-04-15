using Simplify.Web.Model.Validation.Attributes;

namespace Simplify.Web.Tests.Model.Validation.Attributes.MinAttributeTests;

public class MinAttributeTestViewModel
{
	[Min(1)]
	public int IntParam { get; set; }

	[Min((long)1)]
	public long LongParam { get; set; }

	[Min(1d)]
	public double DoubleParam { get; set; }

	[Min(typeof(decimal), "12.5")]
	public decimal DecimalParam { get; set; }
}