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
	/// <param name="data">The data.</param>
	/// <param name="response">The response.</param>
	/// <returns></returns>
	Task WriteAsync(string data, HttpResponse response);

	/// <summary>
	/// Writes the specified data asynchronously.
	/// </summary>
	/// <param name="data">The data.</param>
	/// <param name="response">The response.</param>
	/// <returns></returns>
	Task WriteAsync(byte[] data, HttpResponse response);
}