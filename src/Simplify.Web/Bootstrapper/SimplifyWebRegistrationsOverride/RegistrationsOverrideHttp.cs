using System;
using Simplify.DI;
using Simplify.Web.Http.ResponseWriting;

namespace Simplify.Web.Bootstrapper.SimplifyWebRegistrationsOverride;

/// <summary>
/// Provides the Simplify.Web HTTP override.
/// </summary>
public partial class RegistrationsOverride
{
	/// <summary>
	/// Overrides the `IResponseWriter` registration.
	/// </summary>
	/// <param name="action">The custom registration action.</param>
	public RegistrationsOverride OverrideResponseWriter(Action<IDIRegistrator> action) => AddAction<IResponseWriter>(action);
}