$(document).ready(function () {

    $(window).bind("orientationchange", function (e) {
        CenterInSwipe();
    });


    $("#mobile-remover").click(function () {

        var index = $(this).data("index").toString();
        
        //var matches = $(".matches").data("boxes");
        var removedFrame = $("#mobile-frame-" + index);
        var removedMatch = removedFrame.find(".match");
        var removedImage = removedMatch.find("img");
        var boxes = $(".matches").data("boxes");
        //var matchNumber = parseInt($("#match").text());

        ////$("#matches").text(matches - 1);
        ////$("#match").text(matchNumber - 1);
        $(".matches").data("boxes", boxes - 1);

        removedMatch.data("removed", 1);

        removedImage.animate({ "opacity": 0.2 }, 500, RemovedMatchHanlder(removedMatch));

    });

});


function InitializeSlider(index) {

    delete window.slider;

    window.slider = new Swipe($("#slider")[0], {
        startSlide: index,
        disableScroll: false,
        stopPropagation: true,
        callback: Swiped,
        transitionEnd: UpdateMatchCounts
    });

    CenterInSwipe();
}


function RemovedMatchHanlder(match) {

    if (match.data("removed") == 1) {
        $(".mobile-match-count span").hide();
        $("#match-conjuction").text("removed").show();
    }
    else {

        $("#match-conjuction").text(" of ");
        $(".mobile-match-count span").show();
    }

}


function UpdateMatchCounts(i, e) {

    var index = $(e).find(".match").data("index");
    var page = $(".matches").data("page");
    var current = (page - 1) * 21 + (i + 1);
    var matches = $(".matches").data("boxes");
    
    var slice = $(".match").slice(0, i);

    var removesBefore = 0;
    slice.each(function () {
        if ($(this).data("removed") == 1)
            removesBefore++;
    });

    $("#match").text(current - removesBefore);
    $("#matches").text(matches);
    $("#mobile-remover").data("index", index);
    $(".mobile-match-count span").show();

    RemovedMatchHanlder($("#mobile-frame-" + index).find(".match"));
}

function Swiped(i, e) {
    var load = $(e).find(".match").data("load");

    if (load == "up") {
        LoadMoreMobile(1);
    }

    if (load == "down") {
        LoadMoreMobile(-1);
    }
}

function LoadMoreMobile(delta) {
    var currentPage = parseInt($("#Page").val());
    $("#Page").val(currentPage + delta)

    var data = $("form").serializeArray();

    $(".match-area").load("search\\find", data, function (response) {

        var matches = $(".matches").data("boxes");

        $("#match").text("1");
        $("#match-conjuction").text(" of ");
        $("#matches").text(matches);

        var frames = $(".frame").length;
        var slideIndex = (delta < 0) ? frames - 2 : 1;

        slideIndex = (delta == 0) ? 0 : slideIndex;

        $(".match-area").fadeIn(function () {
            InitializeSlider(slideIndex);
        });

        $(".match-area img").load(function () {
            $(this).animate({ "opacity": 1 }, 10);
            //swipe.js doesn't initialize properly if the area is hidden (width calc's maybe)
        });

        $(".progress-row").hide();
        $(".results-row").show();
     });

}

function CenterInSwipe() {

    var help = $(".frame");
    var target = (help.first().width() - 200) / 2;

    help.each(function () {
        var left = parseInt($(this).css("left"));
        $(this).css({ "left": left + target });
    });
}