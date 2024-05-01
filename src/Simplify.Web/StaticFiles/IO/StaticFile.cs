using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Simplify.System.Extensions;

namespace Simplify.Web.StaticFiles.IO;

/// <summary>
/// Provides static file.
/// </summary>
/// <seealso cref="IStaticFile" />
/// <remarks>
/// Initializes a new instance of the <see cref="StaticFileHandler"/> class.
/// </remarks>
/// <param name="staticFilesPaths">The static files paths.</param>
/// <param name="sitePhysicalPath">The site physical path.</param>
public class StaticFile(IList<string> staticFilesPaths, string sitePhysicalPath) : IStaticFile
{
	private readonly IList<string> _staticFilesPaths = staticFilesPaths;
	private readonly string _sitePhysicalPath = sitePhysicalPath;

	/// <summary>
	/// Determines whether the relative file path is a static file route path.
	/// </summary>
	/// <param name="relativeFilePath">The relative file path.</param>
	public bool IsValidPath(string relativeFilePath) =>
		_staticFilesPaths
			.Where(relativeFilePath.ToLower().StartsWith)
			.Any(_ => File.Exists(_sitePhysicalPath + relativeFilePath));

	public DateTime GetLastModificationTime(string relativeFilePath) => File.GetLastWriteTimeUtc(relativeFilePath).TrimMilliseconds();

	/// <summary>
	/// Gets the file data.
	/// </summary>
	/// <param name="relativeFilePath">The relative file path.</param>
	public async Task<byte[]> GetDataAsync(string relativeFilePath)
	{
		using var stream = File.Open(relativeFilePath, FileMode.Open, FileAccess.Read, FileShare.Read);

		var result = new byte[stream.Length];

#if NETSTANDARD2_0
		await stream.ReadAsync(result, 0, (int)stream.Length);
#else
		await stream.ReadAsync(result.AsMemory(0, (int)stream.Length));
#endif

		return result;
	}
}