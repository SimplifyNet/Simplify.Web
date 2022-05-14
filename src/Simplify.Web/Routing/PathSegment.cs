namespace Simplify.Web.Routing;

/// <summary>
/// Provides path segment element
/// </summary>
public class PathSegment : PathItem
{
	/// <summary>
	/// Initializes a new instance of the <see cref="PathSegment"/> class.
	/// </summary>
	/// <param name="name">The segment name.</param>
	public PathSegment(string name) : base(name)
	{
	}
}