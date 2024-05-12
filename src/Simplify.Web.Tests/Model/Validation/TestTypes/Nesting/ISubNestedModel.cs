using Simplify.Web.Model.Validation.Attributes;

namespace Simplify.Web.Tests.Model.Validation.TestTypes.Nesting;

public interface ISubNestedModel
{
	[Required]
	string? BuiltInType { get; set; }
}