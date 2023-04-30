using System.Threading.Tasks;
using Microsoft.Owin;

namespace Simplify.Web.Core;

/// <summary>
/// Represent response writer
/// </summary>
public interface IResponseWriter
{
	/// <summary>
	/// Writes the specified data.
	/// </summary>
	/// <param name="data">The data.</param>
	/// <param name="response">The response.</param>
	/// <returns></returns>
	void Write(string data, IOwinResponse response);

	/// <summary>
	/// Writes the specified data.
	/// </summary>
	/// <param name="data">The data.</param>
	/// <param name="response">The response.</param>
	/// <returns></returns>
	void Write(byte[] data, IOwinResponse response);

	/// <summary>
	/// Writes the specified data asynchronously.
	/// </summary>
	/// <param name="data">The data.</param>
	/// <param name="response">The response.</param>
	/// <returns></returns>
	Task WriteAsync(string data, IOwinResponse response);

	/// <summary>
	/// Writes the specified data asynchronously.
	/// </summary>
	/// <param name="data">The data.</param>
	/// <param name="response">The response.</param>
	/// <returns></returns>
	Task WriteAsync(byte[] data, IOwinResponse response);
}