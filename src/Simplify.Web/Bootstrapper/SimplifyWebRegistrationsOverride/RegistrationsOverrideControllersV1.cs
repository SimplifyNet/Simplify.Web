using System;
using Simplify.DI;
using Simplify.Web.Controllers.V1.Execution;

namespace Simplify.Web.Bootstrapper.SimplifyWebRegistrationsOverride;

/// <summary>
/// Provides the Simplify.Web controllers v1 override.
/// </summary>
public partial class RegistrationsOverride
{
	/// <summary>
	/// Overrides the `IController1Factory` registration.
	/// </summary>
	/// <param name="action">The custom registration action.</param>
	public RegistrationsOverride OverrideController1Factory(Action<IDIRegistrator> action) => AddAction<IController1Factory>(action);
}