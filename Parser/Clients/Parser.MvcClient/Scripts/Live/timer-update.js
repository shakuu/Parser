$(document).ready(function () {

    var updateToggle = $("#update-toggle");

    window.setInterval(function () {

        if (updateToggle.is(":checked")) {
            $("#update-form").submit();
        }

    }, 1000);
    
});