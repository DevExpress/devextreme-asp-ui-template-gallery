(function () {
    if (window.uitgAppContext?.PopupFormController) return;
    function onSaveTask() {
        throw new Error("Not implemented");
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
        onCancelTask
    }
})()