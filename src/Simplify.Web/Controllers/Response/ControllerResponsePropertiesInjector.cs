using Simplify.DI;
using Simplify.Web.Http.ResponseWriting;
using Simplify.Web.PropertiesInjection;

namespace Simplify.Web.Controllers.Response.Injection;

/// <summary>
/// Provides the controller response properties injector.
/// </summary>
public class ControllerResponsePropertiesInjector(IDIResolver resolver) : ActionModulesAccessorInjector(resolver)
{
	private readonly IDIResolver _resolver = resolver;

	/// <summary>
	/// Injects the controller response properties.
	/// </summary>
	/// <param name="controllerResponse">The controller response.</param>
	public void InjectControllerResponseProperties(ControllerResponse controllerResponse)
	{
		InjectActionModulesAccessorProperties(controllerResponse);

		controllerResponse.ResponseWriter = _resolver.Resolve<IResponseWriter>();
	}
}