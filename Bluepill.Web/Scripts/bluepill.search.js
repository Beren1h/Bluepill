$(document).ready(function () {

    var slider = new Swipe($("#slider")[0], {
        disableScroll: false
    });

    CenterThatBitch();

    //help.css({ "left": target });

    $(window).bind("orientationchange", function (e) {
        CenterThatBitch();
    });

    $(window).resize(function () {
        console.log("wtf");
    });
});


function CenterThatBitch() {

    var help = $(".help");
    var target = (help.first().width() - 200) / 2;

    help.each(function () {
        var left = parseInt($(this).css("left"));
        $(this).css({ "left": left + target });
    });

}