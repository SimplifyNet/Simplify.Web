using Simplify.DI;

namespace Simplify.Web.Core.Controllers.Execution;

/// <summary>
/// Represent controller response builder
/// </summary>
public interface IControllerResponseBuilder
{
	/// <summary>
	/// Builds the controller response properties.
	/// </summary>
	/// <param name="controllerResponse">The controller response.</param>
	/// <param name="resolver">The DI container resolver.</param>
	void BuildControllerResponseProperties(ControllerResponse controllerResponse, IDIResolver resolver);
}