using Simplify.Web.Http;

namespace Simplify.Web.Old.Util;

/// <summary>
/// Provides OWIN and HTTP related utility functions.
/// </summary>
public static class HttpRequestUtil
{
	/// <summary>
	/// Converts HTTP method string to HTTP method.
	/// </summary>
	/// <param name="method">The method.</param>
	/// <returns></returns>
	public static HttpMethod HttpMethodStringToHttpMethod(string method) =>
		method switch
		{
			"GET" => HttpMethod.Get,
			"POST" => HttpMethod.Post,
			"PUT" => HttpMethod.Put,
			"PATCH" => HttpMethod.Patch,
			"DELETE" => HttpMethod.Delete,
			"OPTIONS" => HttpMethod.Options,
			_ => HttpMethod.Undefined
		};
}