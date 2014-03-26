(function ($) {
    $(function () {
        $("ul.task-list").sortable({
            connectWith: "ul.task-list",
            receive: function (event, ui) {
                var issue = $(ui.item).data("github-issue"),
                    from = $(ui.sender).data("username"),
                    to = $(event.target).data("username");

                $.ajax({
                    type: "POST",
                    url: "Home/Transfer",
                    data: { issueNumber: issue, to: to },
                    success: function () { },
                    dataType: "json"
                });
            }
        });

        $("table.issues .trigger").click(function () {
            $(this).toggleClass("selected");
        });
    });
})(jQuery);