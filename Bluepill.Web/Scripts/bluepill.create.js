$(function () {
    $(document).ready(function () {
        InitializeBreadCrumb();
        InitializeAccordion();
        InitializeImageLoad();
        InitializeFormSubmit();
    });
});

function SetHeadingCount() {
    $(".heading img").hide();
    $(".heading span").text($(".add img").data("total") + " files");
}

function InitializeCount() {
    $(".heading img").hide();
    $(".heading span").text($(".add img").data("total") + " files");
}

function InitializeFormSubmit() {
    $(".submit").click(function () {
        var data = $(this).closest("form").serializeArray();
        $(".heading img").show();
        $(".heading span").text("saving");
        $.post("\\bluepill\\create\\savepicture", data, function (response) {

            var json = $.parseJSON(response);

            var img = $(".add img");
            var link = $(".add a");
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
    $(".add img").load(function () {
        $(this).animate({ opacity: 1 }, 200, function () {
            SetHeadingCount();
        });
        ResetAccordion();
    });
}