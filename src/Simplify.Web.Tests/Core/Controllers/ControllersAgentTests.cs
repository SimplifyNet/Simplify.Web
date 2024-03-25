using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using Moq;
using NUnit.Framework;
using Simplify.Web.Core.Controllers;
using Simplify.Web.Meta;
using Simplify.Web.Routing;
using Simplify.Web.Tests.Core.Controllers.TestTypes;

namespace Simplify.Web.Tests.Core.Controllers;

[TestFixture]
public class ControllersAgentTests
{
	private ControllersAgent _agent = null!;
	private Mock<IControllersMetaStore> _metaStore = null!;
	private Mock<IRouteMatcher> _routeMatcher = null!;

	[SetUp]
	public void Initialize()
	{
		_metaStore = new Mock<IControllersMetaStore>();
		_routeMatcher = new Mock<IRouteMatcher>();
		_agent = new ControllersAgent(_metaStore.Object, _routeMatcher.Object);
	}

	[Test]
	public void GetStandardControllersMetaData_StandardControllerAndAll40xControllers_OnlyStandardReturned()
	{
		// Arrange

		var metaStore = Mock.Of<IControllersMetaStore>(x => x.ControllersMetaData ==
			new List<IControllerMetaData>
			{
				Mock.Of<IControllerMetaData>(),
				Mock.Of<IControllerMetaData>(x => x.Role == new ControllerRole(true, false, false)),
				Mock.Of<IControllerMetaData>(x => x.Role == new ControllerRole(false, true, false)),
				Mock.Of<IControllerMetaData>(x => x.Role == new ControllerRole(false, false, true))
			});

		var routeMatcher = Mock.Of<IRouteMatcher>();

		// Act
		var items = new ControllersAgent(metaStore, routeMatcher)
			.GetStandardControllersMetaData();

		// Assert

		Assert.AreEqual(1, items.Count);
		Assert.IsNull(items.First().Role);
	}

	[Test]
	public void GetStandardControllersMetaData_DifferentPriorities_RunOrderingRespected()
	{
		// Arrange

		var metaStore = Mock.Of<IControllersMetaStore>(x => x.ControllersMetaData ==
			new List<IControllerMetaData>
			{
				Mock.Of<IControllerMetaData>(x => x.ControllerType == typeof(TestController1) && x.ExecParameters == new ControllerExecParameters(null, 2)),
				Mock.Of<IControllerMetaData>(x => x.ControllerType == typeof(TestController2) && x.ExecParameters == new ControllerExecParameters(null, 1)),
				Mock.Of<IControllerMetaData>(x => x.ControllerType == typeof(TestController4)),
				Mock.Of<IControllerMetaData>(x => x.ControllerType == typeof(TestController5)),
			});

		var routeMatcher = Mock.Of<IRouteMatcher>();

		// Act
		var items = new ControllersAgent(metaStore, routeMatcher)
			.GetStandardControllersMetaData();

		// Assert

		Assert.AreEqual(4, items.Count);
		Assert.AreEqual("TestController4", items[0].ControllerType.Name);
		Assert.AreEqual("TestController5", items[1].ControllerType.Name);
		Assert.AreEqual("TestController2", items[2].ControllerType.Name);
		Assert.AreEqual("TestController1", items[3].ControllerType.Name);
	}

	[Test]
	public void MatchControllerRoute_NoControllerExecParameters_MatchCalled()
	{
		// Arrange

		var controller = Mock.Of<IControllerMetaData>();
		var sourceRoute = "/foo";
		var httpMethod = "GET";

		// Act
		_agent.MatchControllerRoute(controller, sourceRoute, httpMethod);

		// Assert
		_routeMatcher.Verify(x => x.Match(It.Is<string>(s => s == sourceRoute), It.Is<string>(s => s == null)));
	}

	[Test]
	public void MatchControllerRoute_NoControllerRouteData_MatchCalled()
	{
		// Arrange

		var controller = Mock.Of<IControllerMetaData>(x => x.ControllerType == typeof(TestController1) &&
			x.ExecParameters == new ControllerExecParameters(new Dictionary<HttpMethod, string>(), 0));

		var sourceRoute = "/foo";
		var httpMethod = "GET";

		// Act
		_agent.MatchControllerRoute(controller, sourceRoute, httpMethod);

		// Assert
		_routeMatcher.Verify(x => x.Match(It.Is<string>(s => s == sourceRoute), It.Is<string>(s => s == null)));
	}

