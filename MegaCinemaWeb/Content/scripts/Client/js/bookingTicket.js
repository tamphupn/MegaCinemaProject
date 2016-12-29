//Fill detail ticket
function AddDetailBooking(item) {
    $.ajax({
        url: '/Booking/BookingSession',
        type: 'POST',
        data: {
            sessionFilm: item.attr('sessionFilm'),
            adultTicket: $('.adult').val(),
            adultVip: $('.adult-vip').val(),
        },
        dataType: 'json',
        success: function (respone) {
            if (respone.status) {
                window.location.href = respone.redirectUrl;
            }
        },
        error: function (xhr) {
            console.log("failed");
        }
    });
}


//Booking ticket 
var adult, adultVip;
var xValue, yValue;
var listTicketSelect = [];
var listTicketSubmit = [];

$(document).ready(function () {
    listTicketSelect = [];
    listTicketSubmit = [];
    adult = 0;
    adultVip = 0;

    //assign value ticket counter
    $('.ticket-user-detail ul li span:nth-child(1)')
        .each(function (index) {
            if (index == 0) adult = parseInt($(this).text());
            if (index == 1) adultVip = parseInt($(this).text());
        });

    console.log("số ticket adult " + adult);
    console.log("số ticket adultVip " + adultVip);

    //submit function
    $('.ticket-booking-seat').click(function (event) {
        event.stopImmediatePropagation();
        AddTicketToCart($(this));
    });
    $('.btn-submit-realtime').click(function (event) {
        event.stopImmediatePropagation();
        Update($(this));
    });
});

function ModalConfirm(content) {
    $('.confirm-modal-content').text(content);
    $('#ConfirmModal').modal({
        backdrop: 'static',
        keyboard: false,
    });
}

function AddTicketToCart(item) {
    //Create object
    var ticketSelect = {
        index: 0,
        id: $(item).attr('idseat'),
        nameSeat: $(item).text(),
        xLocation: $(item).attr('xLocation'),
        yLocation: $(item).attr('yLocation'),
        type: "",
    };
    //detect selected or not and set class 
    if (item.hasClass('ticket-clicked')) {
        item.removeClass('ticket-clicked');

        //detect number ticket

        //check normal ticket
        if (item.hasClass('normal-ticket')) {
            adult++;
            $(item).css('background-image', "");
        }
        else
            //hot ticket
            if (item.hasClass('hot-ticket')) {
                adultVip++;
                $(item).css('background-image', "");
            }

        //remove ticket select in queue
        for (var n = 0 ; n < listTicketSelect.length ; n++) {
            if (listTicketSelect[n].id == ticketSelect.id) {
                listTicketSelect.splice(n, 1);
                break;
            }
        }

        console.log("Số counter cho vé người lớn: " + adult + "- Số counter cho vé vip: " + adultVip);
        console.log("Độ dài queue: " + listTicketSelect.length);
        var resultQ;
        for (var n = 0; n < listTicketSelect.length; n++) {
            resultQ = resultQ + listTicketSelect[n].id + " ";
        }
        console.log("các vé trong hàng chờ hiện tại là " + resultQ);
        console.log("-----------");

    } else {
        if (item.hasClass('booked-ticket')) {
            alert('Ghế không còn hiệu lực, vui lòng chọn ghế khác');
            return;
        }

        //detect number ticket

        //check normal ticket
        if (item.hasClass('normal-ticket')) {
            if (adult > 0) {
                ticketSelect.type = 1;
                adult--;
                $(item).css('background-image', "url('../../Content/RoomFilm/selectedseat.png')");
            } else {
                alert('Số lượng vé bạn chọn đã hết, không thể chọn thêm');
                return;
            }

        }

        //hot ticket
        if (item.hasClass('hot-ticket')) {
            if (adultVip > 0) {
                ticketSelect.type = 2;
                adultVip--;
                $(item).css('background-image', "url('../../Content/RoomFilm/selectedseat.png')");
            } else {
                alert('Số lượng vé bạn chọn đã hết, không thể chọn thêm');
                return;
            }

        }

        //add ticket in queue
        if (listTicketSelect.length == 0) {
            ticketSelect.index = 0;
        } else {
            ticketSelect.index = listTicketSelect.length;
        }
        listTicketSelect.push(ticketSelect);

        xValue = item.attr('xLocation');
        yValue = item.attr('yLocation');
        item.addClass('ticket-clicked');
        console.log("Số counter cho vé người lớn: " + adult + "- Số counter cho vé vip: " + adultVip);
        console.log("Độ dài queue: " + listTicketSelect.length);
        var resultQ;
        for (var n = 0; n < listTicketSelect.length; n++) {
            resultQ = resultQ + listTicketSelect[n].id + " ";
        }
        console.log("các vé trong hàng chờ hiện tại là " + resultQ);
        console.log("-----------");
    }
    //update state
}

function Update(item) {
    //clone submit
    for (var i = 0; i < listTicketSelect.length; i++) {
        listTicketSubmit.push(listTicketSelect[i]);
    }

    $.ajax({
        url: '/Booking/UpdateSeatUserTake',
        type: 'POST',
        data: {
            xLocation: xValue,
            yLocation: yValue,
            sessionFilm: $('.ticket-room-time').val(),
            ticketValue: JSON.stringify(listTicketSubmit),
        },
        dataType: 'json',
        success: function (respone) {
            if (respone.status == "OK") {
                alert('đặt vé thành công');
                setTimeout(function() {
                    window.location.href = '/Home/HomePage';
                },2000);            
            }
            else if (respone.status == "KO") {
                alert('đặt vé thất bại');
            }

        },
        error: function (xhr) {
            console.log('failed');
        }
    });
}
//SignalIR

//End of SignalIR

//Booking ticket