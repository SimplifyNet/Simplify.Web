using Simplify.Web.Model.Binding.Attributes;

namespace Simplify.Web.Tests.Model.Binding.Parsers.ListToModelParserTestTypes;

public class TestModelWithExcludedProperty
{
	[Exclude]
	public string? Prop1 { get; set; }
}