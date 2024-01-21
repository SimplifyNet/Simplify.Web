using System.Threading.Tasks;

namespace Simplify.Web;

/// <summary>
/// Asynchronous controllers base class.
/// </summary>
public abstract class AsyncControllerBase : ControllerBase
{
	/// <summary>
	/// Invokes the controller.
	/// </summary>
	public abstract Task<ControllerResponse?> Invoke();
}