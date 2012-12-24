
$("document").ready(function () {

    $(".facet-container").each(function () {
        if ($(this).data("top") == "True") {
            $(this).show("slow");
        }
    });

    $(".facets-area").on("click", ".facet-action", function () {
        $(this).siblings("ul").slideToggle(300);
        return false;
    });

    $(".facets-area").on("click", "label", function (e) {
        $(e.target).toggleClass("on").toggleClass("off");
    });

    $(".facets-area").on("change", "input[type=checkbox]", function (e) {
        var area = $(".facets-area");
        var target = $(e.target);
        var isChecked = target.is(":checked");
        var facet = target.data("facet");
        var container = area.find("#facet-" + facet);

        if (facet != "" && isChecked) {
            target.closest(".facet-container").before(container);
            container.slideToggle(200, "linear", function () {
                $(".facet-action", container).siblings("ul").slideDown(300);
            });
        }
        else if (facet != undefined && !isChecked) {
            var child = area.find("#facet-" + facet);

            if (child.length > 0) {
                $("input[type=checkbox]", child).each(function () {
                    if ($(this).is(":checked")) {
                        $(this).attr("checked", false);
                        $("label[for=" + $(this).attr("id") + "]").trigger("click");
                        $(this).trigger("change");
                    }
                });
                child.slideToggle(300);
            }
        }
        ShowSubmit();
        SetSelection(target);
    });
});

function SetSelection(target) {
    var on = target.closest("ul").find("input[type=checkbox]").is(":checked")

    if (on) {
        target.closest(".facet-container").find("h3").addClass("selections");
    }
    else {
        target.closest(".facet-container").find("h3").removeClass("selections");
    }
}

function ShowSubmit() {
    var show = $("input[type=checkbox]").is(":checked");

    if (show) {
        $(".submit").show();
    }
    else {
        $(".submit").hide();
    }
};

function ResetForm() {
    $("input[type=checkbox]").each(function () {
        $(this).attr("checked", false);
        $("label[for=" + $(this).attr("id") + "]").removeClass("on").addClass("off");
    });

    $(".facet-container").each(function () {
        if ($(this).data("top") == "True") {
            $(this).show();
        }
        else {
            $(this).hide();
        }
        $("label", $(this)).removeClass("on").addClass("off");
        $("h3", $(this)).removeClass("selections");
    });

    $(".facets-area ul").hide();
    $(".submit").hide();
}