using Simplify.Web.Model.Validation.Attributes;

namespace Simplify.Web.Tests.TestEntities
{
	public class TestModelWithExcludedProperty
	{
		[Exclude]
		public string Prop1 { get; set; }
	}
}