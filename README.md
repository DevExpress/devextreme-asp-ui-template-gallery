# DevExtreme UI Template Gallery for ASP.NET Core

This repository includes responsive UI Templates for the most popular UI/UX patterns in web LOB applications.

![UI Template Gallery Overview](/ui-template-gallery.png)

The UI Templates have responsive layouts with DevExtreme UI components. Note that the components use sample data entities without any business logic. You can use these UI Templates in your project and adapt them to your specific business requirements.

## License

**DevExtreme UI Template Gallery is released as a MIT-licensed (free and open-source) add-on to DevExtreme.**

Familiarize yourself with the [DevExtreme License](https://js.devexpress.com/Licensing/). [Free trial is available!](http://js.devexpress.com/Buy/).

## Installation

### Windows

- Clone\Donwload this repository on your PC 
- Open the solution in Visual Studio 2022
- If DevExpress Local Nuget Feed doesn't exist on your PC, obtain your key [online](https://docs.devexpress.com/GeneralInformation/116042/nuget/obtain-your-nuget-feed-credentials#devexpresscom-online-nuget-feed-any-os) and change [Nuget.config](https://github.com/DevExpress/devextreme-asp-ui-template-gallery/blob/dev/NuGet.config)
- Run the project (`F5`)

### Mac OS

- Clone this repository on your PC 
- Open the application folder in the IDE that supports `.NET` projects (e.g. Visual Studio Code)
- Obtain your key [online](https://docs.devexpress.com/GeneralInformation/116042/nuget/obtain-your-nuget-feed-credentials#devexpresscom-online-nuget-feed-any-os) and change [Nuget.config](https://github.com/DevExpress/devextreme-asp-ui-template-gallery/blob/dev/NuGet.config)
- If `.NET` SDK is not installed download (e.g., from the [site](https://dotnet.microsoft.com/en-us/download)) and install it first to get access to .NET CLI. 
- Run `dotnet run` from the root application folder in the IDE terminal.

## Project Structure

- **Controllers:** Contains actions connected to Views.
- **Views:** Contains layout pages.
  - *Auth:* Authorization-related forms.
  - *Shared:* Root layout logic.
  - Other folders match the names of individual views.
- **Models:** Contains application data models.
- **DAL** and **Services:** Data processing logic.
- **Utils:** Helper methods for common tasks.
- **src:** Client-side code. This folder mirrors structure of views and layouts.

## Client-side libraries and build process

- [NPM](https://www.npmjs.com/) manages client libraries: jQuery and DevExtreme.
- Styles are written in SCSS.
- Client logic is written in TypeScript.

Run `F5` to start MSBuild. It will execute the following build steps:

- Node.js check and NPM package restore.
- Required jQuery/DevExtreme assets (scripts, icons, fonts) copied from `node_modules` to `wwwroot` by `CopyTask`.
- SCSS files compiled to CSS by `AspNetCore.SassCompiler`. Output sent to `wwwroot`. You can change this configuration in `appsettings.json`.
- TypeScript compiled to JavaScript by `Microsoft.TypeScript.MSBuild`. Results sent to `wwwroot`.

To include extra third-party client resources:

- Install the packages using NPM.
- Add assets to the [`CopyTask`](https://github.com/DevExpress/devextreme-asp-ui-template-gallery/blob/eca6039179487322bdb682fa68558d34799ca9ec/devextreme-asp-ui-template-gallery.csproj#L18).

## Views

The application layout combines a top toolbar, left navigation menu, and the main content view.

- Navigation menu is grouped. If you click an item, the corresponding view loads in content area.
- Views may have unique style and script files for modularity and customization.

**Add TypeScript script to a view:**
- Create `myview.ts` in `src/ts/views`.
- Link the resulting `myview.js` file in the view markup. 

```html
<script src="~/js/views/myview.js"></script>
```

**Add SCSS to a view:**
- Create `myview.scss` in `src/scss/views`.
- Link the resulting `myview.css` file in the view markup.

```html
<link href="~/css/views/myview.css" rel="stylesheet" />
```

## Navigation

### Custom Router

Navigation between views is handled by a custom router. It supports both full page reload and single-page dynamic updates.

### Navigation Logic

- **Layout Check**  
  - If **target view Layout differs** from the current view Layout, the router performs a **full page reload**.  
  - If **Layouts match**, the router **dynamically replaces only the content**, preserving header, footer, and navigation menu.

- **URL Handling**  
  - The router uses the [`pushState`](https://developer.mozilla.org/en-US/docs/Web/API/History/pushState) method and [`popstate`](https://developer.mozilla.org/en-US/docs/Web/API/Window/popstate_event) event to modify and track browser history.  
  - When you navigate through pages, the router:
    1. Calls `pushState` to add the target URL.  
    2. Loads the target page content.  
    3. Compares the **Layout** of the current and target pages using custom `data-target-placeholder-id` attribute.  
    4. Updates only the content if Layouts match, otherwise reloads the page.  

For detailed implementation, refer to [router.ts](src/ts/router.ts).

### Layout management

- **Partial Updates**  
  - [`_LayoutPartial.cshtml`](Views/Shared/_LayoutPartial.cshtml) is used to send partial updates from the server.  
  - This logic is defined in [`_ViewStart.cshtml`](Views/_ViewStart.cshtml).

- **Authentication Views**  
  - The [`_ViewStart.cshtml`](Views/Auth/_ViewStart.cshtml) file includes additional Layout selection logic.  
  - Authentication views can be displayed either in the main Layout or a specific Layout displayed when users log out.


## Themes

The application supports themes for a consistent look and feel across all views.

-	Themes are managed using SCSS variables defined in the [scss](src/scss) folder.
-	Themes can be [switched dynamically at runtime](https://js.devexpress.com/DevExtreme/Guide/Themes_and_Styles/Predefined_Themes/#Switch_Between_Themes_at_Runtime/Without_Page_Reload).
-	To customize or extend themes, modify the SCSS files in the `css` or `scss` directories.
-	The theme selection can be persisted for each user. For more details, refer to [theme.ts](src/ts/theme.ts).
-	To add a new theme, create a new SCSS file with your color and style overrides and include it in the build process.

## Data Access Layer (DAL)

The application uses Entity Framework Core to manage data.

- Data is fetched from the DevExpress remote data service and stored in a SQLite database file. For details, see [DemoDataFetcher.cs](DAL/DemoDataFetcher.cs).
- For demonstration purposes, a per-user cache is implemented: [SessionDbContextMiddleware.cs](Middleware/SessionDbContextMiddleware.cs).
- First-time user connection triggers SQLite to clone in server memory.
- Only the user accesses their database copy, which is erased after a period of inactivity to free memory.
- [LocalDemoDataContext](DAL/LocalDemoDataContext.cs) contains in-memory static data for demo purposes.
- [DemoDbContext](DAL/DemoDbContext.cs) lists entities used across views.

## Documentation

You can learn more about the ASP.NET Core controls in DevExpress documentation: [Concepts](https://docs.devexpress.com/AspNetCore/400574/devextreme-based-controls/concepts/razor-syntax).

The client-side API is based on [jQuery documentation](https://api.jquery.com/) and is described in the following help topics: 
- [Get and Set Properties](https://js.devexpress.com/DevExtreme/Guide/jQuery_Components/Component_Configuration_Syntax/#Get_and_Set_Properties)
- [Call Methods](https://js.devexpress.com/DevExtreme/Guide/jQuery_Components/Component_Configuration_Syntax/#Call_Methods)
- [Get a UI Component Instance](https://js.devexpress.com/DevExtreme/Guide/jQuery_Components/Component_Configuration_Syntax/#Get_a_UI_Component_Instance)


Need help? Submit a ticker to the [DevExpress Support Center](https://www.devexpress.com/Support/Center/Question/Create).
