﻿using System.Collections.Generic;
using System.Linq;
using Simplify.DI;
using Simplify.Web.Controllers.Execution;
using Simplify.Web.Controllers.Execution.WorkOrder.Director;
using Simplify.Web.Http.ResponseWriting;
using Simplify.Web.Modules.Redirection;
using Simplify.Web.Page.Composition;
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
	/// <summary>
	/// Registers the request handling pipeline.
	/// </summary>
	public virtual void RegisterRequestHandlingPipeline()
	{
		if (TypesToExclude.Contains(typeof(IRequestHandlingPipeline)))
			return;

		BootstrapperFactory.ContainerProvider.Register<IRequestHandlingPipeline, RequestHandlingPipeline>();
	}

	/// <summary>
	/// Registers the request handling pipeline handlers.
	/// </summary>
	public virtual void RegisterRequestHandlingPipelineHandlers()
	{
		if (TypesToExclude.Contains(typeof(IReadOnlyList<IRequestHandler>)))
			return;

		BootstrapperFactory.ContainerProvider.Register<IReadOnlyList<IRequestHandler>>(r =>
		{
			var handlers = new List<IRequestHandler>
			{
				new SetLoginUrlForUnauthorizedRequestHandler(r.Resolve<IRedirector>()),
				new ControllersHandler(
					r.Resolve<IExecutionWorkOrderBuildDirector>(),
					r.Resolve<IControllersExecutor>()),
				new PageGenerationHandler(r.Resolve<IPageComposer>(), r.Resolve<IResponseWriter>())
			};

			if (Settings.StaticFilesEnabled)
				handlers.Insert(0, new StaticFilesHandler(
					r.Resolve<IStaticFileRequestHandlingPipeline>(),
					r.Resolve<IStaticFileProcessingContextFactory>(),
					r.Resolve<IStaticFile>()));

			return handlers;
		});
	}
}