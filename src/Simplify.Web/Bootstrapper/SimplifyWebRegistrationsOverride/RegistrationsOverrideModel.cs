using System;
using Simplify.DI;
using Simplify.Web.Model;

namespace Simplify.Web.Bootstrapper.SimplifyWebRegistrationsOverride;

/// <summary>
/// Provides the Simplify.Web model override.
/// </summary>
public partial class RegistrationsOverride
{
	/// <summary>
	/// Overrides the `IModelHandler` registration.
	/// </summary>
	/// <param name="action">The custom registration action.</param>
	public RegistrationsOverride OverrideModelHandler(Action<IDIRegistrator> action) => AddAction<IModelHandler>(action);
}