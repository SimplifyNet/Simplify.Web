using System.Collections.Generic;

namespace Simplify.Web.Controllers.Meta.MetaStore;

/// <summary>
/// Represents a controllers meta store.
/// </summary>
public interface IControllersMetaStore
{
	/// <summary>
	/// Gets all controllers.
	/// </summary>
	/// <value>
	/// All controllers.
	/// </value>
	IReadOnlyList<IControllerMetadata> AllControllers { get; }

	/// <summary>
	/// Gets the standard controllers (all controllers excluding controllers for specific HTTP codes).
	/// </summary>
	/// <value>
	/// The standard controllers.
	/// </value>
	IReadOnlyList<IControllerMetadata> StandardControllers { get; }

	/// <summary>
	/// Gets the controllers linked to specific routes.
	/// </summary>
	/// <value>
	/// The routed controllers.
	/// </value>
	IReadOnlyList<IControllerMetadata> RoutedControllers { get; }

	/// <summary>
	/// Gets the global (any-route) controllers.
	/// </summary>
	/// <value>
	/// The global controllers.
	/// </value>
	IReadOnlyList<IControllerMetadata> GlobalControllers { get; }

	/// <summary>
	/// Gets the controller for handling HTTP 403 status.
	/// </summary>
	/// <value>
	/// The forbidden controller.
	/// </value>
	IControllerMetadata? ForbiddenController { get; }

	/// <summary>
	/// Gets the controller for handling HTTP 404 status.
	/// </summary>
	/// <value>
	/// The not found controller.
	/// </value>
	IControllerMetadata? NotFoundController { get; }
}