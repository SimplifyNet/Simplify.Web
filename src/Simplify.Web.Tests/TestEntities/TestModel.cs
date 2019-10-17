using Simplify.Web.Model.Validation.Attributes;

namespace Simplify.Web.Tests.TestEntities
{
	public class TestModel
	{
		[Required]
		public string Prop1 { get; set; }
	}
}