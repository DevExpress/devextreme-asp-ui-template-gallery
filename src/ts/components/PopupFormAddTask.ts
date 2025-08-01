(function () {
    if (window.uitgAppContext?.PopupFormController) return;

    let saveMode: TaskFormSaveMode = "insert";

    function onSaveTask() {
        const newData: EmployeeTask = getTaskForm().option("formData");
        if (saveMode === "insert")
            window.uitgAppContext.PlanningTasksController?.addNewTask(newData);
        else
            window.uitgAppContext.PlanningTasksController?.editTask(newData);
        onCancelTask();
    }

    function show(mode: TaskFormSaveMode): JQueryPromise<boolean> {
        saveMode = mode;
        return getPopupForm().show();
    }

    function getPopupForm() {
        return $("#formPopup").dxPopup("instance");
    }

    function getTaskForm() {
        return $("#taskFormDetails").dxForm("instance")
    }

    function onCancelTask() {
        getTaskForm().clear();
        getPopupForm().hide();
    }

    function getSizeQualifier(width: number) {
        if (width <= 420) return 'xs';
        if (width <= 992) return 'sm';
        if (width < 1200) return 'md';
        return 'lg';
    }

    window.uitgAppContext.PopupFormController = {
        getSizeQualifier,
        getPopupForm,
        getTaskForm,
        onSaveTask,
        onCancelTask,
        show
    }
})()