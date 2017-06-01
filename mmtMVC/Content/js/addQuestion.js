

       $(document).ready(function()
       {
           if (document.getElementById('optcnt').value > 7) {
               $("#trmoreopt").css("display", "none");
           }

       });





    $(document).ready(function () {
        document.getElementById('add-option').onclick = duplicate;

        function duplicate ()
        {
            var imagecnt = document.getElementById('optcnt').value;
                
            discnt = parseInt(imagecnt) - 1;
            nextcnt = parseInt(imagecnt) + 1;
            var disid = "#optin-dupli" + imagecnt;
            $(disid).css("display",""); 

            $("#optcnt").val(nextcnt);

            if (nextcnt > 7) {
                $("#trmoreopt").css("display", "none");
            }
        }

    });





     
    function addCombo() {

        var combo = document.getElementById("drptopic").value;


        if (combo == 1) {
            document.getElementById("topictxt").focus();
            document.getElementById("topictxt").style.display = "block";
        }
        else {


            document.getElementById("topictxt").style.display = "none";
              
        }

    }

function checktextques(obj) {
    var value = obj.value;
    if (value == "Enter Your Question " || value == "Enter Your Question") {
        obj.value = "";
    }
    else {

    }
}

function checktexta(obj) {
    var value = obj.value;
    if (value == "This is the First answer option") {
        obj.value = "";
    }
    else {

    }
}
function checktextb(obj) {
    var value = obj.value;
    if (value == "This is the Second answer option") {
        obj.value = "";
    }
    else {

    }
}
function checktextc(obj) {
    var value = obj.value;
    if (value == "This is the Third answer option") {
        obj.value = "";
    }
    else {

    }
}
function checktextd(obj) {
    var value = obj.value;
    if (value == "This is the Fourth answer option") {
        obj.value = "";
    }
    else {

    }
}

