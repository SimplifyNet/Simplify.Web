using System;
using System.Diagnostics;

namespace Simplify.Web.Diagnostics.Measurement;

/// <summary>
/// Provides stopwatch provider
/// </summary>
public class StopwatchProvider : IStopwatchProvider
{
	private Stopwatch? _stopwatch;

	/// <summary>
	/// Starts the measurement.
	/// </summary>
	public void StartMeasurement()
	{
		_stopwatch = new Stopwatch();
		_stopwatch.Start();
	}

	/// <summary>
	/// Stops the and get measurement.
	/// </summary>
	/// <returns></returns>
	public TimeSpan StopAndGetMeasurement()
	{
		if (_stopwatch == null)
			throw new InvalidOperationException("Measurement has not been started");

		_stopwatch.Stop();
		return _stopwatch.Elapsed;
	}
}