using System;
using Microsoft.AspNetCore.Http;

namespace Simplify.Web.Http.ResponseTime;

public static class ResponseTimeExtensions
{
	public static void SetLastModifiedTime(this HttpResponse response, DateTime time) =>
		response.Headers.Append("Last-Modified", time.ToString("r"));

	public static void SetExpiresTime(this HttpResponse response, DateTime time) =>
		response.Headers["Expires"] = new DateTimeOffset(time).ToString("R");
}