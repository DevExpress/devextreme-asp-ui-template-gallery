class SPARouter {
    private static readonly PLACEHOLDER_ATTR = 'data-placeholder-id';
    private static readonly TARGET_PLACEHOLDER_ATTR = 'data-target-placeholder-id';

    init() {
        // optional: auto-bind popstate handler on instantiation
        window.addEventListener('popstate', () => {
            this.handleRoute(document.location.toString());
        });
    }

    public navigate(url: string): void {
        history.pushState({}, '', url);
        this.handleRoute(url);
    }

    private handleRoute(targetUrl: string): void {
        const url = new URL(targetUrl, window.location.origin);
        url.searchParams.append('__PARTIAL', 'true');

        $.get(url.href, (markup: string) => {
            const parser = new DOMParser();
            const doc = parser.parseFromString(markup, "text/html");
            const contentItems = doc.querySelectorAll<HTMLElement>(`[${SPARouter.TARGET_PLACEHOLDER_ATTR}]`);

            contentItems.forEach((content) => {
                const targetAttr = content.getAttribute(SPARouter.TARGET_PLACEHOLDER_ATTR);
                if (!targetAttr) return;

                const placeholder = $(`[${SPARouter.PLACEHOLDER_ATTR}=${targetAttr}]`);

                if (placeholder.length) {
                    placeholder.html(content.innerHTML); // partial update
                } else {
                    window.location.reload(); // fallback to full page load
                }
            });
        });
    }
}