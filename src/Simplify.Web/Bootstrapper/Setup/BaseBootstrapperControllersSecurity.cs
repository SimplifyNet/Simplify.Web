using System;
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
	public virtual void RegisterSecurityChecker()
	{
		if (TypesToExclude.Contains(typeof(ISecurityChecker)))
			return;

		BootstrapperFactory.ContainerProvider.Register<ISecurityChecker, SecurityChecker>(LifetimeType.Singleton);
	}

	public virtual void RegisterSecurityRules()
	{
		if (TypesToExclude.Contains(typeof(IReadOnlyList<ISecurityRule>)))
			return;

		BootstrapperFactory.ContainerProvider.Register<IReadOnlyList<ISecurityRule>>(r =>
			new List<ISecurityRule>
			{
				new UnauthorizedRule(),
				new RoleAuthorizationRule()
			}, LifetimeType.Singleton);
	}
}