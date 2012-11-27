$(document).ready(function () {

    $(".children-container").hide();
    $(".children").hide();
    $(".plus, .minus").hide();


    //$(".children-container").bind("show", function () {
    //    console.log("test");
    //});

    $(".children-container").bind("beforeShow", function () {
        //console.log("bam");
        //$(this).find(".plus").hide();
        //$(this).find(".minus").show();
        console.log($(this).attr("class"));
        $(".plus", $(this)).hide();
        $(".minus", $(this)).show();
    });

    $("input[type=checkbox]").click(function () {
        var id = $(this).attr("id");
        $("." + id).toggle("show", function () {

            //if ($(this).is(":checked")) {
            //    $("." + id + ".plus").show();
            //    $("." + id + ".minus").hide();
            //}
            //else {
            //    $("." + id + ".plus").hide();
            //    $("." + id + ".minus").show();
            //}

        });

        Update($(this));

    });


    //$(".children-container").click(function () {
    //    $(this).toggle();
    //});


    function ToggleChildrenContainer(container) {
        container.find("ul").toggle();

    }

    function Update(checkbox) {

        var id = checkbox.attr("id");
        var add = checkbox.is(":checked");
        //console.log($("." + id + ".children-container").is(":visible"));

        //if ($("." + id + ".children-container").is(":visible")) {
        //    $("." + id + ".children-container.plus").show();
        //    $("." + id + ".children-container.minus").hide();
        //}
        //else {
        //    $("." + id + ".children-container.plus").hide();
        //    $("." + id + ".children-container.minus").show();
        //}



        if (add) {
            $("#selections").append("<option value=\"" + id + "\" id=\"o" + id + "\">" + id + "</option>");

        }
        else {
            $("option[id=o" + id + "]", "#selections").remove();
            $("." + id + ".children").hide();

            var children = $("." + id + ".children li");

            children.each(function () {

                var childCheckbox = $(this).find($("input[type=checkbox]"));
                childCheckbox.attr("checked", false);
                Update(childCheckbox);
                //console.log($(this).attr("id"));
                //Update($("input[type=checkbox]", $(this)));

            });

        }



    }





});





jQuery(function ($) {

    var _oldShow = $.fn.show;

    $.fn.show = function (speed, oldCallback) {
        return $(this).each(function () {
            var
    			obj = $(this),
    			newCallback = function () {
    			    if ($.isFunction(oldCallback)) {
    			        oldCallback.apply(obj);
    			    }

    			    obj.trigger('afterShow');
    			};

            // you can trigger a before show if you want
            obj.trigger('beforeShow');

            // now use the old function to show the element passing the new callback
            _oldShow.apply(obj, [speed, newCallback]);
        });
    }

    //$('#test')
    //	.bind('beforeShow', function () {
    //	    alert('beforeShow');
    //	})
    //	.bind('afterShow', function () {
    //	    alert('afterShow');
    //	})
    //	.show(1000, function () {
    //	    alert('in show callback');
    //	})
    //	.show();

});



