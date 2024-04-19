using System;
using System.Collections.Generic;
using System.Linq;
using Simplify.Web.Attributes.Setup;
using Simplify.Web.Meta.Controllers.Factory;

namespace Simplify.Web.Meta.Controllers.Loader;

public class MetadataLoader(IControllerMetaDataFactoryResolver resolver) : IMetadataLoader
{
	private static IMetadataLoader? _loader;

	/// <summary>
	/// Gets the current loader
	/// </summary>
	public static IMetadataLoader Current
	{
		get => _loader ??= new MetadataLoader(new ControllerMetaDataFactoryResolver(new List<IControllerMetaDataFactory>()));
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

		return LoadMetadata(types, GetTypesToIgnore());
	}

	private IReadOnlyList<IControllerMetadata> LoadMetadata(IEnumerable<Type> types, IEnumerable<Type> typesToIgnore) =>
		types.Where(t => typesToIgnore.All(x => x.FullName != t.FullName))
			.Select(t => resolver.Resolve(t).Create(t))
			.ToList();

	private static IEnumerable<Type> GetTypesToIgnore()
	{
		var ignoreContainingClass = SimplifyWebTypesFinder
									.GetAllTypes()
									.FirstOrDefault(t => t.IsDefined(typeof(IgnoreControllersAttribute), true));

		if (ignoreContainingClass == null)
			return new List<Type>();

		var attributes = ignoreContainingClass.GetCustomAttributes(typeof(IgnoreControllersAttribute), false);

		return ((IgnoreControllersAttribute)attributes[0]).Types;
	}
}
