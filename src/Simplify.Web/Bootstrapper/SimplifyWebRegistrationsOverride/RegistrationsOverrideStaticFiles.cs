using System;
using System.Collections.Generic;
using Simplify.DI;
using Simplify.Web.StaticFiles;
using Simplify.Web.StaticFiles.Context;
using Simplify.Web.StaticFiles.IO;

namespace Simplify.Web.Bootstrapper.SimplifyWebRegistrationsOverride;

/// <summary>
/// Provides the Simplify.Web static files override.
/// </summary>
public partial class RegistrationsOverride
{
	/// <summary>
	/// Overrides the `IStaticFile` registration.
	/// </summary>
	/// <param name="action">The custom registration action.</param>
	public RegistrationsOverride OverrideStaticFiler(Action<IDIRegistrator> action) => AddAction<IStaticFile>(action);

	/// <summary>
	/// Overrides the `IStaticFileProcessingContextFactory` registration.
	/// </summary>
	/// <param name="action">The custom registration action.</param>
	public RegistrationsOverride OverrideStaticFileProcessingContextFactory(Action<IDIRegistrator> action) => AddAction<IStaticFileProcessingContextFactory>(action);

	/// <summary>
	/// Overrides the `IStaticFileRequestHandlingPipeline` registration.
	/// </summary>
	/// <param name="action">The custom registration action.</param>
	public RegistrationsOverride OverrideStaticFileRequestHandlingPipeline(Action<IDIRegistrator> action) => AddAction<IStaticFileRequestHandlingPipeline>(action);

	/// <summary>
	/// Overrides the `IReadOnlyList<IStaticFileRequestHandler>` registration.
	/// </summary>
	/// <param name="action">The custom registration action.</param>
	public RegistrationsOverride OverrideStaticFileRequestHandlingPipelineHandlers(Action<IDIRegistrator> action) => AddAction<IReadOnlyList<IStaticFileRequestHandler>>(action);
}