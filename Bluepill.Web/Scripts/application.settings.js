$(document).ready(function () {

    $(".bar a").button();

    $(".collection-menu").menu({
        selected: function (e, ui) {
            $(".collection-menu").slideToggle(function () {
                $(".ui-button-icon-secondary").toggleClass("ui-icon-arrowthick-1-s ui-icon-arrowthick-1-n");
                var img = new Image();
                var selectedItem = $(ui.item);
                var val = $.trim(selectedItem.text());
                console.log(val.length);
                img.id = "collectionSetImage";
                img.src = "/application/settings/update?uid=" + selectedItem.parent().data("uid") + "&selected=" + $.trim(selectedItem.text());
                img.width = 1;
                img.height = 1;

                $(".ui-icon").remove();
                selectedItem.find("a").append("<span class=\"ui-icon ui-icon-check\"></span>");
            });
        },
    });

    $(".menu-button").click(function (e) {
        var div = $(this).data("menu-selector");
        $(div).css({ top: $(this).position().top + 30, left: $(this).position().left }).slideToggle();
    });

});