using System.Collections.Generic;

namespace Simplify.Web.Controllers.RouteMatching.Matcher;

/// <summary>
/// Represent a parsed controller path.
/// </summary>
public interface IControllerPath
{
	/// <summary>
	/// Gets the controller path items.
	/// </summary>
	IList<PathItem> Items { get; }
}