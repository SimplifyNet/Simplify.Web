using Simplify.Web.Model.Validation.Attributes;

namespace Simplify.Web.Tests.Model.Validation.TestTypes.Inheritance;

public class BaseModel
{
	[Required]
	public BaseNestedModel? NestedProperty { get; set; }
}