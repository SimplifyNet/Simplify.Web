#nullable disable

using Simplify.Web.Model.Validation.Attributes;

namespace Simplify.Web.Tests.TestEntities.HierarchyValidation
{
	public class BaseModel
	{
		[Required]
		public ChildModel CustomType { get; set; }
	}
}