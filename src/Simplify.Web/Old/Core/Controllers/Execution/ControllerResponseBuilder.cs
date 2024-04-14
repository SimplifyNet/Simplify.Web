using Simplify.DI;
using Simplify.Web.Old.Core.AccessorsBuilding;

namespace Simplify.Web.Old.Core.Controllers.Execution;

/// <summary>
/// Provides controller response builder.
/// </summary>
public class ControllerResponseBuilder : ActionModulesAccessorBuilder, IControllerResponseBuilder
{
	/// <summary>
	/// Builds the controller response properties.
	/// </summary>
	/// <param name="controllerResponse">The controller response.</param>
	/// <param name="resolver">The DI container resolver.</param>
	public void BuildControllerResponseProperties(ControllerResponse controllerResponse, IDIResolver resolver)
	{
		BuildActionModulesAccessorProperties(controllerResponse, resolver);

		controllerResponse.ResponseWriter = resolver.Resolve<IResponseWriter>();
	}
}