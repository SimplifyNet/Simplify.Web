using System;
using System.Collections.Generic;
using Simplify.DI;
using Simplify.Web.Controllers.Resolution;

namespace Simplify.Web.Bootstrapper.SimplifyWebRegistrationsOverride;

/// <summary>
/// Provides the Simplify.Web controllers resolution override.
/// </summary>
public partial class RegistrationsOverride
{
	/// <summary>
	/// Overrides the `IControllerResolutionPipeline` registration.
	/// </summary>
	/// <param name="action">The custom registration action.</param>
	public RegistrationsOverride OverrideControllerResolutionPipeline(Action<IDIRegistrator> action) => AddAction<IControllerResolutionPipeline>(action);

	/// <summary>
	/// Overrides the `IReadOnlyList&lt;IControllerResolutionStage&gt;` registration.
	/// </summary>
	/// <param name="action">The custom registration action.</param>
	public RegistrationsOverride OverrideControllerResolutionPipelineStages(Action<IDIRegistrator> action) => AddAction<IReadOnlyList<IControllerResolutionStage>>(action);
}