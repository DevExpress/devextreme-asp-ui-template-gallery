# DevExtreme UI Template Gallery for ASP.NET Core

## Installation

Download the example and use Visual Studio 2022 to open the project. 

## Project Structure

The project contains the following sections:
- Controllers contains actions connected with `Views`
- Views holds structure of the current pages within an application.
  * Auth contains all code related to authorization forms
  * Shared contains logic related to root layots
  * Other folders are equal to name of corresponding pages in the application itself 
- All client-side source code is located in `src` . This folder repeats structure of views and layouts.

### Client-side resources and bundling

This project uses [NPM](https://www.npmjs.com/) to install client-side libraries: jQuery and DevExtreme. All application styles are built from SCSS and TypeScript is used to write client-side logic. 

The project doesn't automatically restore NPM packages. You need to run `npm i` before the first build. 
Then run `F5` and MSBuild will execute the following tasks

- `CopyTask` for jQuery and DevExtreme scripts, icons, and fonts from `node_modules` and moves them to `wwwroot`
- `AspNetCore.SassCompiler` bundles required SCSS scripts into CSS files and moves them to `wwwroot`. You can define/change this config in `appsettings.json`
- `Microsoft.TypeScript.MSBuild` compiles all TS to JS and moves files to `wwwroot` 

### Add additional 3rd-party client-side resources 

To add additional scripts/styles to application it's necessary to install corresponding NPM packages first. 
Then add corresponding files to the `CopyTask`: [link](https://github.com/artem-kurchenko/devextreme-asp-ui-template-gallery/blob/d452b4ad4b26830aec70d3527a7a3a57378b36b9/devextreme-asp-ui-template-gallery.csproj#L11)

### Add more Pages with additional features/components (aka code rules)

- Create a new folder for each group of pages you plan to add to navigation
- If a page includes multiple components, you need to create sub-folders to add partial views to separate logic.
- Partial and layout views should start with the "_" symbol. 
- All code should be written in TS and styles defined in SCSS. You need to create corresponding files in `src`. 
- You need to link scripts and styles as JS/CSS files into corresponding views (e.g. see [link](https://github.com/artem-kurchenko/devextreme-asp-ui-template-gallery/blob/d452b4ad4b26830aec70d3527a7a3a57378b36b9/Views/PlanningTasks/PlanningTasks.cshtml#L2))

### How to add a new view
1) Create a new subdirectory in 'Views' for your new view.
2) Create a regular view in this directory.
3) Open HomeController and add a method that returns the new view.
4) Find TreeView items Razor declaration in Shared/Layout.cshtml and add a new item for your view.

How to add TS scripts for the view

5) Create myview.ts file in src/ts/views.
6) Link myview.js file in the view's code. This JS file will be generated based on the TS file during the build process.
   ```html
   <script src="~/js/views/myview.js"></script>
   ```
  
### How does the Router work? 
We implemented a custom router to navigate between views. The router combines regular and SPA navigation between views. The router reloads pages if a target view's Layout differs from the current view's Layout. If the Layout is the same, footer, header, and the left-side navigation bar remains visible and only the content part is dynamically replaced.
 
Router uses the [pushstate](https://developer.mozilla.org/en-US/docs/Web/API/History/pushState) method and [popstate](https://developer.mozilla.org/en-US/docs/Web/API/Window/popstate_event) event to handle URL modifications. It calls pushstate to push the target URL and then decides if it is necessary to reload the page. For this, the router loads content of the target page and checks if its Layout part equals to the Layout part of the current page. We use the custom "data-target-placeholder-id" attribute for comparison. If the Layout pages are the same, the loaded content replaces the content element. If the Layout pages are different, the page is reloaded.
 
If the page is not reloaded, the lightweight Layout file is selected for the loaded content. This logic is implemented in the _ViewStart.cshtml files. The _ViewStart.cshtml file in the Views/Auth directory has additional Layout selection logic since Auth views can be shown in the main Layout and when users log out.



## Development server

Use the Visual Studio `Run (F5)` command to run the project.

## Further help

You can learn more about the ASP.NET Core components' syntax in our documentation: [Concepts](https://docs.devexpress.com/AspNetCore/400574/devextreme-based-controls/concepts/razor-syntax)
The client-side API is based on jQuery [jQuery documentation](https://api.jquery.com/) and described in the following topics: 
* [Get and Set Properties](https://js.devexpress.com/DevExtreme/Guide/jQuery_Components/Component_Configuration_Syntax/#Get_and_Set_Properties)
* [Call Methods](https://js.devexpress.com/DevExtreme/Guide/jQuery_Components/Component_Configuration_Syntax/#Call_Methods)
* [Get a UI Component Instance](https://js.devexpress.com/DevExtreme/Guide/jQuery_Components/Component_Configuration_Syntax/#Get_a_UI_Component_Instance)

To get more help on DevExtreme submit an issue in the [Support Center](https://www.devexpress.com/Support/Center/Question/Create)
