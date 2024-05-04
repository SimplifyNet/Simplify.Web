using System;
using System.Collections.Generic;
using System.Linq;
using Simplify.DI;
using Simplify.Web.Controllers.Execution;

namespace Simplify.Web.Bootstrapper.Setup;

/// <summary>
/// Provides the bootstrapper controllers execution registration.
/// </summary>
public partial class BaseBootstrapper
{
	/// <summary>
	/// Registers the controllers executor.
	/// </summary>
	public virtual void RegisterControllersExecutor()
	{
		if (TypesToExclude.Contains(typeof(IControllersExecutor)))
			return;

		BootstrapperFactory.ContainerProvider.Register<IControllersExecutor, ControllersExecutor>(LifetimeType.Singleton);
	}

	public virtual void RegisterControllerExecutorResolver()
	{
		if (TypesToExclude.Contains(typeof(IControllerExecutorResolver)))
			return;

		BootstrapperFactory.ContainerProvider.Register<IControllerExecutorResolver, ControllerExecutorResolver>(LifetimeType.Singleton);
	}

	public virtual void RegisterControllerExecutorResolverExecutors()
	{
		if (TypesToExclude.Contains(typeof(IReadOnlyList<IControllerExecutor>)))
			return;

		BootstrapperFactory.ContainerProvider.Register<IReadOnlyList<IControllerExecutor>>(r =>
			new List<IControllerExecutor>
			{
			}, LifetimeType.Singleton);
	}
}