using Simplify.Web.Model.Validation.Attributes;

namespace Simplify.Web.Tests.Model.Validation.TestTypes.Inheritance;

public class BaseNestedModel
{
	[Required]
	public string? BuiltInType { get; set; }
}