using System.Threading.Tasks;
using Microsoft.Owin;

namespace Simplify.Web.Core;

/// <summary>
/// Providers response writer
/// </summary>
public class ResponseWriter : IResponseWriter
{
	/// <summary>
	/// Writes the specified data.
	/// </summary>
	/// <param name="data">The data.</param>
	/// <param name="response">The response.</param>
	/// <returns></returns>
	public void Write(string data, IOwinResponse response)
	{
		response.Write(data);
	}

	/// <summary>
	/// Writes the specified data.
	/// </summary>
	/// <param name="data">The data.</param>
	/// <param name="response">The response.</param>
	/// <returns></returns>
	public void Write(byte[] data, IOwinResponse response)
	{
		response.Write(data);
	}

	/// <summary>
	/// Writes the specified data asynchronously.
	/// </summary>
	/// <param name="data">The data.</param>
	/// <param name="response">The response.</param>
	/// <returns></returns>
	public Task WriteAsync(string data, IOwinResponse response)
	{
		return response.WriteAsync(data);
	}

	/// <summary>
	/// Writes the specified data asynchronously.
	/// </summary>
	/// <param name="data">The data.</param>
	/// <param name="response">The response.</param>
	/// <returns></returns>
	public Task WriteAsync(byte[] data, IOwinResponse response)
	{
		return response.WriteAsync(data);
	}
}