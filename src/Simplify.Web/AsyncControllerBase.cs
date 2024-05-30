using System.Threading.Tasks;

namespace Simplify.Web;

/// <summary>
/// Provides the asynchronous controllers base class.
/// </summary>
/// <seealso cref="ControllerBase" />
public abstract class AsyncControllerBase : ControllerBase
{
	/// <summary>
	/// Invokes the controller.
	/// </summary>
	public abstract Task<ControllerResponse?> Invoke();
}