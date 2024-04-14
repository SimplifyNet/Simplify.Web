using System;
using System.Collections.Generic;
using Simplify.DI;
using Simplify.Web.Old.Bootstrapper;

namespace Simplify.Web.Old;

/// <summary>
/// Provides Simplify.Web extensions for Simplify.DI container provider.
/// </summary>
public static class SimplifyDIContainerProviderExtensions
{
	/// <summary>
	/// Registers `Simplify.Web` types and controllers and use this container as current for `Simplify.Web`.
	/// </summary>
	/// <param name="containerProvider">The container provider.</param>
	/// <param name="registrationsOverride">The `Simplify.Web` types registrations override</param>
	public static IDIContainerProvider RegisterSimplifyWeb(this IDIContainerProvider containerProvider,
		Action<SimplifyWebRegistrationsOverride>? registrationsOverride = null)
	{
		BootstrapperFactory.ContainerProvider = containerProvider;

		BootstrapperFactory
			.CreateBootstrapper()
			.Register(registrationsOverride != null
				? PerformTypesOverride(registrationsOverride)
				: null);

		return containerProvider;
	}

	private static IEnumerable<Type>? PerformTypesOverride(Action<SimplifyWebRegistrationsOverride> registrationsOverride)
	{
		var overrideObj = new SimplifyWebRegistrationsOverride();

		registrationsOverride.Invoke(overrideObj);

		overrideObj.RegisterActions(BootstrapperFactory.ContainerProvider);

		return overrideObj.GetTypesToExclude();
	}
}