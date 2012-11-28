$(document).ready(function () {

    function ChildrenContainerOn(id) {
        
        var a = $("." + id + ".plus-minus a");
        var container = a.closest(".children-container");
        var children = $("." + id + ".children");

        container.css({ "width": 200 });
        children.show();
        a.text("-");
        a.data("toggle-state", "on");

    }

    function ChildrenContainerOff(id) {

        var a = $("." + id + ".plus-minus a");
        var container = a.closest(".children-container");
        var children = $("." + id + ".children");

        children.hide();
        container.css({ "width": 50 });
        a.text("+");
        a.data("toggle-state", "off");
    }

    $(".plus-minus a").click(function (e) {

        var id = $(e.target).closest(".children-container").data("checkbox");

        if ($(e.target).text() == "-") {
            ChildrenContainerOff(id);
        }
        else {
            ChildrenContainerOn(id);
        }
    });

    $("input[type=checkbox]").click(function () {
        Update($(this));
    });

    function Update(checkbox) {
        var id = checkbox.attr("id");
        var add = checkbox.is(":checked");

        if (add) {
            $("#selections").append("<option value=\"" + id + "\" id=\"o" + id + "\">" + id + "</option>");
            $("." + id + ".children-container").show();
        }
        else {
            $("option[id=o" + id + "]", "#selections").remove();
            $("." + id + ".children-container").hide();
        }

        var children = $("." + id + ".children li");

        children.each(function () {
            var childCheckbox = $(this).find($("input[type=checkbox]"));
            childCheckbox.attr("checked", false);
            Update(childCheckbox);
        });

        var container = $("." + id + ".children-container");

        ChildrenContainerOn(id);
    }

});