using System;
using Simplify.Web.Diagnostics.Templates;

namespace Simplify.Web.Diagnostics
{
	/// <summary>
	/// Provides HTTP 500 error page generator
	/// </summary>
	public static class ErrorPageGenerator
	{
		/// <summary>
		/// Generates the HTML page with exception information
		/// </summary>
		/// <param name="e">Exception to get information from.</param>
		/// <param name="hideExceptionDetails">if set to <c>true</c> then exception details will not be shown.</param>
		/// <param name="darkStyle"><c>true</c> if page style should be in dark colors.</param>
		/// <param name="minimalStyle">If set to true then no HTML page style will be added.</param>
		public static string Generate(Exception e,
			bool hideExceptionDetails = false,
			bool darkStyle = false,
			bool minimalStyle = false)
		{
			var exceptionText = hideExceptionDetails
				? null
				: DetailedExceptionInfoBuilder.Build(e, !minimalStyle);

			return minimalStyle
				? Http500MinimalErrorPageBuilder.Build(exceptionText)
				: Http500ErrorPageBuilder.Build(exceptionText, darkStyle);
		}
	}
}