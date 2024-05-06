namespace Simplify.Web.Controllers.Meta;

/// <summary>
/// Provides the controller specific roles.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="ControllerRole" /> class.
/// </remarks>
/// <param name="isForbiddenHandler">if set to <c>true</c> then indicates what controller handles HTTP 403 errors.</param>
/// <param name="isNotFoundHandler">if set to <c>true</c> then indicates what controller handles HTTP 404 errors.</param>
public class ControllerRole(bool isForbiddenHandler = false, bool isNotFoundHandler = false)
{
	/// <summary>
	/// Gets a value indicating whether controller is HTTP 403 error handler.
	/// </summary>
	public bool IsForbiddenHandler { get; } = isForbiddenHandler;

	/// <summary>
	/// Gets a value indicating whether controller is HTTP 404 error handler.
	/// </summary>
	public bool IsNotFoundHandler { get; } = isNotFoundHandler;
}