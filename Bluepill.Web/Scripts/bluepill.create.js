$("document").ready(function () {

    $(".a").each(function () {
        var parent = FindParent($(this));
        if (parent.attr("id") == undefined) {
            $(this).show();
        }
    });


    $(".ancestor").click(function(){
        var here = $(this).closest(".a");
        var parent = here.parent().closest(".a").attr("id");

        //console.log("here = " + here.attr("id") + ", parent = " + parent);

        var facet = $(this).closest(".a");
        var children = facet.children(".a");

        children.each(function () {
            $(this).hide();
        });

        here.data("open", false);
        console.log(here.attr("id") + " : " + here.data("open"));

        return false;
    });

    $(".facet").click(function (e) {
        var facet = $(this).closest(".a");
        var children = facet.children(".a");
        
        children.each(function () {
            $(this).show();
        });

        facet.data("open", true);
        console.log(facet.attr("id") + " : " + facet.data("open"));
    });



});

function FindParent(selector) {
    var closest = selector.closest(".a");
    var parent = closest.parent().closest(".a");
    return parent;
}

function FindChildren(selector) {
    return selector.find(".a");
}