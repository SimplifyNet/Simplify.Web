using System.Threading.Tasks;
using Simplify.Web.Old.Core;

namespace Simplify.Web.Old;

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