function checkthis(obj,x) {

    if(obj.checked)
    {
       
        var parentid = $(obj).parent().parent(".sorting");
        if($(parentid).find('.ninn').val() == "")
        {
            alertify.alert("Please fill out an answer option", function(){

            });
                  
            obj.checked = false;
        } 
        else
        {
            $('#lblchsco').removeClass("validerror");
        }
    }
    else
    {
        obj.checked = false;
    }
           

}





    function checkvalidation() {
        var chkboxcount = 0, rdiocount = 0;
        var questtxtareavalue = document.getElementById("varquestext").value;
         
          
        var error = "";
        var txtmarks = $("#TextBox1").val();
           
        var radioerror = "false";
        if (questtxtareavalue == '') {

                  
            $('html, body').animate({
                scrollTop: $('#divaddques').offset().top
            }, 200);

            $('[id$=err_questxt]').css({

                "display": 'inline'
            });
                
            error = "false";
        }
         
        var txta=$('.texta').val();
        var txtb=$('.textb').val();
     
        if(txta=='')
        {
                
                
            $('html, body').animate({
                scrollTop: $('#divaddques').offset().top
            }, 200);
                
            $('[id$=er_txta]').css({

                "display": 'inline'
            });
               
            error = "false";
             
            
        }
            
        
        if(txtb=='')
        {

            $('html, body').animate({
                scrollTop: $('#divaddques').offset().top
            }, 200);

            $('[id$=er_txtb]').css({

                "display": 'inline'
            });
               
            error = "false";
               
        }

        chkboxcount = $(".sorting input:checked").length

        if ((chkboxcount < 1)) {
               
            $('html, body').animate({
                scrollTop: $('#divaddques').offset().top
            }, 200);

            $('#lblchsco').addClass("validerror");
            return false;
        }
            

            

         
        if (txtmarks == '') {
            

            $('[id$=err_txtmarks]').css({

                "display": 'inline'
            });
                
            error = "false";
        }
        else
        {
               
               
            if (parseInt(txtmarks) <= 0 || isNaN(txtmarks))
            {
                
                $('[id$=err_txtmarks]').css({

                    "display": 'inline'
                });
                
                error = "false";
            }
            else if (parseInt(txtmarks) > 99)
            {
                   
                $('[id$=err_txtmarks]').css({

                    "display": 'inline'
                });
                
                error = "false";
            }
            else
            {
                
            }
               
                
        }

          
        var topicselevalue = $('#rbtnlisttopics input:checked').val();

        if(topicselevalue=="1")
        {
        
            var getname = $("#topictxt").val();
               
            if (getname.length <= 0) {
                $('#er_topictext').css({

                    "display": 'inline-block'
                });
                error = "false";
                   
            }
              

        }
        if(topicselevalue=="2")
        {
            var ddltopic = $('#drptopic').val();
            if (ddltopic == "0") {
                    
                $('#er_ddltopic').css({

                    "display": 'inline-block'
                });
                    
               
                return false;
            }
        }
           
       
           
            
        if (error == "false") {
            return false;
        }
        else
        {
            $.fancybox.open([{ href: '#divquesaddload', modal: true }]);
        }

    }



    $(document).ready(function (e) {
        $('input[type="text"].required,textarea.required,select,input[type="text"].ninn,input[type="text"].ctp-txtbx2').focus(function () {
            $(this).css({
                "border": "",
                "background": ""
            });
            $('div.ctp-txtarea').css({
                "border": "1px solid #A9AFC1",
                "background": ""
            });
            $('div.ctp-txtbx').css({
                "border": "1px solid #A9AFC1",
                "background": ""
            });
            $('[id$=A1]').css({
                "display": 'none'
            });
        });

    });

    function backalert(x)
    {

        $('html, body').animate({
            scrollTop: $('#errimage').offset().top
        }, 200);

    }

    





    $(document).ready(function () {
     
        $(("#topictxt")).keypress(function () {
            $('[id$=er_topictext]').css(
             {
                 "display": 'none'
             });
               
        });



        $(("#varquestext")).keypress(function () {
              
            $('[id$=err_questxt]').css(
             {
                 "display": 'none'
             });
               
        });
        $(("#TextBox1")).keypress(function () {
            $('[id$=err_txtmarks]').css(
             {
                 "display": 'none'
             });
               
        });
        $((".texta")).keypress(function () {

            $('[id$=er_txta]').css(
             {
                 "display": 'none'
             });
        });

        $(".texta").on("keyup", function() {
            if($(this).val()=="")
            {
                var parentid =  $(this).parent().parent(".sorting" );
                var obj = $(parentid).find('.required').children();
                $(obj).attr("checked",false);
                     
            }
        });

        $(".textb").on("keyup", function() {
            if($(this).val()=="")
            {
                var parentid =  $(this).parent().parent(".sorting" );
                var obj = $(parentid).find('.required').children();
                $(obj).attr("checked",false);
                     
            }
        });

        $(".textc").on("keyup", function() {
            if($(this).val()=="")
            {
                var parentid =  $(this).parent().parent(".sorting" );
                var obj = $(parentid).find('.required').children();
                $(obj).attr("checked",false);
                     
            }
        });
        $(".textd").on("keyup", function() {
            if($(this).val()=="")
            {
                var parentid =  $(this).parent().parent(".sorting" );
                var obj = $(parentid).find('.required').children();
                $(obj).attr("checked",false);
                     
            }
        });
        $(".texte").on("keyup", function() {
            if($(this).val()=="")
            {
                var parentid =  $(this).parent().parent(".sorting" );
                var obj = $(parentid).find('.required').children();
                $(obj).attr("checked",false);
                     
            }
        });
        $(".textf").on("keyup", function() {
            if($(this).val()=="")
            {
                var parentid =  $(this).parent().parent(".sorting" );
                var obj = $(parentid).find('.required').children();
                $(obj).attr("checked",false);
                     
            }
        });
        $(".textg").on("keyup", function() {
            if($(this).val()=="")
            {
                var parentid =  $(this).parent().parent(".sorting" );
                var obj = $(parentid).find('.required').children();
                $(obj).attr("checked",false);
                     
            }
        });
        $(".texth").on("keyup", function() {
            if($(this).val()=="")
            {
                var parentid =  $(this).parent().parent(".sorting" );
                var obj = $(parentid).find('.required').children();
                $(obj).attr("checked",false);
                     
            }
        });
           

        $((".textb")).keypress(function () {
            $('[id$=er_txtb]').css(
             {
                 "display": 'none'
             });
               
        });

        $((".textc")).keypress(function () {
            $('[id$=er_txtc]').css(
             {
                 "display": 'none'
             });
               
        });

        $((".textd")).keypress(function () {
            $('[id$=er_txtd]').css(
             {
                 "display": 'none'
             });
               
        });
        $((".texte")).keypress(function () {
            $('[id$=er_txte]').css(
             {
                 "display": 'none'
             });
               
        });
        $((".textf")).keypress(function () {
            $('[id$=er_txtf]').css(
             {
                 "display": 'none'
             });
               
        });
        $((".textg")).keypress(function () {
            $('[id$=er_txtg]').css(
             {
                 "display": 'none'
             });
               
        });
        $((".texth")).keypress(function () {
            $('[id$=er_txth]').css(
             {
                 "display": 'none'
             });
               
        });
        $('[id$=drptopic]').focus(function () {
            $('[id$=er_ddltopic]').css({

                "display": 'none'
            });
        });

        //$(':input[type="text"][class="ninn"]').keypress(function () {
        //    alert('hello');
        //    $('[id*=A1]').css(
        //    {
        //        "display": 'none'
        //    });
               
               
        //});
            

    });




    function changeerror(obj)
    {
        //alert(obj.value);
        $(obj).parent().parent().parent().parent('li').find('[id$=A1]').css(
             {
                 "display": 'none'
             });
        //$('[id*=A1]').css(
        //     {
        //         "display": 'none'
        //     });
    }


                    //show tooltip according to mouse position
    $(document).ready(function () {
        //var tooltips = document.querySelectorAll('.helptool span.innerred');

        //window.onmousemove = function (e) {
        //    var x = (e.clientX - 60) + 'px',
        //        y = (e.clientY - 125) + 'px';
        //    for (var i = 0; i < tooltips.length; i++) {
        //        tooltips[i].style.top = y;
        //        tooltips[i].style.left = x;
        //    }
        //};

        $('.tooltip').tooltipster();

    });


    $(document).ready(function () {
        $("input[name=rbtnlisttopics]:radio").change(function () { // bind a function to the change event
         
            if ($(this).is(":checked")) { // check if the radio is checked
                var val = $(this).val(); // retrieve the value
        
                if (val == "1") {
                     
                    $("#topictxt").focus();
                    document.getElementById("topictxt").style.display = "block";
                      
                    document.getElementById("ddltopic").style.display =  "none"
                    if($("#lblnewtopic").text()!='' )
                    {
                        document.getElementById("clicktoadd").style.display = "block";
                        document.getElementById("lblnewtopic").style.display = "block";
                        document.getElementById("topictxt").style.display = "none";
                           
                        $('[id$=er_topictext]').css({
                            "display": 'none'
                        });
                    }
                }
                else if (val == "2") {
                    document.getElementById("topictxt").style.display = "none";
                       
                    $('[id$=er_topictext]').css({
                        "display": 'none'
                    });
                    document.getElementById("ddltopic").style.display =  "block"
                    document.getElementById("clicktoadd").style.display = "none";
                    document.getElementById("lblnewtopic").style.display = "none";
                }
                else {

                    document.getElementById("topictxt").style.display = "none";
                      
                    $('[id$=er_topictext]').css({
                        "display": 'none'
                    });
                    document.getElementById("ddltopic").style.display =  "none"
                    document.getElementById("clicktoadd").style.display = "none";
                    document.getElementById("lblnewtopic").style.display = "none";

                }
            }
        });

        $("[id$=clicktoadd]").click(function () {
            document.getElementById("topictxt").focus();
            document.getElementById("topictxt").style.display = "block";
            
            document.getElementById("clicktoadd").style.display = "none";
            document.getElementById("lblnewtopic").style.display = "none";
        });

    });



    function openpopup(type)
    {
         
        if (type== "1")
        {
                   
            $.fancybox({
                parent: "form:first",
                href     : "#uploadmedia",
                autoSize : true,
                fitToView: false,
                closeBtn: true,
            });
        }
    }

  
      tinyMCE.init({
          // General options
          mode: "textareas",
          theme: "simple",
          plugins: "lists,pagebreak,advhr,advlink,iespell,inlinepopups,insertdatetime,paste,directionality,noneditable,visualchars,nonbreaking,xhtmlxtras,advlist,autosave,visualblocks",
          editor_selector: "myTextEditor",
          width: 950,
          height: 300,
          // Theme options

          theme_advanced_toolbar_align: "left",
          theme_advanced_statusbar_location: "bottom",
          theme_advanced_resizing: true

      });
