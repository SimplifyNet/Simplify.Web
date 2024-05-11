using System;
using Simplify.DI;
using Simplify.Web.RequestHandling;

namespace Simplify.Web.Bootstrapper.SimplifyWebRegistrationsOverride;

/// <summary>
/// Provides the Simplify.Web diagnostics override.
/// </summary>
public partial class RegistrationsOverride
{
	/// <summary>
	/// Overrides the `IRequestHandler` registration.
	/// </summary>
	/// <param name="action">The custom registration action.</param>
	public RegistrationsOverride OverrideRequestHandler(Action<IDIRegistrator> action) => AddAction<IRequestHandler>(action);
}