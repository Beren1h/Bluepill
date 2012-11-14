$(document).ready(function () {

    $(".collectionSelect").combobox();

    $(".ui-combobox-input").attr("disabled", "disabled");

    $(".signoff").button({
        icons: { primary:"ui-icon-arrowreturnthick-1-w" }
    });

    $(".add").button({
        icons: { primary: "ui-icon-plusthick" }
    });

    $(".search").button({
        icons: { primary: "ui-icon-search" }
    });

    $(".button").button();

    //var activeIndex = $("body").data("navigation-index");

    //$(".nav").each(function () {

    //    if ($(this).data("navigation-index") == activeIndex) {
    //        $(this).css("opacity", "0.2");
    //        $(this).css("cursor", "default");
    //        $(this).click(function () { return false; });
    //    }

    //});

});