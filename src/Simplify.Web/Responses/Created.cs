namespace Simplify.Web.Responses;

/// <summary>
/// Provides the controller string response with 201 HTTP Status Code.
/// </summary>
/// <seealso cref="Content" />
/// <remarks>
/// Initializes a new instance of the <see cref="Created" /> class.
/// </remarks>
/// <param name="content">The string content.</param>
/// <param name="contentType">Type of the content.</param>
public class Created(string content, string contentType = "text/plain") : Content(content, 201, contentType)
{
}