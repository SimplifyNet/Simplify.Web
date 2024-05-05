using System;
using Simplify.DI;
using Simplify.Web.Bootstrapper.Setup;
using Simplify.Web.System;

namespace Simplify.Web.Bootstrapper;

/// <summary>
/// Provides the base and default Simplify.Web bootstrapper factory.
/// </summary>
public static class BootstrapperFactory
{
	private static IDIContainerProvider? _containerProvider;

	/// <summary>
	/// Gets or sets the container provider used by Simplify.Web.
	/// </summary>
	/// <exception cref="ArgumentNullException">value</exception>
	public static IDIContainerProvider ContainerProvider
	{
		get => _containerProvider ??= DIContainer.Current;
		set => _containerProvider = value ?? throw new ArgumentNullException(nameof(value));
	}

	/// <summary>
	/// Creates the bootstrapper.
	/// </summary>
	public static BaseBootstrapper CreateBootstrapper()
	{
		var userBootstrapperType = SimplifyWebTypesFinder.FindTypeDerivedFrom<BaseBootstrapper>();

		return userBootstrapperType != null
			? (BaseBootstrapper)(Activator.CreateInstance(userBootstrapperType) ?? throw new InvalidOperationException())
			: new BaseBootstrapper();
	}
}