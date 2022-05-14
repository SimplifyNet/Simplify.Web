using Simplify.Web.Model.Validation.Attributes;

namespace Simplify.Web.Tests.TestEntities.Inheritance;

public class BaseNestedModel
{
	[Required]
	public string? BuiltInType { get; set; }
}