function addTask() {
    console.log("Add Task for Planning Task Grid");
}

function tabValueChange(e: DevExpress.ui.dxTabs.ItemClickEvent) {
    let url: string = "../Home/GetPlanningTasks";
    if (e.itemData.text === "List") url += "Grid";
    if (e.itemData.text === "Kanban Board") url += "Kanban";
    if (e.itemData.text === "Gantt") url += "Gantt";
    $("#planning-load-panel").dxLoadPanel("show");
    $.get(url).then(data => {
        $(".planning-tasks-content").html(data)
        $("#planning-load-panel").dxLoadPanel("hide");
    });
}

function reload() {

}

function chooseColumnDataGrid() {

}

function exportToPdf() {

}

function exportToXlsx() {

}

function searchDataGrid(e: DevExpress.ui.dxTextBox.InputEvent) {

}