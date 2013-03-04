
$("document").ready(function () {
   
    $(".facet-container").each(function () {
        if ($(this).data("top") == "True") {
            $(this).show("slow");
        }
    });

    //$(".facets-area").on("click", ".facet-action", function () {
    //    $(this).siblings("ul").slideToggle(300);
    //    return false;
    //});

    $(".facet-area label").click(function (e) {
        $(e.target).toggleClass("on").toggleClass("off");
    });

    $(".accordion-body").on("shown", function () {
        console.log("shown");
    })

    $(".facet-area input[type=checkbox]").change(function (e) {

        var area = $(".facet-area");
        var target = $(e.target);
        var isChecked = target.is(":checked");
        var facetId = target.data("facet");
        var container = area.find("#facet-" + facetId);


        if (container.length > 0) {

            if (isChecked) {
                //console.log("turn it on");
                target.closest(".facet-container").after(container.parent());
                //console.log(target.closest(".facet-container").attr("id"));
                //container.collapse("show");
                //target.closest(".facet-action").trigger("click");
                container.parent().show();
                container.parent().find(".accordion-body").collapse("show");
                //container.parent().slideToggle(200, function () {
                //    $(this).find(".accordion-body").collapse("show");
                //});
            }
            else {
                //console.log("turn it off");
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




            //var area = $(".facets-area");
            //var target = $(e.target);
            //var isChecked = target.is(":checked");
            //var facet = target.data("facet");
            //var container = area.find("#facet-" + facet);

            //if (facet != "" && isChecked) {
            //    target.closest(".facet-container").after(container);
            //    container.show(1000, function () {
            //        $(".facet-action", container).siblings("ul").hide(1000);
            //    });
            //}
            //else if (facet != undefined && !isChecked) {
            //    var child = area.find("#facet-" + facet);

            //    if (child.length > 0) {
            //        $("input[type=checkbox]", child).each(function () {
            //            if ($(this).is(":checked")) {
            //                $(this).attr("checked", false);
            //                $("label[for=" + $(this).attr("id") + "]").trigger("click");
            //                $(this).trigger("change");
            //            }
            //        });
            //        child.show(1000);
            //    }
            //}
            ////ShowSubmit();
            //SetSelection(target);
    });

    //$(".facets-area").on("click", "label", function (e) {
    //    //$(e.target).toggleClass("on").toggleClass("off");
    //    $(e.target).toggleClass("on");
    //    //console.log($(e.target).attr("for"));
    //});

    //$(".facets-area").on("change", "input[type=checkbox]", function (e) {
    //    console.log("change");
    //    //var area = $(".facets-area");
    //    //var target = $(e.target);
    //    //var isChecked = target.is(":checked");
    //    //var facet = target.data("facet");
    //    //var container = area.find("#facet-" + facet);

    //    //if (facet != "" && isChecked) {
    //    //    target.closest(".facet-container").before(container);
    //    //    container.slideToggle(200, "linear", function () {
    //    //        $(".facet-action", container).siblings("ul").slideDown(300);
    //    //    });
    //    //}
    //    //else if (facet != undefined && !isChecked) {
    //    //    var child = area.find("#facet-" + facet);

    //    //    if (child.length > 0) {
    //    //        $("input[type=checkbox]", child).each(function () {
    //    //            if ($(this).is(":checked")) {
    //    //                $(this).attr("checked", false);
    //    //                $("label[for=" + $(this).attr("id") + "]").trigger("click");
    //    //                $(this).trigger("change");
    //    //            }
    //    //        });
    //    //        child.slideToggle(300);
    //    //    }
    //    //}
    //    //ShowSubmit();
    //    //SetSelection(target);
    //});

});

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

