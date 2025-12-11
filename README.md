# DevExtreme UI Template Gallery for ASP.NET Core

This repository contains responsive UI Templates that replicate popular UI/UX patterns for web-based Line-of-Business applications. DevExtreme UI Templates deliver responsive layouts using [DevExtreme ASP.NET Core UI controls](https://www.devexpress.com/products/net/controls/asp/core.xml).

![UI Template Gallery Overview](/ui-template-gallery.png)

Note that components use auto-generated data. You can use these UI Templates in your project and modify them based on your business requirements.

## License

**DevExtreme UI Template Gallery is released as a MIT-licensed (free and open-source) add-on to DevExtreme. [Get your 30-day free trial now!](http://js.devexpress.com/Buy/)**

Refer to the following page for additional information on DevExtreme Licensing: [Pricing, Compare Subscriptions](https://js.devexpress.com/Licensing/).

## Templates

The UI Template Gallery includes the following templates:

- [**Task List**](/Views/PlanningTasks/PlanningTasksGrid.cshtml). A task/project management layout with a list, kanban board, and a gantt view. Allows you to add, edit, delete, view, search, and analyze tasks. This template includes:
  - [DataGrid](https://demos.devexpress.com/ASPNetCore/Demo/DataGrid/Overview/)
  - [Gantt](https://demos.devexpress.com/ASPNetCore/Demo/Gantt/Overview)
  - [Sortable](https://demos.devexpress.com/ASPNetCore/Demo/Sortable/Kanban)
  - [Toolbar](https://demos.devexpress.com/ASPNetCore/Demo/Toolbar/Overview)
  - [Tabs](https://demos.devexpress.com/ASPNetCore/Demo/Tabs/Overview)
  - [Popup](https://demos.devexpress.com/ASPNetCore/Demo/Popup/Overview)
  - [Form](https://demos.devexpress.com/ASPNetCore/Demo/Form/Overview)
- [**User Profile**](/Views/CommonUserProfile/UserProfile.cshtml). A form used to edit user profile. This template includes:
  - [Form](https://demos.devexpress.com/ASPNetCore/Demo/Form/Overview)
  - [Popup](https://demos.devexpress.com/ASPNetCore/Demo/Popup/Overview)
  - [Button](https://demos.devexpress.com/ASPNetCore/Demo/Button/PredefinedTypes)
  - [TextBox](https://demos.devexpress.com/ASPNetCore/Demo/TextBox/Overview)
  - [DateBox](https://demos.devexpress.com/ASPNetCore/Demo/DateBox/Overview)
  - [FileUploader](https://demos.devexpress.com/ASPNetCore/Demo/FileUploader/FileSelection)
- [**Sign In Form**](/Views/Auth/Login.cshtml).
- [**Register Form**](/Views/Auth/SignUp.cshtml).
- [**Reset Password Form**](/Views/Auth/ForgotPassword.cshtml).

## Installation

### Windows

- Clone this repository.
- Open the solution in Visual Studio 2022 or later.
- If DevExpress Local Nuget feed did not restore automatically, [get the key](https://docs.devexpress.com/GeneralInformation/116042/nuget/obtain-your-nuget-feed-credentials#devexpresscom-online-nuget-feed-any-os) and update the [Nuget.config](https://github.com/DevExpress/devextreme-asp-ui-template-gallery/blob/dev/NuGet.config) file.
- Run the project (`F5`).

### Mac OS

- Clone this repository.
- Open the project folder in an IDE that supports `.NET` (e.g. Visual Studio Code).
- [Get the key](https://docs.devexpress.com/GeneralInformation/116042/nuget/obtain-your-nuget-feed-credentials#devexpresscom-online-nuget-feed-any-os) and update the [Nuget.config](https://github.com/DevExpress/devextreme-asp-ui-template-gallery/blob/dev/NuGet.config) file.
- If .NET SDK if necessary ([download](https://dotnet.microsoft.com/en-us/download)).
- Run the `dotnet run` command in the terminal from the project root folder.

## Project Structure

- **Controllers:** Contains actions connected to Views.
- **Views:** Contains layout pages.
  - *Auth:* Authorization-related forms.
  - *Shared:* Root layout logic.
  - Other folders match the names of individual views.
- **Models:** Contains application data models.
- **DAL** and **Services:** Implement data processing logic.
- **Utils:** Helper methods for common tasks.
- **src:** Client-side code. This folder mirrors the structure of views and layouts.

## Client-side Libraries and Build Process

- [NPM](https://www.npmjs.com/) manages client libraries: [jQuery](https://www.npmjs.com/package/jquery) and [DevExtreme](https://www.npmjs.com/package/devextreme).
- Styles are written in SCSS.
- Client logic is written in TypeScript.

Run `F5` to start MSBuild. This will execute the following build steps:

- Check Node.js and restore NPM packages
- Copy required jQuery/DevExtreme assets (scripts, icons, fonts) from `node_modules` to `wwwroot`.
- Compile SCSS files to CSS via `AspNetCore.SassCompiler`. The output file is created in the  `wwwroot` folder. You can change the target folder in `appsettings.json`.
- Compile TypeScript to JavaScript via `Microsoft.TypeScript.MSBuild`. The output script is created in `wwwroot`.

To add extra third-party client resources:

- Install packages using NPM.
- Add assets to the [`CopyTask`](https://github.com/DevExpress/devextreme-asp-ui-template-gallery/blob/eca6039179487322bdb682fa68558d34799ca9ec/devextreme-asp-ui-template-gallery.csproj#L18).

## Views

The application layout contains a top toolbar, left navigation menu, and a main content view.

- Navigation menu is grouped. If you click an item, the corresponding view appears in the content area.
- Views may include unique style and script files used for modularity and customization.

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
  - If **a target view Layout differs** from the current view Layout, the router performs a **full page reload**.  
  - If **Layouts match**, the router **dynamically replaces content**, preserving header, footer, and navigation menu.

- **URL Handling**  
  - The router calls the [`pushState`](https://developer.mozilla.org/en-US/docs/Web/API/History/pushState) method and the [`popstate`](https://developer.mozilla.org/en-US/docs/Web/API/Window/popstate_event) event to modify and track browser history.  
  - When you navigate through pages, the router:
    1. Calls `pushState` to add the target URL.  
    2. Loads target page content.  
    3. Compares **Layouts** for current and target pages using custom `data-target-placeholder-id` attribute.  
    4. Updates content if Layouts match, otherwise, reloads the page.  

For implementation details, refer to the following file: [router.ts](src/ts/router.ts).

### Layout management

- **Partial Updates**  
  - [`_LayoutPartial.cshtml`](Views/Shared/_LayoutPartial.cshtml) is used to send partial updates from the server.  
  - The logic that selects between `_LayoutPartial` and `_Layout` is defined in [`_ViewStart.cshtml`](Views/_ViewStart.cshtml).

- **Authentication Views**  
  - The [`_ViewStart.cshtml`](Views/Auth/_ViewStart.cshtml) file includes additional Layout selection logic.  
  - Authentication views can be displayed either in the main Layout or a specific Layout (displayed when users log out).

## Themes

The application supports [themes](https://docs.devexpress.com/AspNetCore/401270/devextreme-based-controls/concepts/themes) for a consistent look & feel across all views.

-	Themes are managed using SCSS variables defined in the [scss](src/scss) folder.
-	Themes can be [switched dynamically at runtime](https://js.devexpress.com/DevExtreme/Guide/Themes_and_Styles/Predefined_Themes/#Switch_Between_Themes_at_Runtime/Without_Page_Reload).
-	To customize or extend themes, modify SCSS files in the `css` or `scss` directories.
-	Theme selection can be persisted for each user. For additional information in this regard, refer to the following file: [theme.ts](src/ts/theme.ts).
-	To add a new theme, create a new SCSS file with your color and style overrides and include it in the build process.

## Data Access Layer (DAL)

The application uses Entity Framework Core to manage data.

- Data is fetched from the DevExpress remote data service and stored in an SQLite database file ([DemoDataFetcher.cs](DAL/DemoDataFetcher.cs)).
- For demonstration purposes, a per-user cache is implemented ([SessionDbContextMiddleware.cs](Middleware/SessionDbContextMiddleware.cs)).
- First-time user connection triggers SQLite to clone in server memory.
- Only the user can access their database copy. This copy is erased after a period of inactivity to free memory.
- [LocalDemoDataContext](DAL/LocalDemoDataContext.cs) contains in-memory static data for demo purposes.
- [DemoDbContext](DAL/DemoDbContext.cs) lists entities used across views.

## Documentation

To learn more about DevExpress ASP.NET Core controls, please review the following: 

- [Get Started](https://docs.devexpress.com/AspNetCore/401026/devextreme-based-controls/get-started/configure-a-visual-studio-project)
- [Concepts](https://docs.devexpress.com/AspNetCore/400574/devextreme-based-controls/concepts/razor-syntax)

Client-side APIs are based on [jQuery](https://api.jquery.com/) and are described in the following help topics:
- [Get and Set Properties](https://js.devexpress.com/DevExtreme/Guide/jQuery_Components/Component_Configuration_Syntax/#Get_and_Set_Properties)
- [Call Methods](https://js.devexpress.com/DevExtreme/Guide/jQuery_Components/Component_Configuration_Syntax/#Call_Methods)
- [Get a UI Component Instance](https://js.devexpress.com/DevExtreme/Guide/jQuery_Components/Component_Configuration_Syntax/#Get_a_UI_Component_Instance)


Need help? Submit a ticket to the [DevExpress Support Center](https://www.devexpress.com/Support/Center/Question/Create).
