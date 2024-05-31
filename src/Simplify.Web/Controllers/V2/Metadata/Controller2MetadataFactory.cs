using System;
using Simplify.Web.Controllers.Meta;
using Simplify.Web.Controllers.Meta.Factory;
using Simplify.Web.System;

namespace Simplify.Web.Controllers.V2.Metadata;

/// <summary>
/// Provides the controller v2 metadata factory.
/// </summary>
/// <seealso cref="IControllerMetadataFactory" />
public class Controller2MetadataFactory : IControllerMetadataFactory
{
	/// <summary>
	/// Determines whether this factory can create a metadata for specified type
	/// </summary>
	/// <param name="controllerType">Type of the controller.</param>
	public bool CanHandle(Type controllerType) => controllerType.IsDerivedFrom(Controller2Types.Types);

	/// <summary>
	/// Creates the controller metadata.
	/// </summary>
	/// <param name="controllerType">Type of the controller.</param>
	public IControllerMetadata Create(Type controllerType) => new Controller2Metadata(controllerType);
}