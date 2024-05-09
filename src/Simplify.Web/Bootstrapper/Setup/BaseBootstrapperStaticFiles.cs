using System;
using System.Collections.Generic;
using System.Linq;
using Simplify.DI;
using Simplify.Web.Http.ResponseWriting;
using Simplify.Web.Modules.ApplicationEnvironment;
using Simplify.Web.Settings;
using Simplify.Web.StaticFiles;
using Simplify.Web.StaticFiles.Context;
using Simplify.Web.StaticFiles.IO;

namespace Simplify.Web.Bootstrapper.Setup;

/// <summary>
/// Provides the bootstrapper static files registration methods.
/// </summary>
public partial class BaseBootstrapper
{
	public virtual void RegisterStaticFile()
	{
		if (TypesToExclude.Contains(typeof(IStaticFile)))
			return;

		BootstrapperFactory.ContainerProvider.Register<IStaticFile>(r =>
			new StaticFile(
				r.Resolve<ISimplifyWebSettings>().StaticFilesPaths,
				r.Resolve<IEnvironment>().AppPhysicalPath),
			LifetimeType.Singleton);
	}

	public virtual void RegisterStaticFileProcessingContextFactory()
	{
		if (TypesToExclude.Contains(typeof(IStaticFileProcessingContextFactory)))
			return;

		BootstrapperFactory.ContainerProvider.Register<IStaticFileProcessingContextFactory, StaticFileProcessingContextFactory>(
			LifetimeType.Singleton);
	}

	public virtual void RegisterStaticFileRequestHandlingPipeline()
	{
		if (TypesToExclude.Contains(typeof(IStaticFileRequestHandlingPipeline)))
			return;

		BootstrapperFactory.ContainerProvider.Register<IStaticFileRequestHandlingPipeline, StaticFileRequestHandlingPipeline>(
			LifetimeType.Singleton);
	}

	public virtual void RegisterStaticFileRequestHandlingPipelineHandlers()
	{
		if (TypesToExclude.Contains(typeof(IReadOnlyList<IStaticFileRequestHandler>)))
			return;

		BootstrapperFactory.ContainerProvider.Register<IReadOnlyList<IStaticFileRequestHandler>>(r =>
			new List<IStaticFileRequestHandler>
			{
				new CachedFileHandler(),
				new NewFileHandler(r.Resolve<IResponseWriter>(), r.Resolve<IStaticFile>())
			}, LifetimeType.Singleton);
	}
}