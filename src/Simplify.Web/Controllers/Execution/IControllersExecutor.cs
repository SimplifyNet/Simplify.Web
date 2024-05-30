using System.Collections.Generic;
using System.Threading.Tasks;

namespace Simplify.Web.Controllers.Execution;

/// <summary>
/// Represents a controllers executor
/// </summary>
public interface IControllersExecutor
{
	/// <summary>
	/// Executes the controllers asynchronously.
	/// </summary>
	/// <param name="controllers">The controllers.</param>
	public Task<ResponseBehavior> ExecuteAsync(IReadOnlyList<IMatchedController> controllers);
}