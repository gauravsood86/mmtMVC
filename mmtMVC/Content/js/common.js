// JavaScript Document

$(document).ready(function(){					
	$('.bxslider13').bxSlider({
		auto: true,
		autoControls: true,
		autoHover: true,
		infiniteLoop: false,
		hideControlOnEnd: true,
		speed: 800
	});				
});
$( "#slidcl" ).click(function() {
	$( "#close-btn" ).click();
});
/*$(function() {
//Script for Textbox, Password & Textarea on click hide value
	$('input[type="text"],input[type="password"], textarea').focus(function () {
		defaultText = $(this).val();
		$(this).val('');
		$(this).addClass('focus');
	});
	$('input[type="text"],input[type="password"], textarea').blur(function () {
		if ($(this).val() == "") {
			$(this).val(defaultText);
			$(this).removeClass('focus');
		}
	});
});*/
$(function() {
	$('input, textarea').placeholder();
});
$(function() {
       var zIndexNumber = 1000;
       // Put your target element(s) in the selector below!
       $(".bx-controls").each(function() {
               $(this).css('zIndex', zIndexNumber);
               zIndexNumber -=999;
       });
});