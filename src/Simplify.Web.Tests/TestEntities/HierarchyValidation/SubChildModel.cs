#nullable disable

using Simplify.Web.Model.Validation.Attributes;

namespace Simplify.Web.Tests.TestEntities.HierarchyValidation
{
	public class SubChildModel
	{
		[Required]
		public string BuiltInType { get; set; }
	}
}