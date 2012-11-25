$(document).ready(function () {

    InitializeBreadCrumb();
    InitializeAccordion();

    $(".match-area").mousewheel(MouseWheelHandler);

    $(".cancel").click(function () {
        ShowResults();
    });

    $(".edit").click(function () {
        ShowInterface();
    });

    $(".submit").click(function () {
            var data = $(this).closest("form").serializeArray();
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

    console.log("page = " + page + ", max = " + max + ", delta = " + delta + ", increment = " + increment);

    if (increment != 0) {

        $("#Page").val(page + increment)
        var data = $("#searchForm").serializeArray();
        $(".match-area").unmousewheel(MouseWheelHandler);

        $(".match-area").load("search\\find", data, function () {
            $(".match-area img").load(function () {
                
                $(this).animate({ opacity: 1 }, 200);
                
                $(".match").draggable();

                $(".trash").droppable({
                    drop: function (e, ui) {
                        $(ui.draggable).remove();
                        //remove from database;
                    },
                    over: function (e, ui) {
                        console.log("hover");
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