var listCnm = new Array();
var listCnmname = new Array();
var listCnm_s = new Array();
var listMv = new Array();
var listMvstrid = new Array();
var listT = new Array();
var listT_tmp = new Array();
var listObj = new Array();
var listObjcompare = new Array();
var listMovie_tmp = new Array();
var index_cnm;
var index_mv;
var index_time;
var countcnm = 0;
var mvsession;

//url = "http://api.megagscinemas.vn/MegaGS.aspx";
var mv_name_df;

function checkcinema(vistacinemaid, namecnm) {

    $('#scroll-all-1').find('.jspPane').empty();
    $('#scroll-all-2').find('.jspPane').empty();
    $('#scroll-all-3').find('.jspPane').empty();
    index_cnm = 1;
    index_mv = 2;
    index_time = 3;

    var a = '<div class="nb-group clearfix">' +
               '<div class="img-nb"><span><i>1</i></span></div>' +
               '<div class="text-nb"><span>chọn rạp</span></div>' +

    '</div>';
    $(a).prependTo($('#scroll-all-1 .jspPane'));
    var y = '<div class="nb-checked">' +
                '<input name="" type="checkbox" checked id=' + vistacinemaid + ' value="' + namecnm + '" onchange="cinema_check($(this),2)">' +
                '<label class="pointers" for=' + vistacinemaid + '><span></span><b>' + namecnm + '</b></label>' +
              '</div>';

    $(y).appendTo($('#scroll-all-1 .jspPane'));


    var b = '<div class="nb-group clearfix">' +
           '<div class="img-nb"><span><i>2</i></span></div>' +
           '<div class="text-nb"><span>chọn phim</span></div>' +
        '</div>';
    $(b).prependTo($('#scroll-all-2 .jspPane'));
    var c = '<div id="scroll-all" class="scroll-pane">' +
                '<div class="nb-group clearfix">' +
                '<div class="img-nb"><span><i>3</i></span></div>' +
                '<div class="text-nb"><span>Lịch chiếu phim</span></div>' +
                '</div>' +
                '<div class="txt-note-all"><i>(*)</i> Chọn vào suất chiếu để đặt vé trực tuyến </div>' +
                '<div class="cinema-group">' +
                '</div>' +
                '</div>';

    $(c).prependTo($('#scroll-all-3 .jspPane'));
    cinema_check($("#" + vistacinemaid), 2);
}
$(window).load(function () {
    $("#radio-check-rap").prop('checked', true);
    mv_name_df = $("#Movie_strName").val();
    checkcinema("MCT", "MEGA GS CAO THẮNG");
    getOrdered();
   
});
function getOrdered() {
    
    $.ajax({
        type: "POST",
        url: "/callApiAjax.aspx/getOrder",
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
           
            $.ajax({
                type: "POST",
                url: "/callApiAjax.aspx/CancelOrder",
                data: "{}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    lticket = [];
                }
            });

            var obj = JSON.parse(msg.d)
            if (obj.Result == 0) {
                if (obj.Order.Sessions != null) {
                    var mv_str = "";
                    var SessionId = "";
                    for (var k = 0; k < obj.Order.Sessions.length; k++) {
                        mv_str = obj.Order.Sessions[k].FilmTitle;
                        SessionId = obj.Order.Sessions[k].SessionId;
                        for (var m = 0; m < obj.Order.Sessions[k].Tickets.length; m++) {
                            var insetTK = true;
                            var TicketTypeCode = obj.Order.Sessions[k].Tickets[m].TicketTypeCode;
                            var PriceCents = obj.Order.Sessions[k].Tickets[m].PriceCents;
                            var ob = {
                                TicketTypeCode: TicketTypeCode,
                                PriceCents: PriceCents,
                                Qly: 1
                            }
                            for (var n = 0; n < lticket.length; n++) {
                                if (TicketTypeCode == lticket[n].TicketTypeCode) {
                                    var sl = parseInt(lticket[n].Qly) + 1;
                                    lticket[n].Qly = sl;
                                    insetTK = false;
                                    break;
                                }
                            }
                            if (insetTK == true)
                                lticket.push(ob);
                        }
                    }
                    $('#checkedMCT .nb-checked').each(function (i) {
                        //$(this).find("input:checkbox").prop("checked", true);
                        var ck = $(this).find("input:checkbox").attr("id");
                        if (ck == mv_str) {
                            var cb_Mv = $(this).find("input:checkbox");
                            $(cb_Mv).prop("checked", true);
                            mvsession = SessionId;
                            movies_check($(cb_Mv), 2)

                        }
                    });
                }
            }
        }
    });

   
}


