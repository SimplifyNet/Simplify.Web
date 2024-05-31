using System.Collections.Generic;
using Simplify.Web.Controllers.Meta.Routing;

namespace Simplify.Web.Controllers.V1.Routing;

/// <summary>
/// Provides the controller v1 route.
/// </summary>
/// <seealso cref="IControllerRoute" />
public class Controller1Route : IControllerRoute
{
	/// <summary>
	/// Initializes a new instance of the <see cref="Controller1Route"/> class.
	/// </summary>
	/// <param name="path">The path.</param>
	public Controller1Route(string? path)
	{
		Path = path ?? "";
		Items = Controller1PathParser.Parse(Path);
	}

	/// <summary>
	/// Gets the path.
	/// </summary>
	/// <value>
	/// The path.
	/// </value>
	public string Path { get; }

	/// <summary>
	/// Gets the controller path items.
	/// </summary>
	public IList<PathItem> Items { get; }
}