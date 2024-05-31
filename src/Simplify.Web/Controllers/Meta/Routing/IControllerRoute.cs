using System.Collections.Generic;

namespace Simplify.Web.Controllers.Meta.Routing;

/// <summary>
/// Represents a controller route.
/// </summary>
public interface IControllerRoute
{
	/// <summary>
	/// Gets the path.
	/// </summary>
	/// <value>
	/// The path.
	/// </value>
	string Path { get; }

	/// <summary>
	/// Gets the controller path items.
	/// </summary>
	/// <value>
	/// The items.
	/// </value>
	IList<PathItem> Items { get; }
}