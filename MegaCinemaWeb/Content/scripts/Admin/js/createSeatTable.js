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
var numberSimpleTicket = 0;
var seatTableState = [];
var typeTicket = -1;
var rows = 0;
var columns = 0;
//0 - simple 
//1 - vip
//-1 - none

$(document).ready(function () {
    numberVipTicket = parseInt($('.vip-ticket-detail').text());
    numberSimpleTicket = parseInt($('.simple-ticket-detail').text());
    rows = parseInt($('#seat-table-height').val());
    columns = parseInt($('#seat-table-width').val());
    init();
    $('#seat-table-create-event').click(function (event) {
        event.stopImmediatePropagation();
        CollectSeatTable();
    });

    //asssign event for ticket
    $('.case-vip-detail')
        .click(function(event) {
            event.stopImmediatePropagation();
            typeTicket = 1;
            $('.case-vip-detail').attr('style', 'color:red');
            $('.case-simple-detail').attr('style', 'color:black');
        });

    $('.case-simple-detail')
       .click(function (event) {
           event.stopImmediatePropagation();
           typeTicket = 0;
           $('.case-vip-detail').attr('style', 'color:black');
           $('.case-simple-detail').attr('style', 'color:red');
       });

    $('.' + settings.seatCss).click(function () {
        if (typeTicket == 1) {
            if ($(this).hasClass('seat-clicked')) {
                numberVipTicket = numberVipTicket + 1;
                $(this).removeClass(settings.selectingSeatCss);
                $(this).removeClass('seat-clicked');
                seatTableState[$(this).attr('title')].Value = 4;
                console.log($(this).attr('title'));
                $(this).attr('value', "");
            }
            else {
                if (numberVipTicket == 0) {
                    ModalConfirm();
                } else {
                    $(this).toggleClass(settings.selectingSeatCss);
                    $(this).addClass('seat-clicked');
                    $(this).attr('value', 2);

                    seatTableState[$(this).attr('title')].Value = 2;
                    console.log(seatTableState[$(this).attr('title')].Value);
                    numberVipTicket = numberVipTicket - 1;
                }
            }
        }
        else if (typeTicket == 0) {
            if ($(this).hasClass('seat-clicked')) {
                numberSimpleTicket = numberSimpleTicket + 1;
                $(this).removeClass('seat-simple-clicked');
                $(this).removeClass('seat-clicked');
                seatTableState[$(this).attr('title')].Value = 4;
                console.log($(this).attr('title'));
                $(this).attr('value', "4");
            }
            else {
                if (numberSimpleTicket == 0) {
                    ModalConfirm();
                } else {
                    $(this).toggleClass('seat-simple-clicked');
                    $(this).addClass('seat-clicked');
                    $(this).attr('value', 1);

                    seatTableState[$(this).attr('title')].Value = 1;
                    console.log(seatTableState[$(this).attr('title')].Value);
                    numberSimpleTicket = numberSimpleTicket - 1;
                }
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
                      'style="top:' + (i * settings.seatHeight).toString() + 'px;left:' + (j * settings.seatWidth).toString() + 'px" title="' + seatNo + '" value="4">' +
                      '<a title="' + seatNo + '">' + seatNo + '</a>' +
                      '</li>');
            seatTableState.push(objectProduct);
        }
    }
    seatTableState = str;
    $('#place').html(str.join(''));
};

function CollectSeatTable() {
    console.log(rows + " " + columns);
    $.ajax({
        url: '/RoomFilm/CreateRoomFilm',
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
            width: columns,
            height: rows,
            seatValueState: JSON.stringify(seatTableState),
        },
        success: function (respone) {
            $.unblockUI();
            if (respone.status === "OK") {
                alert("Đặt thành công");
            } else
                alert("đặt thất bại");
        },
        error: function (xhr) {
            $.unblockUI();
            console.log(xhr.message);
        }
    });
}