using Microsoft.Extensions.Configuration;

namespace Simplify.Web.Settings;

public static class ConfigurationExtensions
{
	public static string TrySetNotNullOrEmptyString(this IConfiguration config, string key, string defaultValue)
	{
		var value = config[key];

		return !string.IsNullOrEmpty(value)
			? value!
			: defaultValue;
	}
}