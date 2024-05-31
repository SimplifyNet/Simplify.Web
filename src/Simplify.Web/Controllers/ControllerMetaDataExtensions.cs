using Simplify.Web.Controllers.Meta;

namespace Simplify.Web.Controllers;

/// <summary>
/// Provides the controller metadata extensions.
/// </summary>
public static class ControllerMetaDataExtensions
{
	/// <summary>
	/// Converts to controller metadata to matched controller.
	/// </summary>
	/// <param name="metaData">The meta data.</param>
	public static IMatchedController ToMatchedController(this IControllerMetadata metaData) =>
		new MatchedController(metaData);
}