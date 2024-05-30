namespace Simplify.Web;

/// <summary>
/// Provides the synchronous controllers base class.
/// </summary>
/// <seealso cref="ControllerBase" />
public abstract class SyncControllerBase : ControllerBase
{
	/// <summary>
	/// Invokes the controller.
	/// </summary>
	public abstract ControllerResponse? Invoke();
}