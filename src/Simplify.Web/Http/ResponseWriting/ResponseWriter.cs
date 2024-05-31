using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Simplify.Web.Http.ResponseWriting;

/// <summary>
/// Provides default HTTP response writer.
/// </summary>
/// <seealso cref="IResponseWriter" />
public class ResponseWriter : IResponseWriter
{
	/// <summary>
	/// Writes the specified data asynchronously.
	/// </summary>
	/// <param name="response">The response.</param>
	/// <param name="data">The data.</param>
	public Task WriteAsync(HttpResponse response, string data) => response.WriteAsync(data);

	/// <summary>
	/// Writes the specified data asynchronously.
	/// </summary>
	/// <param name="response">The response.</param>
	/// <param name="data">The data.</param>
	public Task WriteAsync(HttpResponse response, byte[] data) => response.Body.WriteAsync(data, 0, data.Length);
}