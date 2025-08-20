interface PlanningTasksController {
    showPopupToEditTask(taskData: EmployeeTask): void;
    showPopupToAddTask(taskData: EmployeeTask): void;
    addNewTask: (taskData: EmployeeTask) => void;
    editTask(taskData: EmployeeTask): void;
    tabValueChange: (e: DevExpress.ui.dxTabs.ItemClickEvent) => void;
    getTabsWidth(): number | string;
    getCurrentView(): string;
    reload(): void;
    chooseColumnDataGrid(): void;
    exportToPdf(): void;
    exportToXlsx(): void;
    searchDataGrid(e: DevExpress.ui.dxTextBox.InputEvent): void;
}

interface KanbanTasksController {
    reorder<T>(items: T[], item: T, fromIndex: number, toIndex: number): T[];
    onListReorder(e: DevExpress.ui.dxSortable.ReorderEvent): void;
    onStatusReorder(e: DevExpress.ui.dxSortable.ReorderEvent): void;
    navigateToDetails(taskId: number): void;
    taskEditClick(e: DevExpress.ui.dxButton.ClickEvent, item: EmployeeTask): void;
    showPopupToAddTaskWithStatus(status: string): void;
    onTaskDragStart(e: DevExpress.ui.dxSortable.DragStartEvent): void;
    onTaskDrop(e: DevExpress.ui.dxSortable.AddEvent | DevExpress.ui.dxSortable.ReorderEvent): void;
    addTask(taskData: EmployeeTask): void;
    updateTask(taskData: EmployeeTask): void;
}

interface UserProfileController {
    copyToClipboard(e: DevExpress.ui.dxButton.ClickEvent): void;
    handleChangePasswordClick(e: DevExpress.ui.dxButton.ClickEvent): void;
    onTCancel(e: DevExpress.ui.dxButton.ClickEvent): void;
    onTSave(e: DevExpress.ui.dxButton.ClickEvent): void;
    onScroll(e: DevExpress.ui.dxScrollView.ScrollEvent): void;
    onCancelClick(e: DevExpress.ui.dxButton.ClickEvent): void;
    onSaveClick(e: DevExpress.ui.dxButton.ClickEvent): void;
    passwordEyeClicked(e: DevExpress.ui.dxButton.ClickEvent): void;
    onPasswordValueChanged(e: DevExpress.ui.dxTextBox.ValueChangedEvent): void;
    comparePassword(): string;
    formDataChanged(e: DevExpress.ui.dxForm.OptionChangedEvent): void;
    passwordResetOptionChanged(e: DevExpress.ui.dxForm.FieldDataChangedEvent): void;
}

interface LayoutController {
    getScreenSize(): {
        isXSmall: boolean;
        isSmall: boolean;
        isMedium: boolean;
        isLarge: boolean;
    };
    getDrawer(): DevExpress.ui.dxDrawer;
    getMenu(): DevExpress.ui.dxTreeView;
    isNodeExpanded(): boolean;
    restoreDrawerOpened(): boolean;
    saveDrawerOpened(): void;
    updateSidePanel(): void;
    getBaseUri(): string;
    init(): void;
    navigate(url: string, delay: number): void;
    onMenuButtonClick(): void;
    onTreeViewItemClick(e: any): void;
    onLogoutClick(): void;
}

interface ThemeController {
    init(): void;
    getTheme(): string;
    themeButtonOnInitialized(e: any): void;
    themeSwitcherOnClick(e: any): void;
}

interface EmployeeTask {
    TaskId?: number;
    Id?: number;
    ParentId?: number | null;
    Manager?: string;
    Status?: string;
    Priority?: string;
    StartDate?: Date | null;
    DueDate?: Date | null;
    Progress?: number | null;
    Company?: string;
    Text?: string;
    Owner?: string;
    OrderIndex?: number;
}

interface SPARouter {
    init(): void;
    navigate(url: string): void;
}

interface AuthLayoutController {
    keepTopLayout: any;
    onCreateSubmit(): void;
    onSubmit(e: Event): void;
    onCreateAccountClick(): void;
}

interface PopupFormController {
    getSizeQualifier(width: number): string;
    getPopupForm(): DevExpress.ui.dxPopup;
    getTaskForm(): DevExpress.ui.dxForm;
    onSaveTask(e: DevExpress.ui.dxButton.ClickEvent): void;
    onCancelTask(): void;
    show(mode: TaskFormSaveMode): JQueryPromise<boolean>;
}

type TaskFormSaveMode = "insert" | "update";

type AppConfig = {
    PopupFormController: PopupFormController;
    LayoutController: LayoutController;
    AuthLayoutController: AuthLayoutController;
    KanbanTasksController?: KanbanTasksController;
    PlanningTasksController?: PlanningTasksController;
    UserProfileController?: UserProfileController;
    ThemeController: ThemeController;
    SPARouter: SPARouter;
    Constants: {
        CLASS_STATUS_PREFIX: string;
        CLASS_CELL_STATUS: string;
        DemoFilteredOwnerName: string;
    };
};

interface Window {
  uitgAppContext: AppConfig;
}


