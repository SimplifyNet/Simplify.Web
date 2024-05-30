using Simplify.Web.Controllers.Meta;

namespace Simplify.Web.Controllers.Execution.Resolver;

/// <summary>
/// Represents a controller executor resolver
/// </summary>
public interface IControllerExecutorResolver
{
	/// <summary>
	/// Resolves the specified controller executor.
	/// </summary>
	/// <param name="controller">The controller type.</param>
	IControllerExecutor Resolve(IControllerMetadata controller);
}