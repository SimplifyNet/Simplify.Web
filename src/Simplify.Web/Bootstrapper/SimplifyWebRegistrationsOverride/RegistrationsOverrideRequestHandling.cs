using System;
using System.Collections.Generic;
using Simplify.DI;
using Simplify.Web.RequestHandling;

namespace Simplify.Web.Bootstrapper.SimplifyWebRegistrationsOverride;

/// <summary>
/// Provides the Simplify.Web request handling override.
/// </summary>
public partial class RegistrationsOverride
{
	/// <summary>
	/// Overrides the `IRequestHandlingPipeline` registration.
	/// </summary>
	/// <param name="action">The custom registration action.</param>
	public RegistrationsOverride OverrideRequestHandlingPipeline(Action<IDIRegistrator> action) => AddAction<IRequestHandlingPipeline>(action);

	/// <summary>
	/// Overrides the `IReadOnlyList<IRequestHandler>` registration.
	/// </summary>
	/// <param name="action">The custom registration action.</param>
	public RegistrationsOverride OverrideRequestHandlingPipelineHandlers(Action<IDIRegistrator> action) => AddAction<IReadOnlyList<IRequestHandler>>(action);
}