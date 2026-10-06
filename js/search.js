//Created By:   By Sanjay
//Created On:   Jan 26, 10
//Purpose   :   To specify insan search form javascript function


function selectPhoneDefaultType() 
{
    if (document.getElementById('ctl00_ContentPlaceHolder1_txtphn').value != '') {
        if (document.getElementById('ctl00_ContentPlaceHolder1_rbAllContactNo').checked == false || document.getElementById('ctl00_ContentPlaceHolder1_rbphn').checked == false || document.getElementById('ctl00_ContentPlaceHolder1_rbmob').checked == false || document.getElementById('ctl00_ContentPlaceHolder1_rbothphn').checked == false)
            document.getElementById('ctl00_ContentPlaceHolder1_rbAllContactNo').checked = true;
    }
    else 
    {
        document.getElementById('ctl00_ContentPlaceHolder1_rbAllContactNo').checked == false;
        document.getElementById('ctl00_ContentPlaceHolder1_rbphn').checked == false;
        document.getElementById('ctl00_ContentPlaceHolder1_rbmob').checked == false;
        document.getElementById('ctl00_ContentPlaceHolder1_rbothphn').checked == false;
    }
}