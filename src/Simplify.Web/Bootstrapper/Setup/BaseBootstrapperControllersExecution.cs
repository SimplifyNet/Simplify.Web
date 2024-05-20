using System.Collections.Generic;
using System.Linq;
using Simplify.DI;
using Simplify.Web.Controllers.Execution;
using Simplify.Web.Controllers.Execution.Resolver;
using Simplify.Web.Controllers.Response;
using Simplify.Web.Controllers.V1.Execution;
using Simplify.Web.Controllers.V2.Execution;
using Simplify.Web.Modules.Redirection;

namespace Simplify.Web.Bootstrapper.Setup;

/// <summary>
/// Provides the bootstrapper controllers execution registration.
/// </summary>
public partial class BaseBootstrapper
{
	public virtual void RegisterControllerExecutorResolver()
	{
		if (TypesToExclude.Contains(typeof(IControllerExecutorResolver)))
			return;

		BootstrapperFactory.ContainerProvider.Register<IControllerExecutorResolver, ControllerExecutorResolver>();
	}

	public virtual void RegisterControllerExecutorResolverExecutors()
	{
		if (TypesToExclude.Contains(typeof(IReadOnlyList<IControllerExecutor>)))
			return;

		BootstrapperFactory.ContainerProvider.Register<IReadOnlyList<IControllerExecutor>>(r =>
			[
				new Controller2Executor(r.Resolve<IController2Factory>()),
				new Controller1Executor(r.Resolve<IController1Factory>())
			]);
	}

	public virtual void RegisterControllerResponseExecutor()
	{
		if (TypesToExclude.Contains(typeof(IControllerResponseExecutor)))
			return;

		BootstrapperFactory.ContainerProvider.Register<IControllerResponseExecutor, ControllerResponseExecutor>();
	}

	/// <summary>
	/// Registers the controllers executor.
	/// </summary>
	public virtual void RegisterControllersExecutor()
	{
		if (TypesToExclude.Contains(typeof(IControllersExecutor)))
			return;

		BootstrapperFactory.ContainerProvider.Register<ControllersExecutor>();

		BootstrapperFactory.ContainerProvider.Register<IControllersExecutor>(r =>
			new PreviousPageUrlUpdater(
					r.Resolve<ControllersExecutor>(),
				r.Resolve<IRedirector>()));
	}
}