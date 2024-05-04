namespace Simplify.Web.Controllers.Response.Injection;

/// <summary>
/// Represents a controller response builder.
/// </summary>
public interface IControllerResponsePropertiesInjector
{
	/// <summary>
	/// Builds the controller response properties.
	/// </summary>
	/// <param name="controllerResponse">The controller response.</param>
	void Inject(ControllerResponse controllerResponse);
}