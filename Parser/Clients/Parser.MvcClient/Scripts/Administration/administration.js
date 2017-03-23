$(document).ready(function () {
    $('select').material_select();

    var dropdownMessageType = $('#dropdown-message-type'),
        dropdownPeriodType = $('#dropdown-period-type'),
        updateLogEntriesForm = $('#update-log-entries-form');

    dropdownMessageType.on('change', submitForm);
    dropdownPeriodType.on('change', submitForm);

    function submitForm() {
        updateLogEntriesForm.submit();
    }

});