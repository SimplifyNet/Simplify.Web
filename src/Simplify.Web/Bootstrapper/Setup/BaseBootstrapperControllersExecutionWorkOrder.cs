using System;
using System.Collections.Generic;
using System.Linq;
using Simplify.DI;
using Simplify.Web.Controllers.ExecutionWorkOrder;
using Simplify.Web.Controllers.ExecutionWorkOrder.BuildStages;
using Simplify.Web.Controllers.Resolution;
using Simplify.Web.Controllers.ResolutionHandling;

namespace Simplify.Web.Bootstrapper.Setup;

/// <summary>
/// Provides the bootstrapper controllers execution work order registration.
/// </summary>
public partial class BaseBootstrapper
{
	/// <summary>
	/// Registers the work order build director.
	/// </summary>
	public virtual void RegisterWorkOrderBuildDirector()
	{
		if (TypesToExclude.Contains(typeof(IWorkOrderBuildDirector)))
			return;

		BootstrapperFactory.ContainerProvider.Register<IWorkOrderBuildDirector, WorkOrderBuildDirector>(LifetimeType.Singleton);
	}

	public virtual void RegisterWorkOrderBuildDirectorStages()
	{
		if (TypesToExclude.Contains(typeof(IReadOnlyList<IWorkOrderBuildStage>)))
			return;

		BootstrapperFactory.ContainerProvider.Register<IReadOnlyList<IWorkOrderBuildStage>>(r =>
			new List<IWorkOrderBuildStage>
			{
				new RoutedControllersBuilder(
					r.Resolve<IControllerResolutionPipeline>(),
					r.Resolve<ICrsHandlingPipeline>()),
				new NotFoundBuilder(),
				new GlobalControllersBuilder()
			}, LifetimeType.Singleton);
	}
}