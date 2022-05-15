using System;
using Simplify.DI;
using Simplify.Web.Bootstrapper;

namespace Simplify.Web;

/// <summary>
/// Provides Simplify.Web extensions for Simplify.DI container provider
/// </summary>
public static class SimplifyDIContainerProviderExtensions
{
	/// <summary>
	/// Registers Simplify.Web types and controllers and use this container as current for Simplify.Web.
	/// </summary>
	/// <param name="containerProvider">The container provider.</param>
	public static IDIContainerProvider RegisterSimplifyWeb(this IDIContainerProvider containerProvider, Action<SimplifyWebRegistrationOverride>? registrationOverride = null)
	{
		BootstrapperFactory.ContainerProvider = containerProvider;

		if (registrationOverride != null)
		{
			var overrideSettings = new SimplifyWebRegistrationOverride();

			registrationOverride.Invoke(overrideSettings);

			overrideSettings.RegisterActions(BootstrapperFactory.ContainerProvider);
		}
		else
			BootstrapperFactory.CreateBootstrapper().Register();

		return containerProvider;
	}
}