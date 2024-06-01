using Microsoft.Extensions.Configuration;

namespace Simplify.Web.Settings;

/// <summary>
/// Provides the configuration extensions.
/// </summary>
public static class ConfigurationExtensions
{
	/// <summary>
	/// Gets the value from configuration by the key, if value is null or empty then returns the default value.
	/// </summary>
	/// <param name="config">The configuration.</param>
	/// <param name="key">The key.</param>
	/// <param name="defaultValue">The default value.</param>
	public static string GetValueOrDefaultValue(this IConfiguration config, string key, string defaultValue)
	{
		var value = config[key];

		return !string.IsNullOrEmpty(value)
			? value!
			: defaultValue;
	}
}