﻿namespace Simplify.Web.Old.Routing;

/// <summary>
/// Provides path items base class.
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
	/// <value>
	/// The name of path item.
	/// </value>
	public virtual string Name { get; } = name;
}