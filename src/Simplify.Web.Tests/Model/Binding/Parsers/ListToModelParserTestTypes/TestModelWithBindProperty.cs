
using Simplify.Web.Model.Binding.Attributes;

namespace Simplify.Web.Tests.Model.Binding.Parsers.ListToModelParserTestTypes;

public class TestModelWithBindProperty
{
	[BindProperty("Prop2")]
	public string? Prop1 { get; set; }
}