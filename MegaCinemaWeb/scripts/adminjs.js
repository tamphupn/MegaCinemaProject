$('input[name="fileUpload"]').change(function () {
    var fileName = $(this).val();
    $('#custome-file-upload').text(fileName);
});