using System;
using System.Collections.Generic;
using Simplify.DI;
using Simplify.Web.Controllers.Resolution.Handling;

namespace Simplify.Web.Bootstrapper.SimplifyWebRegistrationsOverride;

/// <summary>
/// Provides the Simplify.Web controllers resolution handling override.
/// </summary>
public partial class RegistrationsOverride
{
	/// <summary>
	/// Overrides the `ICrsHandlingPipeline` registration.
	/// </summary>
	/// <param name="action">The custom registration action.</param>
	public RegistrationsOverride OverrideCrsHandlingPipeline(Action<IDIRegistrator> action) => AddAction<ICrsHandlingPipeline>(action);

	/// <summary>
	/// Overrides the `IReadOnlyList<ICrsHandler>` registration.
	/// </summary>
	/// <param name="action">The custom registration action.</param>
	public RegistrationsOverride OverrideCrsHandlers(Action<IDIRegistrator> action) => AddAction<IReadOnlyList<ICrsHandler>>(action);
}