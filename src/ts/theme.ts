(function () {
    if (window.uitgAppContext?.ThemeController) return;

    function init() {
        DevExpress.ui.themes.current(window.localStorage.getItem("dx-theme") || "fluent.blue.light");
        window.localStorage.setItem("dx-theme", DevExpress.ui.themes.current());
    }

    function getTheme() {
        return DevExpress.ui.themes.current();
    }

    function themeButtonOnInitialized(e: any) {
        const icon = getTheme() === "fluent.blue.light" ? "moon" : "sun";
        e.component?.option("icon", icon);
    }

    function themeSwitcherOnClick(e: any) {
        if (getTheme() === "fluent.blue.light") {
            e.component.option("icon", "sun");
            DevExpress.ui.themes.current("fluent.blue.dark");
        } else {
            e.component.option("icon", "moon");
            DevExpress.ui.themes.current("fluent.blue.light");
        }
        window.localStorage.setItem("dx-theme", getTheme());
    }

    window.uitgAppContext.ThemeController = {
        init,
        getTheme,
        themeButtonOnInitialized,
        themeSwitcherOnClick
    };
})();