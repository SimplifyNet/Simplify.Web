using System;
using System.Collections.Generic;
using Simplify.DI;
using Simplify.Web.Controllers.Security;
using Simplify.Web.Controllers.Security.Rules;

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
	/// Overrides the `UnauthorizedRule` registration.
	/// </summary>
	/// <param name="action">The custom registration action.</param>
	public RegistrationsOverride OverrideUnauthorizedRule(Action<IDIRegistrator> action) => AddAction<UnauthorizedRule>(action);

	/// <summary>
	/// Overrides the `RoleAuthorizationRule` registration.
	/// </summary>
	/// <param name="action">The custom registration action.</param>
	public RegistrationsOverride OverrideRoleAuthorizationRule(Action<IDIRegistrator> action) => AddAction<RoleAuthorizationRule>(action);

	/// <summary>
	/// Overrides the `IReadOnlyList&lt;ISecurityRule&gt;` registration.
	/// </summary>
	/// <param name="action">The custom registration action.</param>
	public RegistrationsOverride OverrideSecurityRules(Action<IDIRegistrator> action) => AddAction<IReadOnlyList<ISecurityRule>>(action);
}