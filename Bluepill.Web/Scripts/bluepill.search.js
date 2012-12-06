$(document).ready(function () {

    //InitializeBreadCrumb();
    //InitializeAccordion();

    $(".match-area").mousewheel(MouseWheelHandler);

    $(".cancel").click(function () {
        ShowResults();
    });

    $(".edit").click(function () {
        ShowInterface();
    });

    $(".submit").click(function () {
            //var data = $(this).closest("form").serializeArray();
            var results = $(".matches")

            results.data("page", "2");

            MouseWheelHandler(null, -1, 0, 0);
    });

    $(".interface").show();
    $(".match-area").hide();
    $(".heading").hide();
    $(".cancel").hide();
    $("#matchCount").hide();
    $("#pageCount").hide();

    $(".button").button();


    //$(".submit").click(function () {
    //    var facets = [];

    //    $("input[type=checkbox]").each(function () {

    //        if ($(this).is(":checked")) {
    //            facets.push($(this).attr("id"));
    //        }
    //    });

    //    //console.log(JSON.stringify(list));
    //    //var data = { "file": "c:\\test\\test.txt", "facets": [{ "0": "1" }, { "1": "5" }] }

    //    //var data = { "File": "c:\\test\\testme    simpl.txt", "List": list }
    //    //var data2 = $("#test").serializeArray();
    //    //var data3 = { "List": list }
    //    //console.log($("#test").serializeArray());
    //    //$.post("\\layout\\create\\save", data3, function () {
    //    //    console.log("posted");
    //    //});

    //    $.ajax({
    //        type: "POST",
    //        url: "\\bluepill\\search\\find",
    //        data: { page: $("#Page").val(), delta: $("#PageDelta").val(), total: $("#TotalPages").val(), selects: facets },
    //        //data: $("#test").serializeArray(),
    //        traditional: true,
    //        success: function (response) {

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

    //        }
    //    });
    //});


});


function IncrememntPage(page, max, delta) {

    if (delta > 0 && page < max) return 1;
    if (delta < 0 && page > 1) return -1;

    return 0;
}

function MouseWheelHandler(event, delta, deltaX, deltaY) {
    
    var results = $(".matches");
    var page = parseInt(results.data("page"));
    var max = parseInt(results.data("max"));
    var increment = IncrememntPage(page, max, delta);

    //console.log("page = " + page + ", max = " + max + ", delta = " + delta + ", increment = " + increment);

    if (increment != 0) {
        
        $("#Page").val(page + increment)
        //var data = $("#searchForm").serializeArray();

        var facets = [];

        $("input[type=checkbox]").each(function () {
            if ($(this).is(":checked")) {
                facets.push($(this).attr("id"));
            }
        });

        var datax = { Page: $("#Page").val(), PageDelta: $("#PageDelta").val(), TotalPages: $("#TotalPages").val(), selects: facets };
        var data2 = $("#searchForm").serializeArray();
        var data = $.param(datax, true);

        console.log(data);
        console.log(data2);

        $(".match-area").unmousewheel(MouseWheelHandler);

        //$.ajax({
        //    type: "POST",
        //    url: "\\bluepill\\search\\find",
        //    data: data,
        //    //data: $("#test").serializeArray(),
        //    traditional: true,
        //    success: function (response) {

        //        var json = $.parseJSON(response);

        //        var img = $(".add img");
        //        var link = $(".add a");
        //        var hidden = $("form #File");

        //        img.css("opacity", 0);
        //        img.attr("src", json.resizedSrc);
        //        img.data("total", json.total);
        //        link.attr("href", json.src);
        //        hidden.val(json.file);

        //        SetHeadingCount();

        //    }
        //});



        $(".match-area").load("search\\find", data, function () {

            $(".match-area img").load(function () {
                
                $(this).animate({ opacity: 1 }, 200);

                
                $(".match").draggable();

                $(".trash").droppable({
                    drop: function (e, ui) {
                        var index = $(ui.draggable).data("index")
                        
                        //remove from database;

                        var img = new Image();

                        img.id = "removeImage" + index;
                        img.src = "/application/picture/removepicture?index=" + index;
                        img.width = 1;
                        img.height = 1;


                        $(".drop-gradient").animate({ opacity: 0.2 }, 200, function () {
                            $(".drop-gradient h2").show();
                        });
                        $(ui.draggable).remove();
                    },
                    over: function (e, ui) {
                        $(".drop-gradient").animate({ opacity: 1 }, 200, function () {
                            $(".drop-gradient h2").hide();
                        });
                    },
                    out: function(e, ui){
                        $(".drop-gradient").animate({ opacity: 0.2 }, 200, function () {
                            $(".drop-gradient h2").show();
                        });
                    }
                });


            });
            UpdatePageDisplay(delta);
            ShowResults();
            UpdateMatchCount();
            $(".match-area").mousewheel(MouseWheelHandler);
        });

    }
    else {
        UpdatePageDisplay(delta);
    }

    return false;

}

function ShowInterface() {
    $(".interface").fadeIn("fast");
    $(".match-area").hide();
    $(".heading").hide();
}

function ShowResults() {
    $(".match-area").fadeIn("fast")
    $(".interface").hide();
    $(".heading").fadeIn("fast");

    if ($(".matches").data("boxes") == "0") {
        $(".match-counts ul").hide();
    }
    else {
        $(".match-counts ul").show();
    }

    $(".cancel").show();
}

function UpdateMatchCount() {
    var count = $(".matches").data("boxes");
    $("#boxes").text(count);
}

function UpdatePageDisplay(delta) {

    var results = $(".matches");
    var current = parseInt($("#currentPage").text());
    var max = parseInt(results.data("max"));
    
    $("#currentPage").text(results.data("page"));
    $("#maxPage").text(results.data("max"));

    if (delta < 0 && current == 1) {
        BlinkPageCount();
    }

    if (delta > 0 && current == max) {
        BlinkPageCount();
    }
}

function BlinkPageCount() {
    $(".pull-left").animate({ opacity: 0 }, 300);
    $(".pull-left").animate({ opacity: 1 }, 300);
}