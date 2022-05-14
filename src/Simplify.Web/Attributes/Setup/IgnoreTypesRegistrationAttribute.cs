using System;

namespace Simplify.Web.Attributes.Setup;

/// <summary>
/// Specify controllers or views types which should be ignored from DI container registration
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class IgnoreTypesRegistrationAttribute : Attribute
{
	/// <summary>
	/// Initializes a new instance of the <see cref="IgnoreTypesRegistrationAttribute"/> class.
	/// </summary>
	/// <param name="types">Controllers or views types which should be ignored from DI container registration</param>
	public IgnoreTypesRegistrationAttribute(params Type[] types) => Types = types;

	/// <summary>
	/// Gets the types of controllers.
	/// </summary>
	/// <value>
	/// The types of controllers.
	/// </value>
	public Type[] Types { get; }
}