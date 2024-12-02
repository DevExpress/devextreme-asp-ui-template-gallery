const navigation = (function () {

    const TOP_LEVEL_VIEW_SELECTOR = "#view-placeholder";
    const NAVIGATABLE_VIEW_SELECTOR = "#navigatable-view-placeholder";
    const NAVIGATOR_VIEW_NAME = "NavigationRoot";
    const GET_NAVDATA_URL = "Navigation/GetNavigationData";
    const GET_VIEW_URL = "Navigation/GetComponent";
    const startupView = "Tasks";

    let navigationData = null;
    let currentViewName;

    function getNavigationData(onlyWithNavigation) {
        return onlyWithNavigation ? navigationData.filter(n => !n.HideNavigation) : navigationData;
    }

    $.get(GET_NAVDATA_URL, function (navData) {
        navigationData = navData;
        if (window.location.pathname === "/") {
            loadView(startupView, false);
        } else {
            const viewName = window.location.pathname.replace("/", "");
            loadView(viewName);
        }
    });

    history.replaceState(startupView, "", document.location.href);

    window.addEventListener('popstate', (e) => {
        if (e.state) {
            loadView(e.state, false);
        }
    });

    function loadView(viewName, pushHistory = true) {
        if (viewName === currentViewName) return;
        const navItem = navigationData.find(n => n.ViewName === viewName);
        if (navItem) {
            const navigationVisible = currentViewName
                ? !navigationData.find(n => n.ViewName === currentViewName).HideNavigation
                : false;
            if (navigationVisible) {
                loadViewCore(navItem.ViewName, navItem.HideNavigation ? TOP_LEVEL_VIEW_SELECTOR : NAVIGATABLE_VIEW_SELECTOR);
            } else {
                if (navItem.HideNavigation) {
                    loadViewCore(navItem.ViewName, TOP_LEVEL_VIEW_SELECTOR);
                } else {
                    loadViewCore(NAVIGATOR_VIEW_NAME, TOP_LEVEL_VIEW_SELECTOR)
                        .then(() => {
                            loadViewCore(navItem.ViewName, NAVIGATABLE_VIEW_SELECTOR);
                        });
                }
            }
            if (pushHistory)
                history.pushState(navItem.ViewName, "", navItem.ViewName);
            currentViewName = viewName;
        } else {
            console.error(`Cannot navigate to '${viewName}'. This view doesn't exist.`);
        }
    }

    function loadViewCore(viewName, targetSelector) {
        const contentLoaded = new Promise(res => {
            $.get(GET_VIEW_URL, { componentName: viewName },
                function (data) {
                    $(targetSelector).html(data);
                    res();
                }
            );
        });
        return contentLoaded;
    }

    function openInitialView() {
        if (navigationData) {
            loadViewByUrl(window.location.pathname);
        }
    };

    return {
        getNavigationData,
        openInitialView,
        loadView
    }
})();