using Simplify.Web.Model.Validation.Attributes;

namespace Simplify.Web.Tests.Old.Model.Binding.Parsers.ListToModelParserTestTypes;

public class TestModel
{
	[Required]
	public string? Prop1 { get; set; }
}