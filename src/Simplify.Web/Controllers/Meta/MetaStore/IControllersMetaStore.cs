using System.Collections.Generic;

namespace Simplify.Web.Controllers.Meta.MetaStore;

/// <summary>
/// Represents a controllers meta store.
/// </summary>
public interface IControllersMetaStore
{
	IReadOnlyList<IControllerMetadata> AllControllers { get; }

	/// <summary>
	/// Gets the standard controllers (all controllers excluding controllers for specific HTTP codes).
	/// </summary>
	IReadOnlyList<IControllerMetadata> StandardControllers { get; }

	/// <summary>
	/// Gets the controllers linked to specific routes.
	/// </summary>
	IReadOnlyList<IControllerMetadata> RoutedControllers { get; }

	/// <summary>
	/// Gets the global (any-route) controllers.
	/// </summary>
	IReadOnlyList<IControllerMetadata> GlobalControllers { get; }

	/// <summary>
	/// Gets the controller for handling HTTP 403 status.
	/// </summary>
	IControllerMetadata? ForbiddenController { get; }

	/// <summary>
	/// Gets the controller for handling HTTP 404 status.
	/// </summary>
	IControllerMetadata? NotFoundController { get; }
}