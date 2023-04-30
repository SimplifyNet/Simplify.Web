using Simplify.Web.ModelBinding.Attributes;

namespace Simplify.Web.Tests.TestEntities;

public class TestModelRegex
{
	[Regex("^[a-zA-Z]+$")]
	public string Prop1 { get; set; }
}