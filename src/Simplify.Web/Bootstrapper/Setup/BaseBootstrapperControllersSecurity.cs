using System.Collections.Generic;
using System.Linq;
using Simplify.DI;
using Simplify.Web.Controllers.Security;
using Simplify.Web.Controllers.Security.Rules;

namespace Simplify.Web.Bootstrapper.Setup;

/// <summary>
/// Provides the bootstrapper controllers security registration.
/// </summary>
public partial class BaseBootstrapper
{
	/// <summary>
	/// Registers the security checker.
	/// </summary>
	public virtual void RegisterSecurityChecker()
	{
		if (TypesToExclude.Contains(typeof(ISecurityChecker)))
			return;

		BootstrapperFactory.ContainerProvider.Register<ISecurityChecker, SecurityChecker>();
	}

	/// <summary>
	/// Registers the security rules unauthorized rule.
	/// </summary>
	public virtual void RegisterSecurityRulesUnauthorizedRule()
	{
		if (TypesToExclude.Contains(typeof(UnauthorizedRule)))
			return;

		BootstrapperFactory.ContainerProvider.Register<UnauthorizedRule>();
	}

	/// <summary>
	/// Registers the security rules authorization rule.
	/// </summary>
	public virtual void RegisterSecurityRulesRoleAuthorizationRule()
	{
		if (TypesToExclude.Contains(typeof(RoleAuthorizationRule)))
			return;

		BootstrapperFactory.ContainerProvider.Register<RoleAuthorizationRule>();
	}

	/// <summary>
	/// Registers the security rules.
	/// </summary>
	public virtual void RegisterSecurityRules()
	{
		if (TypesToExclude.Contains(typeof(IReadOnlyList<ISecurityRule>)))
			return;

		BootstrapperFactory.ContainerProvider.Register<IReadOnlyList<ISecurityRule>>(r =>
			[
				r.Resolve<UnauthorizedRule>(),
				r.Resolve<RoleAuthorizationRule>()
			]);
	}
}