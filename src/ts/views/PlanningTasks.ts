(function () {
    if (window.uitgAppContext?.PlanningTasksController) return;

    let currentView: string = 'Grid';
    const paths = window.location.pathname.split('/').filter(p => p.length > 0);
    if (paths.length === 3) {
        currentView = paths.pop() || 'Grid';
    }

    function showPopupToEditTask(taskData: EmployeeTask) {
        window.uitgAppContext.PopupFormController.show("update").then(() => {
            window.uitgAppContext.PopupFormController.getTaskForm().getEditor("Owner")?.option("readOnly", currentView !== "Grid");
            window.uitgAppContext.PopupFormController.getTaskForm().updateData(taskData);
        });
    }

    function showPopupToAddTask(taskData: EmployeeTask) {
        window.uitgAppContext.PopupFormController.show("insert").then(() => {
            const inputData = taskData || {};
            initNewTask(inputData);
            window.uitgAppContext.PopupFormController.getTaskForm().getEditor("Owner")?.option("readOnly", currentView !== "Grid");
            window.uitgAppContext.PopupFormController.getTaskForm().updateData(inputData);
        });
    }

    function initNewTask(taskData: EmployeeTask) {
        taskData.Priority = "Low";
        taskData.Status = taskData.Status || "Open";
        const start = new Date();
        start.setHours(0, 0, 0, 0);
        const end = new Date();
        end.setHours(23, 59, 59, 999);
        taskData.StartDate = start;
        taskData.DueDate = end;
        if (currentView !== "Grid") {
            taskData.Owner = window.uitgAppContext.Constants.DemoFilteredOwnerName;
        }
    }

    function addNewTask(taskData: EmployeeTask) {
        if (currentView === "Grid") {
            const grid = $('#tasks-grid').dxDataGrid("instance");
            grid.getDataSource().store().insert(taskData).then(() => { grid.refresh(); });
        } else if (currentView === "Gantt") {
            const gantt = $('#tasks-gantt').dxGantt("instance");
            gantt.insertTask(taskData);
        } else if (currentView === "Kanban") {
            window.uitgAppContext.KanbanTasksController?.addTask(taskData);
        }
    }

    function editTask(taskData: EmployeeTask) {
        if (currentView === "Grid") {
            const grid = $('#tasks-grid').dxDataGrid("instance");
            grid.getDataSource().store().update(taskData.TaskId, taskData).then(() => { grid.refresh(); });
        } else if (currentView === "Gantt") {
            const gantt = $('#tasks-gantt').dxGantt("instance");
            gantt.updateTask(taskData.TaskId, taskData);
        } else if (currentView === "Kanban") {
            window.uitgAppContext.KanbanTasksController?.updateTask(taskData);
        }
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
        if (currentView === 'Grid') {
            $('#tasks-grid').dxDataGrid('instance').refresh();
        } else if(currentView === 'Gantt') {
            $('#tasks-gantt').dxGantt('instance').refresh();
        } else if(currentView === 'Kanban') {
            window.uitgAppContext.SPARouter.navigate("/Home/PlanningTasks/Kanban");
        }
    }

    function chooseColumnDataGrid() {
        $('#tasks-grid').dxDataGrid('instance').showColumnChooser();
    }

    function exportToPdf() {
        // @ts-expect-error no typings included
        window.jsPDF = window.jspdf.jsPDF;
        // @ts-expect-error no typings included
        applyPlugin(window.jsPDF);
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
        $('#tasks-grid').dxDataGrid('instance').searchByText(e.component.option('text') ?? '');
    }

    function ganttBeforeSend(operation: string, ajaxSettings: JQuery.PlainObject) {
        if (operation === "insert") {
            const values = JSON.parse(ajaxSettings.data.values);
            values.Owner = window.uitgAppContext.Constants.DemoFilteredOwnerName;
            ajaxSettings.data.values = JSON.stringify(values);
        }
    }

    window.uitgAppContext.PlanningTasksController = {
        showPopupToEditTask,
        showPopupToAddTask,
        addNewTask,
        editTask,
        tabValueChange,
        getTabsWidth,
        getCurrentView,
        reload,
        chooseColumnDataGrid,
        exportToPdf,
        exportToXlsx,
        searchDataGrid,
        ganttBeforeSend
    };
})();
