using Simplify.Web.Old.Model.Validation.Attributes;

namespace Simplify.Web.Tests.Old.Model.Validation.TestTypes.Nesting;

public class NestedModel
{
	[Required]
	public ISubNestedModel? NestedProperty { get; set; }
}