$(document).ready(function () {

    InitializeImageLoad();

    $(".button").button();
    $(".children-container").hide();

    $(".submit").click(function () {
        var facets = [];

        $("input[type=checkbox]").each(function () {

            if ($(this).is(":checked")) {
                facets.push($(this).attr("id"));
            }
        });

        //console.log(JSON.stringify(list));
        //var data = { "file": "c:\\test\\test.txt", "facets": [{ "0": "1" }, { "1": "5" }] }

        //var data = { "File": "c:\\test\\testme    simpl.txt", "List": list }
        //var data2 = $("#test").serializeArray();
        //var data3 = { "List": list }
        //console.log($("#test").serializeArray());
        //$.post("\\layout\\create\\save", data3, function () {
        //    console.log("posted");
        //});

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
    });

    function ChildrenContainerOn(id) {

        var a = $("." + id + ".plus-minus a");
        var container = $("." + id + ".children-container");
        var children = $("." + id + ".children");

        children.show(200);
        a.text("-");
    }

    function ChildrenContainerOff(id) {

        var a = $("." + id + ".plus-minus a");
        var container = $("." + id + ".children-container");
        var children = $("." + id + ".children");

        children.hide(200);
        a.text("+");
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
            $("." + id + ".children-container").show(200);
            //$("." + id + ".children-container").position({
            //    "of": checkbox,
            //    "my": "left top",
            //    "at": "right bottom",
            //    "offset": "3 3",
            //});
        }
        else {
            $("." + id + ".children-container").hide(200);
        }

        var children = $("." + id + ".children li");

        if (!add) {
            children.each(function () {
                               
                var childCheckbox = $(this).find($("input[type=checkbox]"));

                if (childCheckbox.is(":checked")) {
                    childCheckbox.attr("checked", false);
                    Update(childCheckbox);
                }

            });
        }

        ChildrenContainerOn(id);
    }

});




//$(function () {
//    $(document).ready(function () {
//        InitializeBreadCrumb();
//        InitializeAccordion();
//        InitializeImageLoad();
//        InitializeFormSubmit();
//    });
//});

function SetHeadingCount() {
    $(".heading img").hide();
    $(".heading span").text($(".add img").data("total") + " files");
}

//function InitializeCount() {
//    $(".heading img").hide();
//    $(".heading span").text($(".add img").data("total") + " files");
//}

//function InitializeFormSubmit() {
//    $(".submit").click(function () {
//        var data = $(this).closest("form").serializeArray();
//        $(".heading img").show();
//        $(".heading span").text("saving");
//        $.post("\\bluepill\\create\\savepicture", data, function (response) {

//            var json = $.parseJSON(response);

//            var img = $(".add img");
//            var link = $(".add a");
//            var hidden = $("form #File");

//            img.css("opacity", 0);
//            img.attr("src", json.resizedSrc);
//            img.data("total", json.total);
//            link.attr("href", json.src);
//            hidden.val(json.file);

//            SetHeadingCount();
//        });
//    });
//}

function InitializeImageLoad() {
    $(".add img").load(function () {
        $(this).animate({ opacity: 1 }, 200, function () {
            SetHeadingCount();
        });
        //ResetAccordion();
    });
}