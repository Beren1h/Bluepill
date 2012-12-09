$("document").ready(function () {

    //$(".facet-container").on("click", ".facet-action", function (e) {
    //    FacetActionClick($(e.target));
    //});

    //$("input[type=checkbox]").button();
    $(".facet-container").each(function () {

        if ($(this).data("top") == "True") {
            $(this).show();
        }

    });

    $(".submit").click(function () {

        var data = $("form").serializeArray();
        console.log(JSON.stringify(data));


        $.ajax({
            type: "POST",
            url: "\\bluepill\\create\\savepicture",
            data: { file: $("#File").val(), selects: facets },
            //data: $("#test").serializeArray(),
            traditional: true,
            success: function (response) {

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

            }
        });



        ////$(".facet-container").each(function () {
        ////    var facets = [];
        ////});

        //var x = $(".facets-area").serializeArray();

        //console.log(x);


        //var facets = [];

        //$(".facets-area input[type=checkbox]").each(function () {
        //    if ($(this).is(":checked")) {
        //        var id = $(this).closest(".facet-container").data("id");
        //        var x = parseInt(id);
        //        var value = $(this).data("value");
        //        //var data = {  : value }
        //        //facets.push(data);
        //        console.log("facet = " + id + ", aspect = " + $(this).data("value"));
        //    }
        //});

        //console.log(JSON.stringify(facets));
        
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

            target.closest(".facet-container").after(container);
            container.show();
            //if (container.length == 0) {
            //    var data = { id: facet };
            //    $.post("\\bluepill\\create\\getfacet", data, function (response) {

            //        target.closest(".facet-container").after(response);

            //    });
            //}
            //else {
            //    container.show();
            //}
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