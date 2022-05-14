namespace Simplify.Web.Routing;

/// <summary>
/// Provides path items base class
/// </summary>
public abstract class PathItem
{
	/// <summary>
	/// Initializes a new instance of the <see cref="PathItem"/> class.
	/// </summary>
	/// <param name="name">The name of path item.</param>
	protected PathItem(string name)
	{
		Name = name;
	}

	/// <summary>
	/// Gets the name of path item.
	/// </summary>
	/// <value>
	/// The name of path item.
	/// </value>
	public virtual string Name { get; }
}