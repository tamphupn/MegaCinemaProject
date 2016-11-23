$('input[name="fileUpload"]').change(function () {
    var fileName = $(this).val();
    $('#custome-file-upload').text(fileName);
});

$(function () {
    $('#alert-custombox').removeClass('hide');
    $('#alert-custombox').delay(1000).slideUp(500);
});


//Cinema Feature js 
$('.check-box-cinema').is(':checked', function () {
    alert(1);
});
//End of Cinema Feature 