using System.Linq;

namespace Simplify.Web.Meta.Controllers.Extensions;

public static class ControllerMetadataRoleExtensions
{
	public static bool IsSpecialController(this IControllerMetadata x) =>
		x.Role!.Is400Handler ||
		x.Role!.Is403Handler ||
		x.Role!.Is404Handler;

	public static bool Is404Controller(this IControllerMetadata x) => x.Role!.Is404Handler;
}
