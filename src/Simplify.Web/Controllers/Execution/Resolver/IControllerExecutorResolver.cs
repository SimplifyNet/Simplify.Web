using Simplify.Web.Controllers.Meta;

namespace Simplify.Web.Controllers.Execution.Resolver;

public interface IControllerExecutorResolver
{
	IControllerExecutor Resolve(IControllerMetadata controller);
}