using System.Threading.Tasks;

namespace Simplify.Web.Controllers.Response;

/// <summary>
/// Represents a controller response executor
/// </summary>
public interface IControllerResponseExecutor
{
	/// <summary>
	/// Executes this controller response executor asynchronously.
	/// </summary>
	/// <param name="response">The response.</param>
	Task<ResponseBehavior> ExecuteAsync(ControllerResponse response);
}