using Simplify.Web.Model.Validation.Attributes;

namespace Simplify.Web.Tests.TestEntities.HierarchyValidation
{
	public interface ISubChildModel
	{
		[Required]
		string BuiltInType { get; set; }
	}
}