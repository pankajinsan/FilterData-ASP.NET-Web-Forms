//Created By:   By Sanjay
//Created On:   Jan 09, 10
//Purpose   :   To specify add/update insan form javascript function
//Modified By: Pankaj Garg Insan
//Modified on: Mar 10,12

function swapImage() {
    var insanno;
    var image = document.getElementById("img");
    insanno = parseInt(document.getElementById('ContentPlaceHolder1_txtslpnum').value);
    var folder = Math.ceil(insanno/200000);
    image.src = "../Photos/" + folder + "/" + insanno + ".jpg";
//    alert('Here you go!');
}
function swapPicImage() {
    var insanno;
    var image = document.getElementById('img');
    insanno = parseInt(document.getElementById('ctl00_ContentPlaceHolder1_Label2').innerHTML);
    var folder = Math.ceil(insanno / 200000);
    image.src = "../Photos/" + folder + "/" + insanno + ".jpg?" + (new Date()).getTime();
}
function ChangeStaFocus() {
    //ContentPlaceHolder1_
    document.getElementById('ctl00_ContentPlaceHolder1_ddldst').focus();
    //alert('Hello');
    //$('#<%=ddldst.ClientID %>').focus()
}
function ChangeDstFocus() {
    //$('#<%=ddlblk.ClientID %>').focus()
    document.getElementById('ctl00_ContentPlaceHolder1_ddlblk').focus();
}
function ChangeSangatStaFocus() {
    //ContentPlaceHolder1_
    document.getElementById('ctl00_ContentPlaceHolder1_ddlsta').focus();
    //alert('Hello');
    //$('#<%=ddldst.ClientID %>').focus()
}
function ChangeSangatDstFocus() {
    //$('#<%=ddlblk.ClientID %>').focus()
    document.getElementById('ctl00_ContentPlaceHolder1_ddldst').focus();
}
//ddldist
function ChangeSMSDstFocus() {
    //$('#<%=ddlblk.ClientID %>').focus()
    document.getElementById('ctl00_ContentPlaceHolder1_ddldist').focus();
}
function SetDOBFocus() {
    //$('#<%=ddlblk.ClientID %>').focus()
    document.getElementById('ctl00_ContentPlaceHolder1_ddlmrtsts').focus();
}

