using System.Collections.Generic;

namespace Simplify.Web.Core2.Controllers.Routing.Matcher;

/// <summary>
/// Represent parsed controller path.
/// </summary>
public interface IControllerPath
{
	/// <summary>
	/// Gets the controller path items.
	/// </summary>
	/// <value>
	/// The controller path items.
	/// </value>
	IList<PathItem> Items { get; }
}