using Simplify.Web.Model.Validation.Attributes;

namespace Simplify.Web.Tests.TestEntities.Nesting;

public class NestingRootModel
{
	public NestedModel? NestedProperty { get; set; }

	[Required]
	public int? TestInt { get; set; }
}