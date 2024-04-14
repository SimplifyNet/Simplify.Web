using System.Threading.Tasks;
using Simplify.Web.Core;

namespace Simplify.Web;

/// <summary>
/// Provides controllers responses base class.
/// </summary>
public abstract class ControllerResponse : ActionModulesAccessor
{
	/// <summary>
	/// Gets the response writer.
	/// </summary>
	/// <value>
	/// The response writer.
	/// </value>
	public virtual IResponseWriter ResponseWriter { get; internal set; } = null!;

	/// <summary>
	/// Processes this response
	/// </summary>
	public abstract Task<ControllerResponseResult> ExecuteAsync();
}