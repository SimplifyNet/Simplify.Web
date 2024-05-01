using Simplify.Web.Meta.Controllers;

namespace Simplify.Web.Controllers;

public static class ControllerMetaDataExtensions
{
	public static IMatchedController ToMatchedController(this IControllerMetadata metaData) =>
		new MatchedController(metaData);
}