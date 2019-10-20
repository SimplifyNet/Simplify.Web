using Simplify.DI;

namespace Simplify.Web.Bootstrapper
{
	/// <summary>
	/// Provides Simplify.Web extensions for Simplify.DI container provider
	/// </summary>
	public static class SimplifyDIContainerProviderExtensions
	{
		/// <summary>
		/// Registers Simplify.Web types and controllers and use this container as current for Simplify.Web.
		/// </summary>
		/// <param name="containerProvider">The container provider.</param>
		public static IDIContainerProvider RegisterSimplifyWeb(this IDIContainerProvider containerProvider)
		{
			BootstrapperFactory.ContainerProvider = containerProvider;

			BootstrapperFactory.CreateBootstrapper().Register();

			return containerProvider;
		}
	}
}