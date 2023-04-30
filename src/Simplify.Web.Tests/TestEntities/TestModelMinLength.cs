using Simplify.Web.ModelBinding.Attributes;

namespace Simplify.Web.Tests.TestEntities;

public class TestModelMinLength
{
	[MinLength(2)]
	public string Prop1 { get; set; }
}