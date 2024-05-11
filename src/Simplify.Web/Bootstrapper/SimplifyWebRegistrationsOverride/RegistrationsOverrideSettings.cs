using System;
using Simplify.DI;
using Simplify.Web.Settings;

namespace Simplify.Web.Bootstrapper.SimplifyWebRegistrationsOverride;

/// <summary>
/// Provides the Simplify.Web settings override.
/// </summary>
public partial class RegistrationsOverride
{
	/// <summary>
	/// Overrides the `ISimplifyWebSettings` registration.
	/// </summary>
	/// <param name="action">The custom registration action.</param>
	public RegistrationsOverride OverrideSimplifyWebSettings(Action<IDIRegistrator> action) => AddAction<ISimplifyWebSettings>(action);
}