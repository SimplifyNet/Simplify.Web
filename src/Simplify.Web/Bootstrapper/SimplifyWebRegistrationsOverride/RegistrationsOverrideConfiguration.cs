using System;
using Microsoft.Extensions.Configuration;
using Simplify.DI;

namespace Simplify.Web.Bootstrapper.SimplifyWebRegistrationsOverride;

/// <summary>
/// Provides the Simplify.Web configuration registrations override.
/// </summary>
public partial class RegistrationsOverride
{
	/// <summary>
	/// Overrides the `IConfiguration` registration.
	/// </summary>
	/// <param name="registrator">IOC Container registrator.</param>
	public RegistrationsOverride OverrideConfiguration(Action<IDIRegistrator> registrator) => AddAction(typeof(IConfiguration), registrator);
}