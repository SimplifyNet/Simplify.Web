using System;
using Microsoft.AspNetCore.Http;

namespace Simplify.Web.Http.ResponseTime;

/// <summary>
/// Provides the response time extensions.
/// </summary>
public static class ResponseTimeExtensions
{
	/// <summary>
	/// Sets the last modified time.
	/// </summary>
	/// <param name="response">The response.</param>
	/// <param name="time">The time.</param>
	public static void SetLastModifiedTime(this HttpResponse response, DateTime time) =>
		response.Headers.Append("Last-Modified", time.ToString("r"));

	/// <summary>
	/// Sets the expires time.
	/// </summary>
	/// <param name="response">The response.</param>
	/// <param name="time">The time.</param>
	public static void SetExpiresTime(this HttpResponse response, DateTime time) =>
		response.Headers["Expires"] = new DateTimeOffset(time).ToString("R");
}