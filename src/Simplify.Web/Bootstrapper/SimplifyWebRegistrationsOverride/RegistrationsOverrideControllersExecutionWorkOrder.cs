using System;
using System.Collections.Generic;
using Simplify.DI;
using Simplify.Web.Controllers.Execution.WorkOrder;
using Simplify.Web.Controllers.Execution.WorkOrder.BuildStages;
using Simplify.Web.Controllers.Execution.WorkOrder.Director;

namespace Simplify.Web.Bootstrapper.SimplifyWebRegistrationsOverride;

/// <summary>
/// Provides the Simplify.Web controllers execution work order override.
/// </summary>
public partial class RegistrationsOverride
{
	/// <summary>
	/// Overrides the `IExecutionWorkOrderBuildDirector` registration.
	/// </summary>
	/// <param name="action">The custom registration action.</param>
	public RegistrationsOverride OverrideExecutionWorkOrderBuildDirector(Action<IDIRegistrator> action) => AddAction<IExecutionWorkOrderBuildDirector>(action);

	/// <summary>
	/// Overrides the `RoutedControllersBuilder` registration.
	/// </summary>
	/// <param name="action">The custom registration action.</param>
	public RegistrationsOverride OverrideExecutionWorkOrderBuildDirectorRoutedControllersBuilder(Action<IDIRegistrator> action) => AddAction<RoutedControllersBuilder>(action);

	/// <summary>
	/// Overrides the `NotFoundBuilder` registration.
	/// </summary>
	/// <param name="action">The custom registration action.</param>
	public RegistrationsOverride OverrideExecutionWorkOrderBuildDirectorNotFoundBuilder(Action<IDIRegistrator> action) => AddAction<NotFoundBuilder>(action);

	/// <summary>
	/// Overrides the `GlobalControllersBuilder` registration.
	/// </summary>
	/// <param name="action">The custom registration action.</param>
	public RegistrationsOverride OverrideExecutionWorkOrderBuildDirectorGlobalControllersBuilder(Action<IDIRegistrator> action) => AddAction<GlobalControllersBuilder>(action);

	/// <summary>
	/// Overrides the `IReadOnlyList&lt;IExecutionWorkOrderBuildStage&gt;` registration.
	/// </summary>
	/// <param name="action">The custom registration action.</param>
	public RegistrationsOverride OverrideExecutionWorkOrderBuildDirectorStages(Action<IDIRegistrator> action) => AddAction<IReadOnlyList<IExecutionWorkOrderBuildStage>>(action);
}