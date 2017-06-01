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