
$("document").ready(function () {

    InitializeColumns();
    InitializeCheckBox();

    $(".facet-container").each(function () {
        if ($(this).data("top") == "True") {
            $(this).show();
        }
    });

    $(".facet-container label").click(function (e) {
        $(e.target).toggleClass("on").toggleClass("off").toggleClass("selections");
    });

    $(".actions .btn-clear").click(function () {
        console.log("clear");
    });

    $(".actions .btn-submit").click(function () {
        console.log("submit");
    });

    //$(".actions button").click(function (e) {

    //    var target = $(e.target);
        
    //    console.log(target.text());

    //    console.log("post");

    //    //var active = $(e.target).parent().data("active");
        
    //    //if (active == undefined || !active) {
    //    //    return false;
    //    //}
    //    //else {
    //    //    console.log("post");
    //    //}

    //    //var target = $(e.target);
    //    //target.find("input[type=submit]");

    //    //console.log(target.data("active"));

    //});




});


function InitializeCheckBox() {

    $(".facet-container input[type=checkbox]").change(function (e) {
        var area = $(".facet-area");
        var target = $(e.target);
        var isChecked = target.is(":checked");
        var facetId = target.data("facet");
        var container = area.find("#facet-" + facetId);

        var anyChecked = target.closest(".facet-area").find("input[type=checkbox]").is(":checked");
        var checkboxes = target.closest(".facet-container").find("input[type=checkbox]");
        var toggle = target.closest(".facet-container").find(".accordion-heading .accordion-toggle");

        toggle.removeClass("selections");

        //if a facet has any checkbox boxes checked highlight its name
        checkboxes.each(function () {
            if ($(this).is(":checked")) {
                toggle.addClass("selections");
                return false;
            }
        });
        
        //if any checkboxes are checked enable the submit button
        if (anyChecked) {
            $(".actions").find("button").removeAttr("disabled");
            $(".actions").animate({ "opacity": "1" }, 150);
        }
        else {
            $(".actions").find("button").attr("disabled", "disabled");
            $(".actions").animate({ "opacity": "0.4" }, 150);
        }

        //if checked facet has child facet move it under parent and show else hide.  recursive
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
//    console.log("here");
//    if (on) {
//        target.closest(".facet-container").find("a").addClass("selections");
//    }
//    else {
//        target.closest(".facet-container").find("a").removeClass("selections");
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


