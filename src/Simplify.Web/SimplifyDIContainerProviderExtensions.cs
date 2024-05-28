using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Simplify.DI;
using Simplify.Web.Bootstrapper;
using Simplify.Web.Bootstrapper.SimplifyWebRegistrationsOverride;

namespace Simplify.Web;

/// <summary>
/// Provides the Simplify.Web extensions for Simplify.DI container provider.
/// </summary>
public static class SimplifyDIContainerProviderExtensions
{
	/// <summary>
	/// Registers the `Simplify.Web` types and controllers and use this container as current for `Simplify.Web`.
	/// </summary>
	/// <param name="registrator">The registrator.</param>
	/// <param name="configuration">The custom `IConfiguration`, internal `IConfiguration` registration will be overwritten.</param>
	/// <param name="registrationsOverride">The `Simplify.Web` types registrations override.</param>
	/// <param name="containerProvider">Overrides the internal `Simplify.Web` `IDIContainerProvider` with custom.</param>
	public static IDIRegistrator RegisterSimplifyWeb(this IDIRegistrator registrator,
		IConfiguration? configuration = null,
		Action<RegistrationsOverride>? registrationsOverride = null,
		IDIContainerProvider? containerProvider = null)
	{
		if (containerProvider != null)
			BootstrapperFactory.ContainerProvider = containerProvider;

		BootstrapperFactory
			.CreateBootstrapper()
			.Register(configuration,
				registrationsOverride != null
					? PerformTypesOverride(registrationsOverride)
					: null);

		return registrator;
	}

	private static IEnumerable<Type> PerformTypesOverride(Action<RegistrationsOverride> registrationsOverride)
	{
		var overrideObj = new RegistrationsOverride();

		registrationsOverride.Invoke(overrideObj);

		overrideObj.RegisterActions(BootstrapperFactory.ContainerProvider);

		return overrideObj.GetTypesToExclude();
	}
}