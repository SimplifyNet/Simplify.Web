using System.Threading.Tasks;

namespace Simplify.Web.Controllers.Response;

public interface IControllerResponseExecutor
{
	Task<ResponseBehavior> ExecuteAsync(ControllerResponse response);
}