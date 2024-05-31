using System;
using System.Collections.Generic;
using Simplify.Web.Controllers.Meta.Routing;

namespace Simplify.Web.Controllers.V2.Routing;

/// <summary>
/// Provides the controller v 2 route.
/// </summary>
/// <seealso cref="IControllerRoute" />
public class Controller2Route(string path, IDictionary<string, Type> invokeMethodParameters) : IControllerRoute
{
	/// <summary>
	/// Gets the path.
	/// </summary>
	/// <value>
	/// The path.
	/// </value>
	public string Path { get; } = path;

	/// <summary>
	/// Gets the controller path items.
	/// </summary>
	/// <value>
	/// The items.
	/// </value>
	public IList<PathItem> Items { get; } = Controller2PathParser.Parse(path, invokeMethodParameters);
}