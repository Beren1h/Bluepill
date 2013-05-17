$(document).ready(function () {

    Activate("nav-search");

    $(".actions .btn-submit").click(function () {
        $(".criteria-area").hide();
        $(".progress-row").show();
        $(".matches").data("page", "2");
        $(".criteria-row").hide();

        var isMobile = $("body").data("is-mobile");

        if (isMobile == "False") {
            MouseWheelHandler(null, -1, 0, 0);
        }
        else {
            LoadMoreMobile(0)
        }
       
    });

    $(".edit").click(function () {
        $(".match-area").hide();
        $(".criteria-area").show();
        $(".results-row").hide();
        $(".criteria-row").show();
        $(".btn-navbar").addClass("collapsed");
    });

});

function IncrememntPage(page, max, delta) {

    if (delta > 0 && page < max) return 1;
    if (delta < 0 && page > 1) return -1;

    return 0;
}

function LoadMore(showAll) {

    var data = $("form").serializeArray();

    $(".match-area").load("search\\find", data, function (response) {

        var page = $(".matches").data("page");
        var total = $(".matches").data("max");
        var matches = $(".matches").data("boxes");
        $("#page-info").text("page " + page + " of " + total);
        $("#match-info").text(matches + " matches");

        $(".match").on("dragstop", function (e, ui) {
            $(this).removeClass("kill");
        });

        $(".match").draggable({
            revert: true,
            start: function (e, ui) {
                console.log("start");
                //$(ui.draggable.context).animate({"height": 10});
                $(".drag-drop").animate({ backgroundColor: "#000020", color: "#83B0E1" }, "fast");
                $(".drag-drop span").animate({ color: "#83B0E1", opacity: 0.5 }, "fast");
            },
            stop: function (e, ui) {
                console.log("stop");
                $(".drag-drop").animate({ backgroundColor: "#000000", color: "#ffffff" }, "fast");
                $(".drag-drop span").animate({ color: "#1a3e4f", opacity: 0.5 }, "fast");
                //$(ui.draggable.context).removeClass("kill");
            }
        });

        $(".drag-drop").droppable({
            drop: function (e, ui) {
                var index = $(ui.draggable.context).data("index").toString();

                var del = new Image();
                del.id = "removeImage" + index;
                del.src = "/application/picture/removepicture?index=" + index;
                del.width = 1;
                del.height = 1;

                $(ui.draggable).remove();
                $(".drag-drop").animate({ backgroundColor: "#000000", color: "#ffffff" }, "fast");
                $(".drag-drop span").animate({ color: "#1a3e4f", opacity: 0.5 }, "fast");
            },
            over: function (e, ui) {
                $(ui.draggable.context).addClass("drop-ready");
            },
            out: function (e, ui) {
                $(ui.draggable.context).removeClass("drop-ready");
            },
        });

        $(".match-area").mousewheel(MouseWheelHandler);
        $(".match-area").fadeIn();

        $(".match-area img").load(function () {
            $(this).animate({ "opacity": 1 }, 200);
        });

        $(".progress-row").hide();
        $(".results-row").show();
    });
}


function MouseWheelHandler(event, delta, deltaX, deltaY) {

    var results = $(".matches");
    var page = parseInt(results.data("page"));
    var max = parseInt(results.data("max"));
    var increment = IncrememntPage(page, max, delta);
    console.log("increment = " + increment + " page = " + page + " max = " + max + " delta = " + delta);

    if (increment != 0) {

        $("#Page").val(page + increment)

        $(".match-area").unmousewheel(MouseWheelHandler);

        LoadMore();
    }

    return false;
}