function SetFocus(ctrl) {
    //ctrl.focus();
    document.getElementById(ctrl).focus();
    //alert("Please enter valid txt!");
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
    {
        document.getElementById('ctl00_ContentPlaceHolder1_ddlgartyp').value = "D/O";
        document.getElementById('ctl00_ContentPlaceHolder1_ddlmrtsts').value = "Unmarried";
        document.getElementById('ctl00_ContentPlaceHolder1_ddlocc').value = "31";
    }
    if (v1 == "Mrs")
    {
        document.getElementById('ctl00_ContentPlaceHolder1_ddlgartyp').value = "W/O";
        document.getElementById('ctl00_ContentPlaceHolder1_ddlmrtsts').value = "Married";
        document.getElementById('ctl00_ContentPlaceHolder1_ddlocc').value = "30";
    }
    if (v1 == "Mr")
    {
       document.getElementById('ctl00_ContentPlaceHolder1_ddlgartyp').value = "S/O";
       document.getElementById('ctl00_ContentPlaceHolder1_ddlmrtsts').value = "--select--";
    }
}
function GrnsChange() {
    var ddl = document.getElementById('ctl00_ContentPlaceHolder1_ddlgrnmem').value;
    if (ddl == "1") {
        Enable('ctl00_ContentPlaceHolder1_ddlgrndrs');
        SetFocus('ctl00_ContentPlaceHolder1_ddlgrndrs');
    }
    else {
        Disable('ctl00_ContentPlaceHolder1_ddlgrndrs');
        SetValue('ctl00_ContentPlaceHolder1_ddlgrndrs', '0');
        Disable('ctl00_ContentPlaceHolder1_txtgrnnum');
        SetValue('ctl00_ContentPlaceHolder1_txtgrnnum', '');
        Disable('ctl00_ContentPlaceHolder1_txtgrnnum');
        SetFocus('ctl00_ContentPlaceHolder1_ddlpmtdtysts');
    }
}
function GrnDrsChange() {
    //ddlgrndrs
    var ddl = document.getElementById('ctl00_ContentPlaceHolder1_ddlgrndrs').value;
    if (ddl == "1") {
        Enable('ctl00_ContentPlaceHolder1_txtgrnnum');
        SetFocus('ctl00_ContentPlaceHolder1_txtgrnnum');
    }
    else {
        Disable('ctl00_ContentPlaceHolder1_ddlgrndrs');
        Disable('ctl00_ContentPlaceHolder1_txtgrnnum');
        SetValue('ctl00_ContentPlaceHolder1_txtgrnnum', ''); 
          SetFocus('ctl00_ContentPlaceHolder1_ddlpmtdtysts');
    }
}
function PmtDtyChange() {
    var ddl = document.getElementById('ContentPlaceHolder1_ddlpmtdtysts').value;
    if (ddl == "1") {
        Enable('ContentPlaceHolder1_ddlsmtid');
        Enable('ContentPlaceHolder1_ddlsmtder');
    //    SetValue('ContentPlaceHolder1_ddlsmtid', "--Select--");
//        var ddldera = document.getElementById('ContentPlaceHolder1_ddlsmtder');
//        var option = document.createElement("option");
//        option.text = "-- Select --";
//        ddldera.add(option, ddldera[0]); 
     //   SetValue('ContentPlaceHolder1_ddlsmtder', "-- Select --");
        SetFocus('ContentPlaceHolder1_ddlsmtid');
    }
    else {
        Disable('ContentPlaceHolder1_ddlsmtid'); Disable('ContentPlaceHolder1_ddlsmtder');
        SetFocus('ContentPlaceHolder1_txtfrmdat');
    }
}
function EbtStsChange() {
    var ddl = document.getElementById('ctl00_ContentPlaceHolder1_ddlebtsts').value;
    if (ddl == "1") {
        Enable('ctl00_ContentPlaceHolder1_ddlebtid');
        SetFocus('ctl00_ContentPlaceHolder1_ddlebtid');
        SetValue('ctl00_ContentPlaceHolder1_ddlebtid', "-- Select --");
    }
    else {
        Disable('ctl00_ContentPlaceHolder1_ddlebtid');
        SetFocus('ctl00_ContentPlaceHolder1_txtfrmdat');
    }
 }
function Enable(ctrl) {
    document.getElementById(ctrl).disabled = false;
}
function Disable(ctrl) {
    document.getElementById(ctrl).disabled = true;
}
function SetValue(ctrl, text) {
    document.getElementById(ctrl).value = text;
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
    var folderimg = Math.ceil(id/200000);
    window.open("../img.aspx?img=" + folderimg+"/"+id, "temp", "height=400,width=400");
}
function insdetails(insanid) {
    window.open("Details.aspx?insid=" + insanid, "temp", "height=1000,width=600");
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
function characterCounter(controlId, countControlId, maxCharlimit) {

    if (controlId.value.length > maxCharlimit)
        controlId.value = controlId.value.substring(0, maxCharlimit);
    else
        countControlId.value = maxCharlimit - controlId.value.length;
}

function CalculateAge(birthday) {
    //var re=/^(0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])[- /.](19|20)\d\d+$/;
    if (birthday.value != "") {
        birthdayDate = new Date(birthday.value);
        dateNow = new Date();
        var years = dateNow.getFullYear() - birthdayDate.getFullYear();
        var months = dateNow.getMonth() - birthdayDate.getMonth();
        var days = dateNow.getDate() - birthdayDate.getDate();
        if (isNaN(years) || years < 0) {
            document.getElementById('ctl00_ContentPlaceHolder1_txtage').value = "";
            //document.getElementById('ctl00_ContentPlaceHolder1_lblmsg').value = "Input date is incorrect!";
            return confirm('Input DOB is incorrect');
            //return false;
        }
        else {
            if (months < 0 || (months == 0 && days < 0)) {
                //years = parseInt(years) - 1;
                document.getElementById('ctl00_ContentPlaceHolder1_txtage').value = years;

                //return confirm(years);
            }
            else {
                document.getElementById('ctl00_ContentPlaceHolder1_txtage').value = years;
                //return confirm(years);
            }
        }
    }
}