// JScript File
/* Form Validation Script for Common Functions
Developed By	: Sanjay
Date			: Jan 26, 2009
Message			: Common Functions 
Modified By: Pankaj Garg Insan
*/

function phoneKeypress(e)
{
    if ([e.keyCode||e.which]==8) //this is to allow backspace
    return true;
    if ([e.keyCode||e.which]==9) //this is to allow tab
    return true;
    if ([e.keyCode||e.which]==14) //this is to allow shift in
    return true;
    if ([e.keyCode||e.which]==15) //this is to allow shift out
    return true;
    if ([e.keyCode||e.which]==46) //this is to allow period
    return true;
    if ([e.keyCode||e.which]==43) //this is to allow + symbol
    return true;
    if ([e.keyCode||e.which]==45) //this is to allow - symbol
    return true;
    if ([e.keyCode||e.which]==37) //this is to allow left arrow symbol
    return true;
    if ([e.keyCode||e.which]==38) //this is to allow up arrow symbol
    return true;
    if ([e.keyCode||e.which]==39) //this is to allow right arrow symbol
    return true;
    if ([e.keyCode||e.which]==40) //this is to allow down arrow symbol
    return true;
    if ([e.keyCode||e.which]==41) //this is to allow down paranthesis
    return true;
    
    if ([e.keyCode||e.which] < 48 || [e.keyCode||e.which] > 57)
    e.preventDefault? e.preventDefault() : e.returnValue = false;
}


