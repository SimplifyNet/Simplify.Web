#nullable disable

using Simplify.Web.Model.Validation.Attributes;

namespace Simplify.Web.Tests.TestEntities.HierarchyValidation
{
	public class ChildModel
	{
		//[Required]
		//public string BuiltInType { get; set; }

		[Required]
		public SubChildModel CustomType { get; set; }
	}
}