interface PlanningTasksController {
    addTask(): void;
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
    navigateToDetails(): void;
    onClick(item: any): void;
    changePopupVisibility(e: DevExpress.ui.dxButton.ClickEvent): void;
    onTaskDragStart(e: DevExpress.ui.dxSortable.DragStartEvent): void;
    onTaskDrop(e: DevExpress.ui.dxSortable.AddEvent | DevExpress.ui.dxSortable.ReorderEvent): void;
}

interface UserProfileController {
    passwordFormData: {
        currentPassword: string;
        password: string;
        confirmPassword: string;
    };
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

interface SPARouter {
    init(): void;
    navigate(url: string): void;
}


type AppConfig = {
    LayoutController: LayoutController;
    KanbanTasksController?: KanbanTasksController;
    PlanningTasksController?: PlanningTasksController;
    UserProfileController?: UserProfileController;
    ThemeController: ThemeController;
    SPARouter: SPARouter;
};

interface Window {
  uitgAppContext: AppConfig;
}


