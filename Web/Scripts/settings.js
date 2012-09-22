$(document).ready(function () {

    $(".collectionSelect").change(function () {

        $.ajax({
            url: "/application/settings/update",
            data: { "UserName" : "Test", "Collections" : "Whatever" },
            success: function (result) {
                $(".settingsContainer").empty();
                $(".settingsContainer").append(result);
                console.log("pass");
            }
        });

        //$(this).closest("form").submit();

    });

});