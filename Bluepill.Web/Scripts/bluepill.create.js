
$("document").ready(function () {

    var total = $(".loading").data("file-total");

    SetBadgeCount(total);

    if (total == 0) {
        SetFinishedState();
    }
    else {
        InitializeImageLoad();
    }

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

        SetBadgeCount(json.total);

        if (json.total > 0) {

            $(".add img").css("opacity", 0);
            $(".add img").attr("src", json.resizedSrc);
            $(".add a").attr("href", json.url);
            $("form #Url").val(json.url);
            $("form #File").val(json.file);
        }
        else {
            SetFinishedState();
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

function SetFinishedState() {
    $(".content").hide();
    $(".badge").show();
    $(".progress").hide();
}

function SetBadgeCount(total) {

    var qualifier = " files";

    if (total == 1)
        qualifier = " file";

    $(".badge").text(total + qualifier);
}