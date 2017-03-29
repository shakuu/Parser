$(document).ready(function () {

    var usersContainer = $("#users");
    var hiddenUsername = $("#hidden-username-demote");

    usersContainer.on("click", ".btn-demote", function (ev) {
        var target = $(ev.target);

        var username = target.data("username");
        hiddenUsername.val(username);

        $("#demote-form").submit();
    });

});