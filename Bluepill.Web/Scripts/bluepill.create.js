
$("document").ready(function () {

    InitializeImageLoad();

    $(".submit").button();

    $(".submit").click(function () {

        var data = $("form").serializeArray();
        $(".heading img").show();
        $(".heading span").text("saving");
        $.post("\\bluepill\\create\\savepicture", data, function (response) {

            var json = $.parseJSON(response);

            var img = $(".add img");
            var link = $(".add a");
            var hidden = $("form #File");

            img.css("opacity", 0);
            img.attr("src", json.resizedSrc);
            img.data("total", json.total);
            link.attr("href", json.src);
            hidden.val(json.file);

            SetHeadingCount();
            ResetForm();
        });
    });

});

function InitializeImageLoad() {
    $(".add img").load(function () {
        $(this).animate({ opacity: 1 }, 300, function () {
            SetHeadingCount();
        });
    });
}

function SetHeadingCount() {
    $(".heading img").hide();
    $(".heading span").text($(".add img").data("total") + " files");
}