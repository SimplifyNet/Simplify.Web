using Simplify.Web.ModelBinding.Attributes;

namespace Simplify.Web.Tests.TestEntities;

public class TestModelMaxLength
{
	[MaxLength(2)]
	public string Prop1 { get; set; }
}