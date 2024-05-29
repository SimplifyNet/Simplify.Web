using Simplify.DI;
using Simplify.Web.Settings;

namespace Simplify.Web.Diagnostics.Measurements;

/// <summary>
/// Provides the measurement extensions.
/// </summary>
public static class ScopeMeasurementExtensions
{
	/// <summary>
	/// Starts the measurements for specified scope.
	/// </summary>
	/// <param name="scope">The scope.</param>
	public static ILifetimeScope StartMeasurements(this ILifetimeScope scope)
	{
		if (!scope.Resolver.Resolve<ISimplifyWebSettings>().MeasurementsEnabled)
			return scope;

		// Starts execution measurement
		scope.Resolver.Resolve<IStopwatchProvider>().StartMeasurement();

		return scope;
	}
}