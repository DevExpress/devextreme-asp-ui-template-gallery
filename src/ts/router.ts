var SPARouter = (function () {
    const PLACEHOLDER_ATTR: string = 'data-placeholder-id';
    const TARGET_PLACEHOLDER_ATTR: string = 'data-target-placeholder-id';

    function init() {
        window.addEventListener('popstate', function () {
            handleRoute(document.location.toString());
        });
    }

    function navigate(url: string) {
        history.pushState({}, '', url);
        handleRoute(url);
    }

    function handleRoute(targetUrl: string) {
        const url = new URL(targetUrl, window.location.origin);
        url.searchParams.append('__PARTIAL', 'true');
        $.get(url.href, function (markup) {
            const parser = new DOMParser();
            const doc = parser.parseFromString(markup, "text/html");
            const contentItems = doc.querySelectorAll(`[${TARGET_PLACEHOLDER_ATTR}]`);
            contentItems.forEach((content) => {
                const placeholder = $(`[${PLACEHOLDER_ATTR}=${content.getAttribute(TARGET_PLACEHOLDER_ATTR)}]`);
                if (placeholder.length) {
                    placeholder.html(content.innerHTML); // partial update
                }
                else {
                    window.location.reload(); // full update - layout has changed
                }
            });
        });
    }

    return {
        init,
        navigate,
    };
})();