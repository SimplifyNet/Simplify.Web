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
	/// <param name="registrator">IOC Container registrator.</param>
	public RegistrationsOverride OverrideControllerResponseExecutor(Action<IDIRegistrator> registrator) => AddAction(typeof(IControllerResponseExecutor), registrator);
}