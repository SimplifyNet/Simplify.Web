using System;
using System.Collections.Generic;
using System.Linq;
using Simplify.DI;
using Simplify.Web.Controllers.Execution.WorkOrder;
using Simplify.Web.Controllers.Execution.WorkOrder.BuildStages;
using Simplify.Web.Controllers.Execution.WorkOrder.Director;
using Simplify.Web.Controllers.Resolution;
using Simplify.Web.Controllers.Resolution.Handling;

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

		BootstrapperFactory.ContainerProvider.Register<IExecutionWorkOrderBuildDirector, ExecutionWorkOrderBuildDirector>(LifetimeType.Singleton);
	}

	public virtual void RegisterExecutionWorkOrderBuildDirectorStages()
	{
		if (TypesToExclude.Contains(typeof(IReadOnlyList<IExecutionWorkOrderBuildStage>)))
			return;

		BootstrapperFactory.ContainerProvider.Register<IReadOnlyList<IExecutionWorkOrderBuildStage>>(r =>
			new List<IExecutionWorkOrderBuildStage>
			{
				new RoutedControllersBuilder(
					r.Resolve<IControllerResolutionPipeline>(),
					r.Resolve<ICrsHandlingPipeline>()),
				new NotFoundBuilder(),
				new GlobalControllersBuilder()
			}, LifetimeType.Singleton);
	}
}