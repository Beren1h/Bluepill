$(document).ready(function () {

    $(".next").button({
        icons: { secondary: "ui-icon-circle-triangle-e" }
    });

    $(".previous").button({
        icons: { primary: "ui-icon-circle-triangle-w" }
    });

    $(".top").button({
        icons: { secondary: "ui-icon-circle-arrow-n" }
    });

    $("#createSearch").dialog({
        autoOpen: false,
        modal: true,
        resizable: false,
        open: function () {
        },
        close: function () {
            form.html("<h1>loading</h1>");
        }
    });

    $(".create").click(function (e) {
        console.log("wtf");
        $("#createSearch").dialog("open");
        e.preventDefault();
    });


});