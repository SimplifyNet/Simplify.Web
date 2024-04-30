using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Simplify.Web.Http.ResponseWriting;

/// <summary>
/// Represents HTTP response writer.
/// </summary>
public interface IResponseWriter
{
	/// <summary>
	/// Writes the specified data asynchronously.
	/// </summary>
	/// <param name="response">The response.</param>
	/// <param name="data">The data.</param>
	Task WriteAsync(HttpResponse response, string data);

	/// <summary>
	/// Writes the specified data asynchronously.
	/// </summary>
	/// <param name="response">The response.</param>
	/// <param name="data">The data.</param>
	Task WriteAsync(HttpResponse response, byte[] data);
}