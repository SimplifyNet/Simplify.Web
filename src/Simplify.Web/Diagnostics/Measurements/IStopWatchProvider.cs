using System;

namespace Simplify.Web.Diagnostics.Measurements;

/// <summary>
/// Represent a stopwatch provider.
/// </summary>
public interface IStopwatchProvider
{
	/// <summary>
	/// Starts the measurement.
	/// </summary>
	void StartMeasurement();

	/// <summary>
	/// Stops and get the measurement.
	/// </summary>
	TimeSpan StopAndGetMeasurement();
}