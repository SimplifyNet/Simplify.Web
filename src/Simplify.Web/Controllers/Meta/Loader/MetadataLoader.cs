using System;
using System.Collections.Generic;
using System.Linq;
using Simplify.Web.Controllers.Meta.Factory;
using Simplify.Web.Controllers.V1.Metadata;
using Simplify.Web.Controllers.V2.Metadata;
using Simplify.Web.System;

namespace Simplify.Web.Controllers.Meta.Loader;

public class MetadataLoader(IControllerMetadataFactoryResolver resolver) : IMetadataLoader
{
	private static IMetadataLoader? _loader;

	/// <summary>
	/// Gets the current loader
	/// </summary>
	public static IMetadataLoader Current
	{
		get => _loader ??= new MetadataLoader(new ControllerMetadataFactoryResolver(
		[
			new Controller1MetadataFactory()
		]));

		set => _loader = value ?? throw new ArgumentNullException(nameof(value));
	}

	public IReadOnlyCollection<IControllerMetadata> Load()
	{
		var types = SimplifyWebTypesFinder.FindTypesDerivedFrom(Controller2Types.Types);
		types = types.Concat(SimplifyWebTypesFinder.FindTypesDerivedFrom(Controller1Types.Types)).ToList();

		return LoadMetadata(types, SimplifyWebTypesFinder.GetControllerTypesToIgnore());
	}

	private IReadOnlyList<IControllerMetadata> LoadMetadata(IEnumerable<Type> types, IEnumerable<Type> typesToIgnore) =>
		types.Where(t => typesToIgnore.All(x => x.FullName != t.FullName))
			.Select(t => resolver.Resolve(t).Create(t))
			.ToList();
}