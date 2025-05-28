const boardMenuItems = [
    { text: 'Add card' },
    { text: 'Copy list' },
    { text: 'Move list' },
];

const STATUS_ITEMS = ['Open', 'In Progress', 'Deferred', 'Completed'];

$.get("/api/FilteredTasks", function (data: any) {
    console.log("Filtered:", data);
    //debugger;
    //$("#task-list-id");

    $.ajax({
        url: '/Home/TaskMainSortable',
        type: 'POST',
        data: { filteredTasks: JSON.stringify(data.data) },
        success: function (cont: any) {
            $("#kanban-load-panel").dxLoadPanel("instance").hide();
            (window as any).globalData = data;
            $(".main-kanban-sort").html(cont);
            console.log($(".sortable-cards"));
        },
        error: function (xhr) {
            console.error('Error:', xhr.status, xhr.statusText, xhr.responseText);
        }
    });

    //$.post('/Home/TaskMainSortable', { filteredTasks: JSON.stringify(data) }, function (cont: any) {
    //    $("#kanban-load-panel").dxLoadPanel("instance").hide();

        
    //    (window as any).globalData = data;

    //    $(".main-kanban-sort").html(cont);

    //    console.log($(".sortable-cards"));

    //    //$("#scroll-view-id").dxScrollView("instance").update();
    //})
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

function changePopupVisibility(e: DevExpress.ui.dxButton.ClickEvent) {

}
function onTaskDragStart(e: DevExpress.ui.dxSortable.DragStartEvent) {

}

function onTaskDrop(e: DevExpress.ui.dxSortable.AddEvent | DevExpress.ui.dxSortable.ReorderEvent) {

}