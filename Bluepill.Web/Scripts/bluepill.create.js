$("document").ready(function () {

    //$(".facet-container").on("click", ".facet-action", function (e) {
    //    FacetActionClick($(e.target));
    //});

    //$("input[type=checkbox]").button();

    $(".submit").click(function () {
        $(".facets-area input[type=checkbox]").each(function () {
            if ($(this).is(":checked")) {
                var id = $(this).closest(".facet-container").data("id");
                console.log("facet = " + id + ", aspect = " + $(this).data("value"));
            }
        });
    });

    $(".facets-area").on("click", ".facet-action", function () {
        $(this).siblings("ul").toggle("show");
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

            if (container.length == 0) {
                var data = { id: facet };
                $.post("\\bluepill\\create\\getfacet", data, function (response) {

                    target.closest(".facet-container").after(response);

                });
            }
            else {
                container.show();
            }
        }
        else if (facet != undefined && !isChecked) {
            var child = area.find("#facet-" + facet);

            if(child.length > 0){
                $("input[type=checkbox]", child).each(function(){
                    if ($(this).is(":checked")) {
                        $(this).attr("checked", false);
                        $("label[for=" + $(this).attr("id") + "]").trigger("click");
                        $(this).trigger("change");
                    }
                });
                child.hide();
            }
        }
        
    });


});