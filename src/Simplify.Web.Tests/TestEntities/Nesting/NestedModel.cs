using Simplify.Web.Model.Validation.Attributes;

namespace Simplify.Web.Tests.TestEntities.Nesting;

public class NestedModel
{
	[Required]
	public ISubNestedModel? NestedProperty { get; set; }
}