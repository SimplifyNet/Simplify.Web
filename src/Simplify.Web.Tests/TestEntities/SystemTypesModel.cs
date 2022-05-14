using Simplify.Web.Model.Validation.Attributes;

namespace Simplify.Web.Tests.TestEntities;

public class SystemTypesModel
{
	[Required]
	public string? StringProperty { get; set; }

	public int IntProperty { get; set; }

	public bool BoolProperty { get; set; }
}