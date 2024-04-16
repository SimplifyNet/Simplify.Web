using Simplify.Web.Model.Validation.Attributes;

namespace Simplify.Web.Tests.Model.Validation.Attributes.RangeAttributeTests;

public class RangeAttributeTestViewModel
{
	[Range(1, 5)]
	public int IntParam { get; set; }

	[Range((long)1, (long)5)]
	public long LongParam { get; set; }

	[Range(1d, 5d)]
	public double DoubleParam { get; set; }

	[Range(typeof(decimal), "12.5", "15.5")]
	public decimal DecimalParam { get; set; }
}