$(document).ready(function () {

    $(".button").button();

    $(".submit").click(function () {
        var data = $("form").serializeArray();
        var results = $(".matches")
        results.data("page", "2");
        MouseWheelHandler(null, -1, 0, 0);
    });

    $(".cancel").click(function () {
        $(".interface").hide();
        $(".match-area").show();
    });

    $(".edit").click(function () {
        ShowInterface();
    });

    $(".interface").show();
    $(".match-area").hide();
    $(".heading").hide();
    $(".cancel").hide();
    //$("#matchCount").hide();
    //$("#pageCount").hide();
});

function IncrememntPage(page, max, delta) {

    if (delta > 0 && page < max) return 1;
    if (delta < 0 && page > 1) return -1;

    return 0;
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

function BlinkPageCount() {
    $(".pull-left").animate({ opacity: 0 }, 300);
    $(".pull-left").animate({ opacity: 1 }, 300);
}

function UpdateMatchCount() {
    var count = $(".matches").data("boxes");
    $("#boxes").text(count);
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

        //var facets = [];

        //$("input[type=checkbox]").each(function () {
        //    if ($(this).is(":checked")) {
        //        facets.push($(this).attr("id"));
        //    }
        //});

        //var datax = { Page: $("#Page").val(), PageDelta: $("#PageDelta").val(), TotalPages: $("#TotalPages").val(), selects: facets };
        var data = $("form").serializeArray();
        //var data = $.param(datax, true);

        //console.log(data);
        //console.log(data2);

        $(".match-area").unmousewheel(MouseWheelHandler);

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
                    out: function (e, ui) {
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