# Simplify.Web

![Simplify](https://raw.githubusercontent.com/SimplifyNet/Images/master/LogoWeb128x128.png)

[![Nuget Version](https://img.shields.io/nuget/v/Simplify.Web)](https://www.nuget.org/packages/Simplify.Web/)
[![Nuget Download](https://img.shields.io/nuget/dt/Simplify.Web)](https://www.nuget.org/packages/Simplify.Web/)
[![Issues Board](https://img.shields.io/badge/issues-Board-yellow)](https://github.com/users/i4004/projects/6/views/1)
[![Build Package](https://github.com/SimplifyNet/Simplify.Web/actions/workflows/build.yml/badge.svg)](https://github.com/SimplifyNet/Simplify.Web/actions/workflows/build.yml)
[![Libraries.io dependency status for latest release](https://img.shields.io/librariesio/release/nuget/Simplify.Web)](https://libraries.io/nuget/Simplify.Web)
[![CodeFactor Grade](https://img.shields.io/codefactor/grade/github/SimplifyNet/Simplify.Web)](https://www.codefactor.io/repository/github/simplifynet/simplify.web)
![Platform](https://img.shields.io/badge/platform-.NET%206.0%20%7C%20.NET%20Standard%202.1%20%7C%20.NET%20Standard%202.0-lightgrey)

Simplify.Web is an open-source, lightweight, fast, and highly customizable server-side .NET web framework based on ASP.NET Core for building HTTP-based web applications, RESTful APIs, etc.

The framework can be used as:

- An API backend framework
- A mix of API backend + SPA front end (e.g., Angular)
- A traditional backend-generated website

Can be hosted:

- The same way as an ASP.NET Core MVC application (on IIS or as a console application)
- Inside a Windows service

## Main features

- Comes as Microsoft.AspNetCore middleware
- Can be used as an API backend only with front-end frameworks
- Based on MVC and MVVM patterns
- Lightweight & fast
- Uses a switchable IoC container for itself and controllers, views constructor injection ([Simplify.DI](https://github.com/SimplifyNet/Simplify/wiki/Simplify.DI))
- Supports async controllers
- Supports controllers which can run on any request
- Localization-friendly (supports templates, strings, and data files localization by default)
- Uses a fast template engine ([Simplify.Templates](https://github.com/SimplifyNet/Simplify/wiki/Simplify.Templates))
- Mocking-friendly
- Mono-friendly

## Quick start

There is a [templates package](https://github.com/SimplifyNet/Simplify.Web.Templates) available at nuget.org for Simplify.Web. It contains a couple of templates which can be a good starting point for your application.

Install the templates package:

```console
dotnet new -i Simplify.Web.Templates
```

| Template                            | Short Name              |
| :---------------------------------- | :---------------------- |
| Angular template                    | sweb.angular            |
| API template                        | sweb.api                |
| Minimal template                    | sweb.minimal            |
| Windows service hosted API template | sweb.api.windowsservice |

Use the short name to create a project based on the selected template:

```console
dotnet new sweb.angular -n HelloWorldApplication
```

Then just run the project via F5 (it will download all required NuGet and npm packages at the first build).

## [Detailed documentation](https://github.com/SimplifyNet/Simplify.Web/wiki)

### API outgoing JSON controller v2 example

```csharp
[Get("api/v1/weatherTypes")]
public class SampleDataController : Controller2
{
    private static readonly string[] Summaries =
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    public ControllerResponse Invoke()
    {
        try
        {
            return Json(Summaries);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);

            return StatusCode(500);
        }
    }
}
```

### API ingoing JSON controller v2 example

```csharp
[Post("api/v1/sendMessage")]
public class SampleDataController : Controller2<SampleModel>
{
    public ControllerResponse Invoke()
    {
        try
        {
            Trace.WriteLine($"Object with message received: {Model.Message}");

            return NoContent();
        }
        catch (Exception e) when (e is ModelValidationException || e is Newtonsoft.Json.JsonException)
        {
            return StatusCode(400, e.Message);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);

            return StatusCode(500, "Site error!");
        }
    }
}

public class SampleModel
{
    [Required]
    public string Message { get; set; }
}
```

### Simple HTML generation controllers example

#### Static page controller v1 example

```csharp
// Controller will be executed only on HTTP GET request like http://mysite.com/about
[Get("about")]
public class AboutController : Controller
{
    public override ControllerResponse Invoke()
    {
        // About.tpl content will be inserted into {MainContent} in Master.tpl
        return StaticTpl("Static/About", StringTable.PageTitleAbout);
    }
}
```

#### Any page controller v1 with high run priority example

Runs on any request and adds a login panel to pages.

```csharp
// Controller will be executed on any request and will be launched before other controllers (because they have Priority = 0 by default)
[Priority(-1)]
public class LoginPanelController : AsyncController
{
    public override async Task<ControllerResponse> Invoke()
    {
        return Context.Context.Authentication.User == null
            // Data from GuestPanel.tpl will be inserted into {LoginPanel} in Master.tpl
            ? new InlineTpl("LoginPanel", await TemplateFactory.LoadAsync("Shared/LoginPanel/GuestPanel"))
            // Data from LoggedUserPanelView will be inserted into {LoginPanel} in Master.tpl
            : new InlineTpl("LoginPanel", await GetView<LoggedUserPanelView>().Get(Context.Context.Authentication.User.Identity.Name));
    }
}
```

#### View example

```csharp
public class LoggedUserPanelView : View
{
    public async Task<ITemplate> Get(string userName)
    {
        // Loading template from LoggedUserPanel.tpl asynchronously
        var tpl = await TemplateFactory.LoadAsync("Shared/LoginPanel/LoggedUserPanel");

        // Setting userName into {UserName} variable in LoggedUserPanel.tpl
        tpl.Add("UserName", userName);

        return tpl;
    }
}
```

## Example applications

Below is the list of sample applications showing different variations of Simplify.Web usage:

- [Only as an API backend with Angular + Bootstrap UI SPA](https://github.com/SimplifyNet/Simplify.Web/tree/master/src/SampleApps/SampleApp.Angular)
- [Kestrel-based application with backend HTML generation, localization, authentication](https://github.com/SimplifyNet/Simplify.Web/tree/master/src/SampleApps/SampleApp.Classic)

![Simplify](https://raw.githubusercontent.com/SimplifyNet/Simplify.Web/master/images/screenshots/sample-app-classic.png)

- [Simple Kestrel-based application hosted as a Windows service](https://github.com/SimplifyNet/Simplify.Web/tree/master/src/SampleApps/SampleApp.WindowsServiceHosted)

## Contributing

There are many ways in which you can participate in the project. Like most open-source software projects, contributing code is just one of many outlets where you can help improve. Some of the things that you could help out with are:

- Documentation (both code and features)
- Bug reports
- Bug fixes
- Feature requests
- Feature implementations
- Test coverage
- Code quality
- Sample applications

## Related Projects

Additional extensions to Simplify.Web live in their own repositories on GitHub. For example:

- [Simplify.Web.Json](https://github.com/SimplifyNet/Simplify.Web.Json) - JSON serialization/deserialization
- [Simplify.Web.Postman](https://github.com/SimplifyNet/Simplify.Web.Postman) - Postman collection and environment generation
- [Simplify.Web.Swagger](https://github.com/SimplifyNet/Simplify.Web.Swagger) - Swagger generation for controllers
- [Simplify.Web.Multipart](https://github.com/SimplifyNet/Simplify.Web.Multipart) - multipart form model binder
- [Simplify.Web.MessageBox](https://github.com/SimplifyNet/Simplify.Web.MessageBox) - non-interactive server-side message box
- [Simplify.Web.Templates](https://github.com/SimplifyNet/Simplify.Web.Templates) - .NET project templates

## License

Licensed under the GNU LESSER GENERAL PUBLIC LICENSE.
