using Simplify.DI;
using Simplify.Web.Http.ResponseWriting;

namespace Simplify.Web.Controllers.Response.Injectors;

/// <summary>
/// Provides a controller response builder.
/// </summary>
public class ControllerResponsePropertiesInjector(IDIResolver resolver) : ActionModulesAccessorInjector(resolver), IControllerResponsePropertiesInjector
{
	private readonly IDIResolver _resolver = resolver;

	/// <summary>
	/// Builds the controller response properties.
	/// </summary>
	/// <param name="controllerResponse">The controller response.</param>
	public void Inject(ControllerResponse controllerResponse)
	{
		InjectActionModulesAccessorProperties(controllerResponse);

		controllerResponse.ResponseWriter = _resolver.Resolve<IResponseWriter>();
	}
}