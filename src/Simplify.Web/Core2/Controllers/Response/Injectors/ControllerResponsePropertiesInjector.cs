using Simplify.DI;
using Simplify.Web.Core;
using Simplify.Web.Core2.Controllers.Response.Injectors;

namespace Simplify.Web.Core2.Controllers.Response.Injectors;


/// <summary>
/// Provides controller response builder.
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