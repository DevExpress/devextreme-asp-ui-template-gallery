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

    function tabValueChange(e: any) {
        const url = "../Home/GetPlanningTasks";
        currentView = e.itemData.value;

        $("#planning-load-panel").dxLoadPanel("show");
        $.get(`${url}${currentView}`).then(data => {
            $(".planning-tasks-content").html(data);
            $("#planning-load-panel").dxLoadPanel("hide");
            updateToolbarItems(currentView, e.itemIndex);
        });
    }

    function updateToolbarItems(currentView: string, selectedIndex: number) {
        const toolbarInstance = $("#tasksToolbar").dxToolbar("instance");
        const items = toolbarInstance.option("items");
        if (!items) return;

        items.forEach(item => {
            switch (item.name) {
                case "ExportExcel":
                case "SearchItem":
                case "ColumnChooser":
                    item.disabled = currentView !== 'Grid';
                    break;
                case "ExportPDF":
                    item.disabled = currentView === 'Kanban';
                    break;
            }
        });

        toolbarInstance.option({ items: [...items] });

        const tabsInstance = $("#tasksTabs").dxTabs("instance");
        tabsInstance.option({ selectedIndex: selectedIndex });
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
