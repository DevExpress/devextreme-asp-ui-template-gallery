# DevExtreme UI Template Gallery for ASP.NET Core

This project includes responsive UI Templates for popular UI/UX patterns in web LOB applications.You can use these views as building blocks and adjust them to your particular needs.
In addition, this project implements view navigation and theme switching without full re-rendering which may help you optimize your UX.

## Installation

Download the example and use Visual Studio 2022 to open the project. 

## Project Structure

The project contains the following sections:
- The `Controllers` folder contains actions connected with the `Views` folder
- The `Views` folder holds the structure of the current pages within an application.
  * `Auth` contains all code related to authorization forms.
  * `Shared` contains logic related to root layots.
  *  Other folders are equal to name of corresponding pages in the application itself.
- The `Models` contains all models we used in the application.
- The `DAL` and `Services` folder contain data-related logic.
- The `Utils` folder contains helper methods for certain use cases.
- All client-side source code is located in `src` . This folder repeats structure of views and layouts.

## Client-side resources and bundling

This project uses [NPM](https://www.npmjs.com/) to install client-side libraries: jQuery and DevExtreme. All application styles are built from SCSS and TypeScript is used to write client-side logic. 
Then run `F5` and MSBuild will execute the following tasks

- The project checks for Node.js and restores NPM packages. 
- `CopyTask` for jQuery and DevExtreme scripts, icons, and fonts from `node_modules` and moves them to `wwwroot`
- `AspNetCore.SassCompiler` bundles required SCSS scripts into CSS files and moves them to `wwwroot`. You can define/change this config in `appsettings.json`
- `Microsoft.TypeScript.MSBuild` compiles all TS to JS and moves files to `wwwroot` 

### Add additional 3rd-party client-side resources 

To add additional scripts/styles to application it's necessary to install corresponding NPM packages first. 
Then add corresponding files to the `CopyTask`: [link](https://github.com/DevExpress/devextreme-asp-ui-template-gallery/blob/eca6039179487322bdb682fa68558d34799ca9ec/devextreme-asp-ui-template-gallery.csproj#L18)

## Views

The application layout consists of a top toolbar, a left-side navigation menu, and the main content view.
- The left-side navigation menu lists grouped views.
- Each item in the navigation menu opens the corresponding view in the content area.
- Every view can encapsulate its own script and style files, allowing for modular development and easy customization.

### How to add TS scripts for the view

- Create myview.ts file in src/ts/views.
- Link myview.js file in the view's code. This JS file will be generated based on the TS file during the build process.

```html
<script src="~/js/views/myview.js"></script>
```

### How to add CSS scripts for the view

- Create `myview.scss` file in src/scss/views.
- Link the `myview.css` file in the view's code. This CSS file will be generated based on the SCSS file during the build process.

```html
<link href="~/css/views/myview.css" rel="stylesheet" />
```
  
## Navigation

### Custom Router Overview

We implemented a **custom router** to navigate between views. The router supports both **regular navigation** (full reload) and **SPA-like navigation** (dynamic updates).

### Navigation Logic

- **Layout Check**  
  - If the **target view’s Layout differs** from the current view’s Layout, the router performs a **full page reload**.  
  - If the **Layouts match**, the router **preserves the header, footer, and left-side navigation bar**, and **dynamically replaces only the content**.

- **URL Handling**  
  - The router uses the [`pushState`](https://developer.mozilla.org/en-US/docs/Web/API/History/pushState) method and [`popstate`](https://developer.mozilla.org/en-US/docs/Web/API/Window/popstate_event) event to modify and track browser history.  
  - On navigation:
    1. Calls `pushState` to add the target URL.  
    2. Loads the target page content.  
    3. Compares the **Layout part** of the current and target pages using the custom `data-target-placeholder-id` attribute.  
    4. Updates only the content if Layouts match, otherwise reloads the page.  

Refer to the [router.ts](src/ts/router.ts) file for the full implementation.

### Layout Handling

- **Partial Updates**  
  - A lightweight [`_LayoutPartial.cshtml`](Views/Shared/_LayoutPartial.cshtml) is used to send partial updates from the server.  
  - This logic is defined in [`_ViewStart.cshtml`](Views/_ViewStart.cshtml).

- **Authentication Views**  
  - The [`_ViewStart.cshtml`](Views/Auth/_ViewStart.cshtml) file includes **additional Layout selection logic**.  
  - Auth views can be displayed either in the main Layout or a special Layout shown when users log out.


## Themes

The application supports theming to provide a consistent look and feel across all views.
-	Themes are managed via SCSS variables defined in the [scss](src/scss) folder
-	Themes can be switched dynamically at runtime: [Switch_Between_Themes_at_Runtime](https://js.devexpress.com/DevExtreme/Guide/Themes_and_Styles/Predefined_Themes/#Switch_Between_Themes_at_Runtime/Without_Page_Reload)
-	You can customize or extend themes by modifying the SCSS files in the css or scss directories.
-	The theme selection can be persisted for a user, see [theme.ts](src/ts/theme.ts).
-	To add a new theme, create a new SCSS file with your color and style overrides, and include it in the build process.

## DAL (Data Access Layer)

The application uses Entity Framework Core to manage data.
- Data is fetched from the DevExpress remote data service and stored in a SQLite database file. See the [DemoDataFetcher.cs](DAL/DemoDataFetcher.cs) file for details.
- For demonstration purposes, a per-user cache is implemented: [SessionDbContextMiddleware.cs](Middleware/SessionDbContextMiddleware.cs)
- When a user connects to the application for the first time, the SQLite database is cloned and stored in the server's memory.
- Only this user can access their copy, which is erased after a period of inactivity to free memory.
- The [LocalDemoDataContext](DAL/LocalDemoDataContext.cs) file contains in-memory static data for demo purposes.
- The [DemoDbContext](DAL/DemoDbContext.cs) file demonstrates entities we used accors views.

## License ##

**DevExtreme UI Template Gallery is released as a MIT-licensed (free and open-source) add-on to DevExtreme.**

Familiarize yourself with the [DevExtreme License](https://js.devexpress.com/Licensing/). [Free trial is available!](http://js.devexpress.com/Buy/)


## Further help

You can learn more about the ASP.NET Core components' syntax in our documentation: [Concepts](https://docs.devexpress.com/AspNetCore/400574/devextreme-based-controls/concepts/razor-syntax)
The client-side API is based on jQuery [jQuery documentation](https://api.jquery.com/) and described in the following topics: 
* [Get and Set Properties](https://js.devexpress.com/DevExtreme/Guide/jQuery_Components/Component_Configuration_Syntax/#Get_and_Set_Properties)
* [Call Methods](https://js.devexpress.com/DevExtreme/Guide/jQuery_Components/Component_Configuration_Syntax/#Call_Methods)
* [Get a UI Component Instance](https://js.devexpress.com/DevExtreme/Guide/jQuery_Components/Component_Configuration_Syntax/#Get_a_UI_Component_Instance)

To get more help on DevExtreme submit an issue in the [Support Center](https://www.devexpress.com/Support/Center/Question/Create)
