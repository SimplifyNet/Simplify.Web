using System.Collections.Generic;

namespace Simplify.Web.Meta.Controllers;

public interface IMetadataLoader
{
	IReadOnlyCollection<IControllerMetadata> Load();
}
