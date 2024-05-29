using System;
using Simplify.DI;
using Simplify.Web.Diagnostics.Measurements;

namespace Simplify.Web.Bootstrapper.SimplifyWebRegistrationsOverride;

/// <summary>
/// Provides the Simplify.Web diagnostics override.
/// </summary>
public partial class RegistrationsOverride
{
	/// <summary>
	/// Overrides the `IStopwatchProvider` registration.
	/// </summary>
	/// <param name="action">The custom registration action.</param>
	public RegistrationsOverride OverrideStopwatchProvider(Action<IDIRegistrator> action) => AddAction<IStopwatchProvider>(action);
}