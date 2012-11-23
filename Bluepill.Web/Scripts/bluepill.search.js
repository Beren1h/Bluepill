$(document).ready(function () {

    InitializeBreadCrumb();
    InitializeAccordion();

    $(".next").button({
        icons: { secondary: "ui-icon-circle-triangle-e" }
    });

    $(".previous").button({
        icons: { primary: "ui-icon-circle-triangle-w" }
    });

    $(".top").button({
        icons: { secondary: "ui-icon-circle-arrow-n" }
    });

    $(".remove").button({
        icons: { secondary: "ui-icon-circle-close" }
    });

    $(".edit").click(function (e) {
        ShowInterface();
        e.preventDefault();
    });

    $("#display-area").on("DOMSubtreeModified", "#results", function () {
        console.log("load");
    });

    $("#display-area").mousewheel(MouseWheelHandler);

    $("#formCancel").click(function () {
        ShowResults();
    });





    $("#formSubmit").click(function () {
            var data = $(this).closest("form").serializeArray();
            $("#heading img").show();
            $("#heading #message").text("saving");

            var results = $("#results")
            results.data("page", "2");

            MouseWheelHandler(null, -1, 0, 0);
    });

    $("#interface").show();
    $("#results").hide();
    $("#search-controls").hide();
    $("#formCancel").hide();
    $("#matchCount").hide();
    $("#pageCount").hide();

});


function IncrememntPage(page, max, delta) {

    if (delta > 0 && page < max) return 1;
    if (delta < 0 && page > 1) return -1;

    return 0;
}


function MouseWheelHandler(event, delta, deltaX, deltaY) {
        
    var results = $("#results");
    var page = parseInt(results.data("page"));
    var max = parseInt(results.data("max"));
    var increment = IncrememntPage(page, max, delta);

    console.log("page = " + page + ", max = " + max + ", delta = " + delta);

    if (increment != 0) {

        $("#Page").val(page + increment)
        var data = $("#searchForm").serializeArray();
        $("#display-area").unmousewheel(MouseWheelHandler);

        $("#display-area").load("search\\find", data, function () {
            $("#display-area img").load(function () {
                $(this).animate({ opacity: 1 }, 200);


                $(".match").draggable({

                });

                $(".trash").droppable({
                    drop: function (e, ui) {
                        //console.log("drop");
                        //console.log($(ui.item).attr("class"));
                        //var x = $(ui.draggable).find("a").attr("href");
                        //console.log(x);
                        $(ui.draggable).remove();

                    }
                });

                //$(".match").mousedown(function (e) {
                //    console.log("click");
                //    switch (e.button) {
                //        case 1:
                //            console.log("left click");
                //            break;
                //        case 2:
                //            console.log("right click");
                //            break;
                //        default:
                //            console.log("fail");
                //    }

                //});


                //$(".match").click(function () {
                //    console.log("click");
                //});

            });
            UpdatePageDisplay(delta);
            ShowResults();
            UpdateMatchCount();
            $("#display-area").mousewheel(MouseWheelHandler);
        });
    }
    else {
        UpdatePageDisplay(delta);
    }

    return false;

}

function ShowInterface() {
    $("#interface").fadeIn("fast");
    $("#results").hide();
    $("#results-placeholder").hide();
    $("#search-controls").hide();
    $("#matchCount").hide();
    $("#pageCount").hide();
}

function ShowResults() {
    $("#results").fadeIn("fast")
    $("#interface").hide();
    $("#search-controls").fadeIn("fast");
    $("#formCancel").show();
    $("#matchCount").show();
    $("#pageCount").show();
}

function UpdateMatchCount() {
    var count = $("#results").data("boxes");
    $("#boxes").text(count);
}

function UpdatePageDisplay(delta) {

    var results = $("#results");
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
    $("#pageCount").animate({ opacity: 0 }, 300);
    $("#pageCount").animate({ opacity: 1 }, 300);
}