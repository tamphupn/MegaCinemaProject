var settings = {
    rows: $('#seat-table-height').val(),
    cols: $('#seat-table-width').val(),
    rowCssPrefix: 'row-',
    colCssPrefix: 'col-',
    seatWidth: 35,
    seatHeight: 35,
    seatCss: 'seat',
    selectedSeatCss: 'selectedSeat',
    selectingSeatCss: 'selectingSeat'
};

var numberVipTicket = 0;
var seatTableState = [];

$(document).ready(function () {
    numberVipTicket = parseInt($('.vip-ticket-detail').text());
    init();
    $('#seat-table-create-event').click(function (event) {
        event.stopImmediatePropagation();

    });

    $('.' + settings.seatCss).click(function () {
        if ($(this).hasClass('seat-clicked')) {
            numberVipTicket = numberVipTicket + 1;
            $(this).removeClass(settings.selectingSeatCss);
            $(this).removeClass('seat-clicked');
            seatTableState[$(this).attr('title')].Value = 0;
            console.log($(this).attr('title'));
            $(this).attr('value', "");
        }
        else {
            if (numberVipTicket == 0) {
                ModalConfirm();
            } else {
                $(this).toggleClass(settings.selectingSeatCss);
                $(this).addClass('seat-clicked');
                $(this).attr('value', 1);
                
                seatTableState[$(this).attr('title')].Value = 1;
                console.log(seatTableState[$(this).attr('title')].Value);
                numberVipTicket = numberVipTicket - 1;
            }
        }
    });
});


function ModalConfirm() {
    $('#ConfirmModal').modal({
        backdrop: 'static',
        keyboard: false,
    });
}


var init = function (reservedSeat) {
    
    var str = [], seatNo, className;
    for (i = 0; i < settings.rows; i++) {
        for (j = 0; j < settings.cols; j++) {
            seatNo = (i + j * settings.rows + 1);
            className = settings.seatCss + ' ' + settings.rowCssPrefix + i.toString() + ' ' + settings.colCssPrefix + j.toString();
            if ($.isArray(reservedSeat) && $.inArray(seatNo, reservedSeat) != -1) {
                className += ' ' + settings.selectedSeatCss;
            }
            var objectProduct = {};
            objectProduct.Title = seatNo;
            objectProduct.Value = 0;
            str.push('<li class="' + className + '"' +
                      'style="top:' + (i * settings.seatHeight).toString() + 'px;left:' + (j * settings.seatWidth).toString() + 'px" title="' + seatNo + '" value="">' +
                      '<a title="' + seatNo + '">' + seatNo + '</a>' +
                      '</li>');
            seatTableState.push(objectProduct);
        }
    }
    seatTableState = str;
    $('#place').html(str.join(''));
};

function SeatGenerate() {
    var lstResult = [];
    
}

function CollectSeatTable() {
    alert(seatTableState[1].Value);
    var objectResult = {
        rows: $('#seat-table-height').val(),
        cols: $('#seat-table-width').val(),
        tableSeat: null,
    };
}