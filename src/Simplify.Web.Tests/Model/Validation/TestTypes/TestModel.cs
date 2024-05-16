using Simplify.Web.Model.Validation.Attributes;

namespace Simplify.Web.Tests.Model.Validation.TestTypes;

public class TestModel
{
	[Required]
	public string? Prop1 { get; set; }
}