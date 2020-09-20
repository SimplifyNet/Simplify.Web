#nullable disable

using Simplify.Web.Model.Validation.Attributes;

namespace Simplify.Web.Tests.TestEntities.HierarchyValidation
{
	public class ChildModel
	{
		[Required]
		public ISubChildModel CustomType { get; set; }
	}
}