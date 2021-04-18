using System;
using Simplify.Web.Diagnostics.Templates;

namespace Simplify.Web.Diagnostics
{
	/// <summary>
	/// Provides exception information HTML page generator
	/// </summary>
	public static class ErrorPageGenerator
	{
		/// <summary>
		/// Generates the HTML page with exception information
		/// </summary>
		/// <param name="e">Exception to get information from.</param>
		/// <param name="hideExceptionDetails">if set to <c>true</c> then exception details will not be shown.</param>
		/// <param name="darkStyle"><c>true</c> if page style should be in dark colors.</param>
		/// <returns></returns>
		public static string Generate(Exception e, bool hideExceptionDetails = false, bool darkStyle = false) =>
			Http500ErrorPageBuilder.Build(hideExceptionDetails
					? null
					: DetailedExceptionInfoBuilder.Build(e),
				darkStyle);
	}
}