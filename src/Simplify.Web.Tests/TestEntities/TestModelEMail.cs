using Simplify.Web.Model.Validation.Attributes;

namespace Simplify.Web.Tests.TestEntities
{
	public class TestModelEMail
	{
		[EMail]
		public string Prop1 { get; set; }
	}
}