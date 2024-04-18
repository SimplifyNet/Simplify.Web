namespace Simplify.Web.Meta.Controllers;

/// <summary>
/// Provides the controller specific roles.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="ControllerRole" /> class.
/// </remarks>
/// <param name="is400Handler">if set to <c>true</c> then indicates what controller handles HTTP 400 errors.</param>
/// <param name="is403Handler">if set to <c>true</c> then indicates what controller handles HTTP 403 errors.</param>
/// <param name="is404Handler">if set to <c>true</c> then indicates what controller handles HTTP 404 errors.</param>
public class ControllerRole(bool is400Handler = false, bool is403Handler = false, bool is404Handler = false)
{
	/// <summary>
	/// Gets a value indicating whether controller is HTTP 400 error handler.
	/// </summary>
	public bool Is400Handler { get; } = is400Handler;

	/// <summary>
	/// Gets a value indicating whether controller is HTTP 403 error handler.
	/// </summary>
	public bool Is403Handler { get; } = is403Handler;

	/// <summary>
	/// Gets a value indicating whether controller is HTTP 404 error handler.
	/// </summary>
	public bool Is404Handler { get; } = is404Handler;
}