$(document).ready(function () {

    $('input[type="radio"]').change(function () {

        $('#loopmv').val('0');
        $('#scroll-all-1').find('.jspPane').empty();
        $('#scroll-all-2').find('.jspPane').empty();
        $('#scroll-all-3').find('.jspPane').empty();
        listCnm = new Array();
        listCnmname = new Array();
        listCnm_s = new Array();
        listMv = new Array();
        listT = new Array();
        listObj = new Array();
        listT_tmp = new Array();
        listMvstrid = new Array();
        id = $(this).attr('id');
        if (id == "radio-check-rap") {
            // Note edit
            checkcinema("MCT", "MEGA GS CAO THẮNG");
            //index_cnm = 1;
            //index_mv = 2;
            //index_time = 3;
            //var a = '<div class="nb-group clearfix">' +
            //           '<div class="img-nb"><span><i>1</i></span></div>' +
            //           '<div class="text-nb"><span>chọn rạp</span></div>' +
            //        '</div>';
            //$(a).prependTo($('#scroll-all-1 .jspPane'));
            //var b = '<div class="nb-group clearfix">' +
            //       '<div class="img-nb"><span><i>2</i></span></div>' +
            //       '<div class="text-nb"><span>chọn phim</span></div>' +
            //    '</div>';
            //$(b).prependTo($('#scroll-all-2 .jspPane'));

            //getListCinema(1, 2);

        }
        else if (id == "radio-check-phim") {

            index_cnm = 2;
            index_mv = 1;
            index_time = 3;
            var a = '<div class="nb-group clearfix">' +
                       '<div class="img-nb"><span><i>1</i></span></div>' +
                       '<div class="text-nb"><span>chọn phim</span></div>' +
                    '</div>';
            $(a).prependTo($('#scroll-all-1 .jspPane'));
            var b = '<div class="nb-group clearfix">' +
                     '<div class="img-nb"><span><i>2</i></span></div>' +
                     '<div class="text-nb"><span>chọn rạp</span></div>' +
                     '</div>';
            $(b).prependTo($('#scroll-all-2 .jspPane'));

            var c = '<div id="scroll-all" class="scroll-pane">' +
                          '<div class="nb-group clearfix">' +
                          '<div class="img-nb"><span><i>3</i></span></div>' +
                          '<div class="text-nb"><span>Lịch chiếu phim</span></div>' +
                          '</div>' +
                          '<div class="txt-note-all"><i>(*)</i> Chọn vào suất chiếu để đặt vé trực tuyến </div>' +
                          '<div class="cinema-group">' +
                          '</div>' +
                          '</div>'
            ;

            $(c).prependTo($('#scroll-all-3 .jspPane'));
            getListMovies(1);
            // Note edit
            getListCinema(2, 3);

        }
        else {
            
            index_cnm = 2;
            index_mv = 3;
            index_time = 1;
            var a = '<div class="nb-group clearfix">' +
                       '<div class="img-nb"><span><i>1</i></span></div>' +
                       '<div class="text-nb"><span>Lịch chiếu phim</span></div>' +
                       '<div class="txt-note-all"><i>(*)</i> Chọn vào suất chiếu để đặt vé trực tuyến </div>' +
                    '</div>';
            $(a).prependTo($('#scroll-all-1 .jspPane'));
            var b = '<div class="nb-group clearfix">' +
                     '<div class="img-nb"><span><i>2</i></span></div>' +
                     '<div class="text-nb"><span>chọn rạp</span></div>' +
                     '<div class="txt-note-all"><i>(*)</i> Chọn vào suất chiếu để đặt vé trực tuyến </div>' +
                  '</div>';
            $(b).prependTo($('#scroll-all-2 .jspPane'));
            var c = '<div id="scroll-all" class="scroll-pane">' +
                     '<div class="nb-group clearfix">' +
                     '<div class="img-nb"><span><i>3</i></span></div>' +
                     '<div class="text-nb"><span>Lịch chiếu phim</span></div>' +
                     '</div>' +
                     '<div class="txt-note-all"><i>(*)</i> Chọn vào suất chiếu để đặt vé trực tuyến </div>' +
                     '<div class="cinema-group">' +
                     '</div>' +
                     '</div>'
            ;

            $(c).prependTo($('#scroll-all-3 .jspPane'));
            getListTime(1);
            getListCinema(2, 3);
        }
        //var c = '<div id="scroll-all" class="scroll-pane">' +
        //         '<div class="nb-group clearfix">' +
        //         '<div class="img-nb"><span><i>3</i></span></div>' +
        //         '<div class="text-nb"><span>Lịch chiếu phim</span></div>' +
        //         '</div>' +
        //         '<div class="cinema-group">' +
        //         '</div>' +
        //         '</div>'
        //;

        //$(c).prependTo($('#scroll-all-3 .jspPane'));


    });

});
function getListCinema(index, next) {

    $.getJSON(url + '?RequestType=GetCinemaList')
                .done(function (data) {
                    var e = "";
                    $.each(data, function (key, item) {
                        e += '<div class="nb-checked">' +
                        '<input name="" type="checkbox" id="' + item.Cinema_strID + '" value="' + item.Cinema_strName + '" checked="true" onchange="cinema_check($(this),' + next + ')">' +
                        '<label class="pointers" for="' + item.Cinema_strID + '"><span></span><b>' + item.Cinema_strName + '</b></label>' +
                      '</div>';

                    })
                    $(e).appendTo($('#scroll-all-' + index + ' .jspPane'));
                    cinema_check("#MCT", 1)
                    $('.scroll-pane').jScrollPane();
                })

}

function getListMovies(index) {

    $.getJSON(url + '?RequestType=GetMovieNowShowing')
               .done(function (data) {
                   data = comparedata(data)
                   var e = "";
                   $.each(data, function (key, item) {
                       var Mvname = item.Movie_strName_2;
                       if (Mvname == "")
                           Mvname = item.Movie_strName;
                       e += '<div class="nb-checked">' +
                             '<input name="" type="checkbox" strid="' + key + '" id="' + item.Movie_strName + '" value="' + encodeURI(Mvname) + '" onchange="movies_check($(this),2)">' +
                             '<label class="pointers" for="' + item.Movie_strName + '"><span></span><b>' + Mvname + '</b></label>' +
                             '</div><br>';

                   })
                   $(e).appendTo($('#scroll-all-' + index + ' .jspPane'));
                   $('.scroll-pane').jScrollPane();
               })
}

