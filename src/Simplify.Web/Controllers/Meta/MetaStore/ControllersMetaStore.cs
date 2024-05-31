using System;
using System.Collections.Generic;
using System.Linq;
using Simplify.Web.Controllers.Meta.Loader;

namespace Simplify.Web.Controllers.Meta.MetaStore;

/// <summary>
/// Provides the controllers meta information container.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="ControllersMetaStore"/> class.
/// </remarks>
public class ControllersMetaStore : IControllersMetaStore
{
	private static IControllersMetaStore? _current;

	/// <summary>
	/// Initializes a new instance of the <see cref="ControllersMetaStore"/> class.
	/// </summary>
	/// <param name="loader">The loader.</param>
	public ControllersMetaStore(IMetadataLoader loader)
	{
		var items = loader.Load();

		AllControllers = items;
		StandardControllers = items.GetStandardControllers().ToList();
		RoutedControllers = items.GetRoutedControllers().ToList();
		GlobalControllers = items.GetGlobalControllers().ToList();
		ForbiddenController = items.GetHandlerController(HandlerControllerType.ForbiddenHandler);
		NotFoundController = items.GetHandlerController(HandlerControllerType.NotFoundHandler);
	}

	/// <summary>
	/// Gets the current controllers meta store
	/// </summary>
	public static IControllersMetaStore Current
	{
		get => _current ??= new ControllersMetaStore(MetadataLoader.Current);
		set => _current = value ?? throw new ArgumentNullException(nameof(value));
	}

	/// <summary>
	/// Gets all controllers.
	/// </summary>
	/// <value>
	/// All controllers.
	/// </value>
	public IReadOnlyList<IControllerMetadata> AllControllers { get; }

	/// <summary>
	/// Gets the standard controllers (all controllers excluding controllers for specific HTTP codes).
	/// </summary>
	/// <value>
	/// The standard controllers.
	/// </value>
	public IReadOnlyList<IControllerMetadata> StandardControllers { get; }

	/// <summary>
	/// Gets the controllers linked to specific routes.
	/// </summary>
	/// <value>
	/// The routed controllers.
	/// </value>
	public IReadOnlyList<IControllerMetadata> RoutedControllers { get; }

	/// <summary>
	/// Gets the global (any-route) controllers.
	/// </summary>
	/// <value>
	/// The global controllers.
	/// </value>
	public IReadOnlyList<IControllerMetadata> GlobalControllers { get; }

	/// <summary>
	/// Gets the controller for handling HTTP 403 status.
	/// </summary>
	/// <value>
	/// The forbidden controller.
	/// </value>
	public IControllerMetadata? ForbiddenController { get; }

	/// <summary>
	/// Gets the controller for handling HTTP 404 status.
	/// </summary>
	/// <value>
	/// The not found controller.
	/// </value>
	public IControllerMetadata? NotFoundController { get; }
}