var listCnm = new Array();
var listCnmname = new Array();
var listCnm_s = new Array();
var listMv = new Array();
var listMvstrid = new Array();
var listT = new Array();
var listT_tmp = new Array();
var listObj = new Array();
var listMovie_tmp = new Array();
var index_cnm;
var index_mv;
var index_time;
var countcnm = 0;
//url = "http://api.megagscinemas.vn/MegaGS.aspx";
var mv_name_df;

function checkcinema(vistacinemaid, namecnm) {

   
    index_cnm = 1;
    index_mv = 2;
    index_time = 3;

    cinema_check(vistacinemaid, namecnm);
}
$(window).load(function () {
   
    checkcinema("MCT", "MEGA GS CAO THẮNG");
   
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

function comparedata(data)
{
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

function returnNameMV(name) {
    
    name = name.substring(0, 3).trim();

    var tenp = "";
   
    if (name=="2D") {

        tenp = " - 2D SUB/ PHỤ ĐỀ";
    }
    else if (name == "2DD") {

        tenp = " - 2D DUB/ LỒNG TIẾNG";
    }
    else if (name == "3D") {

        tenp = " - 3D SUB/ PHỤ ĐỀ";
    }
    else if (name == "3DD") {

        tenp = " - 3D DUB/ LỒNG TIẾNG";
    }
    else
        tenp = " - 2D SUB/ PHỤ ĐỀ";
    return tenp;
}
function getMoviesfromcinema(obj, index) {
    var id = obj.trim();
    var name = index;
    var b = "";
    $.getJSON(url + '?RequestType=GetMovieListAtCinemas&Cinema_strID=' + id)

           .done(function (data) {
               data=comparedata(data)
               $.each(data, function (key, item) {
                   var Mvname = item.Movie_strName_2;
                   if (Mvname == "")
                       Mvname = item.Movie_strName;
                   //var subname = returnNameMV(item.Movie_strName);
                   //Mvname += subname;
                   if (mv_name_df == item.Movie_strName) {
                      
                       var f = '<input name="" type="checkbox" strid="' + key + '" id="' + item.Movie_strName + '" checked value="' + encodeURI(Mvname) + '" onchange="movies_check($(this),' + index + ')">';
                       b += '<div class="nb-checked">' +
                                f+
                           '<label class="pointers" for="' + item.Movie_strName + '"><span></span><b>' + Mvname + '</b></label>' +
                         '</div><br>';
                      
                       movies_check($(f));
                   }
                   else {
                     
                       b += '<span class="selectOption"  strid="' + key + '" id="' + item.Movie_strName + '"  value="' + encodeURI(Mvname) + '" cinema="' + id + '" onclick="movies_check($(this))">' + Mvname + '</span>';
                   }
               })
              
              $(b).appendTo($('.click_box_2 .selectOptions .bg-opacity .group_lists'));
              //enableSelectBoxes();
           });
   
}

function cinema_check(obj, id) {
  
    var id_cnm = obj;
    var idval = id;
   
    if ($(".click_box_1 span.selected").html() != "") {
        
        listCnm.push(id_cnm);
        listCnmname.push(idval);
       
        getMoviesfromcinema(obj, id);
      
    }
   

}

function movies_check(obj) {
 
    var name = $(obj).html();
    var id_mv = $(obj).attr('id');
    var cinema = $(obj).attr('cinema');
    $('.click_box_2 span').attr('strmv', id_mv)
    $('.click_box_2').find('span.selected').html(id_mv);
    $('.click_box_2 .selectOptions').toggle();
    $('.click_box_3 .selectOptions').toggle();
    $('.bg-close-selectbox').css('display', 'none');


    if (id_mv!="Chọn phim") {
        
        detailTime(id_mv, cinema,name);
            
    }
   

}

function GroupMovies(obj) {
  
    var t = new Array();
    var Movie_strName = obj.Movie_strName;
    for (var i = 0; i < listObj.length; i++) {
        if (listObj[i].Movie_strName == Movie_strName) {
            return false;
            break;
        }
    }
    listObj.push(obj);
    return true;

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
