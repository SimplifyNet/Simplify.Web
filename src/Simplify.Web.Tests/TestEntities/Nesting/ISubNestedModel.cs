using Simplify.Web.Model.Validation.Attributes;

namespace Simplify.Web.Tests.TestEntities.Nesting
{
	public interface ISubNestedModel
	{
		[Required]
		string BuiltInType { get; set; }
	}
}