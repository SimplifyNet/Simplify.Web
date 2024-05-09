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

	public IReadOnlyCollection<IControllerMetadata> AllControllers { get; }

	public IReadOnlyCollection<IControllerMetadata> StandardControllers { get; }
	public IReadOnlyCollection<IControllerMetadata> RoutedControllers { get; }
	public IReadOnlyCollection<IControllerMetadata> GlobalControllers { get; }

	public IControllerMetadata? ForbiddenController { get; }
	public IControllerMetadata? NotFoundController { get; }
}