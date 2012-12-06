$(document).ready(function () {

    $(".button").button();
    $(".children-container").hide();
    //$("#selections").hide();

    //console.log($(".children-container").length);
    //$(".children-container").each(function () {

    //    var id = $(this).data("checkbox");
    //    //var id = "c-2";
    //    var box = $("#" + id);
        
    //    console.log("checkbox = " + id + ", top = " + box.position().top + ", left = " + box.position().left);

    //    //$(div).css({ top: $(this).position().top + 30, left: $(this).position().left }).slideToggle();
    //    //$(this).css({ top: box.position().top + 2 , left: box.position().left + 20});
    //    $(this).offset()

    //});


    $(".submit").click(function () {

        //var s = $("input[type=checkbox]").serializeArray();
        //console.log(JSON.stringify(s));

        var facets = [];

        //$("#Selections option").each(function () {
        //    list.push($(this).val())
        //});


        $("input[type=checkbox]").each(function(){

            if ($(this).is(":checked")) {
                facets.push($(this).attr("id"));
            }
        });

        //console.log(JSON.stringify(list));
        //var data = { "file": "c:\\test\\test.txt", "facets": [{ "0": "1" }, { "1": "5" }] }

        //var data = { "File": "c:\\test\\test.txt", "List": list }
        //var data2 = $("#test").serializeArray();
        //var data3 = { "List": list }

        //$.post("\\layout\\create\\save", data3, function () {
        //    console.log("posted");
        //});

        $.ajax({
            type: "POST",
            url: "\\layout\\create\\save",
            data: { file: "c:\\test\\test.txt", list: facets },
            traditional: true,
            success: function (json) {
                console.log(data);
            }
        });

        //console.log(data);
        //console.log(data2);
        //console.log(data3);


    });

    function ChildrenContainerOn(id) {
        
        var a = $("." + id + ".plus-minus a");
        var container = $("." + id + ".children-container");
        var children = $("." + id + ".children");

        //container.css({ "width": 200 });
        //container.css("display", "block");
        //console.log("x = " + a.parent().position().left + ", y = " + a.parent().position().top);
        //console.log(id);
        //container.show();
        //container.css("width", "75%");
        children.show();
        a.text("-");
        //a.data("toggle-state", "on");

    }

    function ChildrenContainerOff(id) {

        var a = $("." + id + ".plus-minus a");
        //var container = a.closest(".children-container");
        var container = $("." + id + ".children-container");
        var children = $("." + id + ".children");

        //container.hide();
        children.hide();
        //container.css({ "width": 50 });
        //container.css("display", "inline-block");
        a.text("+");
        //container.css("width", "25");
        //a.data("toggle-state", "off");
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
            //$("#Selections").append("<option value=\"" + id + "\" id=\"o" + id + "\">" + id + "</option>");
            $("." + id + ".children-container").show();
            //$("." + id + ".children-container").css({ top: checkbox.position().top + 2 , left: checkbox.position().left + 20 });
            //console.log($("." + id + ".children-container").attr("class"));
            $("." + id + ".children-container").position({
                "of": checkbox,
                "my": "left top",
                "at": "right bottom",
                "offset": "3 3",
            });
            //ChildrenContainerOn(id);
        }
        else {
            //$("option[id=o" + id + "]", "#Selections").remove();
            $("." + id + ".children-container").hide();
            //ChildrenContainerOff(id);
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