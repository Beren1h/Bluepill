$("document").ready(function () {

    $(".nav li").removeClass("active");
    $(".nav li a").unbind("click");

});

function Activate(activeId) {

    $("#" + activeId).addClass("active");
    $("#" + activeId + " a").click(function () { return false; });

}
