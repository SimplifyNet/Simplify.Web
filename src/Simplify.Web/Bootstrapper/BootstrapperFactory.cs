#nullable disable

using System;
using Simplify.DI;
using Simplify.Web.Meta;

namespace Simplify.Web.Bootstrapper
{
	/// <summary>
	/// Bootstrapper factory
	/// </summary>
	public static class BootstrapperFactory
	{
		private static IDIContainerProvider _containerProvider;

		/// <summary>
		/// Gets or sets the container provider used by Simplify.Web.
		/// </summary>
		/// <value>
		/// The container provider.
		/// </value>
		/// <exception cref="ArgumentNullException">value</exception>
		public static IDIContainerProvider ContainerProvider
		{
			get => _containerProvider ?? (_containerProvider = DIContainer.Current);
			set => _containerProvider = value ?? throw new ArgumentNullException(nameof(value));
		}

		/// <summary>
		/// Creates the bootstrapper.
		/// </summary>
		/// <returns></returns>
		public static BaseBootstrapper CreateBootstrapper()
		{
			var userBootstrapperType = SimplifyWebTypesFinder.FindTypeDerivedFrom<BaseBootstrapper>();
			return userBootstrapperType != null ? (BaseBootstrapper)Activator.CreateInstance(userBootstrapperType) : new BaseBootstrapper();
		}
	}
}