namespace Simplify.Web.Responses;

/// <summary>
/// Provides Creted controller string response with 201 HTTP Status Code
/// </summary>
public class Created : Content
{
	/// <summary>
	/// Initializes a new instance of the <see cref="Created" /> class.
	/// </summary>
	/// <param name="content">The string content.</param>
	/// <param name="contentType">Type of the content.</param>
	public Created(string content, string contentType = "text/plain") : base(content, 201, contentType)
	{
	}
}