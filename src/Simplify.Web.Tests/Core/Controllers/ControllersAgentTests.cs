#nullable disable

using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using Moq;
using NUnit.Framework;
using Simplify.Web.Core.Controllers;
using Simplify.Web.Meta;
using Simplify.Web.Routing;

namespace Simplify.Web.Tests.Core.Controllers
{
	[TestFixture]
	public class ControllersAgentTests
	{
		private ControllersAgent _agent;
		private Mock<IControllersMetaStore> _metaStore;
		private Mock<IRouteMatcher> _routeMatcher;

		[SetUp]
		public void Initialize()
		{
			_metaStore = new Mock<IControllersMetaStore>();
			_routeMatcher = new Mock<IRouteMatcher>();
			_agent = new ControllersAgent(_metaStore.Object, _routeMatcher.Object);
		}

		[Test]
		public void GetStandardControllersMetaData_StandartControllerAndAll40xControllers_OnlyStandartReturned()
		{
			// Assign

			_metaStore.SetupGet(x => x.ControllersMetaData)
				.Returns(new List<IControllerMetaData>
				{
					new ControllerMetaData(null),
					new ControllerMetaData(null, null, new ControllerRole(true)),
					new ControllerMetaData(null, null, new ControllerRole(false, true)),
					new ControllerMetaData(null, null, new ControllerRole(false, false, true))
				});

			_agent = new ControllersAgent(_metaStore.Object, _routeMatcher.Object);

			// Act
			var items = _agent.GetStandardControllersMetaData().ToList();

			// Assert
			Assert.AreEqual(1, items.Count);
			Assert.IsNull(items.First().Role);
		}

		[Test]
		public void MatchControllerRoute_NoControllerRouteData_MatchCalled()
		{
			// Act
			_agent.MatchControllerRoute(new ControllerMetaData(null), "/foo", "GET");

			// Assert
			_routeMatcher.Verify(x => x.Match(It.Is<string>(s => s == "/foo"), It.Is<string>(s => s == null)));
		}

		[Test]
		public void MatchControllerRoute_GetControllerRouteGetMethod_MatchCalled()
		{
			// Act
			_agent.MatchControllerRoute(
				new ControllerMetaData(null, new ControllerExecParameters(new ControllerRouteInfo("/foo"))), "/bar", "GET");

			// Assert
			_routeMatcher.Verify(x => x.Match(It.Is<string>(s => s == "/bar"), It.Is<string>(s => s == "/foo")));
		}

		[Test]
		public void MatchControllerRoute_PostControllerRoutePostMethod_MatchCalled()
		{
			// Act
			_agent.MatchControllerRoute(
				new ControllerMetaData(null, new ControllerExecParameters(new ControllerRouteInfo(null, "/foo"))), "/bar", "POST");

			// Assert
			_routeMatcher.Verify(x => x.Match(It.Is<string>(s => s == "/bar"), It.Is<string>(s => s == "/foo")));
		}

		[Test]
		public void MatchControllerRoute_PutControllerRoutePutMethod_MatchCalled()
		{
			// Act
			_agent.MatchControllerRoute(
				new ControllerMetaData(null, new ControllerExecParameters(new ControllerRouteInfo(null, null, "/foo"))), "/bar",
				"PUT");

			// Assert
			_routeMatcher.Verify(x => x.Match(It.Is<string>(s => s == "/bar"), It.Is<string>(s => s == "/foo")));
		}

		[Test]
		public void MatchControllerRoute_PatchControllerRoutePatchMethod_MatchCalled()
		{
			// Act
			_agent.MatchControllerRoute(
				new ControllerMetaData(null, new ControllerExecParameters(new ControllerRouteInfo(null, null, null, "/foo"))), "/bar",
				"PATCH");

			// Assert
			_routeMatcher.Verify(x => x.Match(It.Is<string>(s => s == "/bar"), It.Is<string>(s => s == "/foo")));
		}

		[Test]
		public void MatchControllerRoute_DeleteControllerRouteDeleteMethod_MatchCalled()
		{
			// Act
			_agent.MatchControllerRoute(
				new ControllerMetaData(null, new ControllerExecParameters(new ControllerRouteInfo(null, null, null, null, "/foo"))),
				"/bar", "DELETE");

			// Assert
			_routeMatcher.Verify(x => x.Match(It.Is<string>(s => s == "/bar"), It.Is<string>(s => s == "/foo")));
		}

