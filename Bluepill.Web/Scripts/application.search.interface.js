function InitializeAccordion() {
    $("#accordion").accordion({
        autoHeight: false,
        collapsible: true,
        active: false,
    });
}

function ResetAccordion() {
    $(".breadCrumb").each(function () {
        $(this).text("");
    });

    $("#accordion .aspectContainer").each(function (i) {
        $("input[type=checkbox]", $(this)).attr("checked", false);

        var label = $("label", $(this));
        label.removeClass("ui-state-active");
        label.attr("aria-pressed", false);
    });
    InitializeAccordion();
}

function InitializeBreadCrumb() {
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