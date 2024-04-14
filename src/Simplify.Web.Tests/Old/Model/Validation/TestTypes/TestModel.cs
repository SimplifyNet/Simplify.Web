using Simplify.Web.Old.Model.Validation.Attributes;

namespace Simplify.Web.Tests.Old.Model.Validation.TestTypes;

public class TestModel
{
	[Required]
	public string? Prop1 { get; set; }
}