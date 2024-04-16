using System;

namespace Simplify.Web.Attributes.Setup;

/// <summary>
/// Specifies the controllers or views types which should be ignored from DI container registration.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="IgnoreTypesRegistrationAttribute"/> class.
/// </remarks>
/// <param name="types">The Controller or view types which should be ignored from DI container registration</param>
[AttributeUsage(AttributeTargets.Class)]
public class IgnoreTypesRegistrationAttribute(params Type[] types) : Attribute
{
	/// <summary>
	/// Gets the types of controllers.
	/// </summary>
	public Type[] Types { get; } = types;
}