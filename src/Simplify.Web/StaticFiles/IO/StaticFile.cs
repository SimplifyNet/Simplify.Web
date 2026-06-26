using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Simplify.System.Extensions;

namespace Simplify.Web.StaticFiles.IO;

/// <summary>
/// Provides static file.
/// </summary>
/// <seealso cref="IStaticFile" />
/// <remarks>
/// Initializes a new instance of the <see cref="StaticFile"/> class.
/// </remarks>
/// <param name="staticFilesPaths">The static files paths.</param>
/// <param name="sitePhysicalPath">The site physical path.</param>
public class StaticFile(IReadOnlyList<string> staticFilesPaths, string sitePhysicalPath) : IStaticFile
{
	private readonly string _siteRootFullPath = NormalizeRoot(sitePhysicalPath);

	/// <summary>
	/// Determines whether the relative file path is a static file route path.
	/// </summary>
	/// <param name="relativeFilePath">The relative file path.</param>
	public bool IsValidPath(string relativeFilePath)
	{
		if (string.IsNullOrEmpty(relativeFilePath))
			return false;

		var relativeFilePathForCheck = relativeFilePath.ToLowerInvariant();

		if (!staticFilesPaths.Any(relativeFilePathForCheck.StartsWith))
			return false;

		return TryResolveSafePath(relativeFilePath, out var fullPath) && File.Exists(fullPath);
	}

	/// <summary>
	/// Gets the file last modification time.
	/// </summary>
	/// <param name="relativeFilePath">The relative file path.</param>
	public DateTime GetLastModificationTime(string relativeFilePath) => File.GetLastWriteTimeUtc(ResolveSafePath(relativeFilePath)).TrimMilliseconds();

	/// <summary>
	/// Gets the file data asynchronously.
	/// </summary>
	/// <param name="relativeFilePath">The relative file path.</param>
	public async Task<byte[]> GetDataAsync(string relativeFilePath)
	{
		var fullPath = ResolveSafePath(relativeFilePath);

#if NETSTANDARD2_0
		using var stream = File.Open(fullPath, FileMode.Open, FileAccess.Read, FileShare.Read);

		var result = new byte[stream.Length];

		await stream.ReadAsync(result, 0, (int)stream.Length);

#elif NET7_0_OR_GREATER
		await using var stream = File.Open(fullPath, FileMode.Open, FileAccess.Read, FileShare.Read);

		var result = new byte[stream.Length];

		await stream.ReadExactlyAsync(result);
#else
		await using var stream = File.Open(fullPath, FileMode.Open, FileAccess.Read, FileShare.Read);

		var result = new byte[stream.Length];
		var totalBytesRead = 0;

		while (totalBytesRead < result.Length)
			totalBytesRead += await stream.ReadAsync(result.AsMemory(totalBytesRead, result.Length - totalBytesRead));
#endif

		return result;
	}

	/// <summary>
	/// Gets the file data.
	/// </summary>
	/// <param name="relativeFilePath">The relative file path.</param>
	public byte[] GetData(string relativeFilePath) => File.ReadAllBytes(ResolveSafePath(relativeFilePath));

	private static string NormalizeRoot(string root)
	{
		var full = Path.GetFullPath(root);

		return full.EndsWith(Path.DirectorySeparatorChar.ToString())
			? full
			: full + Path.DirectorySeparatorChar;
	}

	private string ResolveSafePath(string relativeFilePath)
	{
		if (!TryResolveSafePath(relativeFilePath, out var fullPath))
			throw new UnauthorizedAccessException("Static file path resolves outside of the site root: " + relativeFilePath);

		return fullPath;
	}

	private bool TryResolveSafePath(string relativeFilePath, out string fullPath)
	{
		fullPath = string.Empty;

		if (string.IsNullOrEmpty(relativeFilePath))
			return false;

		// Reject path-traversal sequences and NUL bytes before any filesystem call.
		if (relativeFilePath.IndexOf("..", StringComparison.Ordinal) >= 0
			|| relativeFilePath.IndexOf('\0') >= 0)
			return false;

		string resolved;

		try
		{
			resolved = Path.GetFullPath(Path.Combine(_siteRootFullPath, relativeFilePath));
		}
		catch
		{
			return false;
		}

		// Use case-insensitive comparison on platforms with a case-insensitive filesystem
		// (Windows/macOS) so that "/Styles/..." cannot bypass containment by case alone.
#if NET6_0_OR_GREATER
		var caseSensitiveFs = OperatingSystem.IsLinux();
#else
		var caseSensitiveFs = Environment.OSVersion.Platform == PlatformID.Unix
			&& !RuntimeInformation.IsOSPlatform(OSPlatform.OSX);
#endif
		var comparison = caseSensitiveFs ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;

		if (!resolved.StartsWith(_siteRootFullPath, comparison))
			return false;

		fullPath = resolved;

		return true;
	}
}