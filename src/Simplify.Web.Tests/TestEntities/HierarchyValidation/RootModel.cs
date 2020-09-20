#nullable disable

using Simplify.Web.Model.Validation.Attributes;

namespace Simplify.Web.Tests.TestEntities.HierarchyValidation
{
	public class RootModel : BaseModel
	{
		[Required]
		public ChildModel CustomType { get; set; }

		public int TestInt { get; set; }
	}
}