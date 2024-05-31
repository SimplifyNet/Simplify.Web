using System.Collections.Generic;

namespace Simplify.Web.Controllers.Meta.Loader;

/// <summary>
/// Represents a metadata loader.
/// </summary>
public interface IMetadataLoader
{
	/// <summary>
	/// Loads the controller metadata list.
	/// </summary>
	IReadOnlyList<IControllerMetadata> Load();
}