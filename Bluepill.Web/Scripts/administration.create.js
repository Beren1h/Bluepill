$(function () {

    $(document).ready(function () {
        //imageLoaded();
        SetBreadCrumb();
        imageClick();

        $("#accordion").accordion({
            autoHeight: true,
            collapsible: true,
            active: false,
            change: function () {
                //$(".formContainer").animate({ scrollTop: $(".formContainer").prop("scrollHeight") - $(".formContainer").height() }, 250);
            }
        });

            $(".right img").load(function () {
                $(this).animate({ opacity: 1 }, 200);
            });

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

function SetBreadCrumb() {

    $(".aspect-checkbox").change(function () {

        var context = $(this).closest("div.ui-accordion-content");
        var breadCrumb = $("span#breadCrumb-" + $(this).data("facet"));
        var output = "";

        $(".aspect-checkbox", context).each(function () {
            if ($(this).is(":checked")) {

                var label = $("label[for='" + $(this).attr("id") + "']");

                if (output.length > 0) {
                    output += ", ";
                }

                output += label.text();
            }
        });
        breadCrumb.text(output);
    });
}
