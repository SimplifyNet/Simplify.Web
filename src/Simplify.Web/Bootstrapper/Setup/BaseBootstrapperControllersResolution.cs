using System.Collections.Generic;
using System.Linq;
using Simplify.DI;
using Simplify.Web.Controllers.Resolution;
using Simplify.Web.Controllers.Resolution.Stages;

namespace Simplify.Web.Bootstrapper.Setup;

/// <summary>
/// Provides the bootstrapper controllers resolution registration.
/// </summary>
public partial class BaseBootstrapper
{
	/// <summary>
	/// Registers the controller resolution pipeline.
	/// </summary>
	public virtual void RegisterControllerResolutionPipeline()
	{
		if (TypesToExclude.Contains(typeof(IControllerResolutionPipeline)))
			return;

		BootstrapperFactory.ContainerProvider.Register<IControllerResolutionPipeline, ControllerResolutionPipeline>();
	}

	/// <summary>
	/// Registers the controller resolution pipeline route matching stage.
	/// </summary>
	public virtual void RegisterControllerResolutionPipelineRouteMatchingStage()
	{
		if (TypesToExclude.Contains(typeof(RouteMatchingStage)))
			return;

		BootstrapperFactory.ContainerProvider.Register<RouteMatchingStage>();
	}

	/// <summary>
	/// Registers the controller resolution pipeline security check stage.
	/// </summary>
	public virtual void RegisterControllerResolutionPipelineSecurityCheckStage()
	{
		if (TypesToExclude.Contains(typeof(SecurityCheckStage)))
			return;

		BootstrapperFactory.ContainerProvider.Register<SecurityCheckStage>();
	}

	/// <summary>
	/// Registers the controller resolution pipeline stages.
	/// </summary>
	public virtual void RegisterControllerResolutionPipelineStages()
	{
		if (TypesToExclude.Contains(typeof(IReadOnlyList<IControllerResolutionStage>)))
			return;

		BootstrapperFactory.ContainerProvider.Register<IReadOnlyList<IControllerResolutionStage>>(r =>
			[
				r.Resolve<RouteMatchingStage>(),
				r.Resolve<SecurityCheckStage>()
			]);
	}
}