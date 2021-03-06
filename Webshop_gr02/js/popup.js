﻿$.ajaxSetup({ cache: false });

$(document).ready(function () {
    $(".openPopup").live("click", function (e) {
        e.preventDefault();

        $("")
            .addClass("dialog")
            .attr("id", $(this)
            .attr("data-dialog-id"))
            .appendTo("body")
            .dialog({
                title: $(this).attr("data-dialog-title"),
                close: function () { $(this).remove(); },
                modal: true,
                height: 250,
                width: 900,
                left: 0

            })
            .load(this.href);
    });

    $(".close").live("click", function (e) {
        e.preventDefault();
        $(this).closest(".dialog").dialog("close");
    });
});