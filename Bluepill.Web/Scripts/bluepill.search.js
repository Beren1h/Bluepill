$(document).ready(function () {
 
    //$(".edit").parent().hide();
    //$(".edit").parent().siblings("li").text("Criteria");

    //$(".btn-show-all").click(function () {

    //    $(".criteria-area").hide();
    //    $(".progress").show();
    //    $(".matches").data("page", "2");

    //    var isMobile = $(".match-area").data("is-mobile");

    //    if (isMobile == "False") {
    //        MouseWheelHandler(null, -1, 0, 0);
    //    }
    //    else {
    //        LoadMoreMobile(0)
    //    }

    //});

    $(".actions .btn-submit").click(function () {
        //var data = $("form").serializeArray();
        $(".criteria-area").hide();
        $(".progress-row").show();
        $(".matches").data("page", "2");
        //$(".breadcrumb").show();
        //$(".edit").parent().removeClass("active");
        //$(".edit").parent().show();
        //$(".edit").parent().siblings("li").text("Results");
        //$(".matches").data("max", 0);
        //$(".matches").data("boxes", 0);
        $(".criteria-row").hide();
        //$(".results-row").show();

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
        $(".results-row").hide();
        $(".criteria-row").show();
        //$(".infos").hide();
        //$(".infos").show();
        //$(".inner-nav").addClass("offset10");
        //$(".remove").show();
        //$(this).parent().hide();
        //$(this).parent().siblings("li").text("Criteria");
        //console.log($(".edit").parent().siblings().length);
        //$(".breadcrumb").hide();
        //$(".edit").parent().addClass("active");
        $(".btn-navbar").addClass("collapsed");
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

function LoadMore(showAll) {

    var data = $("form").serializeArray();

    //if (showAll) {
    //    //Clear();
    //    data = { "Page": "1", "PageDelta": "0", "TotalPages": "0" }
    //}
    //else {
    //    data = $("form").serializeArray();
    //}
    
    //console.log(data);
    $(".match-area").load("search\\find", data, function (response) {

        var page = $(".matches").data("page");
        var total = $(".matches").data("max");
        var matches = $(".matches").data("boxes");
        //console.log(response);
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

        $(".progress-row").hide();
        $(".results-row").show();
        //$(".infos").show();
        //$(".inner-nav").removeClass("offset10");
        //$(".remove").show();


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