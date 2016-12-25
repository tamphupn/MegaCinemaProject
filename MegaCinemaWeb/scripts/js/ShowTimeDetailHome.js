var DofW = new Array();
var cnm;
var mv;
var titlep;
var sub = "";
var counttime = 0;
var iddate = new Array();
var countdate = 0;
var nameold;
var namenew;
var sub_tt = "";
var str_name = "";
function detailTime(mvp, cnmmv, name) {
    counttime = 0;
    nameold = name;
    $('#coutdetail').val(counttime);
    $('.click_box_3 .bg-opacity').empty();
    cnm = cnmmv;
    mv = mvp.split(',');

    loaddata(mv[0]);

}
function loaddata(mvc) {
    str_name = mvc;
    Jsons = new Array();
    var str = url + "?RequestType=GetSessionTimeByCinemaAndMovie&Cinema_strID=" + cnm + "&Movie_strName=" + encodeURIComponent(mvc) + "";
    //console.log(str)
    $.getJSON(str)
                      .done(function (data) {
                          Jsons = data;
                          groupDate();

                      });
}
function groupDate() {
    namenew = nameold;
    titlep = $('.click_box_2').find('span.selected').html();

    var ttsub = mv[counttime].substring(0, 3).trim();
    sub_tt = ttsub;
    if (ttsub == "2D") {

        sub = " - 2D SUB/ PHỤ ĐỀ";
    }
    else if (ttsub == "2DD") {

        sub = " - 2D DUB/ LỒNG TIẾNG";
    }
    else if (ttsub == "3D") {

        sub = " - 3D SUB/ PHỤ ĐỀ";
    }
    else if (ttsub == "3DD") {

        sub = " - 3D DUB/ LỒNG TIẾNG";
    }
    else {
        sub = " - 2D SUB/ PHỤ ĐỀ";
        sub_tt = "2D";
    }

    titlep += sub;

    namenew += " " + sub;
   
    counttime++;
    DofW = new Array();

    DofW.push(Jsons[0]);
    var begin = Jsons[0];

    for (var i = 1; i < Jsons.length; i++) {
        var g = new Date(begin.Session_dtmDate_Time);
        var d = g.getDay();
        var g1 = new Date(Jsons[i].Session_dtmDate_Time);
        var d1 = g1.getDay();

        if (d != d1) {
            DofW.push(Jsons[i]);
            begin = Jsons[i];
        }

    }
    display();

    $('#coutdetail').val(counttime);
    var kk = $('#coutdetail').val();

    if (Number(kk) < mv.length)
        loaddata(mv[kk]);
}
function display() {

    var day_next = new Array();
    var day_next_format = new Array();
    var day_right = new Array();
    var timeNow = new Date();
    var dnownext = "";
    var year = timeNow.getFullYear();
    day_next.push(timeNow);
    var dnow = timeNow.getDate() + '-' + (timeNow.getMonth() + 1);
    var mark_day;
    day_next_format.push(dnow);
    var noCT = 0;
    for (var i = 1; i < 3; i++) {
        var newdate = new Date(timeNow);
        newdate.setDate(newdate.getDate() + i)
        var dd = newdate.getDate();
        var mm = newdate.getMonth() + 1;
        var someFormattedDate = dd + '-' + mm;
        day_next.push(newdate);
        day_next_format.push(someFormattedDate);
        mark_day = newdate;
    }

    var day_left = ["Hôm nay (" + day_next_format[0] + ")", "Ngày mai  (" + day_next_format[1] + ")", "Ngày mốt (" + day_next_format[2] + ")"];
   
    var count = 0;
    var ttMv = "<p style='margin-top: 15px;margin-bottom: 15px !important;text-align: left;padding: 0 10px !important; color: #ffd455;font-size: 16px !important;'>" + namenew + "</p>";
 
    $(ttMv).appendTo($('.click_box_3 .bg-opacity'));
    var mv = $('.click_box_2 span').attr('strmv')
   
    for (var i = 0; i < 3; i++) {
        var dayshow = '<div class="date-item">';


        var rs = groupTime(day_next[i]);

        if (rs.length > 0) {
            count++;
            dayshow += '<p>' + day_left[i] + '</p>'
            dayshow += '<div class="cust_padd_left">';
            for (var j = 0; j < rs.length; j++) {
                //dayshow += '<a href="/SelectTickets.aspx?cinemacode=' + cnm + '&txtSessionId=' + rs[j].s + '"><span class="selectOption custom-time">' + rs[j].ti + '</span></a>';
             
                dayshow += '<a href="javascript:void(0)" class="check-booking"><span class="selectOption custom-time" namemv="' + str_name + '" subMv="' + sub_tt + '" txtSessionId=' + rs[j].s + ' onclick="bookingClick(this)">' + rs[j].ti + '</span></a>';

            }
            dayshow += '</div>';

            noCT++;
        }

        dayshow += '</div>';
        $(dayshow).appendTo($('.click_box_3 .bg-opacity'));
    }
    if (DofW == "") {
        if (noCT <= 0)
            $("#emptyCT").css("display", "block");

        return false;
    }

    for (var j = 0; j < DofW.length; j++) {

        var g = new Date(DofW[j].Session_dtmDate_Time);
        g.setHours(0, 0, 0, 0);
        mark_day.setHours(0, 0, 0, 0);
        if (g > mark_day) {

            var d = g.getDay();

            g.setDate(g.getDate())
            var dd = g.getDate();
            var mm = g.getMonth() + 1;
            var someFormattedDate = dd + '-' + mm;
            var b = '<div class="date-item">';
            b += '<p>' + getday(d) + "&nbsp;(&nbsp;" + someFormattedDate + "&nbsp;)" + '</p>'
            b += '<div class="cust_padd_left">';
            //alert(str_name)
            var rs = groupTime(DofW[j].Session_dtmDate_Time);
            if (rs.length > 0) {


                for (var k = 0; k < rs.length; k++) {
                   // b += '<a href="/SelectTickets.aspx?cinemacode=' + cnm + '&txtSessionId=' + rs[k].s + '"><span class="selectOption custom-time">' + rs[k].ti + '</span></a>';
                    b += '<a href="javascript:void(0)" class="check-booking"><span class="selectOption custom-time" namemv="' + str_name + '" subMv="' + sub_tt + '" txtSessionId=' + rs[k].s + ' onclick="bookingClick(this)">' + rs[k].ti + '</span></a>';

                }
                b += '</div>';

                noCT++;
            }

            b += '</div>';
            $(b).appendTo($('.click_box_3 .bg-opacity'));
        }

    }

    if (count > 0 && mv.length == counttime) {

        $('.bg-close-selectbox').css('display', 'block');
        $('.click_box_3 .selectOptions').toggle();
    }
    $('.click_box_2').find('span.selected').html(nameold);
}

