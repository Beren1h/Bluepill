$(document).ready(function () {

    InitializeBreadCrumb();
    InitializeAccordion();

    $(".next").button({
        icons: { secondary: "ui-icon-circle-triangle-e" }
    });

    $(".previous").button({
        icons: { primary: "ui-icon-circle-triangle-w" }
    });

    $(".top").button({
        icons: { secondary: "ui-icon-circle-arrow-n" }
    });

    $(".remove").button({
        icons: { secondary: "ui-icon-circle-close" }
    });

    $(".create").click(function (e) {
        ShowInterface();
        e.preventDefault();
    });

    $("#formCancel").click(function () {
        //$(".breadCrumb").each(function () {
        //    $(this).text("");
        //});
        //$(".aspect-checkbox").each(function () {
        //    $(this).attr("checked", false);
        //});
        //$(".aspectLabel").each(function () {
        //    $(this).attr("aria-pressed", false);
        //    $(this).removeClass("ui-state-active");
        //});
        ShowResults();
    });

    $("#formSubmit").click(function () {
        //$("#results").data("display-state", "on");
            var data = $(this).closest("form").serializeArray();
            $("#heading img").show();
            $("#heading #message").text("saving");
            $.post("\\administration\\search\\find", data, function (response) {

                var json = $.parseJSON(response);
                console.log(json);
                //var img = $("div.right img");
                //var link = $("div.right a");
                //var hidden = $("form #File");

                //img.css("opacity", 0);
                //img.attr("src", json.resizedSrc);
                //img.data("total", json.total);
                //link.attr("href", json.src);
                //hidden.val(json.file);

                //SetHeadingCount();
            });



        ShowResults();
    });

    $("#interface").show();
    $("#results").hide();
    $("#search-controls").hide();
    $("#formCancel").hide();
    $("#matchCount").hide();
    $("#pageCount").hide();
    //$("#results-placeholder").show();

});

function ShowInterface() {
    $("#interface").fadeIn("fast");
    $("#results").hide();
    $("#results-placeholder").hide();
    $("#search-controls").hide();
    $("#matchCount").hide();
    $("#pageCount").hide();
}

function ShowResults() {
    //var displayState = $("#results").data("display-state");

    //if (displayState == "on") {
    //    $("#results").fadeIn("fast")
    //    $("#results-placeholder").hide();
    //}
    //else {
    //    $("#results").hide();
    //    $("#results-placeholder").fadeIn("fast")
    //}
        
    $("#results").fadeIn("fast")
    $("#interface").hide();
    $("#search-controls").fadeIn("fast");
    $("#formCancel").show();
    $("#matchCount").show();
    $("#pageCount").show();

}