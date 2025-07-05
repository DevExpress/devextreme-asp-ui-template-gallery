"use strict";
class SPARouter {
    init() {
        // optional: auto-bind popstate handler on instantiation
        window.addEventListener('popstate', () => {
            this.handleRoute(document.location.toString());
        });
    }
    navigate(url) {
        history.pushState({}, '', url);
        this.handleRoute(url);
    }
    handleRoute(targetUrl) {
        const url = new URL(targetUrl, window.location.origin);
        url.searchParams.append('__PARTIAL', 'true');
        $.get(url.href, (markup) => {
            const parser = new DOMParser();
            const doc = parser.parseFromString(markup, "text/html");
            const contentItems = doc.querySelectorAll(`[${SPARouter.TARGET_PLACEHOLDER_ATTR}]`);
            contentItems.forEach((content) => {
                const targetAttr = content.getAttribute(SPARouter.TARGET_PLACEHOLDER_ATTR);
                if (!targetAttr)
                    return;
                const placeholder = $(`[${SPARouter.PLACEHOLDER_ATTR}=${targetAttr}]`);
                if (placeholder.length) {
                    placeholder.html(content.innerHTML); // partial update
                }
                else {
                    window.location.reload(); // fallback to full page load
                }
            });
        });
    }
}
SPARouter.PLACEHOLDER_ATTR = 'data-placeholder-id';
SPARouter.TARGET_PLACEHOLDER_ATTR = 'data-target-placeholder-id';
