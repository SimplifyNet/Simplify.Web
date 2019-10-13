using Simplify.Web.Model.Validation.Attributes;

namespace Simplify.Web.Tests.TestEntities
{
	public class TestModelRequiredCustomArray
	{
		[Required]
		public TestModelEMail[] Prop1 { get; set; }
	}
}