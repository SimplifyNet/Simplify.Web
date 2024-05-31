using System;

namespace Simplify.Web.Controllers.V2.Metadata;

/// <summary>
/// Provides the controller v2 types.
/// </summary>
public static class Controller2Types
{
	/// <summary>
	/// Gets the types.
	/// </summary>
	/// <value>
	/// The types.
	/// </value>
	public static Type[] Types =>
	[
		typeof(Controller2),
		typeof(Controller2<>)
	];
}