function getListTime(index) {

    $.getJSON(url + '?RequestType=GetDateList')
               .done(function (data) {
                   var e = "";
                   $.each(data, function (key, item) {
                       e += '<div class="nb-checked">' +
                            '<input name="" type="checkbox" id="' + item.Value + '" value="" onchange="time_check($(this))">' +
                            '<label class="pointers" for="' + item.Value + '"><span></span><b>' + item.Name + '</b></label>' +
                            '</div><br>';

                   })
                   $(e).appendTo($('#scroll-all-' + index + ' .jspPane'));
                   $('.scroll-pane').jScrollPane();
               })
}
function comparedata(data) {

    var t = [];

    for (var i = 0; i < listmvs.length; i++) {

        try {
            var mvstr = listmvs[i].Movie_strName.split(',');
            //alert(mvstr)

            for (var j = 0; j < data.length; j++) {

                if (mvstr.indexOf(data[j].Movie_strName) > -1) {


                    if (t.length > 0) {
                        var isert = true;
                        for (var v = 0; v < t.length; v++) {

                            if (t[v].Movie_strName.indexOf(data[j].Movie_strName) > -1) {

                                isert = false;
                                data.splice(j, 1);
                                break;
                            }

                        }
                        if (isert == true) {

                            data[j].Movie_strName = listmvs[i].Movie_strName;

                            t.push(data[j]);

                            data.splice(j, 1);
                            break;
                        }
                    }
                    else {

                        data[j].Movie_strName = listmvs[i].Movie_strName;

                        t.push(data[j]);
                        data.splice(j, 1);

                    }
                }
                //else
                //alert("3:"+data[j].Movie_strName)

            }
        }
        catch (e) {

        }
        // console.log(listmvs[i].Movie_strName);
    }

    // alert(data.length + "-" + data[0].Movie_strName_2)
    if (data.length > 0) {

        for (var k = 0; k < data.length; k++) {
            var insert = true;
            for (var h = 0; h < listmvs.length; h++) {
                var mvstr = listmvs[h].Movie_strName.split(',');
                //console.log("array:"+mvstr)
                //console.log(data[k].Movie_strName)
                if (mvstr.indexOf(data[k].Movie_strName) > -1) {
                    //data.splice(k, 1);
                    insert = false;
                    break;
                }

            }
            if (insert == true)
                t.push(data[k]);
        }
    }
    return t;
}
function getMoviesfromcinema(obj, index) {

    var id = $(obj).attr('id');
    var name = $(obj).attr('value');
    // console.log(url + '?RequestType=GetMovieListAtCinemas&Cinema_strID=' + id)
    // alert(id+"/"+name+"/"+index)
    var b = '<div id="checked' + id + '">';
    //console.log(url + '?RequestType=GetMovieListAtCinemas&Cinema_strID=' + id)
    $.getJSON(url + '?RequestType=GetMovieListAtCinemas&Cinema_strID=' + id)

           .done(function (data) {

               data = comparedata(data)
               $.each(data, function (key, item) {
                   var Mvname = item.Movie_strName_2;
                   if (Mvname == "")
                       Mvname = item.Movie_strName;

                   if (mv_name_df == item.Movie_strName) {
                       var f = '<input name="" type="checkbox" strid="' + key + '" id="' + item.Movie_strName + '" checked value="' + encodeURI(Mvname) + '" onchange="movies_check($(this),' + index + ')">';
                       b += '<div class="nb-checked">' +
                                f +
                           '<label class="pointers" for="' + item.Movie_strName + '"><span></span><b>' + Mvname + '</b></label>' +
                         '</div><br>';

                       movies_check($(f), index);
                   }
                   else {

                       b += '<div class="nb-checked">' +
                       '<input name="" type="checkbox"  strid="' + key + '" id="' + item.Movie_strName + '" value="' + encodeURI(Mvname) + '" onchange="movies_check($(this),' + index + ')">' +
                       '<label class="pointers" for="' + item.Movie_strName + '"><span></span><b>' + Mvname + '</b></label>' +
                     '</div><br>';
                   }
               })
               b += '</div>';
               $(b).appendTo($('#scroll-all-' + index + ' .jspPane'));
               $('.scroll-pane').jScrollPane();
           });

}

