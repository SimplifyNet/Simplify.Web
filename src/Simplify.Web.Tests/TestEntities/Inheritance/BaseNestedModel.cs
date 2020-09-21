using Simplify.Web.Model.Validation.Attributes;

#nullable disable

namespace Simplify.Web.Tests.TestEntities.Inheritance
{
	public class BaseNestedModel
	{
		[Required]
		public string BuiltInType { get; set; }
	}
}