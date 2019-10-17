using Simplify.Web.Model.Binding.Attributes;

namespace Simplify.Web.Tests.TestEntities
{
	public class TestModelWithExcludedProperty
	{
		[Exclude]
		public string Prop1 { get; set; }
	}
}