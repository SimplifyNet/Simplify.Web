using System.Collections.Generic;
using Simplify.Web.Model.Validation.Attributes;

namespace Simplify.Web.Tests.TestEntities
{
	public class TestModelRequiredCustomGenericList
	{
		[Required]
		public IList<TestModelEMail> Prop1 { get; set; }
	}
}