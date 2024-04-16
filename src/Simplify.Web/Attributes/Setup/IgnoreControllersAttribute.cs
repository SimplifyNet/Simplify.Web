using System;

namespace Simplify.Web.Attributes.Setup;

/// <summary>
/// Specifies the controllers types which should be ignored by Simplify.Web.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="IgnoreControllersAttribute"/> class.
/// </remarks>
/// <param name="types">The controller types which should be ignored by Simplify.Web</param>
[AttributeUsage(AttributeTargets.Class)]
public class IgnoreControllersAttribute(params Type[] types) : Attribute
{
	/// <summary>
	/// Gets the types of controllers.
	/// </summary>
	public Type[] Types { get; } = types;
}