/* Site.js */

(function () {
    var $sidebarAndWrapper = $("#sidebar,#wrapper");

    $("#sideBarToggle").on("click", function () {
        $sidebarAndWrapper.toggleClass("hide-sidebar");
        $sidebarAndWrapper.toggleClass("slide-wrapper");

        if ($sidebarAndWrapper.hasClass("hide-sidebar")) {
            $(this).text("Show Sidebar");
        }
        else {
            $(this).text("Hide Sidebar");
        }
    });
})();
