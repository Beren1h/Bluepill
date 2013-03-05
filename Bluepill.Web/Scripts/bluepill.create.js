
$("document").ready(function () {

    InitializeImageLoad();

    //$(".submit").button();

    //$(".submit").click(function () {
    //    var data = $("form").serializeArray();
    //    $(".heading img").show();
    //    $(".heading span").text("saving");
    //    $.post("\\bluepill\\create\\savepicture", data, function (response) {

    //        var json = $.parseJSON(response);

    //        $(".heading").data("total", json.total);

    //        if (json.total > 0) {

    //            var img = $(".add img");
    //            var link = $(".add a");
    //            var hidden = $("form #File");

    //            img.css("opacity", 0);
    //            img.attr("src", json.resizedSrc);

    //            link.attr("href", json.src);

    //            $("form #File").val(json.file);
    //            $("form #Url").val(json.url);
    //        }
    //        else {
    //            $(".facets").remove();
    //            $(".add").remove();
    //            $(".heading").addClass("height12");
    //        }

    //        SetHeadingCount();
    //        ResetForm();
    //    });
    //});

    //$("body").addClass("content-gradient");

});

function InitializeImageLoad() {
    $(".add img").load(function () {
        $(this).animate({ opacity: 1 }, 300, function () {
            //SetHeadingCount();
        });
    });
}

//function SetHeadingCount() {

//    var total = $(".heading").data("total") 

//    $(".heading img").hide();

//    if (parseInt(total) == 0) {
//        $(".heading span").text("upload images to begin");
//        $(".add").remove();
//    }
//    else {
//        $(".heading span").text(total + " files");
//        $(".facets-area").fadeIn();
//    }

//}