using System;
using Simplify.DI;
using Simplify.Web.Controllers.Response;

namespace Simplify.Web.Bootstrapper.SimplifyWebRegistrationsOverride;

/// <summary>
/// Provides the Simplify.Web controllers execution override.
/// </summary>
public partial class RegistrationsOverride
{
	/// <summary>
	/// Overrides the `IControllerResponseExecutor` registration.
	/// </summary>
	/// <param name="action">The custom registration action.</param>
	public RegistrationsOverride OverrideControllerResponseExecutor(Action<IDIRegistrator> action) => AddAction<IControllerResponseExecutor>(action);
}