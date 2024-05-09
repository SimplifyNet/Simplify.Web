using System.Threading.Tasks;
using Simplify.DI;
using Simplify.Web.Controllers.Response.Injection;

namespace Simplify.Web.Controllers.Response;

public class ControllerResponseExecutor(IDIResolver resolver)
	: ControllerResponsePropertiesInjector(resolver), IControllerResponseExecutor
{
	public async Task<ResponseBehavior> ExecuteAsync(ControllerResponse response)
	{
		InjectControllerResponseProperties(response);

		return await response.ExecuteAsync();
	}
}