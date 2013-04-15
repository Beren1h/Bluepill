$(document).ready(function () {

    //InitializeSlider(0);

    //window.slider.transitionEnd = Swiped;

    //$(window).bind("orientationchange", function (e) {
    //    CenterInSwipe();
    //});

});


function InitializeSlider(index) {

    delete window.slider;

    window.slider = new Swipe($("#slider")[0], {
        startSlide: index,
        disableScroll: false,
        stopPropagation: true,
        callback: Swiped,
        //transitionEnd: Swiped
    });

    console.log("initialize");

    CenterInSwipe();
}




function Swiped(i, e) {
    //var slider = window.slider;

    var load = $(e).find(".match").data("load");
    var page = $(".matches").data("page");


    if (load == "up") {
        LoadMoreMobile(1);
    }

    if (load == "down") {
        LoadMoreMobile(-1);
    }


    //var index = slider.getPos();
    //var max = slider.getNumSlides() - 1;
    //var upIndex = $(e).find(".wrap").data("upIndex");
    //var downIndex = $(e).find(".wrap").data("downIndex");

    //if (upIndex > -1) {


    //    GetResults(upIndex, 1, false);


    //}


    //if (downIndex > -1) {

    //    GetResults(downIndex, 3, true);

    //}

}

function LoadMoreMobile(delta) {

    var currentPage = parseInt($("#Page").val());
    $("#Page").val(currentPage + delta)

    var data = $("form").serializeArray();

    //alert($("#Page").val());

    $(".match-area").load("search\\find", data, function (response) {

        var page = $(".matches").data("page");
        var total = $(".matches").data("max");
        var matches = $(".matches").data("boxes");

        $("#page-info").text("page " + page + " of " + total);
        $("#match-info").text(matches + " matches");

        var frames = $(".frame").length;
        var slideIndex = (delta < 0) ? frames - 2 : 1;

        //if this is the initial page 1 load set the index to 0
        slideIndex = (delta == 0) ? 0 : slideIndex;

        //alert("frames = " + frames + " delta = " + delta + " slide index = " + slideIndex);

        $(".match-area").fadeIn(function () {
            InitializeSlider(slideIndex);
        });

        //var match = (page - 1) * 22 + window.slider.getPos() + 1;
        //$("#item-" + page + "-" + window.slider.getPos()).text(match);

        $(".z").each(function (i) {
            //console.log((page - 1) * 21 + (i + 1));
            $(this).text((page - 1) * 21 + (i+1));
        });

        $(".match-area img").load(function () {
            $(this).animate({ "opacity": 1 }, 10);
            //swipe.js doesn't initialize properly if the area is hidden (width calc's maybe)
        });


        $(".progress").hide();
        $(".infos").show();

     });


}

//function GetResults(index, startIndex, check) {


//    $.ajax({
//        url: "\\bluepill\\search\\test",
//        data: { "index": index },
//        success: function (response) {


//            $(".results-area").html(response);

//            $(".swipe").fadeIn();

//            if (check) {
//                var slides = $(response.trim()).find(".frame");
//                if (slides.length < 5) {
//                    startIndex = startIndex - 1;
//                }

//            }

//            InitializeSlider(startIndex);

//        }
//    });

//}

function CenterInSwipe() {

    var help = $(".frame");
    var target = (help.first().width() - 200) / 2;

    help.each(function () {
        var left = parseInt($(this).css("left"));
        $(this).css({ "left": left + target });
    });

}