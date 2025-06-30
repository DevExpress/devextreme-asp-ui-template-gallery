type AppConfig = {
    LayoutController: LayoutController;
    PlanningTasksController?: PlanningTasksController;
    ThemeController: ThemeController;
    SPARouter: SPARouter;  
};
interface Window {
  uitgAppContext: AppConfig;
}

