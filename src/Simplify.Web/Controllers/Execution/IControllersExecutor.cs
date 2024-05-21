using System.Collections.Generic;
using System.Threading.Tasks;

namespace Simplify.Web.Controllers.Execution;

public interface IControllersExecutor
{
	public Task<ResponseBehavior> ExecuteAsync(IReadOnlyList<IMatchedController> controllers);
}