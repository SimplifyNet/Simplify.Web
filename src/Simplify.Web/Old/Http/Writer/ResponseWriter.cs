using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Simplify.Web.Old.Http.Writer;

/// <summary>
/// Provides default HTTP response writer.
/// </summary>
public class ResponseWriter : IResponseWriter
{
	/// <summary>
	/// Writes the specified data.
	/// </summary>
	/// <param name="data">The data.</param>
	/// <param name="response">The response.</param>
	public void Write(string data, HttpResponse response) => response.WriteAsync(data).Wait();

	/// <summary>
	/// Writes the specified data.
	/// </summary>
	/// <param name="data">The data.</param>
	/// <param name="response">The response.</param>
	public void Write(byte[] data, HttpResponse response) => response.Body.Write(data, 0, data.Length);

	/// <summary>
	/// Writes the specified data asynchronously.
	/// </summary>
	/// <param name="data">The data.</param>
	/// <param name="response">The response.</param>
	public Task WriteAsync(string data, HttpResponse response) => response.WriteAsync(data);

	/// <summary>
	/// Writes the specified data asynchronously.
	/// </summary>
	/// <param name="data">The data.</param>
	/// <param name="response">The response.</param>
	public Task WriteAsync(byte[] data, HttpResponse response) => response.Body.WriteAsync(data, 0, data.Length);
}