$(function () {
    $(document).ready(function () {
        InitializeBreadCrumb();
        InitializeAccordion();
        InitializeImageLoad();
        InitializeFormSubmit();
    });
});

function SetHeadingCount() {
    $("#heading img").hide();
    $("#heading #message").text($("div.right img").data("total") + " files");
}

function InitializeCount() {
    $("#heading img").hide();
    $("#heading #message").text($("div.right img").data("total") + " files");
}

function InitializeFormSubmit() {
    $("#formSubmit").click(function () {
        var data = $(this).closest("form").serializeArray();
        $("#heading img").show();
        $("#heading #message").text("saving");
        $.post("\\administration\\create\\savepicture", data, function (response) {

            var json = $.parseJSON(response);

            var img = $("div.right img");
            var link = $("div.right a");
            var hidden = $("form #File");

            img.css("opacity", 0);
            img.attr("src", json.resizedSrc);
            img.data("total", json.total);
            link.attr("href", json.src);
            hidden.val(json.file);

            SetHeadingCount();
        });
    });
}

function InitializeImageLoad() {
    $(".right img").load(function () {
        $(this).animate({ opacity: 1 }, 200, function () {
            SetHeadingCount();
        });
        ResetAccordion();
    });
}