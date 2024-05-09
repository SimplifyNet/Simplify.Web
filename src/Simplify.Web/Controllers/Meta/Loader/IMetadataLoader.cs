using System.Collections.Generic;

namespace Simplify.Web.Controllers.Meta.Loader;

public interface IMetadataLoader
{
	IReadOnlyList<IControllerMetadata> Load();
}