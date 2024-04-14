using System;

namespace Simplify.Web.Old.Attributes.Setup;

/// <summary>
/// Specify controllers types which should be ignored by Simplify.Web.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="IgnoreControllersAttribute"/> class.
/// </remarks>
/// <param name="types">Controllers types which should be ignored by Simplify.Web</param>
[AttributeUsage(AttributeTargets.Class)]
public class IgnoreControllersAttribute(params Type[] types) : Attribute
{

	/// <summary>
	/// Gets the types of controllers.
	/// </summary>
	/// <value>
	/// The types of controllers.
	/// </value>
	public Type[] Types { get; } = types;
}