function groupTime(obj) {
    var t = new Array();
    var g1 = new Date(obj);
    var dd1 = g1.getDate();
    var mm1 = g1.getMonth() + 1;
    var yy1 = g1.getFullYear();
    var someFormattedDate1 = dd1 + '-' + mm1 + '-' + yy1;

    for (var i = 0; i < Jsons.length; i++) {

        var g = new Date(Jsons[i].Session_dtmDate_Time);

        var dd = g.getDate();
        var mm = g.getMonth() + 1;
        var yy = g.getFullYear();
        var someFormattedDate = dd + '-' + mm + '-' + yy;

        if (someFormattedDate == someFormattedDate1) {

            var g = Jsons[i].Session_dtmDate_Time.split(' ');
            var time = g[1].split(':');
            var compareT = Jsons[i].Session_dtmDate_Time.split(' ');

            if (compareT[2] == "PM" && Number(compareT[1].split(':')[0]) < 12) {
                time[0] = Number(time[0]) + 12;
            }
            if (compareT[2] == "AM" && Number(compareT[1].split(':')[0]) == 12) {
                time[0] = "0";
            }
            //var kq = time[0] + ":" + time[1]
            var kq = {
                ti: time[0] + ":" + time[1],
                s: Jsons[i].Session_strID
            }

            t.push(kq);
        }
    }

    return t;
}