		[Test]
		public void MatchControllerRoute_PostControllerRouteGetMethod_MatchNotCalled()
		{
			// Act
			var result = _agent.MatchControllerRoute(
				new ControllerMetaData(null, new ControllerExecParameters(new ControllerRouteInfo(null, "/foo"))), "/bar", "GET");

			// Assert

			Assert.IsNull(result);
			_routeMatcher.Verify(x => x.Match(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
		}

		[Test]
		public void MatchControllerRoute_UndefinedMethod_MatchNotCalled()
		{
			// Act
			var result = _agent.MatchControllerRoute(
				new ControllerMetaData(null, new ControllerExecParameters(new ControllerRouteInfo("/foo"))), "/bar", "FOO");

			// Assert

			Assert.IsNull(result);
			_routeMatcher.Verify(x => x.Match(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
		}

		[Test]
		public void GetHandlerController_NoController_Null()
		{
			// Assign

			_metaStore.SetupGet(x => x.ControllersMetaData).Returns(new List<IControllerMetaData>());
			_agent = new ControllersAgent(_metaStore.Object, _routeMatcher.Object);

			// Act & Assert
			Assert.IsNull(_agent.GetHandlerController(HandlerControllerType.Http404Handler));
		}

		[Test]
		public void GetHandlerController_HaveController_ControllerMetaDataReturned()
		{
			// Assign

			_metaStore.SetupGet(x => x.ControllersMetaData).Returns(new List<IControllerMetaData>
			{
				new ControllerMetaData(null, null, new ControllerRole(false, false, true))
			});

			_agent = new ControllersAgent(_metaStore.Object, _routeMatcher.Object);

			// Act
			var metaData = _agent.GetHandlerController(HandlerControllerType.Http404Handler);

			// Assert

			Assert.IsTrue(metaData.Role.Is404Handler);
		}

		[Test]
		public void IsAnyPageController_AnyPageController_True()
		{
			// Assign
			var metaData = new ControllerMetaData(null);

			// Act & Assert
			Assert.IsTrue(_agent.IsAnyPageController(metaData));
		}

		[Test]
		public void IsAnyPageController_404Handler_False()
		{
			// Assign
			var metaData = new ControllerMetaData(null, null, new ControllerRole(false, false, true));

			// Act & Assert
			Assert.IsFalse(_agent.IsAnyPageController(metaData));
		}

		[Test]
		public void IsAnyPageController_GetRoute_False()
		{
			// Assign
			var metaData = new ControllerMetaData(null, new ControllerExecParameters(new ControllerRouteInfo("/")));

			// Act & Assert
			Assert.IsFalse(_agent.IsAnyPageController(metaData));
		}

		[Test]
		public void IsAnyPageController_PostRoute_False()
		{
			// Assign
			var metaData = new ControllerMetaData(null, new ControllerExecParameters(new ControllerRouteInfo(null, "/")));

			// Act & Assert
			Assert.IsFalse(_agent.IsAnyPageController(metaData));
		}

		[Test]
		public void IsAnyPageController_PutRoute_False()
		{
			// Assign
			var metaData = new ControllerMetaData(null, new ControllerExecParameters(new ControllerRouteInfo(null, null, "/")));

			// Act & Assert
			Assert.IsFalse(_agent.IsAnyPageController(metaData));
		}

		[Test]
		public void IsAnyPageController_PatchRoute_False()
		{
			// Assign
			var metaData = new ControllerMetaData(null, new ControllerExecParameters(new ControllerRouteInfo(null, null, null, "/")));

			// Act & Assert
			Assert.IsFalse(_agent.IsAnyPageController(metaData));
		}

		[Test]
		public void IsAnyPageController_DeleteRoute_False()
		{
			// Assign
			var metaData = new ControllerMetaData(null, new ControllerExecParameters(new ControllerRouteInfo(null, null, null, null, "/")));

			// Act & Assert
			Assert.IsFalse(_agent.IsAnyPageController(metaData));
		}

		[Test]
		public void IsSecurityRulesViolated_NoSecurityRules_Ok()
		{
			// Assign
			var metaData = new ControllerMetaData(null);

			// Act & Assert
			Assert.AreEqual(SecurityRuleCheckResult.Ok, _agent.IsSecurityRulesViolated(metaData, null));
		}

		[Test]
		public void IsSecurityRulesViolated_AuthorizationRequiredNotAuthorized_NotAuthenticated()
		{
			// Assign
			var metaData = new ControllerMetaData(null, null, null, new ControllerSecurity(true));

			// Act & Assert
			Assert.AreEqual(SecurityRuleCheckResult.NotAuthenticated, _agent.IsSecurityRulesViolated(metaData, null));
		}

		// TODO
		//[Test]
		//public void IsSecurityRulesViolated_AuthorizationRequiredAuthorized_Ok()
		//{
		//	// Assign

		//	var metaData = new ControllerMetaData(null, null, null, new ControllerSecurity(true));
		//	var claims = new List<Claim>
		//	{
		//		new Claim(ClaimTypes.Name, "Foo")
		//	};

		//	var id = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
		//	var user = new ClaimsPrincipal(id);

		//	// Act & Assert
		//	Assert.AreEqual(SecurityRuleCheckResult.Ok, _agent.IsSecurityRulesViolated(metaData, user));
		//}

		// TODO
		//[Test]
		//public void IsSecurityRulesViolated_AuthorizationRequiredWithGroupAuthorizedNoGroups_Forbidden()
		//{
		//	// Assign

		//	var metaData = new ControllerMetaData(null, null, null,
		//		new ControllerSecurity(true, new List<string> { "Admin", "User" }));

		//	var claims = new List<Claim>
		//	{
		//		new Claim(ClaimTypes.Name, "Foo")
		//	};

		//	var id = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
		//	var user = new ClaimsPrincipal(id);

		//	// Act & Assert
		//	Assert.AreEqual(SecurityRuleCheckResult.Forbidden, _agent.IsSecurityRulesViolated(metaData, user));
		//}

		// TODO
		//[Test]
		//public void IsSecurityRulesViolated_AuthorizationRequiredWithGroupAuthorizedNotInGroup_Forbidden()
		//{
		//	// Assign

		//	var metaData = new ControllerMetaData(null, null, null, new ControllerSecurity(true, new List<string> { "Admin" }));
		//	var claims = new List<Claim>
		//	{
		//		new Claim(ClaimTypes.Name, "Foo"),
		//		new Claim(ClaimTypes.Role, "User")
		//	};

		//	var id = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
		//	var user = new ClaimsPrincipal(id);

		//	// Act & Assert
		//	Assert.AreEqual(SecurityRuleCheckResult.Forbidden, _agent.IsSecurityRulesViolated(metaData, user));
		//}

		[Test]
		public void IsSecurityRulesViolated_AuthorizationRequiredWithGroupNotAuthorized_NotAuthenticated()
		{
			// Assign

			var metaData = new ControllerMetaData(null, null, null, new ControllerSecurity(true, new List<string> { "Admin, User" }));

			// Act & Assert
			Assert.AreEqual(SecurityRuleCheckResult.NotAuthenticated, _agent.IsSecurityRulesViolated(metaData, null));
		}

		// TODO
		//[Test]
		//public void IsSecurityRulesViolated_AuthorizationRequiredWithGroupAuthorizedInGroup_Ok()
		//{
		//	// Assign

		//	var metaData = new ControllerMetaData(null, null, null, new ControllerSecurity(true, new List<string> { "Admin", "User" }));
		//	var claims = new List<Claim>
		//	{
		//		new Claim(ClaimTypes.Name, "Foo"),
		//		new Claim(ClaimTypes.Role, "User")
		//	};

		//	var id = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
		//	var user = new ClaimsPrincipal(id);

		//	// Act & Assert
		//	Assert.AreEqual(SecurityRuleCheckResult.Ok, _agent.IsSecurityRulesViolated(metaData, user));
		//}

		[Test]
		public void IsSecurityRulesViolated_UserExistNotAuthenticatedUser_NotAuthenticated()
		{
			// Assign

			var metaData = new ControllerMetaData(null, null, null, new ControllerSecurity(true));

			var id = new Mock<IIdentity>();
			id.Setup(x => x.IsAuthenticated).Returns(false);
			var user = new ClaimsPrincipal(id.Object);

			// Act & Assert
			Assert.AreEqual(SecurityRuleCheckResult.NotAuthenticated, _agent.IsSecurityRulesViolated(metaData, user));
		}

		[Test]
		public void IsSecurityRulesViolated_UserExistNotAuthenticatedUserWithAllowedUserRoles_NotAuthenticated()
		{
			// Assign

			var metaData = new ControllerMetaData(null, null, null, new ControllerSecurity(true, new List<string> { "User" }));

			var id = new Mock<IIdentity>();
			id.Setup(x => x.IsAuthenticated).Returns(false);
			var user = new ClaimsPrincipal(id.Object);

			// Act & Assert
			Assert.AreEqual(SecurityRuleCheckResult.NotAuthenticated, _agent.IsSecurityRulesViolated(metaData, user));
		}
	}
}