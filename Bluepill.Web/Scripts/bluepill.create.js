$("document").ready(function () {

    InitializeImageLoad();

    $(".facet-container").each(function () {
        if ($(this).data("top") == "True") {
            $(this).show();
        }
    });

    $(".submit").click(function () {

        var data = $("form").serializeArray();
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

            //SetHeadingCount();
            ResetForm();
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

        console.log(facet + ", " + isChecked);

        if (facet != "" && isChecked) {
            target.closest(".facet-container").after(container);
            container.show();
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


function InitializeImageLoad() {
    $(".add img").load(function () {
        $(this).animate({ opacity: 1 }, 200, function () {
            SetHeadingCount();
        });
        //ResetAccordion();
    });
}

function ResetForm() {

    $("input[type=checkbox]").each(function () {
        $(this).attr("checked", false);
        $("label[for=" + $(this).attr("id") + "]").removeClass("on").addClass("off");
        //$("label[for=" + $(this).attr("id") + "]").trigger("click");
        //$(this).trigger("change");
        //console.log($(this).attr("id"));
    });
   

    $(".facet-container").each(function () {
        if ($(this).data("top") == "True") {
            $(this).show();
        }
        else {
            $(this).hide();
        }
    });

    $(".facets-area ul").hide();

}