namespace Simplify.Web.Old;

/// <summary>
/// Synchronous controllers base class.
/// </summary>
public abstract class SyncControllerBase : ControllerBase
{
	/// <summary>
	/// Invokes the controller.
	/// </summary>
	public abstract ControllerResponse? Invoke();
}