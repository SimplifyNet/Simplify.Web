#nullable disable

using System;
using Simplify.Templates;
using Simplify.Web.Modules;
using Simplify.Web.Responses;

namespace Simplify.Web;

/// <summary>
/// Controllers base class
/// </summary>
public abstract class ControllerBase : ResponseShortcutsControllerBase
{
	/// <summary>
	/// Gets the route parameters.
	/// </summary>
	/// <value>
	/// The route parameters.
	/// </value>
	public virtual dynamic RouteParameters { get; internal set; }
}