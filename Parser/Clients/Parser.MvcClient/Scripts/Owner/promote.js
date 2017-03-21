$(document).ready(function () {

    var usersContainer = $("#users");
    var hiddenUsername = $("#hidden-username");

    usersContainer.on("click", ".btn-promote", function (ev) {
        var target = $(ev.target);

        var username = target.data("username");
        hiddenUsername.val(username);

        $("#promote-form").submit();
    });

});