using Simplify.DI;

namespace Simplify.Web.Diagnostics
{
	/// <summary>
	/// Provides measurement extensions
	/// </summary>
	public static class ScopeMeasurementExtensions
	{
		/// <summary>
		/// Starts the measurements for specified scope.
		/// </summary>
		/// <param name="scope">The scope.</param>
		/// <returns></returns>
		public static ILifetimeScope StartMeasurements(this ILifetimeScope scope)
		{
			// Starts execution measurement
			scope.Resolver.Resolve<IStopwatchProvider>().StartMeasurement();

			return scope;
		}
	}
}