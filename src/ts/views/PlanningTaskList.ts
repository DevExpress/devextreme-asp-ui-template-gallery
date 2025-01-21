$(function () {
    const dateType: DevExpress.ui.dxDateBox.DateType = 'datetime';
    DevExpress.ui.notify(`message from TS: ${dateType}`)
})

function addTask() {
    console.log("Add Task for Planning Task Grid");
}

function tabValueChange(e: DevExpress.ui.dxTabs.ItemClickEvent) {
    console.log("tabValueChanged", e);
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