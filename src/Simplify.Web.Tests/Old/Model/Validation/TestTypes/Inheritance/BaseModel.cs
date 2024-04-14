using Simplify.Web.Old.Model.Validation.Attributes;

namespace Simplify.Web.Tests.Old.Model.Validation.TestTypes.Inheritance;

public class BaseModel
{
	[Required]
	public BaseNestedModel? NestedProperty { get; set; }
}