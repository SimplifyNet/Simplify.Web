using System;
using System.Collections.Generic;
using Simplify.DI;
using Simplify.Web.Controllers.Execution;
using Simplify.Web.Controllers.Execution.Resolver;
using Simplify.Web.Controllers.Response;

namespace Simplify.Web.Bootstrapper.SimplifyWebRegistrationsOverride;

/// <summary>
/// Provides the Simplify.Web controllers execution override.
/// </summary>
public partial class RegistrationsOverride
{
	/// <summary>
	/// Overrides the `IControllerExecutorResolver` registration.
	/// </summary>
	/// <param name="action">The custom registration action.</param>
	public RegistrationsOverride OverrideControllerExecutorResolver(Action<IDIRegistrator> action) => AddAction<IControllerExecutorResolver>(action);

	/// <summary>
	/// Overrides the `IReadOnlyList<IControllerExecutor>` registration.
	/// </summary>
	/// <param name="action">The custom registration action.</param>
	public RegistrationsOverride OverrideControllerExecutorResolverExecutors(Action<IDIRegistrator> action) => AddAction<IReadOnlyList<IControllerExecutor>>(action);

	/// <summary>
	/// Overrides the `IControllerResponseExecutor` registration.
	/// </summary>
	/// <param name="action">The custom registration action.</param>
	public RegistrationsOverride OverrideControllerResponseExecutor(Action<IDIRegistrator> action) => AddAction<IControllerResponseExecutor>(action);

	/// <summary>
	/// Overrides the `IControllersExecutor` registration.
	/// </summary>
	/// <param name="action">The custom registration action.</param>
	public RegistrationsOverride OverrideControllersExecutor(Action<IDIRegistrator> action) => AddAction<IControllersExecutor>(action);
}