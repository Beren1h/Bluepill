
$("document").ready(function () {

    InitializeColumns();
    InitializeCheckBox();

    $(".facet-container").each(function () {
        if ($(this).data("top") == "True") {
            $(this).show();
        }
    });

    $(".facet-container label").click(function (e) {
        ToggleLabel($(e.target));
    });

    $(".actions .btn-clear").click(function () {
        Clear();
    });

});


function Clear() {

    var tops = $(".facet-container[data-top=True]");
    var checks = tops.find("input:checked");

    checks.each(function () {
        $(this).attr("checked", false);
        $(this).trigger("change");
        ToggleLabel($("label[for=" + $(this).attr("id") + "]"));
    });

    tops.find(".accordion-body.in").collapse("hide");
}

function ToggleLabel(target){
    target.toggleClass("on").toggleClass("off").toggleClass("selections");
}


function InitializeCheckBox() {

    $(".facet-container input[type=checkbox]").change(function (e) {

        var target = $(e.target);
        var dependentId = target.data("facet");
        var checks = target.closest(".facet-container").find("input:checked");
        var toggle = target.closest(".facet-container").find(".accordion-heading .accordion-toggle");

        if (checks.length > 0) {
            toggle.addClass("selections");
            $(".actions").find("button").removeAttr("disabled");
            $(".actions").animate({ "opacity": "1" }, 150);
        }
        else {
            toggle.removeClass("selections");
            $(".actions").find("button").attr("disabled", "disabled");
            $(".actions").animate({ "opacity": "0.4" }, 150);
        }

        if (dependentId != "") {

            var dependent = $("#facet-" + dependentId);

            if(target.is(":checked")){
                target.closest(".facet-container").after(dependent.parent());
                dependent.parent().show();
                dependent.collapse("show");
            }
            else {
                var checks = dependent.find("input:checked");

                //if accordion body is already collapsed just hide parent, otherwise collapse it
                //and let hidden function hide parent.
                if (dependent.hasClass("in")) {
                    dependent.data("automatic-hide", true);
                    dependent.collapse("hide");
                }
                else {
                    dependent.parent().hide();
                }

                checks.each(function () {
                    console.log($(this).attr("id"));
                    $(this).attr("checked", false);
                    $(this).trigger("change");
                    ToggleLabel($("label[for=" + $(this).attr("id") + "]"));
                });
            }
        }

    });
        
}

$(".accordion-body").on("hidden", function (e) {
    var target = $(e.target);

    if (target.data("automatic-hide")) {
        target.parent().hide("fold", 50);
        target.data("automatic-hide", false);
    }
});

function InitializeColumns() {
    var aspectLists = $(".aspect-list");

    aspectLists.each(function () {
        var perColumn = $(this).data("per-column");
        var lis = $(this).find("li");

        lis.unwrap();

        for (var i = 0; i < lis.length; i++) {
            if (i % perColumn == 0) {
                lis.slice(i, i + perColumn).wrapAll("<div class=\"span4\"><ul></ul></div>");
            }
        }
    });
}