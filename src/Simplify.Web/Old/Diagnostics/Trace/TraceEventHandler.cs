using Microsoft.AspNetCore.Http;

namespace Simplify.Web.Old.Diagnostics.Trace;

/// <summary>
/// HTTP requests trace delegate.
/// </summary>
public delegate void TraceEventHandler(HttpContext context);