using Simplify.Web.ModelBinding.Attributes;

namespace Simplify.Web.Tests.TestEntities;

public class TestModelWithBindProperty
{
	[BindProperty("Prop2")]
	public string Prop1 { get; set; }
}