using System;
using System.Collections.Generic;
using System.Linq;
using Simplify.DI;
using Simplify.Web.Controllers.Resolution;
using Simplify.Web.Controllers.Resolution.Stages;
using Simplify.Web.Controllers.Resolution.State;
using Simplify.Web.Controllers.RouteMatching.Resolver;
using Simplify.Web.Controllers.Security;

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

		BootstrapperFactory.ContainerProvider.Register<IControllerResolutionPipeline, ControllerResolutionPipeline>(LifetimeType.Singleton);
	}

	public virtual void RegisterControllerResolutionPipelineStages()
	{
		if (TypesToExclude.Contains(typeof(IReadOnlyList<IControllerResolutionStage>)))
			return;

		BootstrapperFactory.ContainerProvider.Register<IReadOnlyList<IControllerResolutionStage>>(r =>
			new List<IControllerResolutionStage>
			{
				new RouteMatchingStage(
					r.Resolve<IRouteMatcherResolver>()),
				new SecurityCheckStage(
					r.Resolve<ISecurityChecker>())
			}, LifetimeType.Singleton);
	}
}