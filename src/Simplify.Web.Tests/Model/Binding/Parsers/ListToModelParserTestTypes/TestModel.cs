using Simplify.Web.Model.Validation.Attributes;

namespace Simplify.Web.Tests.Model.Binding.Parsers.ListToModelParserTestTypes;

public class TestModel
{
	[Required]
	public string? Prop1 { get; set; }
}