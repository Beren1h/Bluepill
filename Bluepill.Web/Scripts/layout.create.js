$(document).ready(function () {

    $(".top-bar .button").button({
        //icons: { primary: "ui-icon-gear" }
    });

    $(".collection-menu").menu({
        selected: function (e, ui) {
            $(".collection-menu").slideToggle(function () {
                $(".ui-button-icon-secondary").toggleClass("ui-icon-arrowthick-1-s ui-icon-arrowthick-1-n");
                    var img = new Image();
                    var selectedItem = $(ui.item);
                    img.id = "collectionSetImage";
                    img.src = "/application/settings/update?uid=" + selectedItem.parent().data("uid") + "&selected=" + selectedItem.text();
                    img.width = 1;
                    img.height = 1;

                    $(".ui-icon").remove();
                    selectedItem.find("a").append("<span class=\"ui-icon ui-icon-check\"></span>");
            });
        },
    });

    $(".menu-button").click(function (e) {
        var div = $(this).data("menu-selector");
        $(div).slideToggle();
    });

    //function ToggleMenu(selector) {
    //    selector.slideToggle();
    //}

});