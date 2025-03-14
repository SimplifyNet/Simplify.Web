# Simplify.Web + Angular example

## Pre-Requisites

* .NET 7 or higher
* Node JS 18.13.0 or higher

## Debug Launch options

### Via VS Code

1. Run debug of `SampleApp.Angular` profile in VS Code (or VS Codium)
2. Launch front end separately via `npm run start` from `ClientApp`
3. Go to <https://localhost:10900> URL

### Via Microsoft Visual Studio

1. Run debug of `SampleApp.Angular` profile in Microsoft Visual Studio
2. <http://localhost:5000> URL will be opened in prowser, wait some time and user will be redirected to <https://localhost:10900> URL

#### Commands sequence

1. Visual Studio will open <http://localhost:5000> URL
2. SPA backend will execute `npm install` command
2. SPA backend will execute `npm run start` which will trigger `ng build` and `ng serve` commands
4. After successful launch of Angular front end application user will be redirected to <http://localhost:5000> URL
