$(document).ready(function () {
 
    $(".actions .btn-submit").click(function () {
        //var data = $("form").serializeArray();
        $(".criteria-area").hide();
        $(".progress").show();
        $(".matches").data("page", "2");
        //$(".matches").data("max", 0);
        //$(".matches").data("boxes", 0);

        var isMobile = $(".match-area").data("is-mobile");

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
        $(".infos").hide();
    });

});


function SetWorkingState(message, callback) {
    //$(".content").hide();
    //$(".badge").hide();
    //$(".bar span").text(message);
    //$(".progress").show(callback);
}

function IncrememntPage(page, max, delta) {

    if (delta > 0 && page < max) return 1;
    if (delta < 0 && page > 1) return -1;

    return 0;
}

function LoadMore() {

    var data = $("form").serializeArray();
    $(".match-area").load("search\\find", data, function (response) {

        var page = $(".matches").data("page");
        var total = $(".matches").data("max");
        var matches = $(".matches").data("boxes");
        console.log(response);
        $("#page-info").text("page " + page + " of " + total);
        $("#match-info").text(matches + " matches");

        $(".match").draggable({
            revert: true
        });

        $(".match-area").mousewheel(MouseWheelHandler);

        $(".match-area").fadeIn();

        $(".match-area img").load(function () {
            $(this).animate({ "opacity": 1 }, 200);
        });

        $(".progress").hide();
        $(".infos").show();

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

        //var isMobile = $(".match-area").data("is-mobile");
        
        LoadMore();

        //$(".match-area").load("search\\find", data, function (response) {

        //    if (isMobile == "True") {

        //        //swipe.js doesn't initialize properly if the area is hidden (width calc's maybe)
        //        $(".match-area").fadeIn(function () {
        //            InitializeSlider(0);
        //        });

        //    }
        //    else {

        //        $(".match").draggable({
        //            revert: true
        //        });

        //        $(".match-area").mousewheel(MouseWheelHandler);

        //        $(".match-area").fadeIn();

        //        $(".match-area img").load(function () {
        //            $(this).animate({ "opacity": 1 }, 200);
        //        });


        //    }


            //$(".progress").hide();
            //$(".infos").show();



        //});
    }
    else {
        //UpdatePageDisplay(delta);
    }

    return false;
}