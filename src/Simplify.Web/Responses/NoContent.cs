namespace Simplify.Web.Responses;

/// <summary>
/// Provides empty controller response with 204 status code
/// </summary>
public class NoContent : StatusCode
{
	/// <summary>
	/// Initializes a new instance of the <see cref="NoContent"/> class.
	/// </summary>
	public NoContent() : base(204)
	{
	}
}