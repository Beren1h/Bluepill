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

function InitializeAccordion() {
    $("#accordion").accordion({
        autoHeight: true,
        collapsible: true,
        active: false,
    });
}

function ResetAccordion() {
    $(".breadCrumb").each(function () {
        $(this).text("");
    });

    $("#accordion .aspectContainer").each(function (i) {
        $("input[type=checkbox]", $(this)).attr("checked", false);

        var label = $("label", $(this));
        label.removeClass("ui-state-active");
        label.attr("aria-pressed", false);
    });
    InitializeAccordion();
}

function InitializeBreadCrumb() {
    $(".aspect-checkbox").change(function () {

        var context = $(this).closest("div.ui-accordion-content");
        var breadCrumb = $("span#breadCrumb-" + $(this).data("facet"));
        var output = "";

        $(".aspect-checkbox", context).each(function () {
            if ($(this).is(":checked")) {

                var label = $("label[for='" + $(this).attr("id") + "']");

                if (output.length > 0) {
                    output += ", ";
                }

                output += label.text();
            }
        });
        breadCrumb.text(output);
    });
}