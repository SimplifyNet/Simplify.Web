using System;
using System.Diagnostics;
using System.Globalization;

namespace Simplify.Web.Diagnostics
{
	/// <summary>
	/// Provides detailed exception info builder
	/// </summary>
	public static class DetailedExceptionInfoBuilder
	{
		/// <summary>
		/// Builds the detailed exception info.
		/// </summary>
		/// <param name="e">The exception.</param>
		/// <param name="htmlFormatting">if set to <c>true</c> then HTML formatting will be added to text.</param>
		public static string? Build(Exception e, bool htmlFormatting)
		{
			var trace = new StackTrace(e, true);

			if (trace.FrameCount == 0)
				return null;

			var fileLineNumber = trace.GetFrame(0)?.GetFileLineNumber();
			var fileColumnNumber = trace.GetFrame(0)?.GetFileColumnNumber();

			var positionPrefix = fileLineNumber == 0 && fileColumnNumber == 0
				? ""
				: $"[{fileLineNumber}:{fileColumnNumber}]";

			var result = (htmlFormatting ? "<b>" : "")
				   + $"{positionPrefix} {e.GetType()} : {e.Message}"
				   + (htmlFormatting ? "</b>" : "")
				   + $"{Environment.NewLine}{trace}{BuildInnerExceptionData(1, e.InnerException, htmlFormatting)}";

			if (htmlFormatting)
				result = result.Replace(Environment.NewLine, "<br />");

			return result;
		}

		private static string? BuildInnerExceptionData(int currentLevel, Exception? e, bool htmlFormatting)
		{
			if (e == null)
				return null;

			var trace = new StackTrace(e, true);

			if (trace.FrameCount == 0)
				return null;

			var fileLineNumber = trace.GetFrame(0)?.GetFileLineNumber();
			var fileColumnNumber = trace.GetFrame(0)?.GetFileColumnNumber();

			var positionPrefix = fileLineNumber == 0 && fileColumnNumber == 0
				? ""
				: $"[{fileLineNumber}:{fileColumnNumber}]";

			var levelText = currentLevel > 1
				? " " + currentLevel.ToString(CultureInfo.InvariantCulture)
				: "";

			return (htmlFormatting ? "<br /><b>" : "")
				   + $"[Inner Exception{levelText}]{positionPrefix} {e.GetType()} : {e.Message}"
				   + (htmlFormatting ? "</b>" : "")
				   + $"{Environment.NewLine}{trace}{BuildInnerExceptionData(currentLevel + 1, e.InnerException, htmlFormatting)}";
		}
	}
}