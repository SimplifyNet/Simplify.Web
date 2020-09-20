#nullable disable

using Simplify.Web.Model.Validation.Attributes;

namespace Simplify.Web.Tests.TestEntities.HierarchyValidation
{
	public class RootModel
	{
		//[Required]
		//public string BuiltInType { get; set; }

		[Required]
		public ChildModel CustomType { get; set; }
	}
}