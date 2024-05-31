using System.Collections.Generic;
using System.Linq;

namespace Simplify.Web.Http;

/// <summary>
/// Provides the converter
/// </summary>
public static class Converter
{
	/// <summary>
	/// Converts the HTTP method string to HTTP method.
	/// </summary>
	/// <param name="httpMethod">The HTTP method string representation.</param>
	public static HttpMethod HttpMethodStringToToHttpMethod(string httpMethod)
	{
		var item = Relations.HttpMethodToToHttpMethodStringRelation.FirstOrDefault(x => x.Value == httpMethod);

		return default(KeyValuePair<HttpMethod, string>).Equals(item) ? HttpMethod.Undefined : item.Key;
	}
}