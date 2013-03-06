
$("document").ready(function () {

    InitializeColumns();
    InitializeCheckBox();

    $(".facet-container").each(function () {
        if ($(this).data("top") == "True") {
            $(this).show();
        }
    });

    $(".facet-container label").click(function (e) {
        $(e.target).toggleClass("on").toggleClass("off");
    });

});

function InitializeCheckBox() {

    $(".facet-container input[type=checkbox]").change(function (e) {

        var area = $(".facet-area");
        var target = $(e.target);
        var isChecked = target.is(":checked");
        var facetId = target.data("facet");
        var container = area.find("#facet-" + facetId);

        if (container.length > 0) {

            if (isChecked) {
                target.closest(".facet-container").after(container.parent());
                container.parent().show();
                container.parent().find(".accordion-body").collapse("show");
            }
            else {
                container.parent().hide();
                var checks = container.parent().find("input[type=checkbox]");
                checks.each(function (i) {
                    if ($(this).is(":checked")) {
                        $(this).attr("checked", false);
                        $("label[for=" + $(this).attr("id") + "]").trigger("click");
                        $(this).trigger("change");
                    }
                });
            }
        }
    });

}

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

//function SetSelection(target) {
//    var on = target.closest("ul").find("input[type=checkbox]").is(":checked")

//    if (on) {
//        target.closest(".facet-container").find("h3").addClass("selections");
//    }
//    else {
//        target.closest(".facet-container").find("h3").removeClass("selections");
//    }
//}

//function ShowSubmit() {
//    var show = $("input[type=checkbox]").is(":checked");

//    if (show) {
//        $(".submit").show();
//    }
//    else {
//        $(".submit").hide();
//    }
//};

//function ResetForm() {
//    $("input[type=checkbox]").each(function () {
//        $(this).attr("checked", false);
//        $("label[for=" + $(this).attr("id") + "]").removeClass("on").addClass("off");
//    });

//    $(".facet-container").each(function () {
//        if ($(this).data("top") == "True") {
//            $(this).show();
//        }
//        else {
//            $(this).hide();
//        }
//        $("label", $(this)).removeClass("on").addClass("off");
//        $("h3", $(this)).removeClass("selections");
//    });

//    $(".facets-area ul").hide();
//    $(".submit").hide();
//}

