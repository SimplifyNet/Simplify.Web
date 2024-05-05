using System;
using System.Collections.Generic;
using System.Linq;
using Simplify.Web.Meta.Controllers.Factory;

namespace Simplify.Web.Meta.Controllers.Loader;

public class MetadataLoader(IControllerMetadataFactoryResolver resolver) : IMetadataLoader
{
	private static IMetadataLoader? _loader;

	/// <summary>
	/// Gets the current loader
	/// </summary>
	public static IMetadataLoader Current
	{
		get => _loader ??= new MetadataLoader(new ControllerMetadataFactoryResolver(new List<IControllerMetadataFactory>()));
		set => _loader = value ?? throw new ArgumentNullException(nameof(value));
	}

	public IReadOnlyCollection<IControllerMetadata> Load()
	{
		var types = SimplifyWebTypesFinder.FindTypesDerivedFrom<Controller2>();

		types = types.Concat(SimplifyWebTypesFinder.FindTypesDerivedFrom(typeof(Controller2<>))).ToList();
		types = types.Concat(SimplifyWebTypesFinder.FindTypesDerivedFrom<Controller>()).ToList();
		types = types.Concat(SimplifyWebTypesFinder.FindTypesDerivedFrom<AsyncController>()).ToList();
		types = types.Concat(SimplifyWebTypesFinder.FindTypesDerivedFrom(typeof(Controller<>))).ToList();
		types = types.Concat(SimplifyWebTypesFinder.FindTypesDerivedFrom(typeof(AsyncController<>))).ToList();

		return LoadMetadata(types, SimplifyWebTypesFinder.GetControllerTypesToIgnore());
	}

	private IReadOnlyList<IControllerMetadata> LoadMetadata(IEnumerable<Type> types, IEnumerable<Type> typesToIgnore) =>
		types.Where(t => typesToIgnore.All(x => x.FullName != t.FullName))
			.Select(t => resolver.Resolve(t).Create(t))
			.ToList();
}
