
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

    ////    var toggle = $(this).closest(".facet-container").find(".accordion-heading .accordion-toggle");
        

    //    var checkboxes = $(this).closest(".facet-container").find("input[type=checkbox]");
    //    var selections = false;

    //    checkboxes.each(function (i) {
    //        if ($(this).is(":checked")) {
    //            //toggle.addClass("selections");
    //            selections = true;
    //            return false;
    //        }
    //        //toggle.removeClass("selections");
    //    });

    //    console.log(selections);
    ////    console.log(on);
    ////    if (on)
    ////        toggle.addClass("selections");
    ////    else
    ////        toggle.removeClass("selections");


    });


    $("input[type=submit]").click(function (e) {
        console.log("post");

        //var active = $(e.target).parent().data("active");
        
        //if (active == undefined || !active) {
        //    return false;
        //}
        //else {
        //    console.log("post");
        //}

        //var target = $(e.target);
        //target.find("input[type=submit]");

        //console.log(target.data("active"));

    });




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
            $(".submit").find("input[type=submit]").removeAttr("disabled");
            $(".submit").animate({ "opacity": "1" }, 150);
        }
        else {
            $(".submit").find("input[type=submit]").attr("disabled", "disabled");
            $(".submit").animate({ "opacity": "0.4" }, 150);
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


