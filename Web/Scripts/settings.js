﻿$(document).ready(function () {

    $(".collectionSelect").combobox();
    //$(".collectionSelect").show();

    $(".ui-combobox-input").attr("disabled", "disabled");

    $(".signoff").button({
        icons: { primary:"ui-icon-arrowreturnthick-1-w" }
    });

});
