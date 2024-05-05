using System;
using System.Collections.Generic;
using System.Linq;
using Simplify.Web.Meta.Controllers.Loader;

namespace Simplify.Web.Meta.Controllers;

/// <summary>
/// Provides the controllers meta information container.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="ControllersMetaStore"/> class.
/// </remarks>
/// <param name="resolver">The factory resolver</param>
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
		Controller403 = items.GetHandlerController(HandlerControllerType.Http403Handler);
		Controller404 = items.GetHandlerController(HandlerControllerType.Http404Handler);
	}

	/// <summary>
	/// Gets the current controllers meta store
	/// </summary>
	public static IControllersMetaStore Current
	{
		get => _current ??= new ControllersMetaStore(MetadataLoader.Current);
		set => _current = value ?? throw new ArgumentNullException(nameof(value));
	}

	public IReadOnlyCollection<IControllerMetadata> AllControllers { get; } = new List<IControllerMetadata>();

	public IReadOnlyCollection<IControllerMetadata> StandardControllers { get; } = new List<IControllerMetadata>();
	public IReadOnlyCollection<IControllerMetadata> RoutedControllers { get; } = new List<IControllerMetadata>();
	public IReadOnlyCollection<IControllerMetadata> GlobalControllers { get; } = new List<IControllerMetadata>();

	public IControllerMetadata? Controller403 { get; }
	public IControllerMetadata? Controller404 { get; }
}