function getCinemafromMovies(obj, index) {

    var name = $(obj).attr('id');

    $.getJSON(url + '?RequestType=GetCinemasByMoviesList&Movie_strName=' + name)
                .done(function (data) {
                    var a = "";
                    $.each(data, function (key, item) {
                        for (var i = 0; i < listCnm_s.length; i++) {
                            if (item.Cinema_strID == listCnm_s[i]) {
                                countcnm++;
                                return false;
                            }
                        }
                        listCnm_s.push(item.Cinema_strID);
                        a += '<div class="nb-checked">' +
                               '<input name="" type="checkbox" id="' + item.Cinema_strID + '" value="' + item.Cinema_strName + '" onchange="cinema_check($(this))">' +
                               '<label class="pointers" for="' + item.Cinema_strID + '"><span></span><b>' + item.Cinema_strName + '</b></label>' +
                             '</div><br>';

                    })
                    $(a).appendTo($('#scroll-all-' + index + ' .jspPane'));
                    //$('#choose' + index).append(" <b>Appended text</b>.");
                })

}
function GroupByTime(obj) {
    var t = [];
    var g1 = new Date(obj.Session_dtmDate_Time);
    var d1 = g1.getDay();

    for (var i = 0; i < listObj.length; i++) {
        var g = new Date(listObj[i].Session_dtmDate_Time);
        var d = g.getDay();
        if (d == d1 && obj.Movie_strID == listObj[i].Movie_strID && obj.Session_dtmDate_Time.split(' ')[0] == listObj[i].Session_dtmDate_Time.split(' ')[0]) {
            return false;
        }
    }
    listObj.push(obj);

    for (var i = 0; i < Jsons.length; i++) {
        var g = new Date(Jsons[i].Session_dtmDate_Time);
        var d = g.getDay();
        if (d == d1 && obj.Session_dtmDate_Time.split(' ')[0] == Jsons[i].Session_dtmDate_Time.split(' ')[0]) {
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

function cinema_check(obj, id) {

    var id_cnm = $(obj).attr('id');
    var idval = $(obj).attr('value');

    if ($(obj).is(':checked')) {
        listCnm.push(id_cnm);
        listCnmname.push(idval);
        if (index_cnm == 1) {

            var name = $(obj).attr('value');

            var b = '<div class="address-all ' + id_cnm + '">' +
                       '<span>' + name + '</span>';
            '</div>';

            $(b).appendTo($('.cinema-group'));
            getMoviesfromcinema(obj, id);

        }
        if (index_cnm == 2 && index_time == 3) {

            var name = $(obj).attr('value');
            var b = '<div class="address-all ' + id_cnm + '">' +
                       '<span>' + name + '</span>';
            '</div>';

            $(b).appendTo($('.cinema-group'));
            getTimefromMovies_cinema(obj);
        }
        if (index_cnm == 2 && index_time == 1) {
            var name = $(obj).attr('value');

            var b = '<div class="address-all ' + id_cnm + '">' +
                       '<span>' + name + '</span>';
            '</div>';

            $(b).appendTo($('.cinema-group'));
            for (var i = 0; i < listT.length; i++) {
                var g = new Date(listT[i]);
                var dd = format_d_m_y(listT[i]);
                var d = g.getDay();
                var c = '<div class="address-item" id="date' + listT[i] + '">' +
                            '<span>' + getday(d) + ',&nbsp;' + dd + '</span>' +
                           '<div class="calendar-item" id="cale' + listT[i] + '">' +

                        '</div>';
                c += '</div>';
                $(c).appendTo($('.' + id_cnm + ''));
                listMvFromTime(id_cnm, listT[i], "cale" + listT[i]);
            }

            // listMvFromTime(id_cnm);
        }
    }
    else {

        if (index_cnm == 1) {
            listObj = new Array();
            var it = $('.cinema-group .' + id_cnm);
            $(it).remove();
            var mv = $("#checked" + id_cnm);
            $(mv).remove();
        }
        if (index_cnm == 2) {
            listObj = new Array();
            var it = $('.cinema-group .' + id_cnm);
            $(it).remove();
            // var mv = $("#checked" + id_cnm);
            //$(mv).remove();
        }
        for (var i = 0; i < listCnm.length; i++) {
            if (id_cnm == listCnm[i]) {
                listCnm.splice(i, 1);
                listCnmname.splice(i, 1);
            }
        }
    }


}
function movies_check(obj, id) {
    $('#loopmv').val('0');
    var id_mv = $(obj).attr('id').replace(/,/g, '');
    var strid = $(obj).attr('strid');

    if ($(obj).is(':checked')) {

        listMv.push(id_mv);
        listMvstrid.push(strid);

        if (index_mv == 1) {
            // Note edit
            //getCinemafromMovies(obj, id);
            listObj = new Array();
            for (var i = 0; i < listMv.length; i++) {

                if (id_mv == listMv[i]) {
                    getTimefromMovies(obj, i)
                    break;
                }
            }
        }
        else if (index_mv == 2) {
            listObj = new Array();
            for (var i = 0; i < listMv.length; i++) {

                if (id_mv == listMv[i]) {

                    getTimefromMovies(obj, i)
                    break;
                }
            }
        }
    }
    else {
        $('#loopmv').val('0');
        if (index_mv == 2 || index_mv == 1) {

            var it = $('.it' + strid);
            $(it).remove();

        }
        for (var i = 0; i < listMv.length; i++) {
            if (id_mv == listMv[i]) {
                listMv.splice(i, 1);
                listMvstrid.splice(i, 1);
            }
        }
        $('.scroll-pane').jScrollPane();
    }

}

function time_check(obj) {

    var id_t = $(obj).attr('id');
    var idval = $(obj).attr('value');
    // alert(id_t+"-----"+idval)
    if ($(obj).is(':checked')) {
        listT.push(id_t);

        for (var i = 0; i < listCnm.length; i++) {

            var g = new Date(id_t);
            var day = format_d_m_y(id_t);
            var d = g.getDay();
            var c = '<div class="address-item" id="date' + id_t + '">' +
                        '<span>' + getday(d) + ',&nbsp;' + day + '</span>' +
                       '<div class="calendar-item" id="cale' + id_t + '">' +

                    '</div>';
            c += '</div>';
            $(c).appendTo($('.' + listCnm[i] + ''));

            listMvFromTime(listCnm[i], id_t, "cale" + id_t);
        }

    }
    else {

        for (var i = 0; i < listT.length; i++) {

            if (id_t == listT[i]) {
                var it = $('#date' + id_t);
                $(it).remove();
                listT.splice(i, 1);

            }
        }
    }

}
function getTimefromMovies_cinema(obj) {

    var cnm = $(obj).attr('id');
    var show = new Array();
    for (var i = 0; i < listMv.length; i++) {
        var namemv = listMv[i];
        var id = listMvstrid[i];
        //console.log($('#'+namemv).attr('id'));
        movie_ajax(cnm, namemv, id)
    }

}
function movie_ajax(cnm, name, id) {

    $.getJSON(url + '?RequestType=GetSessionTimeByCinemaAndMovie&Cinema_strID=' + cnm + '+&Movie_strName=' + encodeURIComponent(name))
                .done(function (data) {
                    Jsons = data;
                    var b = '<div class="address-item" id="it' + id + '">' +
                           '<span>' + name + '</span>';
                    $.each(data, function (key, item) {
                        var rs = GroupByTime(item);

                        if (rs != false) {
                            var g = item.Session_dtmDate_Time.split(' ');
                            var day = format_d_m_y(item.Session_dtmDate_Time);
                            var time = g[1].split(':');
                            var date = new Date(item.Session_dtmDate_Time);
                            var dayofweek = getday(date.getDay());

                            b += '<div class="calendar-item">' +
                                         '<p>' + dayofweek + ", " + day + '</p>' +
                                         '<ul class="clearfix">';
                            for (var i = 0; i < rs.length; i++) {
                                // b += '<li><span>' + rs[i] + '</span></li>';
                                //b += '<li><a href="/SelectTickets.aspx?cinemacode=' + cnm + '&txtSessionId=' + rs[i].s + '"><span>' + rs[i].ti + '</span></a></li>';
                                b += '<li><a href="javascript:void(0)" class="check-booking"><span namemv="' + name + '" txtSessionId=' + rs[i].s + '>' + rs[i].ti + '</span></a></li>';
                            }
                            b += '</ul>' +
                                     '</div>'
                            ;
                        } else {

                        }

                    })
                    b += '</div>';

                    $(b).appendTo($('.cinema-group .' + cnm + ''));

                    $('.scroll-pane').jScrollPane();
                });

}
function GroupMovies(obj) {

    var t = new Array();

    var Movie_strName = obj.Movie_strName.replace(/,/g, '###');

    for (var i = 0; i < listObj.length; i++) {

        if (listObj[i].Movie_strName == Movie_strName) {
            return false;
            break;
        }
    }
    listObj.push(obj);

    return true;

}
function GroupMoviesselectday(obj) {

    var t = new Array();
    var rs = true;

    var Movie_strName = obj.Movie_strName_2.replace(/,/g, '###');
    // console.log(listObj)
    for (var i = 0; i < listObjcompare.length; i++) {
        if (listObjcompare[i].Movie_strName_2 == Movie_strName) {

            rs = false;
            break;
        }
    }
    listObjcompare.push(obj);

    return rs;

}
function GroupDate(obj) {

    var t = new Array();
    var g1 = new Date(obj.Session_dtmDate_Time);
    var d1 = g1.getDay();
    var Movie_strName = obj.Movie_strName;

    // if (Movie_strName == "GAI GIA LAM CHIEU") {
    for (var i = 0; i < Jsons.length; i++) {

        var g = new Date(Jsons[i].Session_dtmDate_Time);
        var d = g.getDay();
        var Movie = Jsons[i].Movie_strName;
        if (d == d1 && Movie == Movie_strName) {
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
    // }
    for (var a = 0; a < t.length - 1; a++) {

        for (var b = a + 1; b < t.length; b++) {

            var time1 = t[a].ti.replace(":", "");
            var time2 = t[b].ti.replace(":", "");
            if (Number(time1) > Number(time2)) {

                var tam = t[a];
                t[a] = t[b];
                t[b] = tam;
            }

        }
    }


    return t;
}
function listMvFromTime(cnm, dt, index) {

    var namestr = "";
    listObjcompare = new Array();
    var b = "";
    
    $.getJSON(url + '?RequestType=GetSessionsByCinemaAndDate&Cinema_strID=' + cnm + '&Date=' + dt)
                   .done(function (data) {
                       //data = comparedata(data);
                       Jsons = data;
                       if (Jsons == "") {
                           b += '<div class="calendar-item">' +
                                  '<p>Lịch chiếu của phim chưa được cập nhật!</p>' +
                                '</div>';
                           return false
                       }

                       $.each(data, function (key, item) {
                           b = "";
                         
                           var arrayMv = item.Movie_strName.split(',');
                           // for (var ji = 0; ji < arrayMv.length; ji++) {
                           var rs = GroupMovies(item);
                           //console.log(GroupMoviesselectday(item.Movie_strName_2))
                           var tmp = (GroupMoviesselectday(item))
                           
                           var icon = createIcon(item.Movie_strName, dt);
                   
                           namestr = item.Movie_strName;
                          
                           var insert3 = namestr.substr(3).trim();
                          
                           if (insert3=="") {
                              
                               insert3 = namestr;
                           }
                           var links = insert3.replace(/[^a-zA-Z ]/g, "").replace(/\s/g, "");
                         
                           var Mvname = item.Movie_strName_2;
                           if (Mvname == "")
                               Mvname = item.Movie_strName;
                        
                           if (rs == true) {
                              
                               b += '<div class="calendar-item ' + links + '">' +
                                    '<p>' + Mvname + '</p>' + icon + '</div>';
                               var b2 = '<ul class="clearfix">';
                            
                               var time = GroupDate(item);
                               var insert4 = namestr.substring(0, 3).trim();
                               
                               for (var j = 0; j < time.length; j++) {

                                   //b2 += '<li><a href="/SelectTickets.aspx?cinemacode=' + cnm + '&txtSessionId=' + time[j].s + '"><span>' + time[j].ti + '</span></a></li>';
                                   b2 += '<li><a href="javascript:void(0)" class="check-booking"><span namemv="' + arrayMv + '" txtSessionId=' + time[j].s + ' imgmv="" submv="' + insert4 + '" onclick="bookingClick(this)">' + time[j].ti + '</span></a></li>';
                                   //b += '<li><span>' + time[j] + '</span></li>';
                               }
                               b2 += '</ul>';
                               // }
                           
                               
                               if (insert4 == "2D")
                                   insert4 = "2d_pd_showtime"
                               else if (insert4 == "2DD")
                                   insert4 = "2d_lt_showtime"

                               else if (insert4 == "3D")
                                   insert4 = "3d_pd_showtime"
                               else if (insert4 == "3DD")
                                   insert4 = "3d_lt_showtime"
                               else
                                   insert4 = "2d_pd_showtime"
                             
                               var sublinks = namestr.substring(0, 3).trim().replace(/[^a-zA-Z ]/g, "").replace(/\s/g, "");
                               
                               var links2 = (insert4 + sublinks + links + dt);
                             
                                if (tmp == true) {
                                   
                                    $(b).appendTo($('#' + index));
                                    //if (item.Movie_strName!="I.T")
                                   
                                   $(b2).appendTo($('#cale' + dt + ' .' + links + '').find('#' + links2 + ''));
                                   
                               }
                               else {
                                   var subicon = createsubIcon(namestr, dt);
                                   var classsub = createclasssubIcon(namestr, dt);
                                   $(subicon).appendTo($('#cale' + dt).find(' .' + links + '').find('.movie_sub_content .movie_sub'));
                                   $(classsub).appendTo($('#cale' + dt).find(' .' + links + '').find('.movie_sub_content .tab-content'));
                                   $(b2).appendTo($('#cale' + dt).find(' .' + links + '').find('#' + links2 + ''));
                               }

                           }
                           //if (insert3 == "2D")
                           //    $(b2).appendTo($(b).find(' #2d_pd_showtime' + links + ''));
                           //else if (insert3 == "2DD")
                           //    $(b2).appendTo($(b).find(' #2d_lt_showtime' + links + ''));
                           //else if (insert3 == "3D")
                           //    $(b2).appendTo($(b).find('#3d_pd_showtime' + links + ''));
                           //else if (insert3 == "3DD")
                           //    $(b2).appendTo($(b).find('#3d_lt_showtime' + links + ''));
                           //else
                           // $(b2).appendTo($('#' + index));
                       
                       })
                       $('#cale' + dt + ' .movie_sub_content').each(function (index) {
                           var arrayMV = []
                           $(this).find('.movie_sub .sub_item').each(function (index) {
                                arrayMV.push($(this).attr("mvroot"));
                              
                           });
                           $(this).find('.tab-content a span').attr('imgmv', arrayMV);
                       });
                       
                       // $(b).appendTo($('#' + index));

                       $('.scroll-pane').jScrollPane();
                       listObj = new Array();
                   });

}
function checkMVDD(MV_Strname) {

}
function createclasssubIcon(MV_Strname, date) {
    if (MV_Strname.length <= 3) {
        MV_Strname += MV_Strname;
    }
    var icons = "";
    var name = MV_Strname.split(',');
    var strid = MV_Strname.replace(/[^a-zA-Z ]/g, "").replace(/\s/g, "");
    strid += date.replace(/\s/g, "");

    for (var h = 0; h < name.length; h++) {
        var insert2 = name[h].substring(0, 3).trim();
        var pg = "";
        var at = "";
        if (h == 0)
            at = "in active";
        if (insert2 == "2D")
            pg = "2d_pd_showtime";
        else if (insert2 == "2DD")
            pg = "2d_lt_showtime";
        else if (insert2 == "3D")
            pg = "3d_pd_showtime";
        else if (insert2 == "3DD")
            pg = "3d_lt_showtime";
        else
            pg = "2d_pd_showtime";

        icons += '<div id="' + pg + strid + '" class="tab-pane fade "></div>';

    }

    return icons;
}
function createsubIcon(MV_Strname, date) {
    var subroot = MV_Strname;
    if (MV_Strname.length <= 3) {
        MV_Strname += MV_Strname;
    }
    var icons = "";
    var name = MV_Strname.split(',');
   
    var strid = MV_Strname.replace(/[^a-zA-Z ]/g, "").replace(/\s/g, "");
    strid += date.replace(/\s/g, "");

    for (var h = 0; h < name.length; h++) {

        var insert = name[h].substring(0, 3).trim();
        var tt = "";
        var subtt = "";
        var pg = "";
        var act = "";
        if (h == 0)
            act = "active";
        if (insert == "2DD") {
            tt = "2D";
            subtt = "Lồng tiếng";
            pg = "2d_lt_showtime";
        }
        else if (insert == "2D") {
            tt = "2D";
            subtt = "Phụ đề";
            pg = "2d_pd_showtime";
        }
        else if (insert == "3DD") {
            tt = "3D";
            subtt = "Lồng tiếng";
            pg = "3d_lt_showtime";
        }
        else if (insert == "3D") {
            tt = "3D";
            subtt = "Phụ đề";

            pg = "3d_pd_showtime";
        }
        else {
            tt = "2D";
            subtt = "Phụ đề";
            pg = "2d_pd_showtime";
        }
        icons += '<div mvroot="' + subroot + '" class="sub_item custom_sub" onclick="iconclick(this)"><a data-toggle="tab" href="#' + pg + strid + '"><div class="ganre" style="font-size: 20px;">' + tt + '</div><div class="viet_sub" style="padding-bottom: 4px;">' + subtt + '</div></a></div>';

    }

    return icons
}
function createIcon(MV_Strname, date) {
    var subroot = MV_Strname;
    if (MV_Strname.length<=3)
    {
        MV_Strname += MV_Strname;
    }
    var icons = '<div class="movie_sub_content">' +
                '<div class="movie_sub clearfix" style="padding-top: 0px!important;padding-bottom: 15px!important;">';
    var name = MV_Strname.split(',');
    var strid = MV_Strname.replace(/[^a-zA-Z ]/g, "").replace(/\s/g, "");
    strid += date.replace(/\s/g, "");
   
    for (var h = 0; h < name.length; h++) {
        var insert = name[h].substring(0, 3).trim();
      
        var tt = "";
        var subtt = "";
        var pg = "";
        var act = "";
        if (h == 0)
            act = "active";
        if (insert == "2DD") {
            tt = "2D";
            subtt = "Lồng tiếng";
            pg = "2d_lt_showtime";
        }
        else if (insert == "2D") {
            tt = "2D";
            subtt = "Phụ đề";
            pg = "2d_pd_showtime";
        }
        else if (insert == "3DD") {
            tt = "3D";
            subtt = "Lồng tiếng";
            pg = "3d_lt_showtime";
        }
        else if (insert == "3D") {
            tt = "3D";
            subtt = "Phụ đề";

            pg = "3d_pd_showtime";
        }
        else {
            tt = "2D";
            subtt = "Phụ đề";
            pg = "2d_pd_showtime";
        }
        icons += '<div mvroot="' + subroot + '" class="sub_item custom_sub" onclick="iconclick(this)"><a data-toggle="tab" href="#' + pg + strid + '" class="' + act + '"><div class="ganre" style="font-size: 20px;">' + tt + '</div><div class="viet_sub" style="padding-bottom: 4px;">' + subtt + '</div></a></div>';
      
    }
    icons += '</div>' +
             '<div class="tab-content custom_tab">';
    for (var h = 0; h < name.length; h++) {
        var insert2 = name[h].substring(0, 3).trim();
        var pg = "";
        var at = "";
        if (h == 0)
            at = "in active";
        if (insert2 == "2D")
            pg = "2d_pd_showtime";
        else if (insert2 == "2DD")
            pg = "2d_lt_showtime";
        else if (insert2 == "3D")
            pg = "3d_pd_showtime";
        else if (insert2 == "3DD")
            pg = "3d_lt_showtime";
        else
            pg = "2d_pd_showtime";
        
        icons += '<div id="' + pg + strid + '" class="tab-pane fade ' + at + '"></div>';

    }
    icons += '</div>';
    icons += '</div>';
    
    return icons;
}
function returnNameMV(name) {

    var tenp = "";
    if (name.indexOf("2D") > -1) {

        tenp = " - Phim 2D, Phụ đề";
    }
    if (name.indexOf("2DD") > -1) {

        tenp = " - Phim 2D, Lồng tiếng";
    }
    if (name.indexOf("3D") > -1) {

        tenp = " - Phim 3D, Phụ đề";
    }
    if (name.indexOf("3DD") > -1) {

        tenp = " - Phim 3D, Lồng tiếng";
    }
    return tenp;
}
function iconclick(obj) {
    var obj2 = $(obj).parent();
    $(obj2).find('a').removeClass('active');
    $(obj).find('a').addClass('active');

    //alert($(obj).find('a').attr("href"))
    $('.scroll-pane').jScrollPane();

}
function getTimefromMovies(obj, index) {
   
    //$('#loopmv').val('0');
    var icons = '<div class="movie_sub_content">' +
                '<div class="movie_sub clearfix" style="padding-top: 0px!important;padding-bottom: 15px!important;">';
    var name = $(obj).attr('id').split(',');
    var strid = $(obj).attr('strid');
    var sub_t = "";
    var sbMv = [];
    for (var h = 0; h < name.length; h++) {
        var insert = name[h].substring(0, 3).trim();
        
        sub_t = insert;
        var tt = "";
        var subtt = "";
        var pg = "";
        var act = "";
        if (h == 0)
            act = "active";
        if (insert == "2DD") {
            tt = "2D";
            subtt = "Lồng tiếng";
            pg = "2d_lt_showtime";
        }
        else if (insert == "2D") {
            tt = "2D";
            subtt = "Phụ đề";
            pg = "2d_pd_showtime";
        }
        else if (insert == "3DD") {
            tt = "3D";
            subtt = "Lồng tiếng";
            pg = "3d_lt_showtime";
        }
        else if (insert == "3D") {
            tt = "3D";
            subtt = "Phụ đề";

            pg = "3d_pd_showtime";
        }
        else {
            tt = "2D";
            subtt = "Phụ đề";
            pg = "2d_pd_showtime";
            sub_t = "2D";
        }
       
        icons += '<div class="sub_item custom_sub" onclick="iconclick(this)"><a namemv="' + name[h] + '" data-toggle="tab" href="#' + pg + strid + '" class="' + act + '"><div class="ganre" style="font-size: 20px;">' + tt + '</div><div class="viet_sub" style="padding-bottom: 4px;">' + subtt + '</div></a></div>';
        sbMv.push(sub_t);
    }
    
    icons += '</div>' +
             '<div class="tab-content custom_tab">';

    for (var h = 0; h < name.length; h++) {
       
        var insert2 = name[h].substring(0, 3).trim();
        var pg = "";
        var at = "";
        if (h == 0)
            at = "in active";
        if (insert2 == "2D")
            pg = "2d_pd_showtime";
        else if (insert2 == "2DD")
            pg = "2d_lt_showtime";
        else if (insert2 == "3D")
            pg = "3d_pd_showtime";
        else if (insert2 == "3DD")
            pg = "3d_lt_showtime";
        else
            pg = "2d_pd_showtime";

        icons += '<div id="' + pg + strid + '" class="tab-pane fade ' + at + '"></div>';

    }
    icons += '</div>';
    icons += '</div>';



    var Mvname = $(obj).attr('value');
    if (Mvname == "")
        Mvname = name[h];

    var j = $('#loopmv').val();

    if (Number(j) < name.length) {
       
        for (var i = 0; i < listCnm.length; i++) {
            var Idcnm = listCnm[i];
            var b2 = "";
            var tenp = returnNameMV(name[j]);

            var b = '<div class="address-item it' + strid + '">' +
                        '<span>' + decodeURI(Mvname) + '</span>' + icons;


            //alert(listCnm[i] + "-----" + name[j]);
            //console.log(url + '?RequestType=GetSessionTimeByCinemaAndMovie&Cinema_strID=' + listCnm[i] + '+&Movie_strName=' + encodeURIComponent(name[j]))
            $.getJSON(url + '?RequestType=GetSessionTimeByCinemaAndMovie&Cinema_strID=' + listCnm[i] + '+&Movie_strName=' + encodeURIComponent(name[j]))
                    .done(function (data) {
                        Jsons = data;

                        $.each(data, function (key, item) {

                            var rs = GroupByTime(item);

                            if (rs != false) {
                                var g = item.Session_dtmDate_Time.split(' ');
                                var day = format_d_m_y(item.Session_dtmDate_Time);

                                var time = g[1].split(':');
                                var date = new Date(item.Session_dtmDate_Time);
                                var dayofweek = getday(date.getDay());

                                b2 += '<div class="calendar-item">' +
                                              '<p>' + dayofweek + ", " + day + '</p>' +
                                              '<ul class="clearfix">';
                                for (var i = 0; i < rs.length; i++) {
                                    //b2 += '<li><a href="/SelectTickets.aspx?cinemacode=' + Idcnm + '&txtSessionId=' + rs[i].s + '"><span>' + rs[i].ti + '</span></a></li>';
                                    b2 += '<li><a href="javascript:void(0)" class="check-booking"><span imgmv="' + name + '" namemv="' + name[j] + '" txtSessionId=' + rs[i].s + ' submv="' + sbMv[j] + '" onclick="bookingClick(this)">' + rs[i].ti + '</span></a></li>';


                                }
                                b2 += '</ul>' +
                                         '</div>'
                                ;
                            } else {

                            }

                        })
                        //alert(b)

                        b += '</div>';
                        if (j == 0)
                            $(b).appendTo($('.cinema-group .' + Idcnm + ''));

                        var insert3 = name[j].substring(0, 3).trim();
                        if (insert3 == "2D") {
                            $(b2).appendTo($('.it' + strid).find(' .movie_sub_content .custom_tab #2d_pd_showtime' + strid + ''));
                        }
                        else if (insert3 == "2DD") {
                            $(b2).appendTo($('.it' + strid).find(' .movie_sub_content .custom_tab #2d_lt_showtime' + strid + ''));
                            //$('.it' + strid).find(' .movie_sub_content .custom_tab #2d_lt_showtime' + strid + '').addClass("in active");
                            //  $('.it' + strid).find(' .movie_sub_content .custom_tab #2d_lt_showtime' + strid + '').removeClass("in active");
                        }
                        else if (insert3 == "3D") {
                            $(b2).appendTo($('.address-item it' + strid).find('.movie_sub_content #3d_pd_showtime' + strid + ''));
                        }
                        else if (insert3 == "3DD") {
                            $(b2).appendTo($('.address-item it' + strid).find('.movie_sub_content #3d_lt_showtime' + strid + ''));
                        }
                        else {
                            $(b2).appendTo($('.it' + strid).find('.movie_sub_content #2d_pd_showtime' + strid + ''));
                        }
                        //

                        $('.scroll-pane').jScrollPane();
                        j++;
                        $('#loopmv').val(j);
                        if (Number(j) < name.length)
                            getTimefromMovies(obj, index);
                        //if (lticket.length > 0) {
                        //    if ($("#PanelerrOnepay").hasClass("errOp")) {
                        //       $('.check-booking').each(function () {
                        //            if ($(this).find("span").attr("txtsessionid") == mvsession) {
                        //                bookingClick($(this).find("span"));
                        //            }
                        //        });

                        //        $("#PanelTK").hide();
                        //    }
                        //}
                        //else {
                        //    $('.errOp').hide();
                        //}
                    });

        }

    }

}

function getday(n) {
    var weekday = new Array(7);
    weekday[0] = "Chủ nhật";
    weekday[1] = "Thứ 2";
    weekday[2] = "Thứ 3";
    weekday[3] = "Thứ 4";
    weekday[4] = "Thứ 5";
    weekday[5] = "Thứ sáu";
    weekday[6] = "Thứ 7";

    return weekday[n];
}
function format_d_m_y(n) {

    var dt = new Date(n);

    var rs = dt.getDate() + "-" + Number(dt.getMonth() + 1) + "-" + dt.getFullYear();
    return rs;
}
function listMoviestimechoose(obj) {

    var time = $(obj).attr('id');
    for (var i = 0; i < listCnm.length; i++) {
        var b = "";
        $.getJSON(url + '?RequestType=GetSessionsByCinemaAndDate&Cinema_strID=' + listCnm[i] + '&Date=' + time)
                       .done(function (data) {
                           $.each(data, function (key, item) {

                               b += '<div class="nb-checked">' +
                                      '<input name="" type="checkbox" id="' + item.Movie_strName + '" value="">' +
                                      '<label class="pointers" for="' + item.Movie_strName + '"><span></span><b>' + item.Movie_strName + '</b></label>' +
                                    '</div><br>';
                           })
                           b += '</div>';
                           $(b).appendTo($('#choose3'));

                       });
    }
}


