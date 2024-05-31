using System.Threading.Tasks;
using Simplify.DI;

namespace Simplify.Web.Controllers.Response;

/// <summary>
/// Provides the controller response executor
/// </summary>
/// <seealso cref="ControllerResponsePropertiesInjector" />
/// <seealso cref="IControllerResponseExecutor" />
public class ControllerResponseExecutor(IDIResolver resolver)
	: ControllerResponsePropertiesInjector(resolver), IControllerResponseExecutor
{
	/// <summary>
	/// Executes this controller response executor asynchronously.
	/// </summary>
	/// <param name="response">The response.</param>
	public async Task<ResponseBehavior> ExecuteAsync(ControllerResponse response)
	{
		InjectControllerResponseProperties(response);

		return await response.ExecuteAsync();
	}
}