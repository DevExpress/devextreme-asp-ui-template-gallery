"use strict";
(function () {
    var _a;
    if ((_a = window.uitgAppContext) === null || _a === void 0 ? void 0 : _a.SPARouter)
        return;
    const PLACEHOLDER_ATTR = 'data-placeholder-id';
    const TARGET_PLACEHOLDER_ATTR = 'data-target-placeholder-id';
    function init() {
        window.addEventListener('popstate', () => {
            handleRoute(document.location.toString());
        });
    }
    function navigate(url) {
        history.pushState({}, '', url);
        handleRoute(url);
    }
    function handleRoute(targetUrl) {
        const url = new URL(targetUrl, window.location.origin);
        url.searchParams.append('__PARTIAL', 'true');
        $.get(url.href, (markup) => {
            const parser = new DOMParser();
            const doc = parser.parseFromString(markup, "text/html");
            const contentItems = doc.querySelectorAll(`[${TARGET_PLACEHOLDER_ATTR}]`);
            contentItems.forEach((content) => {
                const targetAttr = content.getAttribute(TARGET_PLACEHOLDER_ATTR);
                if (!targetAttr)
                    return;
                const placeholder = $(`[${PLACEHOLDER_ATTR}=${targetAttr}]`);
                if (placeholder.length) {
                    placeholder.html(content.innerHTML);
                }
                else {
                    window.location.reload();
                }
            });
        });
    }
    window.uitgAppContext.SPARouter = {
        init,
        navigate
    };
})();
