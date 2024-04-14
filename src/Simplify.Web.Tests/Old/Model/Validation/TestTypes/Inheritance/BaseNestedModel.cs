using Simplify.Web.Old.Model.Validation.Attributes;

namespace Simplify.Web.Tests.Old.Model.Validation.TestTypes.Inheritance;

public class BaseNestedModel
{
	[Required]
	public string? BuiltInType { get; set; }
}