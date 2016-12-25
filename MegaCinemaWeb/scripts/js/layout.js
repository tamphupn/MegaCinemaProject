$(document).ready(function(){

  // == bxslider ==//
  $('.bxslider').bxSlider({
		mode: 'fade',
		auto: true,
		speed: 500,
		captions: true,
		//slideWidth:845,
		adaptiveHeight: true,
		autoHover: true
	});

	 // == bxslider Rap & Gia Ve==//
	 $('.bxslider_rgv').bxSlider({
		mode: 'horizontal',
		auto: true,
		speed: 500,
		captions: true,
		//slideWidth:845,
		adaptiveHeight: true,
		pager:false
	});

	// == Slick slider ==//
	$('.autoplay').slick({
	  slidesToShow:4,
	  slidesToScroll:1,               
	  variableWidth: true,
	  infinite:false
	  //autoplay: true,
	  //autoplaySpeed: 2000,
	});
	
	
	// == Slick slider Special Offer ==//
	$('.spec').slick({
	  slidesToShow:3,
	  slidesToScroll:1,
	  //autoplay: true,
	  //autoplaySpeed: 2000,
	});
	//== slider five Detail Movie
	$('.slide_five').slick({
	  slidesToShow:5,
	  slidesToScroll:1,
	  //autoplay: true,
	  //autoplaySpeed: 2000,
	});
	$('.slide_five .slick-prev').addClass('prev-cus-1');
	$('.slide_five .slick-next').addClass('next-cus-1');
	
	//== Auto Other Films 
	$('.auto_other').slick({
		slidesToShow: 6,
		slidesToScroll: 1,
		autoplay: true,
		autoplaySpeed: 2000,
	});
	
	//== Auto chi tiet - uu dai
	$('.auto_chitiet-ud').slick({
		slidesToShow:4,
		slidesToScroll: 1,
		autoplay: true,
		autoplaySpeed: 2000,		
	});
	$('.slide-ud-khac .slick-prev').addClass('prev-cus-2');
	$('.slide-ud-khac .slick-next').addClass('next-cus-2');
	$('.rap-gv-slider .bx-next').addClass('next-custom');
	
	// == Scroll
	$('.scroll-pane').jScrollPane();

	// == Scroll register Club
	$('.scroll-club').jScrollPane({
		'autoReinitialise':true,
		'autoReinitialiseDelay':true,
		'animateScroll': true
	});
	
	// == Scroll Thanh Toan
	$('.scroll-pay2').jScrollPane();

	/* Menu Gioi Thieu */
		
	/* Menu items */
	/*var submenu = $(this).children('.introduce-menu-items');
	submenu.toggle('fast');*/

	$('.introduce-menu').on('click','.open',function(e){
		if( e.target != this ) 
			return;
		$(this).addClass('closes').removeClass('open');
		$(this).find('.introduce-menu-items').stop().slideDown(200);
	});
	$('.introduce-menu').on('click','.closes',function(e){
		if( e.target != this ) 
			return;
		$(this).addClass('open').removeClass('closes');
		$(this).find('.introduce-menu-items').stop().slideUp(200);
	});
	
		
	/* add CSS Text */
	$('.introduce-menu > ul > li > a').click(function(){
		if($(this).hasClass('active')){
			$(this).addClass('active');
		}
		else{
			$('.introduce-menu > ul > li > a').removeClass('active');
			$(this).addClass('active');
		}		
	});
	$('.introduce-menu-items li a').click(function(){
		$('.introduce-menu-items li a').removeClass('actives');
		if($(this).hasClass('actives')){
			$(this).removeClass('actives');
		}
		else{
			$(this).addClass('actives');
		}
	});
	/* End Menu Gioi Thieu */
	
	/* tab Drawer */
	$('.drawer-item').drawer({	
		// slide animation speed
		slideSpeed: 200
	});
	
	/* == popup Detail Iframe == */
	var deleted_iframe = $('.iframe-trailer').remove();
	$('.open-popup').click(function(){
	    // Append
	    var srcTL = $(this).attr('data-youtube-id');
	    var str = "https://www.youtube.com/embed/" + srcTL + "?autoplay=1";
	    $(deleted_iframe).find("iframe").attr('src', str);
		$('.show-content').append(deleted_iframe);
		// Popup Display: block
		$('.popup-main').fadeIn(500);
		$('.popup-opacity').css('display', 'block');
		// Close popup.
		$('.popup-content .close-popup i').click(function(){
			$('.popup-main').fadeOut(0);
			$('.popup-opacity').css('display', 'none');
			$('.iframe-trailer').remove();
		});
	});
	
	/* == popup Detail Image == */
	var deleted_img = $('.img-trailer').remove();
	$('.popup-img-item').click(function(){
		// Append
		$('.show-content').append(deleted_img);
		// Popup Display: block
		$('.popup-main').fadeIn(500);
		$('.popup-opacity').css('display', 'block');
		// Close popup.
		$('.popup-content .close-popup i').click(function(){
			$('.popup-main').fadeOut(0);
			$('.popup-opacity').css('display', 'none');
			$('.img-trailer').remove();
		});
	});
	
	//* Popup Maps 
	
	$('.popup-map').click(function () {
	    
	    var str = $(this).attr("src")
	   
	    var maps = '<div class="frame-1"><iframe src="' + str + '" width="790" height="450" frameborder="0" style="border:0" allowfullscreen></iframe></div>';
		
		$('.show-content').append(maps);
		// Popup Display: block
		$('.popup-main').fadeIn(500);
		$('.popup-opacity').css('display', 'block');
		// Close popup.
		$('.popup-content .close-popup i').click(function(){
			$('.popup-main').fadeOut(0);
			$('.popup-opacity').css('display', 'none');
			// remove
			$('.img-trailer').remove();
			$('.frame-1').remove();
		});
	});

	//active css Items Chon Gia Ve 
	/*$('.vien-item-group a').click(function(){
		$('.vien-item-group a').removeClass('active');
		if($(this).hasClass('active')){
			$(this).removeClass('active');
		}
		else{
			$(this).addClass('active');
		}
	});*/
	//active Mega gs-Club
	$('.tab-club-all .nav-tabs > li').click(function(){
		$('.arrow-up').removeClass('active');
		$(this).find('.arrow-up').addClass('active');
	});
	
    // Value Register
	if ($(window).width() >= 960 && $(window).width() <= 1024) {

	    zoom = $(window).width() / $("body").width() * 85;
	    document.body.style.zoom = zoom + "%";
	    document.documentElement.style.zoom = "100%";
	    $('.slide-group .slide-right .slide-text div div span').css('font-size', '11pt');
	    $('.events .event-right').css({
	        'padding': '0 7px',
            'width': '268px'
	    });
	    
	}
	else {
	   
	};

    // slide_selectBox
	enableSelectBoxes();

	
});

