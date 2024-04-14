namespace Simplify.Web.Routing;

/// <summary>
/// Provides path segment element.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="PathSegment"/> class.
/// </remarks>
/// <param name="name">The segment name.</param>
public class PathSegment(string name) : PathItem(name)
{
}