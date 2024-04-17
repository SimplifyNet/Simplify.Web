namespace Simplify.Web.Controllers.RouteMatching.Matcher;

/// <summary>
/// Provides the path items base class.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="PathItem"/> class.
/// </remarks>
/// <param name="name">The name of path item.</param>
public abstract class PathItem(string name)
{
	/// <summary>
	/// Gets the name of path item.
	/// </summary>
	public virtual string Name { get; } = name;
}