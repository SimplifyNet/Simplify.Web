namespace Simplify.Web.Responses;

/// <summary>
/// Represents the HTTP <c>Content-Disposition</c> type used by the <see cref="File" /> response.
/// </summary>
public enum ContentDispositionType
{
	/// <summary>
	/// The file should be downloaded as an attachment (<c>attachment; filename="..."</c>).
	/// </summary>
	Attachment,

	/// <summary>
	/// The file should be displayed inline by the client (<c>inline; filename="..."</c>).
	/// </summary>
	Inline
}
