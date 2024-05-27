using System;
using System.Collections.Generic;
using Simplify.DI;
using Simplify.Web.Controllers.Security;

namespace Simplify.Web.Bootstrapper.SimplifyWebRegistrationsOverride;

/// <summary>
/// Provides the Simplify.Web controllers security override.
/// </summary>
public partial class RegistrationsOverride
{
	/// <summary>
	/// Overrides the `ISecurityChecker` registration.
	/// </summary>
	/// <param name="action">The custom registration action.</param>
	public RegistrationsOverride OverrideSecurityChecker(Action<IDIRegistrator> action) => AddAction<ISecurityChecker>(action);

	/// <summary>
	/// Overrides the `IReadOnlyList<ISecurityRule>` registration.
	/// </summary>
	/// <param name="action">The custom registration action.</param>
	public RegistrationsOverride OverrideSecurityRules(Action<IDIRegistrator> action) => AddAction<IReadOnlyList<ISecurityRule>>(action);
}