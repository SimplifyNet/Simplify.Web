using System;

namespace Simplify.Web.Utils.DateTimes;

public static class DateTimeExtensions
{
	/// <summary>
	/// Removes the milliseconds from DataTime.
	/// </summary>
	/// <param name="dt">The source date and time.</param>
	public static DateTime TrimMilliseconds(this DateTime dt) => new(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second, 0);
}