function UnderElement(elem, e) {
    var elemWidth = $(elem).width();
    var elemHeight = $(elem).height();
    var elemPosition = $(elem).offset();
    var elemPosition2 = new Object;
    elemPosition2.top = elemPosition.top + elemHeight;
    elemPosition2.left = elemPosition.left + elemWidth;
    return ((e.pageX > elemPosition.left && e.pageX < elemPosition2.left) && (e.pageY > elemPosition.top && e.pageY < elemPosition2.top))
}

function enableSelectBoxes() {
   

    // click_box_1
    $('.click_box_1').find('span.selected').click(function () {
        $('.click_box_1 .selectOptions').toggle();           
        $('.bg-close-selectbox').css('display', 'block');
    });
    $('.click_box_1').find('span.selectOption').click(function () {
        $('.click_box_1').find('span.selected').html($(this).html());
        $('.click_box_1 .selectOptions').toggle();
        $('.bg-close-selectbox').css('display', 'none');            
    });

    // click_box_2
    $('.click_box_2').find('span.selected').click(function () {
        $('.click_box_2 .selectOptions').toggle();
        $('.bg-close-selectbox').css('display', 'block');
    });
    $('.click_box_2').find('span.selectOption').click(function () {
        $('.click_box_2').find('span.selected').html($(this).html());
        $('.click_box_2 .selectOptions').toggle();
        $('.bg-close-selectbox').css('display', 'none');
    });

    // click_box_3
    $('.click_box_3').find('span.selected').click(function () {
        $('.click_box_3 .selectOptions').toggle();
        $('.bg-close-selectbox').css('display', 'block');
    });
    $('.click_box_3').find('span.selectOption').click(function () {
        $('.click_box_3').find('span.selected').html($(this).html());
        $('.click_box_3 .selectOptions').toggle();
        $('.bg-close-selectbox').css('display', 'none');
    });

    $('.bg-close-selectbox').click(function () {
        $('.click_box_1 .selectOptions').css('display', 'none');
        $('.click_box_2 .selectOptions').css('display', 'none');
        $('.click_box_3 .selectOptions').css('display', 'none');
        $('.bg-close-selectbox').css('display', 'none');
    });

    /* active movie_sub - Page Detail Movie - Home
    ------------------------------------*/
    $('.movie_sub .sub_item a').click(function () {
        $('.movie_sub .sub_item a').removeClass('active');
        if ($(this).hasClass('active')) {
           
        } else {
            $(this).addClass('active');
        }
    });

}