(function () {
    if (window.uitgAppContext?.PlanningTasksController) return;

    let currentView: string = 'Grid';
    const paths = window.location.pathname.split('/').filter(p => p.length > 0);
    if (paths.length === 3) {
        currentView = paths.pop() || 'Grid';
    }

    function addTask() {
        DevExpress.ui.notify("Add Task for Planning Task Grid");
    }

    function tabValueChange(e: DevExpress.ui.dxTabs.ItemClickEvent) {
        let url: string = `/Home/PlanningTasks/${e.itemData.value}`;
        currentView = e.itemData.value;
        window.uitgAppContext.SPARouter.navigate(url);
    }

    function getTabsWidth() {
        const { isXSmall } = window.uitgAppContext.LayoutController!.getScreenSize();
        return isXSmall ? 220 : 'auto';
    }

    function getCurrentView() {
        return currentView;
    }

    function reload() {
        // Implementation for reload
    }

    function chooseColumnDataGrid() {
        // Implementation for chooseColumnDataGrid
    }

    function exportToPdf() {
        // Implementation for exportToPdf
    }

    function exportToXlsx() {
        // Implementation for exportToXlsx
    }

    function searchDataGrid(e: any) {
        // Implementation for searchDataGrid
    }

    window.uitgAppContext.PlanningTasksController = {
        addTask,
        tabValueChange,
        getTabsWidth,
        getCurrentView,
        reload,
        chooseColumnDataGrid,
        exportToPdf,
        exportToXlsx,
        searchDataGrid
    };
})();
