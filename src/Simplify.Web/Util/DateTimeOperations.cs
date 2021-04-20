using System;

namespace Simplify.Web.Util
{
	/// <summary>
	/// Provides DateTime utility methods
	/// </summary>
	public static class DateTimeOperations
	{
		/// <summary>
		/// Removes milliseconds from DataTime
		/// </summary>
		/// <param name="dt">Date and time.</param>
		/// <returns></returns>
		public static DateTime TrimMilliseconds(DateTime dt) => new(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second, 0);
	}
}