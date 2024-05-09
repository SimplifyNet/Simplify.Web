using System.Collections.Generic;
using System.Linq;
using Simplify.DI;
using Simplify.Web.Controllers.Resolution.Handling;
using Simplify.Web.Controllers.Resolution.Handling.Stages;

namespace Simplify.Web.Bootstrapper.Setup;

/// <summary>
/// Provides the bootstrapper controllers resolution handling registration.
/// </summary>
public partial class BaseBootstrapper
{
	/// <summary>
	/// Registers the controllers resolution state handling pipeline.
	/// </summary>
	public virtual void RegisterCrsHandlingPipeline()
	{
		if (TypesToExclude.Contains(typeof(ICrsHandlingPipeline)))
			return;

		BootstrapperFactory.ContainerProvider.Register<ICrsHandlingPipeline, CrsHandlingPipeline>(LifetimeType.Singleton);
	}

	public virtual void RegisterCrsHandlers()
	{
		if (TypesToExclude.Contains(typeof(IReadOnlyList<ICrsHandler>)))
			return;

		BootstrapperFactory.ContainerProvider.Register<IReadOnlyList<ICrsHandler>>(r =>
			[
				new UnauthorizedHandler(),
				new ForbiddenHandler(),
				new MatchedControllerHandler()
			], LifetimeType.Singleton);
	}
}