using System;
using System.Threading.Tasks;

namespace Simplify.Web.Controllers.V2.Metadata;

/// <summary>
/// Provides the controller v2 valid return types.
/// </summary>
public static class Controller2ValidReturnTypes
{
	/// <summary>
	/// Gets the types.
	/// </summary>
	/// <value>
	/// The types.
	/// </value>
	public static Type[] Types =>
	[
		typeof(void),
		typeof(ControllerResponse),
		typeof(Task),
		typeof(Task<ControllerResponse>)
	];
}