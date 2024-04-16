using System;
using System.Collections.Generic;
using System.Linq;
using Simplify.DI;
using Simplify.Web.Meta;

namespace Simplify.Web.Bootstrapper;

/// <summary>
/// Provides the base and default Simplify.Web bootstrapper.
/// </summary>
public class BaseBootstrapper
{
	/// <summary>
	/// Provides the `Simplify.Web` types to exclude from registrations.
	/// </summary>
	protected IEnumerable<Type> TypesToExclude { get; private set; } = [];

	/// <summary>
	/// Registers the types in container.
	/// </summary>
	public void Register(IEnumerable<Type>? typesToExclude = null)
	{
		if (typesToExclude != null)
			TypesToExclude = typesToExclude;

		var typesToIgnore = SimplifyWebTypesFinder.GetTypesToIgnore();

		RegisterControllers(typesToIgnore);
		RegisterViews(typesToIgnore);
	}

	/// <summary>
	/// Registers the controllers.
	/// </summary>
	/// <param name="typesToIgnore">The types to ignore.</param>
	public virtual void RegisterControllers(IEnumerable<Type> typesToIgnore)
	{
		foreach (var controllerMetaData in ControllersMetaStore.Current.ControllersMetaData
					 .Where(controllerMetaData => typesToIgnore.All(x => x != controllerMetaData.ControllerType)))
			BootstrapperFactory.ContainerProvider.Register(controllerMetaData.ControllerType, LifetimeType.Transient);
	}

	/// <summary>
	/// Registers the views.
	/// </summary>
	/// <param name="typesToIgnore">The types to ignore.</param>
	public virtual void RegisterViews(IEnumerable<Type> typesToIgnore)
	{
		foreach (var viewType in ViewsMetaStore.Current.ViewsTypes
					 .Where(viewType => typesToIgnore.All(x => x != viewType)))
			BootstrapperFactory.ContainerProvider.Register(viewType, LifetimeType.Transient);
	}
}