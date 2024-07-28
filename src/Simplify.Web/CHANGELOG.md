# Changelog

## [5.1.0] - 2024-07-28

### Added

- Static files in-memory cache (#267)

### Fixed

- Web context is not available on WriteErrorResponseAsync causing NRE

## [5.0.0] - 2024-06-05

### Breaking

- Internal framework restructure and rewrite (http request and response handling related functionality and metadata rewritten from scratch). Can affect any customizations to related functionality

### Added

- Version 2 controllers
- Built-in Json response using System.Text.Json
- Built-in JSON model binder (enabled by default)
- Switchable measurements (StopwatchProvider), disabled by default

### Removed

- .NET Framework 4.8 explicit support
- `UseSimplifyWebWithoutRegistrations` and  `UseSimplifyWebNonTerminalWithoutRegistrations` `IApplicationBuilder` methods
- `ISimplifyWebSettings` override via `RegisterSimplifyWebSettings`
- `IConfiguration` override via `OverrideConfiguration` or `RegisterConfiguration`
- 400 special controller attribute

### Changed

- `UseSimplifyWeb` and `UseSimplifyWebNoNTerminal` now require passing `true` to automatically register it's own bootstrapper registrations
- `RegisterSimplifyWeb` now extension method of`IDIRegistrator` instead of `IDIContainerProvider`, custom internal `IDIContainerProvider` can be passed via method parameter, if required
- Internal `IConfiguration` registration override via `RegisterSimplifyWeb`
- Static files disabled by default (when disabled, static files IOC container registrations will be skipped)
- `Environment` split to `Environment` and `DynamicEnvironment`
- V1 controllers RouteParameters defaulted to empty ExpandoObject to avoid NRE
- `SimplifyWebSettings` loading thru binder
- Controllers search on execution optimized
- XML comments revisited/updated

### Fixed

- Multiple one route controllers handling
- Multiple middlewares having same `IsTerminal` status

### Dependencies

- Switched to explicit Simplify.System 1.6.2 instead of internal Simplify.System.Sources
- Microsoft.Extensions.Configuration.Json bump to 8.0.0
- Microsoft.Extensions.Configuration.Binder 8.0.1 added
- Simplify.Templates bump to 2.0.2
- Internal Simplify.Xml.Sources bump to 1.4

## [4.9.1] - 2024-04-16

### Fixed

- Model Validation Attribute For Range Of Numbers (#257)
- Model Validation Attributes For Min Max (#256)

## [4.9.0] - 2024-01-08

### Added

- Add Model Validation Attribute For Range Of Numbers (#236)
- Add Model Validation Attributes For Min Max (#251)

## [4.8.1] - 2023-12-20

### Fixed

- Missing Set content type `text/plain` for all string responses by default for shortcut methods (#247)

## [4.8.0] - 2023-12-20

### Changed

- ***!Important!*** Setting language from cookie on requests disabled by default, to enable set `AcceptCookieLanguage` setting to `true` in `SimplifyWebSettings` (#246)
- `AcceptBrowserLanguage` setting renamed to `AcceptHeaderLanguage` (#246)
- The way framework checks that applying language is valid

### Fixed

- Invariant language setting and check

### Added

- Set content type `text/plain` for all string responses by default (#247)
- `AcceptCookieLanguage` setting

## [4.7.1] - 2022-11-07

### Fixed

- Controller with `IList<T>` model validation exception (#240)

## [4.7.0] - 2022-08-20

### Removed

- .NET 5 support
- .NET Core 3.1 support
- .NET Framework 4.6.2 support

### Added

- .NET Standard 2.1 support
- `Created` response with HTTP 201 status (#226)
- Additional comments

### Dependencies

- Simplify.DI bump to 4.2.10
- Simplify.Templates bump to 2.0.1
- Internal Simplify.Sting.Sources bump to 1.2.2
- Internal Simplify.System.Sources bump to 1.6.2
- Internal Simplify.Xml.Sources bump to 1.3.1

#### For target frameworks .NET Standard 2.1, .NET Standard 2.0, .NET Framework 4.8

- Microsoft.Extensions.Configuration.Json bump to 3.1.32

- Microsoft.AspNetCore.Http dependency removed
- Microsoft.AspNetCore.Hosting.Abstractions dependency removed

## [4.6.0] - 2022-05-17

### Added

- Changing framework modules via lambda expression in `RegisterSimplifyWeb` (#191)

### Changed

- `RegisterSimplifyWeb` moved to `Simplify.Web` namespace

### Fixed

- `Content` controller response should have 'text/plain' type by default (#192)

### Security

- URL redirection from remote source (#189)
- Log entries created from user input (#190)

### Dependencies

- `Simplify.DI` bump to 4.2.1

## [4.5.1] - 2022-04-27

### Dependencies

- Simplify.DI bump to 4.2 (PR#187)
- Microsoft.Extensions.Configuration.Json bump to 3.1.24 for .NET Standard 2.0 and .NET 4.6.2

## [4.5.0] - 2021-11-23

### Added

- Possibility to use base class between user controller and controller base class (#167)

## [4.4.3] - 2021-10-19

### Fixed

- Controllers sorting by run priority

### Dependencies

- Simplify.DI bump to 4.1.1

## [4.4.2] - 2021-07-18

### Dependencies

- Simplify.DI bump to 4.1
- Microsoft.Extensions.Configuration.Json bump to 3.1.17 and moved to .NET Standard 2.0 and .NET 4.6.2 dependencies

## [4.4.1] - 2021-06-07

### Fixed

- Nullable controllers responses (#155)

### Dependencies

- Simplify.DI bump to 4.0.20
- Microsoft.Extensions.Configuration.Json bump to 3.1.16

## [4.4.0] - 2021-04-25

### New

- Possibility to reuse model between controllers in same scope (#145)

## [4.3.0] - 2021-04-22

### New

- New built-in exception page design (#140)
- Built-in exception page light/dark design switch option (#142)
- Display minimal text exception page in case of WebContext.IsAjax (#141)
- .NET 5.0 explicit support
- Refactoring to nullable

### Changed

- String table items without name ignore

### Fixed

- Context.User.Identity check for null
- Assembly.FullName null check added

### Dependencies

- Microsoft.Extensions.Configuration.Json bump to 3.1.14
- Simplify.System.Sources bump to 1.6.1
- Simplify.Xml.Sources bump to 1.3
