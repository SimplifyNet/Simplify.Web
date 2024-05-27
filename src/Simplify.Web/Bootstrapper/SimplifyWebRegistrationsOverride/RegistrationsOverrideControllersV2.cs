using System;
using Simplify.DI;
using Simplify.Web.Controllers.V2.Execution;

namespace Simplify.Web.Bootstrapper.SimplifyWebRegistrationsOverride;

/// <summary>
/// Provides the Simplify.Web controllers v2 override.
/// </summary>
public partial class RegistrationsOverride
{
	/// <summary>
	/// Overrides the `IController2Factory` registration.
	/// </summary>
	/// <param name="action">The custom registration action.</param>
	public RegistrationsOverride OverrideController2Factory(Action<IDIRegistrator> action) => AddAction<IController2Factory>(action);
}