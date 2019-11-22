$(document).ready(function ($) {
	show();
    $(".top").click(function () {
        $("html,body").animate({
            scrollTop: 0
        }, 900);
        return false;
    });
   
	
    $(window).resize(function () {
       show();
    });
    $(window).scroll(function () {
        //alert($('#main').offset().top);
        if ($(window).scrollTop() > $('#container h3').offset().top) {
            $('.top').show();
        } else {
            $('.top').hide();
        }

    });
	
	function show(){
		 if ($(document).width() > 768) {
            $(document).scrollTop(0);
        } else {
            $(document).scrollTop($('#main').offset().top - 10);
        }
        if ($(window).scrollTop() > $('#container h3').offset().top) {
            $('.top').show();
        } else {
            $('.top').hide();
        }
		}

})