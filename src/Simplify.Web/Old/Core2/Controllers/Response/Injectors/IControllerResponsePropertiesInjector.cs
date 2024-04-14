namespace Simplify.Web.Old.Core2.Controllers.Response.Injectors;

/// <summary>
/// Represents controller response builder.
/// </summary>
public interface IControllerResponsePropertiesInjector
{
	/// <summary>
	/// Builds the controller response properties.
	/// </summary>
	/// <param name="controllerResponse">The controller response.</param>
	void Inject(ControllerResponse controllerResponse);
}