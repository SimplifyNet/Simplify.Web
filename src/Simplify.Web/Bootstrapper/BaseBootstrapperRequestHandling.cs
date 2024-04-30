using System;
using System.Collections.Generic;
using System.Linq;
using Simplify.DI;
using Simplify.Web.RequestHandling;

namespace Simplify.Web.Bootstrapper;

/// <summary>
/// Provides the base bootstrapper request handling registration methods.
/// </summary>
public partial class BaseBootstrapper
{
	public virtual void RegisterRequestHandlingPipeline()
	{
		if (TypesToExclude.Contains(typeof(IRequestHandlingPipeline)))
			return;

		BootstrapperFactory.ContainerProvider.Register<IRequestHandlingPipeline, RequestHandlingPipeline>(LifetimeType.Singleton);
	}

	public virtual void RegisterRequestHandlingPipelineHandlers()
	{
		if (TypesToExclude.Contains(typeof(IReadOnlyList<IRequestHandler>)))
			return;

		BootstrapperFactory.ContainerProvider.Register<IReadOnlyList<IRequestHandler>>(r =>
			new List<IRequestHandler>
			{
				// new StaticFilesHandler(null, null, r.Resolve<IStaticFile>())
			}, LifetimeType.Singleton);
	}
}