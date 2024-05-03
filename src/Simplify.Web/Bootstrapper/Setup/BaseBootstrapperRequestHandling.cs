using System;
using System.Collections.Generic;
using System.Linq;
using Simplify.DI;
using Simplify.Web.Controllers.Execution;
using Simplify.Web.Controllers.ExecutionWorkOrder;
using Simplify.Web.RequestHandling;
using Simplify.Web.RequestHandling.Handlers;
using Simplify.Web.StaticFiles;
using Simplify.Web.StaticFiles.Context;
using Simplify.Web.StaticFiles.IO;

namespace Simplify.Web.Bootstrapper.Setup;

/// <summary>
/// Provides the bootstrapper request handling registrations.
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
				new StaticFilesHandler(
					r.Resolve<IStaticFileRequestHandlingPipeline>(),
					r.Resolve<IStaticFileProcessingContextFactory>(),
					r.Resolve<IStaticFile>()),
				new ControllersHandler(
					r.Resolve<IWorkOrderBuildDirector>(),
					r.Resolve<IControllersExecutor>())
			}, LifetimeType.Singleton);
	}
}