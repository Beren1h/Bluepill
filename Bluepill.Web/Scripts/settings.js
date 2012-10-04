$(document).ready(function () {

    $(".collectionSelect").combobox();

    $(".ui-combobox-input").attr("disabled", "disabled");

    $(".signoff").button({
        icons: { primary:"ui-icon-arrowreturnthick-1-w" }
    });

});
