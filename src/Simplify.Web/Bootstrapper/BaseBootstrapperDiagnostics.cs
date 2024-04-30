﻿using System.Linq;
using Simplify.DI;
using Simplify.Web.Diagnostics.Measurement;

namespace Simplify.Web.Bootstrapper;

/// <summary>
/// Provides the bootstrapper diagnostics registrations.
/// </summary>
public partial class BaseBootstrapper
{
	/// <summary>
	/// Registers the stopwatch provider.
	/// </summary>
	public virtual void RegisterStopwatchProvider()
	{
		if (TypesToExclude.Contains(typeof(IStopwatchProvider)))
			return;

		BootstrapperFactory.ContainerProvider.Register<IStopwatchProvider, StopwatchProvider>();
	}
}