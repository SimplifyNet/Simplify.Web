using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Simplify.Web.Core2.Http.Writers;

/// <summary>
/// Represent response writer.
/// </summary>
public interface IResponseWriter
{
    /// <summary>
    /// Writes the specified data.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="response">The response.</param>
    /// <returns></returns>
    void Write(string data, HttpResponse response);

    /// <summary>
    /// Writes the specified data.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="response">The response.</param>
    /// <returns></returns>
    void Write(byte[] data, HttpResponse response);

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