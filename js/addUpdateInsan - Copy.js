//Created By:   By Sanjay
//Created On:   Jan 09, 10
//Purpose   :   To specify add/update insan form javascript function


function swapImage() {
    var insanno;
    var image = document.getElementById("img");
    insanno = parseInt(document.getElementById('ctl00_ContentPlaceHolder1_txtslpnum').value);
    image.src = "../../photos/" + insanno + ".jpg";
}

function m_sts_ckh() {
    var v1 = document.getElementById('ctl00_ContentPlaceHolder1_ddlmrtsts').value;
    if (v1 == "--select--") {
        alert("Please check the marital status \nAnd also check \nMr,Miss,Mrs and S/O,W/O,D/O");
        //aspnetForm.ctl00_ContentPlaceHolder1_ddlmrtsts.focus();	
    }
}

function genchange() {
    var v1 = document.getElementById('ctl00_ContentPlaceHolder1_ddlgen').value;
    if (v1 == "Miss")
        document.getElementById('ctl00_ContentPlaceHolder1_ddlgartyp').value = "D/O";
         document.getElementById('ctl00_ContentPlaceHolder1_ddlmrtsts').value = "Unmarried";
    if (v1 == "Mrs")
        document.getElementById('ctl00_ContentPlaceHolder1_ddlgartyp').value = "W/O";
    if (v1 == "Mr")
        document.getElementById('ctl00_ContentPlaceHolder1_ddlgartyp').value = "S/O";
}

function correctNaamYear() 
{
    var e = document.getElementById('ctl00_ContentPlaceHolder1_txtnamyer');
    if (e.value != '') 
    {
        var d = new Date();
        if (e.value < 1000) 
            e.value = d.getFullYear() - e.value;
    }
}

function abc(id) {
    window.open("../cal.aspx?nam=" + id, "temp", "height=250,width=250");
}
function img(id) {
    window.open("../img.aspx?img=" + id, "temp", "height=400,width=400");
}

function setddlvil() {
    var txt = document.getElementById('ctl00_ContentPlaceHolder1_txtvil');
    var ddl = document.getElementById('ctl00_ContentPlaceHolder1_ddlvil');
    var i = 0;
    if (txt.value == '') {
        ddl.options[0].selected = 'true';
        txt.value = '-- Select --';
    }
    else {
        for (i = 0; i < ddl.options.length; i++) {
            if (ddl.options[i].text.toLowerCase() == txt.value.toLowerCase()) {
                ddl.options[i].selected = 'true';
                txt.value = ddl.options[i].text;
                return;
            }
        }
    }
}
function ValidVillage(src, args) {
    var ddl = document.getElementById('ctl00_ContentPlaceHolder1_ddlvil');
    if (args.Value == ddl.options[ddl.selectedIndex].text)
    { args.IsValid = true; }
    else
    { args.IsValid = false; }
}
function fun() {
    var e = document.getElementById(ctl00_ContentPlaceHolder1_UpdatePanel5)
    if (e) {
        if (e.Style.Display != 'block') {
            e.Style.Display = 'block';
            e.Style.Visiblity = 'visible';
        }
        else {
            e.Style.Display = 'none';
            e.Style.Visiblity = 'hidden';
        }
    }
    window.alert(e);
}