using System;
using System.Collections.Generic;
using System.Linq;
using Simplify.Web.Controllers.Meta.Factory;
using Simplify.Web.Controllers.V1.Metadata;
using Simplify.Web.System;

namespace Simplify.Web.Controllers.Meta.Loader;

public class MetadataLoader(IControllerMetadataFactoryResolver resolver, IEnumerable<Type> controllersTypes) : IMetadataLoader
{
	private static readonly IList<IControllerMetadataFactory> DefaultControllersFactories =
	[
		new Controller1MetadataFactory()
	];

	private static readonly IEnumerable<Type> DefaultControllersTypes = Controller1Types.Types;

	private static IMetadataLoader? _loader;

	/// <summary>
	/// Gets the current loader
	/// </summary>
	public static IMetadataLoader Current
	{
		get => _loader ??= new MetadataLoader(new ControllerMetadataFactoryResolver(DefaultControllersFactories), DefaultControllersTypes);
		set => _loader = value ?? throw new ArgumentNullException(nameof(value));
	}

	public IReadOnlyList<IControllerMetadata> Load() => LoadMetadata(SimplifyWebTypesFinder.FindTypesDerivedFrom(controllersTypes), SimplifyWebTypesFinder.GetControllerTypesToIgnore());

	private IReadOnlyList<IControllerMetadata> LoadMetadata(IEnumerable<Type> types, IEnumerable<Type> typesToIgnore) =>
			types.Where(t => typesToIgnore.All(x => x.FullName != t.FullName))
			.Select(t => resolver.Resolve(t).Create(t))
			.ToList();
}