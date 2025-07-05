class PlanningTasksController {
    private currentView: string = 'Grid';
    
    constructor() {
        const paths = window.location.pathname.split('/').filter(p => p.length > 0);
        if (paths.length === 3) {
            this.currentView = paths.pop()!;
        }
    }

    addTask(): void {
        //$("#taskFormPopup").dxPopup("instance").show();
        DevExpress.ui.notify("Add Task for Planning Task Grid");
    }

    tabValueChange = (e: DevExpress.ui.dxTabs.ItemClickEvent): void => {
        const url: string = "../Home/GetPlanningTasks";
        this.currentView = e.itemData.value;

        $("#planning-load-panel").dxLoadPanel("show");
        $.get(`${url}${this.currentView}`).then(data => {
            $(".planning-tasks-content").html(data);
            $("#planning-load-panel").dxLoadPanel("hide");
            this.updateToolbarItems(this.currentView, e.itemIndex);
            if (this.currentView === 'Kanban') {
                loadKanban();
            }
        });
    }

    private updateToolbarItems(currentView: string, selectedIndex: number): void {
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

        toolbarInstance.option({ "items": [...items] });
        const tabsInstance = $("#tasksTabs").dxTabs("instance");
        tabsInstance.option({ selectedIndex: selectedIndex });
    }

    getTabsWidth(): number | string {
        //@ts-ignore
        const { isXSmall } = LayoutController.getScreenSize();
        return isXSmall ? 220 : 'auto';
    }

    getCurrentView(): string {
        return this.currentView;
    }

    reload(): void {
        // Implementation for reload
    }

    chooseColumnDataGrid(): void {
        // Implementation for chooseColumnDataGrid
    }

    exportToPdf(): void {
        // Implementation for exportToPdf
    }

    exportToXlsx(): void {
        // Implementation for exportToXlsx
    }

    searchDataGrid(e: DevExpress.ui.dxTextBox.InputEvent): void {
        // Implementation for searchDataGrid
    }

    onCancelClick(e: DevExpress.ui.dxButton.ClickEvent) {

    }

    onSaveClick(e: DevExpress.ui.dxButton.ClickEvent) {

    }
}

