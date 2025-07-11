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
        if (currentView === 'Grid') {
            $('#tasks-grid').dxDataGrid('instance').refresh();
        } else if(currentView === 'Gantt') {
            $('#tasks-gantt').dxGantt('instance').refresh();
        }else if(currentView === 'Kanban') {
            $('#kanban-scroll-view').dxScrollView('instance').update();
        }
    }

    function chooseColumnDataGrid() {
        $('#tasks-grid').dxDataGrid('instance').showColumnChooser();
        // Implementation for chooseColumnDataGrid
    }

    function exportToPdf() {
        // @ts-expect-error no typings included
        window.jsPDF = window.jspdf.jsPDF;
        // @ts-expect-error no typings included
        applyPlugin(window.jsPDF);
        // Implementation for exportToPdf
        let currentView = getCurrentView();
        if (currentView === 'Grid') {
            // @ts-expect-error no typings included
            const doc = new jsPDF();
            DevExpress.pdfExporter.exportDataGrid({
                jsPDFDocument: doc,
                component: $('#tasks-grid').dxDataGrid('instance'),
            }).then(() => {
                doc.save('Tasks.pdf');
            });
        } else {
            DevExpress.pdfExporter.exportGantt(
                {
                    component: $('#tasks-gantt').dxGantt('instance'),
                    // @ts-expect-error no typings included
                    createDocumentMethod: (args) => new jsPDF(args),
                },
            ).then((doc) => doc.save('gantt.pdf'));
        }
    }

    function exportToXlsx() {
        // Implementation for exportToXlsx

        // @ts-expect-error no typings included
        const workbook = new ExcelJS.Workbook();
        const worksheet = workbook.addWorksheet('Main sheet');

        DevExpress.excelExporter.exportDataGrid({
            component: $('#tasks-grid').dxDataGrid('instance'),
            worksheet,
            autoFilterEnabled: true,
        }).then(() => {
            workbook.xlsx.writeBuffer().then((buffer: any) => {
                // @ts-expect-error no typings included
                saveAs(new Blob([buffer], { type: 'application/octet-stream' }), 'DataGrid.xlsx');
            });
        });
    }

    function searchDataGrid(e: DevExpress.ui.dxTextBox.ValueChangedEvent) {
        // Implementation for searchDataGrid
        $('#tasks-grid').dxDataGrid('instance').searchByText(e.component.option('text') ?? '');
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
