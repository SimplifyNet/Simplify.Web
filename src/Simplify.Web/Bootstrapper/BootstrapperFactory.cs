using System;
using Simplify.Web.Meta;

namespace Simplify.Web.Bootstrapper;

/// <summary>
/// Bootstrapper factory
/// </summary>
public static class BootstrapperFactory
{
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