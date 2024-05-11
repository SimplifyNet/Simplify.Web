using System;
using System.Collections.Generic;
using Simplify.DI;
using Simplify.Web.Page.Composition;
using Simplify.Web.Page.Generation;

namespace Simplify.Web.Bootstrapper.SimplifyWebRegistrationsOverride;

/// <summary>
/// Provides the Simplify.Web page override.
/// </summary>
public partial class RegistrationsOverride
{
	/// <summary>
	/// Overrides the `IPageComposer` registration.
	/// </summary>
	/// <param name="action">The custom registration action.</param>
	public RegistrationsOverride OverridePageComposer(Action<IDIRegistrator> action) => AddAction<IPageComposer>(action);

	/// <summary>
	/// Overrides the `IReadOnlyList<IPageCompositionStage>` registration.
	/// </summary>
	/// <param name="action">The custom registration action.</param>
	public RegistrationsOverride OverridePageCompositionStages(Action<IDIRegistrator> action) => AddAction<IReadOnlyList<IPageCompositionStage>>(action);

	/// <summary>
	/// Overrides the `IPageGenerator` registration.
	/// </summary>
	/// <param name="action">The custom registration action.</param>
	public RegistrationsOverride OverridePageGenerator(Action<IDIRegistrator> action) => AddAction<IPageGenerator>(action);
}