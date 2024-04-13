using Simplify.DI;
using Simplify.Web.Core;
using Simplify.Web.Core2.Controllers.Response.Injectors;

namespace Simplify.Web.Core2.Controllers.Response;


/// <summary>
/// Provides controller response builder.
/// </summary>
public class ControllerResponseAssembler(IDIResolver resolver) : ActionModulesAccessorInjector(resolver), IControllerResponseAssembler
{
	private readonly IDIResolver _resolver = resolver;

	/// <summary>
	/// Builds the controller response properties.
	/// </summary>
	/// <param name="controllerResponse">The controller response.</param>
	public void InjectControllerResponseProperties(ControllerResponse controllerResponse)
	{
		InjectActionModulesAccessorProperties(controllerResponse);

		controllerResponse.ResponseWriter = _resolver.Resolve<IResponseWriter>();
	}
}