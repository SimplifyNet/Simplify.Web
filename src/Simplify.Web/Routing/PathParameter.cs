using System;

namespace Simplify.Web.Routing;

/// <summary>
/// Provides path parameter element
/// </summary>
public class PathParameter : PathItem
{
	/// <summary>
	/// Initializes a new instance of the <see cref="PathParameter"/> class.
	/// </summary>
	/// <param name="name">The name of path parameter.</param>
	/// <param name="type">The type of path parameter.</param>
	public PathParameter(string name, Type type) : base(name)
	{
		Type = type;
	}

	/// <summary>
	/// Gets the type of path parameter.
	/// </summary>
	/// <value>
	/// The type of path parameter.
	/// </value>
	public Type Type { get; private set; }
}