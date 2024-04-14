using System;

namespace Simplify.Web.Old.Routing;

/// <summary>
/// Provides path parameter element.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="PathParameter"/> class.
/// </remarks>
/// <param name="name">The name of path parameter.</param>
/// <param name="type">The type of path parameter.</param>
public class PathParameter(string name, Type type) : PathItem(name)
{

	/// <summary>
	/// Gets the type of path parameter.
	/// </summary>
	/// <value>
	/// The type of path parameter.
	/// </value>
	public Type Type { get; } = type;
}