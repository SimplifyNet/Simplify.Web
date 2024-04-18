using System;

namespace Simplify.Web.Diagnostics.Measurement;

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
	/// Stops the and get measurement.
	/// </summary>
	TimeSpan StopAndGetMeasurement();
}