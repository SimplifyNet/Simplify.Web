namespace Simplify.Web.Controllers.V1.Matcher;

/// <summary>
/// Provides the path items base class.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="PathItem"/> class.
/// </remarks>
/// <param name="name">The name of the path item.</param>
public abstract class PathItem(string name)
{
	/// <summary>
	/// Gets the name of the path item.
	/// </summary>
	public virtual string Name { get; } = name;
}