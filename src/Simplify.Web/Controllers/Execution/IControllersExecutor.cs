using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Simplify.Web.Controllers.Execution;

public interface IControllersExecutor
{
	public Task<ResponseBehavior> ExecuteAsync(IReadOnlyList<IMatchedController> Controllers, HttpContext context);
}