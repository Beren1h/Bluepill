$(function () {

    $(document).ready(function () {
        imageLoaded();
        imageClick();
    });

    $(window).load(function () {
        //all images loaded at this point
        $("#newFiles").show();
        $("#heading").html(total + " files");
    });

    function imageLoaded() {
        loaded++;
        if(loaded != display){
            $("#message").html(loaded + " of " + display);
        }
    }

    var images = $("img.newImage");
    var total = $("#totalFileCount").html();
    var display = images.length;
    var loaded = 0;

    images.each(function () {
        if (this.complete) {
            imageLoaded.call(this);
        } else {
            $(this).one("load", imageLoaded);
        }
    });

});

function imageClick() {
    $(".showDialog").each(function () {
        $(this).click(function (e) {
            
            var form = $("#createDialog");

            form.dialog({
                autoOpen: false,
                width: 1000,
                height: 550,
                modal: true,
                resizeable: false,
                close: function () {
                    form.html("<h1>loading</h1>");
                }
            });

            form.dialog("open");
            e.preventDefault();
        });
    });
}