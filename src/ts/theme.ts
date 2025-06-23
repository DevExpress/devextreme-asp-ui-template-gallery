class ThemeController {
 
  init() {
    DevExpress.ui.themes.current(window.localStorage.getItem("dx-theme") || "fluent.blue.light");
    window.localStorage.setItem("dx-theme", DevExpress.ui.themes.current());
  }

  getTheme() {
    return DevExpress.ui.themes.current();
  }

  themeButtonOnInitialized = (e: DevExpress.ui.dxButton.InitializedEvent) => {
    const icon = this.getTheme() === "fluent.blue.light" ? "moon" : "sun";
    e.component?.option("icon", icon);
  }

  themeSwitcherOnClick = (e: DevExpress.ui.dxButton.ClickEvent) => {
    if (this.getTheme() === "fluent.blue.light") {
      e.component.option("icon", "sun");
      DevExpress.ui.themes.current("fluent.blue.dark");
    } else {
      e.component.option("icon", "moon");
      DevExpress.ui.themes.current("fluent.blue.light");
    }
    window.localStorage.setItem("dx-theme",  this.getTheme());
  }
}