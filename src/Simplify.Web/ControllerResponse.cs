using System.Threading.Tasks;
using Simplify.Web.Http.ResponseWriting;

namespace Simplify.Web;

/// <summary>
/// Provides the controllers responses base class.
/// </summary>
/// <seealso cref="ActionModulesAccessor" />
public abstract class ControllerResponse : ActionModulesAccessor
{
	/// <summary>
	/// Gets the HTTP response writer.
	/// </summary>
	/// <value>
	/// The response writer.
	/// </value>
	public virtual IResponseWriter ResponseWriter { get; internal set; } = null!;

	/// <summary>
	/// Executes this response
	/// </summary>
	public abstract Task<ResponseBehavior> ExecuteAsync();
}