using System.Collections.Generic;

namespace Simplify.Web.Old.Routing;

/// <summary>
/// Provides parsed controller path.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="ControllerPath"/> class.
/// </remarks>
/// <param name="items">The items.</param>
public class ControllerPath(IList<PathItem> items) : IControllerPath
{

	/// <summary>
	/// Gets the controller path items.
	/// </summary>
	/// <value>
	/// The controller path items.
	/// </value>
	public IList<PathItem> Items { get; } = items;
}