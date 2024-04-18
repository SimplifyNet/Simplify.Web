using Simplify.DI;

namespace Simplify.Web.Diagnostics.Measurement;

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
		// Starts execution measurement
		scope.Resolver.Resolve<IStopwatchProvider>().StartMeasurement();

		return scope;
	}
}