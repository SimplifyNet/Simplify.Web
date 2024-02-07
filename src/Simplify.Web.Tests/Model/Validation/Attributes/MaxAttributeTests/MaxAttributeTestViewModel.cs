using Simplify.Web.Model.Validation.Attributes;

namespace Simplify.Web.Tests.Model.Validation.Attributes.MaxAttributeTests;

public class MaxAttributeTestViewModel
{
	[Max(1)]
	public int IntParam { get; set; }

	[Max((long)1)]
	public long LongParam { get; set; }

	[Max(1d)]
	public double DoubleParam { get; set; }

	[Max(typeof(decimal), "12.5")]
	public decimal DecimalParam { get; set; }
}