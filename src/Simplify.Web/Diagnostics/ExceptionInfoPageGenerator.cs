#nullable disable

using System;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using Simplify.System;
using Simplify.Templates;

namespace Simplify.Web.Diagnostics
{
	/// <summary>
	/// Provides exception information HTML page generator
	/// </summary>
	public static class ExceptionInfoPageGenerator
	{
		/// <summary>
		/// Generates the HTML page with exception information
		/// </summary>
		/// <param name="e">Exception to get information from.</param>
		/// <param name="hideExceptionDetails">if set to <c>true</c> then exception details will not be shown.</param>
		/// <returns></returns>
		public static string Generate(Exception e, bool hideExceptionDetails = false)
		{
			if (e == null)
				return null;

			var tpl = TemplateBuilder.FromCurrentAssembly("Diagnostics.ExceptionInfoPage.html").Build();

			tpl.Set("Simplify.Web.Version", new AssemblyInfo(Assembly.GetCallingAssembly()).Version);

			if (hideExceptionDetails)
			{
				tpl.Set("ExceptionDetails", "");
				return tpl.Get();
			}

			var detailsTpl = TemplateBuilder.FromCurrentAssembly("Diagnostics.ExceptionIDetails.html").Build();

			var trace = new StackTrace(e, true);

			if (trace.FrameCount == 0)
				return null;

			var fileLineNumber = trace.GetFrame(0).GetFileLineNumber();
			var fileColumnNumber = trace.GetFrame(0).GetFileColumnNumber();

			var positionPrefix = fileLineNumber == 0 && fileColumnNumber == 0 ? "" : $"[{fileLineNumber}:{fileColumnNumber}]";

			detailsTpl.Set("StackTrace",
				$"<b>{positionPrefix} {e.GetType()} : {e.Message}</b>{Environment.NewLine}{trace}{GetInnerExceptionData(1, e.InnerException)}"
					.Replace(Environment.NewLine, "<br />"));

			tpl.Set("ExceptionDetails", detailsTpl);

			return tpl.Get();
		}

		private static string GetInnerExceptionData(int currentLevel, Exception e)
		{
			if (e == null)
				return null;

			var trace = new StackTrace(e, true);

			if (trace.FrameCount == 0)
				return null;

			var fileLineNumber = trace.GetFrame(0).GetFileLineNumber();
			var fileColumnNumber = trace.GetFrame(0).GetFileColumnNumber();
			var positionPrefix = fileLineNumber == 0 && fileColumnNumber == 0 ? "" : $"[{fileLineNumber}:{fileColumnNumber}]";
			var levelText = currentLevel > 1 ? " " + currentLevel.ToString(CultureInfo.InvariantCulture) : "";

			return
				$"<br /><b>[Inner Exception{levelText}]{positionPrefix} {e.GetType()} : {e.Message}</b>{Environment.NewLine}{trace}{GetInnerExceptionData(currentLevel + 1, e.InnerException)}";
		}
	}
}