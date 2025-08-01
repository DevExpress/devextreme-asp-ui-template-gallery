(function () {
    if (window.uitgAppContext?.KanbanTasksController) return;

    // @ts-ignore
    const kanbanOrderStore = DevExpress.data.AspNet.createStore({
        key: 'Id',
        loadUrl: `/api/KanbanOrder/GetOrder`,
        insertUrl: `/api/KanbanOrder/UpdateOrder`,
        onBeforeSend(method: string, ajaxOptions: JQuery.PlainObject) {
            ajaxOptions.xhrFields = { withCredentials: true };
            ajaxOptions.contentType = "application/json";
            ajaxOptions.data = JSON.stringify(ajaxOptions.data);
        },
    });

    // @ts-ignore
    const tasksStore = DevExpress.data.AspNet.createStore({
        key: "TaskId",
        updateUrl: "/api/Tasks/UpdateTask",
        insertUrl: "/api/Tasks",
        onBeforeSend(method: string, ajaxOptions: JQuery.PlainObject) {
            if (method === "update") {
                const { key, values } = ajaxOptions.data;
                const formData = new FormData();
                formData.append('key', key);

                if (typeof values === "string") {
                    formData.append('values', values);
                } else {
                    formData.append('values', JSON.stringify(values));
                }

                ajaxOptions.data = formData;
                ajaxOptions.contentType = false;
                ajaxOptions.processData = false;
            }
        }
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

    function onStatusReorder(e: DevExpress.ui.dxSortable.ReorderEvent) {
        let newOrder: string[] = [];
        $(".list-title span").each(function () {
            const text = $(this).text();
            if (newOrder.indexOf(text) === -1) {
                newOrder.push(text);
            }
        })

        kanbanOrderStore.insert({ Statuses: newOrder });
    }

    function navigateToDetails(taskId: number) {
        DevExpress.ui.notify(`Navigate to task details (Id = ${taskId})`);
    }

    function taskEditClick(e: DevExpress.ui.dxButton.ClickEvent, task: EmployeeTask) {
        e.event?.stopPropagation();
        window.uitgAppContext.PlanningTasksController?.showPopupToEditTask(task)
        DevExpress.ui.notify(`Edit task: ${task.Text}`);
    }

    function showPopupToAddTaskWithStatus(status: string) {
        window.uitgAppContext.PlanningTasksController?.showPopupToAddTask({ Status: status });
    }

    function addTask(taskData: EmployeeTask): void {
        tasksStore.insert(taskData).then(() => {
                window.uitgAppContext.SPARouter.navigate("/Home/PlanningTasks/Kanban");
            })
            .fail((xhr: JQueryXHR, status: string, error: string) => {
                DevExpress.ui.notify(`${error} (${status})`);
            });
    }

    function updateTask(taskData: EmployeeTask): void {
        tasksStore.update(taskData.TaskId, taskData).then(() => {
                window.uitgAppContext.SPARouter.navigate("/Home/PlanningTasks/Kanban");
            })
            .fail((xhr: JQueryXHR, status: string, error: string) => {
                DevExpress.ui.notify(`${error} (${status})`);
            });
    }

    function onTaskDragStart(e: DevExpress.ui.dxSortable.DragStartEvent) { }

    function onTaskDrop(e: DevExpress.ui.dxSortable.AddEvent | DevExpress.ui.dxSortable.ReorderEvent) {
        const $item = $(e.itemElement);
        const taskId = $item.data("task-id");
        const newStatus = e.toComponent.element().closest('.list').find('.list-title span').text();

        if (!taskId || !newStatus) return;

        tasksStore.update(taskId, { Status: newStatus, NewOrderIndex: e.toIndex });
    }

    window.uitgAppContext.KanbanTasksController = {
        reorder,
        onListReorder,
        onStatusReorder,
        navigateToDetails,
        taskEditClick,
        showPopupToAddTaskWithStatus,
        onTaskDragStart,
        onTaskDrop,
        addTask,
        updateTask
    };
}) ();