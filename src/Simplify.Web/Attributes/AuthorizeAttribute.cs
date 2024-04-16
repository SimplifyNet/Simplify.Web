using System;
using System.Collections.Generic;
using Simplify.Web.Utils.Strings;

namespace Simplify.Web.Attributes;

/// <summary>
/// Indicates whether the controller requires user authorization.
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class AuthorizeAttribute : Attribute
{
	/// <summary>
	/// Initializes a new instance of the <see cref="AuthorizeAttribute"/> class.
	/// </summary>
	/// <param name="requiredUserRoles">Required user roles.</param>
	public AuthorizeAttribute(string? requiredUserRoles = null) =>
		RequiredUserRoles = requiredUserRoles != null
			? requiredUserRoles.ParseCommaSeparatedList()
			: [];

	/// <summary>
	/// Initializes a new instance of the <see cref="AuthorizeAttribute"/> class.
	/// </summary>
	/// <param name="requiredUserRoles">The required user roles.</param>
	public AuthorizeAttribute(params string[] requiredUserRoles) => RequiredUserRoles = requiredUserRoles;

	/// <summary>
	/// Gets the required user roles.
	/// </summary>
	public IEnumerable<string> RequiredUserRoles { get; }
}