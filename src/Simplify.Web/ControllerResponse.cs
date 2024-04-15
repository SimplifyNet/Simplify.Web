using System.Threading.Tasks;
using Simplify.Web.Http.ResponseWriting;

namespace Simplify.Web;

/// <summary>
/// Provides the controllers responses base class.
/// </summary>
public abstract class ControllerResponse : ActionModulesAccessor
{
	/// <summary>
	/// Gets the HTTP response writer.
	/// </summary>
	public virtual IResponseWriter ResponseWriter { get; internal set; } = null!;

	/// <summary>
	/// Executes this response
	/// </summary>
	public abstract Task<ControllerResponseResult> ExecuteAsync();
}