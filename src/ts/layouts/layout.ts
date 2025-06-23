class LayoutController {
    private DRAWER_OPENED_KEY = "DevExtremeASPTemplateGallery-drawer-opened";
    private breakpoints = {
        xSmallMedia: window.matchMedia("(max-width: 599.99px)"),
        smallMedia: window.matchMedia("(min-width: 600px) and (max-width: 959.99px)"),
        mediumMedia: window.matchMedia("(min-width: 960px) and (max-width: 1279.99px)"),
        largeMedia: window.matchMedia("(min-width: 1280px)")
    };

    getScreenSize() {
        return {
            isXSmall: this.breakpoints.xSmallMedia.matches,
            isSmall: this.breakpoints.smallMedia.matches,
            isMedium: this.breakpoints.mediumMedia.matches,
            isLarge: this.breakpoints.largeMedia.matches,
        };
    }

    getDrawer() {
        return $("#layout-drawer").dxDrawer("instance");
    }

    getMenu() {
        return $("#navigationTree").dxTreeView("instance");
    }

    isNodeExpanded = () => {
        return this.breakpoints.largeMedia.matches;
    }

    restoreDrawerOpened() {
        const isLarge = this.breakpoints.largeMedia.matches;
        if (!isLarge) return false;
        const state = sessionStorage.getItem(this.DRAWER_OPENED_KEY);
        if (state === null) return isLarge;
        return state === "true";
    }

    saveDrawerOpened() {
        sessionStorage.setItem(this.DRAWER_OPENED_KEY, this.getDrawer().option("opened") === true ? "true" : "false");
    }

    updateSidePanel() {
        const isXSmall = this.breakpoints.xSmallMedia.matches;
        const isLarge = this.breakpoints.largeMedia.matches;
        this.getDrawer().option({
            openedStateMode: isLarge ? "shrink" : "overlap",
            revealMode: isXSmall ? "slide" : "expand",
            minSize: isXSmall ? 0 : 48,
            shading: !isLarge,
            opened: isLarge
        });
        this.saveDrawerOpened();

        const treeMenu = this.getMenu();
        if (isLarge)
            treeMenu.expandAll();
        else
            treeMenu.collapseAll();
    }
    getBaseUri() {
        const { protocol, hostname, port } = window.location;
        const baseUri = `${protocol}//${hostname}${port ? `:${port}` : ''}`;
        return baseUri.replace(/\/$/, '');
    }
    init() {
        $.each(this.breakpoints, (_, media) => {
            media.addEventListener('change', (e) => {
                if (e.matches)
                    this.updateSidePanel();
            });
        });
        this.updateSidePanel();
        // @ts-expect-error  experimental feature
        window.navigation.addEventListener("navigate", (e: any) => {
            const treeMenu = this.getMenu();
            const items = treeMenu.option("items");
            if (items && items.length > 0) {
               const leafs = items.reduce((acc, cur) => {
                   const children = Array.isArray(cur.items) ? cur.items : [];
                   // @ts-expect-error to-do, no correct type found
                   return [ ...acc, ...children];
                }, []);
               
                const targetItem = leafs?.find((i: any) => e.destination.url === (this.getBaseUri() + i.path));
                treeMenu.selectItem(targetItem);
            }
        });
    }

    navigate(url: string, delay: number) {
        if (url)
            setTimeout(function () {
                window.aspUITGlobal.SPARouter.navigate(url);
            }, delay);
    }

    onMenuButtonClick = () => {
        const drawer = this.getDrawer();
        drawer.toggle();
        this.saveDrawerOpened();
        const opened = drawer.option("opened");
        const treeMenu = this.getMenu();
        if (opened)
            treeMenu.expandAll()
        else
            treeMenu.collapseAll()
    }

    onTreeViewItemClick = (e: any) => {
        const drawer = this.getDrawer();
        const savedOpened = this.restoreDrawerOpened();
        const actualOpened = drawer.option("opened");
        const treeMenu = this.getMenu();
        if (!actualOpened) {
            drawer.show();
            treeMenu.expandItem(e.itemData);
            const selectedItems = treeMenu.getSelectedNodeKeys();
            if (selectedItems.length)
                treeMenu.expandItem(selectedItems[0]);
        } else {
            if (e.node.children.length > 0) return;

            const willHide = !savedOpened || !this.breakpoints.largeMedia.matches;
            const willNavigate = !e.itemData.selected;

            if (willHide) {
                drawer.hide();
                treeMenu.collapseAll();
            }

            if (true || willNavigate)
                this.navigate(e.itemData.path, willHide ? 400 : 0);
        }
    }

    onLogoutClick() {
        window.aspUITGlobal.SPARouter.navigate("/Auth/Login");
    }
}