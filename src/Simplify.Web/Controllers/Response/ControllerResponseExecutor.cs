using System.Threading.Tasks;
using Simplify.Web.Controllers.Response.Injection;

namespace Simplify.Web.Controllers.Response;

public class ControllerResponseExecutor(IControllerResponsePropertiesInjector controllerResponsePropertiesInjector) : IControllerResponseExecutor
{
	public async Task<ResponseBehavior> ExecuteAsync(ControllerResponse response)
	{
		controllerResponsePropertiesInjector.Inject(response);

		return await response.ExecuteAsync();
	}
}