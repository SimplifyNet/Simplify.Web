using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Simplify.DI;
using Simplify.Web.Old.Core.Controllers;
using Simplify.Web.Old.Core.Controllers.Execution;

namespace Simplify.Web.Old.Bootstrapper;

/// <summary>
/// Base and default Simplify.Web bootstrapper.
/// </summary>
// ReSharper disable once ClassTooBig
public class BaseBootstrapper
{
	/// <summary>
	/// Provides `Simplify.Web` types to exclude from registrations.
	/// </summary>
	protected IEnumerable<Type> TypesToExclude { get; private set; } = new List<Type>();

	/// <summary>
	/// Registers the types in container.
	/// </summary>
	// ReSharper disable once MethodTooLong

	public void Register(IEnumerable<Type>? typesToExclude = null)
	{
		if (typesToExclude != null)
			TypesToExclude = typesToExclude;

		// Registering non Simplify.Web types

		RegisterConfiguration();

		// Registering Simplify.Web core types

		RegisterController1Factory();
		RegisterController2Factory();
		RegisterControllersAgent();
		RegisterControllerResponseBuilder();
		RegisterController1Executor();
		RegisterController2Executor();
		RegisterVersionedControllerExecutorsList();
		RegisterControllerExecutor();
	}

	#region Simplify.Web types registration

	/// <summary>
	/// Registers the configuration.
	/// </summary>
	public virtual void RegisterConfiguration()
	{
		if (TypesToExclude.Contains(typeof(IConfiguration)))
			return;

		var environmentName = global::System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

		var builder = new ConfigurationBuilder()
			.AddJsonFile("appsettings.json", true)
			.AddJsonFile($"appsettings.{environmentName}.json", true);

		BootstrapperFactory.ContainerProvider.Register<IConfiguration>(r => builder.Build(), LifetimeType.Singleton);
	}

	/// <summary>
	/// Registers the controller v1 factory.
	/// </summary>
	public virtual void RegisterController1Factory()
	{
		if (TypesToExclude.Contains(typeof(IController1Factory)))
			return;

		BootstrapperFactory.ContainerProvider.Register<IController1Factory, Controller1Factory>(LifetimeType.Singleton);
	}

	/// <summary>
	/// Registers the controller v2 factory.
	/// </summary>
	public virtual void RegisterController2Factory()
	{
		if (TypesToExclude.Contains(typeof(IController2Factory)))
			return;

		BootstrapperFactory.ContainerProvider.Register<IController2Factory, Controller2Factory>(LifetimeType.Singleton);
	}

	/// <summary>
	/// Registers the controllers agent.
	/// </summary>
	public virtual void RegisterControllersAgent()
	{
		if (TypesToExclude.Contains(typeof(IControllersAgent)))
			return;

		BootstrapperFactory.ContainerProvider.Register<IControllersAgent, ControllersAgent>(LifetimeType.Singleton);
	}

	/// <summary>
	/// Registers the controller response builder.
	/// </summary>
	public virtual void RegisterControllerResponseBuilder()
	{
		if (TypesToExclude.Contains(typeof(IControllerResponseBuilder)))
			return;

		BootstrapperFactory.ContainerProvider.Register<IControllerResponseBuilder, ControllerResponseBuilder>(LifetimeType.Singleton);
	}

	/// <summary>
	/// Registers the controller v1 executor.
	/// </summary>
	public virtual void RegisterController1Executor()
	{
		if (TypesToExclude.Contains(typeof(Controller1Executor)))
			return;

		BootstrapperFactory.ContainerProvider.Register<Controller1Executor>(LifetimeType.Singleton);
	}

	/// <summary>
	/// Registers the controller v2 executor.
	/// </summary>
	public virtual void RegisterController2Executor()
	{
		if (TypesToExclude.Contains(typeof(Controller2Executor)))
			return;

		BootstrapperFactory.ContainerProvider.Register<Controller2Executor>(LifetimeType.Singleton);
	}

	/// <summary>
	/// Registers the versioned controller executors list.
	/// </summary>
	public virtual void RegisterVersionedControllerExecutorsList()
	{
		if (TypesToExclude.Contains(typeof(IList<IVersionedControllerExecutor>)))
			return;

		BootstrapperFactory.ContainerProvider.Register<IList<IVersionedControllerExecutor>>(r => new List<IVersionedControllerExecutor>
		{
			r.Resolve<Controller1Executor>(),
			r.Resolve<Controller2Executor>()
		}, LifetimeType.Singleton);
	}

	/// <summary>
	/// Registers the controller executor.
	/// </summary>
	public virtual void RegisterControllerExecutor()
	{
		if (TypesToExclude.Contains(typeof(IControllerExecutor)))
			return;

		BootstrapperFactory.ContainerProvider.Register<IControllerExecutor, ControllerExecutor>(LifetimeType.Singleton);
	}

	#endregion Simplify.Web types registration
}