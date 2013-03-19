
$("document").ready(function () {

    InitializeImageLoad();

});

function InitializeImageLoad() {
    $(".add img").load(function () {

        $(this).animate({ opacity: 1 }, 300, function () {
            SetActionState();
            Clear();
        });

    });
}

$(".actions .btn-submit").click(function () {

    SetWorkingState("Saving ...", SavePicutre());

});


function SavePicutre() {

    var data = $("form").serializeArray();

    $.post("\\bluepill\\create\\savepicture", data, function (response) {

        var json = $.parseJSON(response);

        if (json.total > 0) {

            $(".add img").css("opacity", 0);
            $(".add img").attr("src", json.resizedSrc);
            $(".add a").attr("href", json.url);
            $("form #Url").val(json.url);
            $(".badge").text(json.total + " files");
        }

    });


}

function SetWorkingState(message, callback) {
    $(".content").hide();
    $(".badge").hide();
    $(".bar span").text(message);
    $(".progress").show(callback);
}

function SetActionState() {
    $(".progress").hide();
    $(".content").fadeIn();
    $(".badge").fadeIn();
}