	[TestCase("/foo", HttpMethod.Get, "/bar", "GET")]
	[TestCase("/foo", HttpMethod.Post, "/bar", "POST")]
	[TestCase("/foo", HttpMethod.Put, "/bar", "PUT")]
	[TestCase("/foo", HttpMethod.Patch, "/bar", "PATCH")]
	[TestCase("/foo", HttpMethod.Delete, "/bar", "DELETE")]
	[TestCase("/foo", HttpMethod.Options, "/bar", "OPTIONS")]
	public void MatchControllerRoute_SpecifiedControllerRouteSpecifiedMethod_MatchCalled(string controllerRoute,
		HttpMethod controllerHttpMethod,
		string sourceRoute,
		string httpMethod)
	{
		// Arrange

		var controller = Mock.Of<IControllerMetaData>(x => x.ControllerType == typeof(TestController1) &&
			x.ExecParameters == new ControllerExecParameters(new Dictionary<HttpMethod, string> { { controllerHttpMethod, controllerRoute } }, 0));

		// Act
		_agent.MatchControllerRoute(controller, sourceRoute, httpMethod);

		// Assert
		_routeMatcher.Verify(x => x.Match(It.Is<string>(s => s == sourceRoute), It.Is<string>(s => s == controllerRoute)));
	}

