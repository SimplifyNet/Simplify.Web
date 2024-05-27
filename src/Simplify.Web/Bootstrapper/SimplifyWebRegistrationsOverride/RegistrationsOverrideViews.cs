using System;
using Simplify.DI;
using Simplify.Web.Views;

namespace Simplify.Web.Bootstrapper.SimplifyWebRegistrationsOverride;

/// <summary>
/// Provides the Simplify.Web views override.
/// </summary>
public partial class RegistrationsOverride
{
	/// <summary>
	/// Overrides the `IViewFactory` registration.
	/// </summary>
	/// <param name="action">The custom registration action.</param>
	public RegistrationsOverride OverrideViewFactory(Action<IDIRegistrator> action) => AddAction<IViewFactory>(action);
}