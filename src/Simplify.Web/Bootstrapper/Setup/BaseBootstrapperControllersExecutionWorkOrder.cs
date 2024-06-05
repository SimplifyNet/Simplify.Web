using System.Collections.Generic;
using System.Linq;
using Simplify.DI;
using Simplify.Web.Controllers.Execution.WorkOrder;
using Simplify.Web.Controllers.Execution.WorkOrder.BuildStages;
using Simplify.Web.Controllers.Execution.WorkOrder.Director;

namespace Simplify.Web.Bootstrapper.Setup;

/// <summary>
/// Provides the bootstrapper controllers execution work order registration.
/// </summary>
public partial class BaseBootstrapper
{
	/// <summary>
	/// Registers the work order build director.
	/// </summary>
	public virtual void RegisterExecutionWorkOrderBuildDirector()
	{
		if (TypesToExclude.Contains(typeof(IExecutionWorkOrderBuildDirector)))
			return;

		BootstrapperFactory.ContainerProvider.Register<IExecutionWorkOrderBuildDirector, ExecutionWorkOrderBuildDirector>();
	}

	/// <summary>
	/// Registers the execution work order build director routed controllers builder.
	/// </summary>
	public virtual void RegisterExecutionWorkOrderBuildDirectorRoutedControllersBuilder()
	{
		if (TypesToExclude.Contains(typeof(RoutedControllersBuilder)))
			return;

		BootstrapperFactory.ContainerProvider.Register<RoutedControllersBuilder>();
	}

	/// <summary>
	/// Registers the execution work order build director not found builder.
	/// </summary>
	public virtual void RegisterExecutionWorkOrderBuildDirectorNotFoundBuilder()
	{
		if (TypesToExclude.Contains(typeof(NotFoundBuilder)))
			return;

		BootstrapperFactory.ContainerProvider.Register<NotFoundBuilder>(LifetimeType.Singleton);
	}

	/// <summary>
	/// Registers the execution work order build director global controllers
	/// </summary>
	public virtual void RegisterExecutionWorkOrderBuildDirectorGlobalControllersBuilder()
	{
		if (TypesToExclude.Contains(typeof(GlobalControllersBuilder)))
			return;

		BootstrapperFactory.ContainerProvider.Register<GlobalControllersBuilder>(LifetimeType.Singleton);
	}

	/// <summary>
	/// Registers the execution work order build director stages.
	/// </summary>
	public virtual void RegisterExecutionWorkOrderBuildStages()
	{
		if (TypesToExclude.Contains(typeof(IReadOnlyList<IExecutionWorkOrderBuildStage>)))
			return;

		BootstrapperFactory.ContainerProvider.Register<IReadOnlyList<IExecutionWorkOrderBuildStage>>(r =>
			[
				r.Resolve<RoutedControllersBuilder>(),
				r.Resolve<NotFoundBuilder>(),
				r.Resolve<GlobalControllersBuilder>()
			]);
	}
}