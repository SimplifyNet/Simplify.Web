using Microsoft.AspNetCore.Http;

namespace Simplify.Web.Diagnostics
{
	/// <summary>
	/// HTTP requests trace delegate
	/// </summary>
	public delegate void TraceEventHandler(HttpContext context);
}