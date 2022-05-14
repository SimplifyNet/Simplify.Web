using Simplify.Web.Model.Validation.Attributes;

namespace Simplify.Web.Tests.TestEntities.Inheritance;

public class BaseModel
{
	[Required]
	public BaseNestedModel? NestedProperty { get; set; }
}