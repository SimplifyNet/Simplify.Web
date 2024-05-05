using Simplify.Web.Controllers.Meta;

namespace Simplify.Web.Controllers.Execution;

public interface IControllerExecutorResolver
{
	IControllerExecutor Resolve(IControllerMetadata controller);
}