/* Site.js */

(function () {
    var $sidebarAndWrapper = $("#sidebar,#wrapper");
    var $icon = $("#sideBarToggle i.fa");
    var $footer = $("#footer");

    $("#sideBarToggle").on("click", function () {
        $sidebarAndWrapper.toggleClass("hide-sidebar");
        $sidebarAndWrapper.toggleClass("slide-wrapper");
        $footer.toggleClass("slide-footer");

        if ($sidebarAndWrapper.hasClass("hide-sidebar")) {
            $icon.removeClass("fa-angle-left");
            $icon.addClass("fa-angle-right");
        }
        else {
            $icon.removeClass("fa-angle-right");
            $icon.addClass("fa-angle-left");
        }
    });
})();
