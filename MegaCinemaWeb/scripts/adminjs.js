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

//Film insert 

$(document).ready(function () {
    $('[data-toggle="tooltip"]').tooltip();
    $('.film-information-detail').hide();
    $('#film-get-detail').click(function(event){
        event.stopImmediatePropagation();
        GetDataFilm($('#film-create-name').val());
    });
    $('#btn-predition-film-submit').click(function (event) {
        event.stopImmediatePropagation();
        
        PreditionFilm($('#film-create-name').val());
    });

    $('#deleteConfirm').click(function (event) {
        event.stopImmediatePropagation();

        window.location.href = $(this).attr('url');
    });
});

function ModalConfirm(Url) {
    $('#ConfirmModal').modal({
        backdrop : 'static',
        keyboard: false,
    });
    $('#deleteConfirm').attr('url',Url);
}


function ConvertDate(value) {
    var pattern = /Date\(([^)]+)\)/;
    var results = pattern.exec(value);
    var dt = new Date(parseFloat(results[1]));
    return dt.getDate() + "/" + (dt.getMonth() + 1) + "/" + dt.getFullYear();
};

function ConvertCurrency(nStr) {
    nStr += '';
    var x = nStr.split('.');
    var x1 = x[0];
    var x2 = x.length > 1 ? '.' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1)) {
        x1 = x1.replace(rgx, '$1' + ',' + '$2');
    }
    return x1 + x2;
};

function PreditionFilm(filmName) {
    $.ajax({
        url: '/Film/FilmPredition',
        type: 'POST',
        dataType: 'JSON',
        async: true,
        beforeSend: function () {
            $.blockUI({
                message: '<img src="../../../Assets/loading.gif" style="width:150px; height:150px"/>',
                css: {
                    border: 'none',
                    backgroundColor: 'transparent'
                }
            });
        },

        data: {
            filmName: filmName,
        },

        success: function (respone) {
            //Binding data           
            $.unblockUI();
            var result = respone.value;
            //Show data to predition 
            $('ul.form-group-film-information-area').attr('visibility', 'none');

            //Show form
            $('.form-group-film-information-area li:last-child').attr('style', '');
            var temp;
            if (result == 0) temp = "FLOP";
            if (result == 1) temp = "HIT";
            if (result == 2) temp = "SUPER HIT";
            $('.film-detail-predidtion').text(temp);
        },
        error: function (xhr) {
            console.log(xhr.message);
        }
    });
}

function GetDataFilm(filmName) {
    $.ajax({
        url: '/Film/GetDetailFilmFromIMDB',
        type: 'POST',
        dataType: 'JSON',
        async: true,
        beforeSend: function () {
            $.blockUI({
                message: '<img src="../../../Assets/loading.gif" style="width:150px; height:150px"/>',
                css: {
                    border: 'none',
                    backgroundColor: 'transparent'
                }
            });
        },
        data : {
            filmName: filmName,
        },
        success: function(respone) {
            //Binding data 
            var result = respone.value;
            $.unblockUI();

            //Show data to predition 
            $('.film-detail-name').text(result.MovieName);
            $('.film-detail-duration').text(result.MovieDuration);
            $('.film-detail-release').text(result.MovieReleaseDate);
            $('.film-detail-budget').text(ConvertCurrency(result.MovieBudget));
            $('.film-detail-genre').text(result.MovieGenre);
            $('.film-detail-actor').text(result.MovieStarActor);
            $('.film-detail-director').text(result.MovieDirector);
            $('.film-detail-production').text(result.MovieProduction);
            $('.film-detail-predidtion').text(result.MovieBudget);
            $('.form-group-film-detail-poster').attr('src', result.MovieLinkPoster);
            $('.film-detail-predidtion').text(result.MovieBudget);
            $('.film-detail-trailer').attr('src', result.MovieLinkTrailer);
            //Show form
            $('.film-information-detail').show();
        },
        error: function(xhr) {
            console.log(xhr.message);
        }
    });
    
}

//End of film insert 