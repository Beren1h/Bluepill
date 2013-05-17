$(document).ready(function () {

    $(window).bind("orientationchange", function (e) {
        CenterInSwipe();
    });

    SetRemoveHanlder();

});

function SetRemoveHanlder() {

    $("#mobile-remover").click(function () {
        var match = $(".match[data-list-position=" + $(this).data("target") + "]");
        var img = match.find("img");
        var matches = $(".match");
        var slice = matches.slice(match.data("index") + 1);

        var del = new Image();
        del.id = "removeImage" + match.data("index");
        del.src = "/application/picture/removepicture?index=" + match.data("index");
        del.width = 1;
        del.height = 1;

        match.data("removed", 1);
        img.animate({ "opacity": 0.2 }, 500, UpdateMatchCountDisplay(match.data("removed")));

        slice.each(function () {
            var position = $(this).data("list-position");
            $(this).data("list-position", position - 1);
        });

        var adjustedBoxCount = $(".matches").data("boxes") - 1;
        $(".matches").data("boxes", adjustedBoxCount);
        $("#matches").text(adjustedBoxCount);
    });
}

function InitializeSlider(index) {

    delete window.slider;

    window.slider = new Swipe($("#slider")[0], {
        startSlide: index,
        disableScroll: false,
        stopPropagation: true,
        //callback: Swiped,
        //transitionEnd: UpdateMatchCounts
        //callback: Start,
        transitionEnd: SwipeEnd 
    });

    CenterInSwipe();
}

function SwipeEnd(i, e) {
    var load = $(e).find(".loader").data("load");
    var match = $(e).find(".match");
    var position = match.data("list-position");

    if (load == "up") {
        LoadMoreMobile(1);
    }

    if (load == "down") {
        LoadMoreMobile(-1);
    }

    if (position != null) {
        $("#match").text(position);
        $("#mobile-remover").data("target", position);
        UpdateMatchCountDisplay(match.data("removed"));
    }
}

function UpdateMatchCountDisplay(removed) {

    if (removed == 1) {
        $(".mobile-match-count span").hide();
        $("#match-conjuction").text("removed").show();
    }
    else {

        $("#match-conjuction").text(" of ");
        $(".mobile-match-count span").show();
    }
}

function LoadMoreMobile(delta) {

    var currentPage = parseInt($("#Page").val());
    $("#Page").val(currentPage + delta)

    var data = $("form").serializeArray();

    $(".match-area").load("search\\find", data, function (response) {

        var current = $(".match:first").data("list-position");
        var startIndex = 0;

        if (delta < 0) {
            startIndex = $(".frame").length - 2;
            current = $(".match:last").data("list-position");
        }

        if (delta > 0) {
            startIndex = 1;
        }

        $(".match-area").fadeIn(function () {
            InitializeSlider(startIndex);
        });

        UpdateMatchCountDisplay(0);

        $("#match").text(current);
        $("#match-conjuction").text(" of ");
        $("#matches").text($(".matches").data("boxes"));

        $("#mobile-remover").data("target", current);

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