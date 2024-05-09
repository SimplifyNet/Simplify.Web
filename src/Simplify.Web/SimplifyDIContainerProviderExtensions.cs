using System;
using System.Collections.Generic;
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
	/// <param name="containerProvider">The container provider.</param>
	/// <param name="registrationsOverride">The `Simplify.Web` types registrations override</param>
	public static IDIContainerProvider RegisterSimplifyWeb(this IDIContainerProvider containerProvider,
		Action<RegistrationsOverride>? registrationsOverride = null)
	{
		BootstrapperFactory.ContainerProvider = containerProvider;

		BootstrapperFactory
			.CreateBootstrapper()
			.Register(registrationsOverride != null
				? PerformTypesOverride(registrationsOverride)
				: null);

		return containerProvider;
	}

	private static IEnumerable<Type> PerformTypesOverride(Action<RegistrationsOverride> registrationsOverride)
	{
		var overrideObj = new RegistrationsOverride();

		registrationsOverride.Invoke(overrideObj);

		overrideObj.RegisterActions(BootstrapperFactory.ContainerProvider);

		return overrideObj.GetTypesToExclude();
	}
}