	[TestCase("/foo", HttpMethod.Post, "/bar", "GET")]
	[TestCase("/foo", HttpMethod.Get, "/bar", "FOO")]
	public void MatchControllerRoute_SpecifiedControllerRouteSpecifiedMethod_MatchNotCalled(string controllerRoute,
		HttpMethod controllerHttpMethod,
		string sourceRoute,
		string httpMethod)
	{
		// Arrange

		var controller = Mock.Of<IControllerMetaData>(x =>
			x.ExecParameters == new ControllerExecParameters(new Dictionary<HttpMethod, string> { { controllerHttpMethod, controllerRoute } }, 0));

		// Act
		var result = _agent.MatchControllerRoute(controller, sourceRoute, httpMethod);

		// Assert

		Assert.IsNull(result);
		_routeMatcher.Verify(x => x.Match(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
	}

	[Test]
	public void GetHandlerController_NoController_Null()
	{
		// Arrange

		var metaStore = Mock.Of<IControllersMetaStore>(x => x.ControllersMetaData == new List<IControllerMetaData>());
		var agent = new ControllersAgent(metaStore, null!);

		// Act
		var result = agent.GetHandlerController(HandlerControllerType.Http404Handler);

		// Assert
		Assert.That(result, Is.Null);
	}

	[Test]
	public void GetHandlerController_HaveController_ControllerMetaDataReturned()
	{
		// Assign

		var metaStore = Mock.Of<IControllersMetaStore>(x => x.ControllersMetaData == new List<IControllerMetaData>
		{
			Mock.Of<IControllerMetaData>(x => x.Role == new ControllerRole(false, false, true))
		});

		_agent = new ControllersAgent(metaStore, _routeMatcher.Object);

		// Act
		var metaData = _agent.GetHandlerController(HandlerControllerType.Http404Handler)!;

		// Assert

		Assert.That(metaData.Role!.Is404Handler, Is.True);
	}

	[Test]
	public void IsAnyPageController_AnyPageController_True()
	{
		// Arrange
		var metaData = Mock.Of<IControllerMetaData>();

		// Act
		var result = _agent.IsAnyPageController(metaData);

		// Asset
		Assert.That(result, Is.True);
	}

	[Test]
	public void IsAnyPageController_AnyPageControllerWithEmptyRoutes_True()
	{
		// Arrange
		var metaData = Mock.Of<IControllerMetaData>(x => x.ExecParameters == new ControllerExecParameters(null, 0));

		// Act
		var result = _agent.IsAnyPageController(metaData);

		// Asset
		Assert.That(result, Is.True);
	}

	[Test]
	public void IsAnyPageController_404Handler_False()
	{
		// Arrange
		var metaData = Mock.Of<IControllerMetaData>(x => x.Role == new ControllerRole(false, false, true));

		// Act
		var result = _agent.IsAnyPageController(metaData);

		// Asset
		Assert.That(result, Is.False);
	}

	[TestCase("/", HttpMethod.Get)]
	[TestCase("/", HttpMethod.Post)]
	[TestCase("/", HttpMethod.Put)]
	[TestCase("/", HttpMethod.Patch)]
	[TestCase("/", HttpMethod.Delete)]
	[TestCase("/", HttpMethod.Options)]
	public void IsAnyPageController_GetRoute_False(string controllerRoute,
		HttpMethod controllerHttpMethod)
	{
		// Arrange
		var metaData = Mock.Of<IControllerMetaData>(x =>
			x.ExecParameters == new ControllerExecParameters(new Dictionary<HttpMethod, string> { { controllerHttpMethod, controllerRoute } }, 0));

		// Act
		var result = _agent.IsAnyPageController(metaData);

		// Asset
		Assert.That(result, Is.False);
	}

	[Test]
	public void IsSecurityRulesViolated_NoSecurityRules_Ok()
	{
		// Arrange
		var metaData = Mock.Of<IControllerMetaData>();

		// Act
		var result = _agent.IsSecurityRulesViolated(metaData, null!);

		// Asset
		Assert.That(result, Is.EqualTo(SecurityRuleCheckResult.Ok));
	}

	[Test]
	public void IsSecurityRulesViolated_AuthorizationRequiredNotAuthorized_NotAuthenticated()
	{
		// Arrange
		var metaData = Mock.Of<IControllerMetaData>(x => x.Security == new ControllerSecurity(true, null));

		// Act
		var result = _agent.IsSecurityRulesViolated(metaData, null!);

		// Asset
		Assert.That(result, Is.EqualTo(SecurityRuleCheckResult.NotAuthenticated));
	}

	[Test]
	public void IsSecurityRulesViolated_AuthorizationRequiredAuthorized_Ok()
	{
		// Arrange

		var metaData = Mock.Of<IControllerMetaData>(x => x.Security == new ControllerSecurity(true, null));

		var claims = new List<Claim>
		{
			new(ClaimTypes.Name, "Foo")
		};

		var id = new ClaimsIdentity(claims, "test");
		var user = new ClaimsPrincipal(id);

		// Act
		var result = _agent.IsSecurityRulesViolated(metaData, user);

		// Asset
		Assert.That(result, Is.EqualTo(SecurityRuleCheckResult.Ok));
	}

	[Test]
	public void IsSecurityRulesViolated_AuthorizationRequiredWithGroupAuthorizedNoGroups_Forbidden()
	{
		// Arrange

		var metaData = Mock.Of<IControllerMetaData>(x => x.Security == new ControllerSecurity(true, new List<string>
		{
			"Admin",
			"User"
		}));

		var claims = new List<Claim>
		{
			new(ClaimTypes.Name, "Foo")
		};

		var id = new ClaimsIdentity(claims, "test");
		var user = new ClaimsPrincipal(id);

		// Act
		var result = _agent.IsSecurityRulesViolated(metaData, user);

		// Asset
		Assert.That(result, Is.EqualTo(SecurityRuleCheckResult.Forbidden));
	}

	[Test]
	public void IsSecurityRulesViolated_AuthorizationRequiredWithGroupAuthorizedNotInGroup_Forbidden()
	{
		// Arrange

		var metaData = Mock.Of<IControllerMetaData>(x => x.Security == new ControllerSecurity(true, new List<string>
		{
			"Admin"
		}));

		var claims = new List<Claim>
		{
			new(ClaimTypes.Name, "Foo"),
			new(ClaimTypes.Role, "User")
		};

		var id = new ClaimsIdentity(claims, "test");
		var user = new ClaimsPrincipal(id);

		// Act
		var result = _agent.IsSecurityRulesViolated(metaData, user);

		// Asset
		Assert.That(result, Is.EqualTo(SecurityRuleCheckResult.Forbidden));
	}

	[Test]
	public void IsSecurityRulesViolated_AuthorizationRequiredWithGroupNotAuthorized_NotAuthenticated()
	{
		// Arrange

		var metaData = Mock.Of<IControllerMetaData>(x => x.Security == new ControllerSecurity(true, new List<string>
		{
			"Admin",
			"User"
		}));

		// Act
		var result = _agent.IsSecurityRulesViolated(metaData, null!);

		// Asset
		Assert.That(result, Is.EqualTo(SecurityRuleCheckResult.NotAuthenticated));
	}

	[Test]
	public void IsSecurityRulesViolated_AuthorizationRequiredWithGroupAuthorizedInGroup_Ok()
	{
		// Arrange

		var metaData = Mock.Of<IControllerMetaData>(x => x.Security == new ControllerSecurity(true, new List<string>
		{
			"Admin",
			"User"
		}));

		var claims = new List<Claim>
		{
			new(ClaimTypes.Name, "Foo"),
			new(ClaimTypes.Role, "User")
		};

		var id = new ClaimsIdentity(claims, "test");
		var user = new ClaimsPrincipal(id);

		// Act
		var result = _agent.IsSecurityRulesViolated(metaData, user);

		// Asset
		Assert.That(result, Is.EqualTo(SecurityRuleCheckResult.Ok));
	}

	[Test]
	public void IsSecurityRulesViolated_UserExistNotAuthenticatedUser_NotAuthenticated()
	{
		// Arrange

		var metaData = Mock.Of<IControllerMetaData>(x => x.Security == new ControllerSecurity(true, null));

		var id = Mock.Of<IIdentity>(x => x.IsAuthenticated == false);
		var user = new ClaimsPrincipal(id);

		// Act
		var result = _agent.IsSecurityRulesViolated(metaData, user);

		// Asset
		Assert.That(result, Is.EqualTo(SecurityRuleCheckResult.NotAuthenticated));
	}

	[Test]
	public void IsSecurityRulesViolated_UserExistNotAuthenticatedUserWithAllowedUserRoles_NotAuthenticated()
	{
		// Arrange

		var metaData = Mock.Of<IControllerMetaData>(x => x.Security == new ControllerSecurity(true, new List<string>
			{
				"User"
			}));

		var id = Mock.Of<IIdentity>(x => x.IsAuthenticated == false);
		var user = new ClaimsPrincipal(id);

		// Act
		var result = _agent.IsSecurityRulesViolated(metaData, user);

		// Asset
		Assert.That(result, Is.EqualTo(SecurityRuleCheckResult.NotAuthenticated));
	}
}