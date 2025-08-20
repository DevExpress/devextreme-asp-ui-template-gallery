(function () {
     const app = window.uitgAppContext;
     $(document)
            .off('click.spaLink', 'a.spa-link')
            .on('click.spaLink', 'a.spa-link', function (e) {
                e.preventDefault();
                const href = (this as HTMLAnchorElement).getAttribute('href') ?? '';
                app.SPARouter.navigate(href);
            });

    if (window.uitgAppContext?.AuthLayoutController) return;
 
    function onCreateSubmit() {    
        app.SPARouter.navigate(`/${app.AuthLayoutController.keepTopLayout ? "Home" : "Auth"}/Login`);
    }

    function onSubmit(e: Event) {
        $("#submitLoadingIndicator").dxLoadIndicator("instance").option("visible", true);
        $("#sign-in-text").hide();
        location.href = "/";
    }

    function onCreateAccountClick(): void {
        const app = window.uitgAppContext;
        app.SPARouter.navigate(`/${app.AuthLayoutController.keepTopLayout ? "Home" : "Auth"}/SignUp`);
    }

 
    window.uitgAppContext.AuthLayoutController = {
        keepTopLayout: false,
        onCreateSubmit,
        onSubmit,
        onCreateAccountClick
    }
})()