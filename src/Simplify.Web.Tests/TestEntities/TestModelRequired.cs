using System.Collections.Generic;
using Simplify.Web.Model.Validation.Attributes;

namespace Simplify.Web.Tests.TestEntities
{
	public class TestModelRequired
	{
		[Required]
		public string Prop1 { get; set; }

		[Required]
		public IList<string> Prop2 { get; set; }

		[Required]
		public TestModelEMail Prop3 { get; set; }
	}
}