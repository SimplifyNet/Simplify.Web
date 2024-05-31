using System;

namespace Simplify.Web.Controllers.V1.Metadata;

/// <summary>
/// Provides the controller v1 types.
/// </summary>
public static class Controller1Types
{
	/// <summary>
	/// Gets the types.
	/// </summary>
	/// <value>
	/// The types.
	/// </value>
	public static Type[] Types =>
	[
		typeof(Controller),
		typeof(AsyncController),
		typeof(Controller<>),
		typeof(AsyncController<>),
	];
}