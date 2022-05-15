using Simplify.Web;

namespace SampleApp.Classic.Views;

public class MessageBoxView : View
{
	public string Get(string message) => TemplateFactory.Load("MessageBox").Set("Message", message).Get();
}