//function ValidateKeypress(e) {
//    if ([e.keyCode || e.which] == 8) //this is to allow backspace
//        return true;
//    if ([e.keyCode || e.which] == 9) //this is to allow tab
//        return true;
//    if ([e.keyCode || e.which] == 37) //this is to allow left arrow symbol
//        return true;
//    if ([e.keyCode || e.which] == 38) //this is to allow up arrow symbol
//        return true;
//    if ([e.keyCode || e.which] == 39) //this is to allow right arrow symbol
//        return true;
//    if ([e.keyCode || e.which] == 40) //this is to allow down arrow symbol
//        return true;
//    if (e.keyCode == 46 && e.which == 0) //this is to allow delete
//        return true;
////    if ([e.keyCode || e.which] == 16) //this is to allow Shift
////        return true;
////    if ([e.keyCode || e.which] == 173) //this is to allow UNDERSCORE
////        return true;
//    //    if ([e.keyCode || e.which] == 95) //this is to allow - symbol
//    //        return true;
//    //this to allow 0 to 9(48 to 57)
//    //    if ([e.keyCode || e.which] < 48 || [e.keyCode || e.which] > 57)
//    //        e.preventDefault ? e.preventDefault() : e.returnValue = false;
//    var allow = 'ABCDEFGHIJKLMNOPQRSTUVWXYZ_*-/';
//    var k;
//    k = document.all ? parseInt(e.keyCode) : parseInt(e.which);
//    return (allow.indexOf(String.fromCharCode(k)) != -1);
////    var charCode = (e.which) ? e.which : event.keyCode
////    if (charCode > 47 && charCode < 58 || charCode == 127 || charCode == 8 || charCode == 95) {
////        return true;
////    }
////    else {
////        return false;
////    }
//}
function numberKeypress(e) {
    if ([e.keyCode || e.which] == 8) //this is to allow backspace
        return true;
    if ([e.keyCode || e.which] == 9) //this is to allow tab
        return true;
    if ([e.keyCode || e.which] == 37) //this is to allow left arrow symbol
        return true;
    if ([e.keyCode || e.which] == 38) //this is to allow up arrow symbol
        return true;
    if ([e.keyCode || e.which] == 39) //this is to allow right arrow symbol
        return true;
    if ([e.keyCode || e.which] == 40) //this is to allow down arrow symbol
        return true;
    if (e.keyCode == 46 && e.which == 0) //this is to allow delete
        return true;
    //    if ([e.keyCode||e.which]==46) //this is to allow period 
    //    return true;
    //    if ([e.keyCode||e.which]==43) //this is to allow + symbol
    //    return true;
    //    if ([e.keyCode||e.which]==45) //this is to allow - symbol
    //    return true;
    //this to allow 0 to 9(48 to 57)
    if ([e.keyCode || e.which] < 48 || [e.keyCode || e.which] > 57)
        e.preventDefault ? e.preventDefault() : e.returnValue = false;
}
function charKeypress(e) {
//    if ([e.keyCode || e.which] == 8) //this is to allow backspace
//        return true;
//    if ([e.keyCode || e.which] == 9) //this is to allow tab
//        return true;
//    if ([e.keyCode || e.which] == 37) //this is to allow left arrow symbol
//        return true;
//    if ([e.keyCode || e.which] == 38) //this is to allow up arrow symbol
//        return true;
//    if ([e.keyCode || e.which] == 32) //this is to Space
//        return true;
//    if ([e.keyCode || e.which] == 40) //this is to allow down arrow symbol
//        return true;
//    if (e.keyCode == 46 && e.which == 0) //this is to allow delete
//        return true;
    //    if ([e.keyCode||e.which]==46) //this is to allow period 
    //    return true;
    //    if ([e.keyCode||e.which]==43) //this is to allow + symbol
    //    return true;
    //    if ([e.keyCode||e.which]==45) //this is to allow - symbol
    //    return true;
    //this to allow A to Z (65 to 90) 64 and 91
    //|| (k > 96 && k < 123)
//    if ([e.keyCode || e.which] < 65 || [e.keyCode || e.which] > 90 || [e.keyCode || e.which] < 97 || [e.keyCode || e.which] > 122)
    //        e.preventDefault ? e.preventDefault() : e.returnValue = false;
    var key = e.keyCode;
    if (!((key == 8) || (key == 9) || (key == 32) || (key == 46) || (key >= 35 && key <= 40) || (key >= 65 && key <= 90))) {
        e.preventDefault();
    }
}
function decimalKeypress(e)
{
    if ([e.keyCode||e.which]==8) //this is to allow backspace
    return true;
    if ([e.keyCode||e.which]==9) //this is to allow tab
    return true;
    if ([e.keyCode||e.which]==37) //this is to allow left arrow symbol
    return true;
//    if ([e.keyCode||e.which]==38) //this is to allow up arrow symbol
//    return true;
    if ([e.keyCode||e.which]==39) //this is to allow right arrow symbol
    return true;
//    if ([e.keyCode||e.which]==40) //this is to allow down arrow symbol
//    return true;
    if (e.keyCode==46 && e.which == 0) //this is to allow delete
    return true;
    
    if ([e.keyCode||e.which]==46) //this is to allow period
    return true;
    
    if ([e.keyCode||e.which]==38) //this is to block Shift+&
    return false;
    
    if ([e.keyCode||e.which]==40) //this is to block Shift+(
    return false;
    
//    if ([e.keyCode||e.which]==43) //this is to allow + symbol
//    return true;
//    if ([e.keyCode||e.which]==45) //this is to allow - symbol
//    return true;
    if ([e.keyCode||e.which] < 48 || [e.keyCode||e.which] > 57)
    e.preventDefault? e.preventDefault() : e.returnValue = false;
}
 function validate(key) {
        //getting key code of pressed key
        var keycode = (key.which) ? key.which : key.keyCode;
        //comparing pressed keycodes
        if (!(keycode == 8 || keycode == 9 || keycode == 46 || keycode == 37 || keycode == 38 || keycode == 39 || keycode == 40) && (keycode < 48 || keycode > 57) && (keycode < 96 || keycode > 105)) {
            return false;
        }
        else {
            return true;
        }
        
    }
    function abc(key) {
        alert('validate');
    }

    function toUpper(obj, e) {
        var key = e.keyCode;
        if (key >= 65 && key <= 90) {
            var mystring = obj.value;
            var sp = mystring.split(' ');
            var wl = 0;
            var f, r;
            var word = new Array();
            for (i = 0; i < sp.length; i++) {
                f = sp[i].substring(0, 1).toUpperCase();
                r = sp[i].substring(1).toLowerCase();
                word[i] = f + r;
            }
            newstring = word.join(' ');
            obj.value = newstring;
            return true;
        }
    }
    function testletter(event) {
        str = event.target.value;
        event.value = str.replace(/(^|\s|[\-\,\.])\w/g, function (cWrd)
        { return cWrd.toUpperCase() });
    }
    function upCaseWords(mystring) {
        regex = /(^|\s|\-)./g;
        return mystring.replace(regex, function (v) { return v.toUpperCase(); });
    }
    function allTitleCase(inStr) { return inStr.replace(/\w\S*/g, function(tStr) { return tStr.charAt(0).toUpperCase() + tStr.substr(1).toLowerCase(); }); }
    //onblur="toUpper(this.value);"
//    function toUpper(mystring) {
//        var sp = mystring.split(' ');
//        var wl = 0;
//        var f, r;
//        var word = new Array();
//        for (i = 0; i < sp.length; i++) {
//            f = sp[i].substring(0, 1).toUpperCase();
//            r = sp[i].substring(1);
//            word[i] = f + r;
//        }
//        newstring = word.join(' ');
//        document.getElementById('keyword').value = newstring;
//        return true;
//    }