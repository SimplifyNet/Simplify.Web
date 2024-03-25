using Moq;

namespace Simplify.Web.Tests.Core.Controllers.Execution.Controller2ExecutorTestTypes;

public class AllParamsController : Controller2
{
	public virtual void Invoke(
		string stringParam,
		int intParam,
		decimal decimalParam,
		bool boolParam,
		string[] stringArrayParam,
		int[] intArrayParam,
		decimal[] decimalArrayParam,
		bool[] boolArrayParam)
	{
		StringParam = stringParam;
		IntParam = intParam;
		DecimalParam = decimalParam;
		BoolParam = boolParam;
		StringArrayParam = stringArrayParam;
		IntArrayParam = intArrayParam;
		DecimalArrayParam = decimalArrayParam;
		BoolArrayParam = boolArrayParam;
	}

	public string? StringParam { get; private set; }
	public int IntParam { get; private set; }
	public decimal DecimalParam { get; private set; }
	public bool BoolParam { get; private set; }
	public string[]? StringArrayParam { get; private set; }
	public int[]? IntArrayParam { get; private set; }
	public decimal[]? DecimalArrayParam { get; private set; }
	public bool[]? BoolArrayParam { get; private set; }
}
