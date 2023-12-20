namespace Simplify.Web.Responses;

/// <summary>
/// Provides Created controller string response with 201 HTTP Status Code
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="Created" /> class.
/// </remarks>
/// <param name="content">The string content.</param>
/// <param name="contentType">Type of the content.</param>
public class Created(string content, string contentType = "text/plain") : Content(content, 201, contentType)
{
}