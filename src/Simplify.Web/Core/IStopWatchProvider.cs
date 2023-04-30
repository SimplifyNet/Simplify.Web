using System;

namespace Simplify.Web.Core;

/// <summary>
/// Represent stopwatch provider
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
	/// <returns></returns>
	TimeSpan StopAndGetMeasurement();
}