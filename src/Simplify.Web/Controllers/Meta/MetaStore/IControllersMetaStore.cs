using System.Collections.Generic;

namespace Simplify.Web.Controllers.Meta.MetaStore;

/// <summary>
/// Represents a controllers meta store.
/// </summary>
public interface IControllersMetaStore
{
	IReadOnlyCollection<IControllerMetadata> AllControllers { get; }

	/// <summary>
	/// Gets the standard controllers (all controllers excluding controllers for specific HTTP codes).
	/// </summary>
	IReadOnlyCollection<IControllerMetadata> StandardControllers { get; }

	/// <summary>
	/// Gets the controllers linked to specific routes.
	/// </summary>
	IReadOnlyCollection<IControllerMetadata> RoutedControllers { get; }

	/// <summary>
	/// Gets the global (any-route) controllers.
	/// </summary>
	IReadOnlyCollection<IControllerMetadata> GlobalControllers { get; }

	/// <summary>
	/// Gets the controller for handling HTTP 403 status.
	/// </summary>
	IControllerMetadata? ForbiddenController { get; }

	/// <summary>
	/// Gets the controller for handling HTTP 404 status.
	/// </summary>
	IControllerMetadata? NotFoundController { get; }
}