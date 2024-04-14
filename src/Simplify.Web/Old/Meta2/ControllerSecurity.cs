using System.Collections.Generic;

namespace Simplify.Web.Old.Meta2;

/// <summary>
/// Provides controller security information.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="ControllerSecurity" /> class.
/// </remarks>
/// <param name="isAuthorizationRequired">if set to <c>true</c> then indicates whether controller requires user authorization.</param>
/// <param name="requiredUserRoles">The required user roles.</param>
public class ControllerSecurity(bool isAuthorizationRequired = false, IEnumerable<string>? requiredUserRoles = null)
{

	/// <summary>
	/// Gets a value indicating whether controller requires user authorization.
	/// </summary>
	/// <value>
	/// <c>true</c> if controller requires authorization; otherwise, <c>false</c>.
	/// </value>
	public bool IsAuthorizationRequired { get; } = isAuthorizationRequired;

	/// <summary>
	/// Gets the required user roles.
	/// </summary>
	/// <value>
	/// The required user roles.
	/// </value>
	public IEnumerable<string>? RequiredUserRoles { get; } = requiredUserRoles;
}