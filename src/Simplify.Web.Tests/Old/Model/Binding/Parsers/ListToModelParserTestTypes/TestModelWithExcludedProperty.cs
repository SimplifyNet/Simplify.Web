using Simplify.Web.Old.Model.Binding.Attributes;

namespace Simplify.Web.Tests.Old.Model.Binding.Parsers.ListToModelParserTestTypes;

public class TestModelWithExcludedProperty
{
	[Exclude]
	public string? Prop1 { get; set; }
}