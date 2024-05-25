using System;

namespace Simplify.Web.Controllers.Meta.Routing;

/// <summary>
/// Provides the path parameter element.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="PathParameter"/> class.
/// </remarks>
/// <param name="name">The name of the path parameter.</param>
/// <param name="type">The type of the path parameter.</param>
public class PathParameter(string name, Type type) : PathItem(name)
{
	/// <summary>
	/// Gets the type of the path parameter.
	/// </summary>
	public Type Type { get; } = type;
}