using System.Collections.Generic;

namespace Simplify.Web.Controllers.V1.Matcher;

/// <summary>
/// Provides the parsed controller path.
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
	public IList<PathItem> Items { get; } = items;
}