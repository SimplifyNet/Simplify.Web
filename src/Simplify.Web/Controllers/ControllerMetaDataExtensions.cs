using Simplify.Web.Controllers.Meta;

namespace Simplify.Web.Controllers;

public static class ControllerMetaDataExtensions
{
	public static IMatchedController ToMatchedController(this IControllerMetadata metaData) =>
		new MatchedController(metaData);
}