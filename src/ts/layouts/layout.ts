(function () {
    if (window.uitgAppContext?.LayoutController) return;

    const DRAWER_OPENED_KEY = "DevExtremeASPTemplateGallery-drawer-opened";
    const breakpoints = {
        xSmallMedia: window.matchMedia("(max-width: 599.99px)"),
        smallMedia: window.matchMedia("(min-width: 600px) and (max-width: 959.99px)"),
        mediumMedia: window.matchMedia("(min-width: 960px) and (max-width: 1279.99px)"),
        largeMedia: window.matchMedia("(min-width: 1280px)")
    };

    function getScreenSize() {
        return {
            isXSmall: breakpoints.xSmallMedia.matches,
            isSmall: breakpoints.smallMedia.matches,
            isMedium: breakpoints.mediumMedia.matches,
            isLarge: breakpoints.largeMedia.matches,
        };
    }

    function getDrawer() {
        return $("#layout-drawer").dxDrawer("instance");
    }

    function getMenu() {
        return $("#navigationTree").dxTreeView("instance");
    }

    function isNodeExpanded() {
        return breakpoints.largeMedia.matches;
    }

    function restoreDrawerOpened() {
        const isLarge = breakpoints.largeMedia.matches;
        if (!isLarge) return false;
        const state = sessionStorage.getItem(DRAWER_OPENED_KEY);
        if (state === null) return isLarge;
        return state === "true";
    }

    function saveDrawerOpened() {
        sessionStorage.setItem(DRAWER_OPENED_KEY, getDrawer().option("opened") === true ? "true" : "false");
    }

    function updateSidePanel() {
        const isXSmall = breakpoints.xSmallMedia.matches;
        const isLarge = breakpoints.largeMedia.matches;
        getDrawer().option({
            openedStateMode: isLarge ? "shrink" : "overlap",
            revealMode: isXSmall ? "slide" : "expand",
            minSize: isXSmall ? 0 : 48,
            shading: !isLarge,
            opened: isLarge
        });
        saveDrawerOpened();

        const treeMenu = getMenu();
        if (isLarge)
            treeMenu.expandAll();
        else
            treeMenu.collapseAll();
    }

    function getBaseUri() {
        const { protocol, hostname, port } = window.location;
        const baseUri = `${protocol}//${hostname}${port ? `:${port}` : ''}`;
        return baseUri.replace(/\/$/, '');
    }

    function init() {
        $.each(breakpoints, (_, media) => {
            media.addEventListener('change', (e) => {
                if (e.matches)
                    updateSidePanel();
            });
        });
        updateSidePanel();
        // @ts-expect-error  experimental feature
        window.navigation.addEventListener("navigate", (e) => {
            const treeMenu = getMenu();
            const items = treeMenu.option("items");
            if (items && items.length > 0) {
               const leafs = items.reduce((acc: any[], cur: any) => {
                   const children = Array.isArray(cur.items) ? cur.items : [];
                   return [ ...acc, ...children];
                }, []);
                const targetItem = leafs?.find((i: any) =>
                    e.destination.url === (getBaseUri() + i.path) ||
                    (e.destination.url.includes("PlanningTasks") && i.path.includes("PlanningTasks"))
                );
                treeMenu.selectItem(targetItem);
            }
        });
    }

    function navigate(url: string, delay: number) {
        if (url)
            setTimeout(function () {
                window.uitgAppContext.SPARouter.navigate(url);
            }, delay);
    }

    function onMenuButtonClick() {
        const drawer = getDrawer();
        drawer.toggle();
        saveDrawerOpened();
        const opened = drawer.option("opened");
        const treeMenu = getMenu();
        if (opened)
            treeMenu.expandAll();
        else
            treeMenu.collapseAll();
    }

    function onTreeViewItemClick(e: any) {
        const drawer = getDrawer();
        const savedOpened = restoreDrawerOpened();
        const actualOpened = drawer.option("opened");
        const treeMenu = getMenu();
        if (!actualOpened) {
            drawer.show();
            treeMenu.expandItem(e.itemData);
            const selectedItems = treeMenu.getSelectedNodeKeys();
            if (selectedItems.length)
                treeMenu.expandItem(selectedItems[0]);
        } else {
            if (e.node.children.length > 0) return;

            const willHide = !savedOpened || !breakpoints.largeMedia.matches;
            const willNavigate = !e.itemData.selected;

            if (willHide) {
                drawer.hide();
                treeMenu.collapseAll();
            }

            if (true || willNavigate)
                navigate(e.itemData.path, willHide ? 400 : 0);
        }
    }

    function onLogoutClick() {
        window.uitgAppContext.SPARouter.navigate("/Auth/Login");
    }

    window.uitgAppContext.LayoutController = {
        getScreenSize,
        getDrawer,
        getMenu,
        isNodeExpanded,
        restoreDrawerOpened,
        saveDrawerOpened,
        updateSidePanel,
        getBaseUri,
        init,
        navigate,
        onMenuButtonClick,
        onTreeViewItemClick,
        onLogoutClick
    };
})();