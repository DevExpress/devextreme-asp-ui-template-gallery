const boardMenuItems = [
    { text: 'Add card' },
    { text: 'Copy list' },
    { text: 'Move list' },
];

const STATUS_ITEMS = ['Open', 'In Progress', 'Deferred', 'Completed'];

$.get("/api/Tasks", function (data: any) {
    console.log("Filtered:", data);

    $.ajax({
        url: '/Home/TaskMainSortable',
        type: 'POST',
        data: { filteredTasks: JSON.stringify(data.data) },
        success: function (cont: any) {
            $("#kanban-load-panel").dxLoadPanel("instance").hide();
            $("#kanban-sortable-id").html(cont);

            $("#planning-tasks-toolbar").dxToolbar('instance').repaint();
            $("#planning-tasks-tabs").dxTabs('instance').option("selectedIndex", 1);
        },
        error: function (xhr) {
            console.error('Error:', xhr.status, xhr.statusText, xhr.responseText);
        }
    });
});

const reorder = <T,>(items: T[], item: T, fromIndex: number, toIndex: number) => {
    let result = items;
    if (fromIndex >= 0) {
        result = [...result.slice(0, fromIndex), ...result.slice(fromIndex + 1)];
    }

    if (toIndex >= 0) {
        result = [...result.slice(0, toIndex), item, ...result.slice(toIndex)];
    }

    return result;
};

function onListReorder(e: DevExpress.ui.dxSortable.ReorderEvent) {
    $("#sortable-id").dxSortable('instance');
}

function navigateToDetails() {
    console.log("Navigating to task details...");
}

function onClick(item:any) {
    console.log("Edit button clicked for item:", item);
}

function changePopupVisibility(e: DevExpress.ui.dxButton.ClickEvent) {

}
function onTaskDragStart(e: DevExpress.ui.dxSortable.DragStartEvent) {

}

function onTaskDrop(e: DevExpress.ui.dxSortable.AddEvent | DevExpress.ui.dxSortable.ReorderEvent) {

}