using System;
using System.Collections.Generic;
using System.Linq;

namespace Simplify.Web.Controllers.Meta;

/// <summary>
/// Provides the controller metadata route extensions.
/// </summary>
public static class ControllerMetadataRouteExtensions
{
	/// <summary>
	/// Gets the standard controllers.
	/// </summary>
	/// <param name="list">The list.</param>
	public static IEnumerable<IControllerMetadata> GetStandardControllers(this IEnumerable<IControllerMetadata> list) => list
		.Where(x => !x.IsSpecialController());

	/// <summary>
	/// Gets the routed controllers.
	/// </summary>
	/// <param name="list">The list.</param>
	public static IEnumerable<IControllerMetadata> GetRoutedControllers(this IEnumerable<IControllerMetadata> list) => list
		.GetStandardControllers()
		.Where(x => x.ContainsRoute());

	/// <summary>
	/// Gets the global controllers.
	/// </summary>
	/// <param name="list">The list.</param>
	public static IEnumerable<IControllerMetadata> GetGlobalControllers(this IEnumerable<IControllerMetadata> list) => list
		.GetStandardControllers()
		.Where(x => !x.ContainsRoute());

	/// <summary>
	/// Gets the handler controller.
	/// </summary>
	/// <param name="list">The list.</param>
	/// <param name="controllerType">Type of the controller.</param>
	public static IControllerMetadata? GetHandlerController(this IEnumerable<IControllerMetadata> list, HandlerControllerType controllerType) =>
		controllerType switch
		{
			HandlerControllerType.ForbiddenHandler => list.FirstOrDefault(x => x.Role is { IsForbiddenHandler: true }),
			HandlerControllerType.NotFoundHandler => list.FirstOrDefault(x => x.Role is { IsNotFoundHandler: true }),
			_ => throw new InvalidOperationException("Invalid controller type: " + controllerType)
		};

	/// <summary>
	/// Determines whether the controller is special controller
	/// </summary>
	/// <param name="controller">The controller.</param>
	public static bool IsSpecialController(this IControllerMetadata controller) =>
		controller.Role
			is { IsForbiddenHandler: true }
			or { IsNotFoundHandler: true };

	/// <summary>
	/// Determines whether the controller contains route.
	/// </summary>
	/// <param name="controller">The controller.</param>
	public static bool ContainsRoute(this IControllerMetadata controller) =>
		controller.ExecParameters != null &&
		controller.ExecParameters.Routes.Any(route => route.Value != null);
}