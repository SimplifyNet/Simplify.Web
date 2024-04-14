using System;

namespace Simplify.Web.Old.Attributes.Setup;

/// <summary>
/// Specify controllers or views types which should be ignored from DI container registration.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="IgnoreTypesRegistrationAttribute"/> class.
/// </remarks>
/// <param name="types">Controllers or views types which should be ignored from DI container registration</param>
[AttributeUsage(AttributeTargets.Class)]
public class IgnoreTypesRegistrationAttribute(params Type[] types) : Attribute
{

	/// <summary>
	/// Gets the types of controllers.
	/// </summary>
	/// <value>
	/// The types of controllers.
	/// </value>
	public Type[] Types { get; } = types;
}