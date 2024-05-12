using Simplify.Web.Model.Validation.Attributes;

namespace Simplify.Web.Tests.Model.Validation.TestTypes.Nesting;

public class NestedModel
{
	[Required]
	public ISubNestedModel? NestedProperty { get; set; }
}