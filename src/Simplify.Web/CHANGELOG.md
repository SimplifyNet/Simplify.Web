# Changelog

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
