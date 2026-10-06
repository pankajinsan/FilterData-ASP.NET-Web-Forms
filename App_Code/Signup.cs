using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MySql.Data.MySqlClient;
using System.Configuration;
/// <summary>
/// Summary description for Signup
/// </summary>
public class Signup
{
    #region Properties
    public int UserId { get; set; }
    public string RandomPassword { get; set; }
    public string DBMD5Password { get; set; }
    public string Name { get; set; }
    public string FatherName { get; set; }
    public int CountryId { get; set; }
    public int StateId { get; set; }
    public int DistrictId { get; set; }
    public int BlockId { get; set; }
    public string Address { get; set; }
    public int InsanNo { get; set; }
    public string WhatsGroupName { get; set; }
    public string WhatsMobileNo { get; set; }
    public string MobileNo { get; set; }
    public int Categoryid { get; set; }
    public DateTime DOB { get; set; }
    public string PrimaryEmailId { get; set; }
    public string OtherEmailId { get; set; }
    public string TwitterHandle { get; set; }
    public string TwitterHandleOthers { get; set; }
    public bool ParshadTaken { get; set; }
    public int Qualification { get; set; }
    public int Profession { get; set; }
    public string Skill { get; set; }
    public string SkillOther { get; set; }
    public int NaamYear { get; set; }
    public string UserOwn { get; set; }
    public string Gender { get; set; }
    public string ModifiedBy { get; set; }
    public int SubCategoryId { get; set; }
    public int CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public int Counting { get; set; }
    public DateTime SewaDate { get; set; }
    public int AssignedSMID { get; set; }
    public string AssignedSMComments { get; set; }
    public string AssignedDMName { get; set; }
    public string AssignedDmContactNo { get; set; }
    public string BloodGroup { get; set; }
    public string Comments { get; set; }
    public int SewaPoints { get; set; }
    public string Password { get; set; }
    #endregion
    public Signup()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public Signup(int userid,string name,string fathername,int countryid,int stateid,int districtid,int blockid,string address,int insanno,string whatsgroupname,string whatsmobileno,string mobileno,int category,DateTime dob,string primaryemailid,string otheremailid,string twitterhandle,bool pasrshadtaken,int qualification,int profession,string skills,string skillother,int naamyear,string userown,string gender,string modifiedby,string bloodgrp,string twitterhandleothers)
    {
        UserId = userid;
        Name = name;
        FatherName = fathername;
        CountryId = countryid;
        StateId = stateid;
        DistrictId = districtid;
        BlockId = blockid;
        Address = address;
        InsanNo = insanno;
        WhatsGroupName = whatsgroupname;
        WhatsMobileNo = whatsmobileno;
        MobileNo = mobileno;
        Categoryid = category;
        DOB = dob;
        PrimaryEmailId = primaryemailid;
        OtherEmailId = otheremailid;
        TwitterHandle = twitterhandle;
        ParshadTaken = pasrshadtaken;
        Qualification = qualification;
        Profession = profession;
        Skill = skills;
        SkillOther = skillother;
        NaamYear = naamyear;
        UserOwn = userown;
        Gender = gender;
        ModifiedBy = modifiedby;
        BloodGroup = bloodgrp;
        TwitterHandleOthers = twitterhandleothers;
    }
    /// <summary>
    /// Submit Sewa Report
    /// </summary>
    /// <param name="subcategoryid"></param>
    /// <param name="counting"></param>
    /// <param name="createdby"></param>
    /// <param name="twhandle"></param>
    /// <param name="date"></param>
    public Signup(int subcategoryid, int counting, int createdby, string twhandle, DateTime date, string comments, int sewapoints, DateTime createdon)
    {
        SubCategoryId = subcategoryid;
        Counting = counting;
        CreatedBy = createdby;
        TwitterHandle = twhandle;
        SewaDate = date;
        Comments = comments;
        SewaPoints = sewapoints;
        CreatedOn = createdon;
    }
    /// <summary>
    /// Approve User
    /// </summary>
    /// <param name="name"></param>
    /// <param name="primaryemailid"></param>
    /// <param name="assignedwhatsgrp"></param>
    /// <param name="assignedsmname"></param>
    /// <param name="smcomments"></param>
    /// <param name="dmname"></param>
    /// <param name="dmcontactno"></param>
    /// <param name="userid"></param>
    /// <param name="modby"></param>
    public Signup(string name,string primaryemailid, string assignedwhatsgrp, int assignedsmid, string smcomments, string dmname, string dmcontactno, int userid, string modby)
    {
        Name = name;
        PrimaryEmailId = primaryemailid;
        WhatsGroupName = assignedwhatsgrp;
        AssignedSMID = assignedsmid;
        AssignedSMComments = smcomments;
        AssignedDMName = dmname;
        AssignedDmContactNo = dmcontactno;
        UserId = userid;
        ModifiedBy = modby;
    }
    public Signup(string name, string emailaddress, string password, string mobile)
    {
        Name = name;
        PrimaryEmailId = emailaddress;
        Password = password;
        MobileNo = mobile;
    }
    public bool RegisterSignUp()
    {
        try
        {
           
            int r = 0;
            string qry = "insert into Users(Username,Password,Role,Name,EmailId,Mobile,CreatedOn)values(@EmailId,@Password,@Role,@Name,@EmailId,@Mobile,now())";
            MySqlParameter EmailIdP = new MySqlParameter("@EmailId", PrimaryEmailId);
            MySqlParameter PasswordP = new MySqlParameter("@Password", Password);
            MySqlParameter RoleP = new MySqlParameter("@Role", "User");
            MySqlParameter NameP = new MySqlParameter("@Name", Name);
            MySqlParameter MobileP = new MySqlParameter("@Mobile", MobileNo);
            MySqlParameter[] p = { EmailIdP, PasswordP, RoleP, NameP, MobileP };
            r = MySqlHelper.ExecuteNonQuery(common.GetConnectionString(), qry, p);
            if (r > 0)
            {
                return true;
            }
            else
            {
                Exception ex = new Exception("Error In Sign Up!!");
                throw ex;
            }
        }
        catch
        {
            Exception ex = new Exception("Error In Sign Up!!");
            throw ex;
        }
    }
    public bool UpdateSignUp()
    {
        try
        {
            //usrcategory=@usrcategory
            //usrpriemail=@usrpriemail
            int r = 0;
            string qry = "update tbusr set usrname=@usrname,usrfatnam=@usrfatnam,usrcouid=@usrcouid,usrstaid=@usrstaid,usrdstid=@usrdstid,usrblkid=@usrblkid,usradd=@usradd,usrinsnum=@usrinsnum,wgrpnam=@wgrpnam,wmobno=@wmobno,usrmob=@usrmob,usrcategory=@usrcategory,usrdob=@usrdob,usremailother=@usremailother,twhandle=@twhandle,parshadtaken=@parshadtaken,usreduid=@usreduid,usrprofessionid=@usrprofessionid,usrskillid=@usrskillid,usrnaamyear=@usrnaamyear,hobby=@hobby,usrown=@usrown,lastuptdate=now(),lastuptby=@lastuptby,skillother=@skillother,bloodgrp=@bloodgrp,twhandleother=@twhandleother where usrid=@usrid";
            MySqlParameter useridP = new MySqlParameter("@usrid", UserId);
            MySqlParameter usrnameP = new MySqlParameter("@usrname", Name);
            MySqlParameter usrfatnamP = new MySqlParameter("@usrfatnam", FatherName);
            MySqlParameter usrcouidP = new MySqlParameter("@usrcouid", CountryId);
            MySqlParameter usrstaidP = new MySqlParameter("@usrstaid", StateId);
            MySqlParameter usrdstidP = new MySqlParameter("@usrdstid", DistrictId);
            MySqlParameter usrblkidP = new MySqlParameter("@usrblkid", BlockId);
            MySqlParameter usraddP = new MySqlParameter("@usradd", Address);
            MySqlParameter usrinsnumP = new MySqlParameter("@usrinsnum", InsanNo);
            MySqlParameter wgrpnamP = new MySqlParameter("@wgrpnam", WhatsGroupName);
            MySqlParameter wmobnoP = new MySqlParameter("@wmobno", WhatsMobileNo);
            MySqlParameter usrmobP = new MySqlParameter("@usrmob", MobileNo);
            MySqlParameter usrcategoryP = new MySqlParameter("@usrcategory", Categoryid);
            MySqlParameter usrdobP = new MySqlParameter("@usrdob", DOB);
            MySqlParameter usrpriemailP = new MySqlParameter("@usrpriemail", PrimaryEmailId);
            MySqlParameter usremailotherP = new MySqlParameter("@usremailother", OtherEmailId);
            MySqlParameter twhandleP = new MySqlParameter("@twhandle", TwitterHandle);
            MySqlParameter parshadtakenP = new MySqlParameter("@parshadtaken", ParshadTaken);
            MySqlParameter usreduidP = new MySqlParameter("@usreduid", Qualification);
            MySqlParameter usrprofessionidP = new MySqlParameter("@usrprofessionid", Profession);
            MySqlParameter usrskillidP = new MySqlParameter("@usrskillid", Skill);
            MySqlParameter usrnaamyearP = new MySqlParameter("@usrnaamyear", NaamYear);
            MySqlParameter hobbyP = new MySqlParameter("@hobby", "");
            MySqlParameter usrusrnamP = new MySqlParameter("@usrusrnam", "");
            MySqlParameter usrpwdP = new MySqlParameter("@usrpwd", "");
            MySqlParameter usrownP = new MySqlParameter("@usrown", UserOwn);
            MySqlParameter lastuptbyP = new MySqlParameter("@lastuptby", ModifiedBy);
            MySqlParameter skillotherP = new MySqlParameter("@skillother", SkillOther);
            MySqlParameter bloodgrpP = new MySqlParameter("@bloodgrp", BloodGroup);
            MySqlParameter twhandleotherP = new MySqlParameter("@twhandleother", TwitterHandleOthers);
            MySqlParameter[] p = { useridP, usrnameP, usrfatnamP, usrcouidP, usrstaidP, usrdstidP, usrblkidP, usraddP, usrinsnumP, wgrpnamP, wmobnoP, usrmobP, usrcategoryP, usrdobP, usrpriemailP, usremailotherP, twhandleP, parshadtakenP, usreduidP, usrprofessionidP, usrskillidP, usrnaamyearP, hobbyP, usrusrnamP, usrpwdP, usrownP, lastuptbyP, skillotherP, bloodgrpP, twhandleotherP };
            r = MySqlHelper.ExecuteNonQuery(common.GetConnectionString(), qry, p);
            if (r > 0)
            {
                return true;
            }
            else
            {
                Exception ex = new Exception("Error In Update Profile Details!!");
                throw ex;
            }
        }
        catch
        {
            Exception ex = new Exception("Error In Update Profile Details!!");
            throw ex;
        }
    }
    public bool SaveReportDetails()
    {
        try
        {
            string qry = "insert into tbreport(subcategoryid,userid,counting,createdby,createdon,twitterhandle,sewadate,comments,sewapoints)values(@subcategoryid,@userid,@counting,@createdby,@createdon,@twitterhandle,@sewadate,@comments,@sewapoints)";
            MySqlParameter subcategoryidP = new MySqlParameter("@subcategoryid", SubCategoryId);
            MySqlParameter useridP = new MySqlParameter("@userid", CreatedBy);
            MySqlParameter countingP = new MySqlParameter("@counting", Counting);
            MySqlParameter createdbyP = new MySqlParameter("@createdby", CreatedBy);
            MySqlParameter twitterhandleP = new MySqlParameter("@twitterhandle", TwitterHandle);
            MySqlParameter sewadateP = new MySqlParameter("@sewadate", SewaDate);
            MySqlParameter commentsP = new MySqlParameter("@comments", Comments);
            MySqlParameter sewapointsP = new MySqlParameter("@sewapoints", SewaPoints);
            MySqlParameter createdonP = new MySqlParameter("@createdon", CreatedOn);
            MySqlParameter[] p = { subcategoryidP, useridP, countingP, createdbyP, twitterhandleP, sewadateP, commentsP, sewapointsP, createdonP };
            int r = MySqlHelper.ExecuteNonQuery(common.GetConnectionString(), qry, p);
            if (r > 0)
            {
                return true;
            }
            else
            {
                Exception ex = new Exception("Save Report Details Error!!");
                throw ex;
            }
        }
        catch 
        {
            Exception ex = new Exception("Save Report Details Error!!");
            throw ex;
        }
    }
    public DataRow CheckReportSubmitGivenDate()
    {
        try
        {
            //bool result = false;
            string qry = "select socialname,subname,sewadate from tbreport rep left join tbsubsocial subso on rep.subcategoryid=subso.subsocialidpk left join tbsocial social on subso.subid=social.socialid where rep.subcategoryid=@id and rep.userid=@userid and rep.sewadate>=@startdate and rep.sewadate<=@enddate";
            MySqlParameter subcategoryidP = new MySqlParameter("@id", SubCategoryId);
            MySqlParameter useridP = new MySqlParameter("@userid", CreatedBy);
            MySqlParameter sewadateP = new MySqlParameter("@startdate", SewaDate);
            MySqlParameter enddateP = new MySqlParameter("@enddate", SewaDate.AddDays(1).AddSeconds(-1));
            MySqlParameter[] p = { subcategoryidP, useridP, sewadateP, enddateP };
            DataRow dr = MySqlHelper.ExecuteDataRow(common.GetConnectionString(), qry, p);
            return dr;
        }
       catch
        {
            Exception ex = new Exception("Check Report Submit for Given Date Error!!");
            throw ex;
        }
    }
    public bool CheckForDuplicate(string tablename, string colname,string colvalue)
    {
        bool result = false;
        try
        {
            string qry = "select " + colname + " from " + tablename + " where " + colname + "='" + colvalue + "'";
            DataRow dr = MySqlHelper.ExecuteDataRow(common.GetConnectionString(), qry);
            if (dr != null)
            {
                result = true;
                
            }
            return result;
        }
        catch
        {
            Exception ex = new Exception("Error In Check For Duplicate!");
            throw ex;
        }
    }
    public DataTable GetUserData(string countryid, string stateid, string districtid, string blockid, string email, string status, string TextName, string TextTwitterName, string mobileno, string whatsappmobile, string insanno)
    {
        try
        {
            MySqlParameter countryidP = new MySqlParameter();
            MySqlParameter stateidP = new MySqlParameter();
            MySqlParameter districtidP = new MySqlParameter();
            MySqlParameter blockidP = new MySqlParameter();
            MySqlParameter mobilenoP = new MySqlParameter();
            MySqlParameter whatsappmobileP = new MySqlParameter();
            MySqlParameter emailP = new MySqlParameter();
            MySqlParameter TWNameP = new MySqlParameter();
            MySqlParameter UserNameP = new MySqlParameter();
            MySqlParameter genderP = new MySqlParameter();
            MySqlParameter insannoP = new MySqlParameter();
            //left join tblog log on usr.usrid=log.LOGIN_USER_ID
            string qry = "select * from tbusr usr left join tbcountry cou on usr.usrcouid=cou.countryid left join tbstate sta on usr.usrstaid=sta.stateid left join tbdistrict dst on usr.usrdstid=dst.districtid left join tbblock blk on usr.usrblkid=blk.blockid where 1=1";
            //**********************Gender Condition********************************
            //if (!Global.IsAdmin)
            //{
            //    qry += " and usr.usrgen=@Gender";
            //    genderP = new MySqlParameter("@Gender", Global.GenderData);
            //}
            
            //**********************************************************************
            if (countryid != "")
            {
                qry += " and usr.usrcouid=@countryid";
                countryidP = new MySqlParameter("@countryid", countryid);
            }
            if (stateid != "")
            {
                qry += " and usr.usrstaid=@stateid";
                stateidP = new MySqlParameter("@stateid", stateid);
            }
            if (districtid != "")
            {
                qry += " and usr.usrdstid=@districtid";
                districtidP = new MySqlParameter("@districtid", districtid);
            }
            if (blockid != "")
            {
                qry += " and usr.usrblkid=@blockid";
                blockidP = new MySqlParameter("@blockid", blockid);
            }
            if (mobileno != "")
            {
                qry += " and usr.usrmob=@mobileno";
                mobilenoP = new MySqlParameter("@mobileno", mobileno);
            }
            if (whatsappmobile != "")
            {
                qry += " and usr.wmobno=@whatsappmobile";
                whatsappmobileP = new MySqlParameter("@whatsappmobile", whatsappmobile);
            }
            if (insanno != "")
            {
                qry += " and usr.usrinsnum=@insanno";
                insannoP = new MySqlParameter("@insanno", insanno);
            }
            if (email != "")
            {
                qry += " and usr.usrpriemail like @email";
                emailP = new MySqlParameter("@email", email + "%");
            }
            if (status != "All")
            {
                qry += " and usr.isapproved='" + status + "'";
            }
            if (!string.IsNullOrEmpty(TextName))
            {
                qry += " and usr.usrpriemail like @UserName";
                UserNameP = new MySqlParameter("@UserName", "%" + TextName + "%");
            }
            if (!string.IsNullOrEmpty(TextTwitterName))
            {
                qry += " and usr.twhandle=@twname";
                TWNameP = new MySqlParameter("@twname", TextTwitterName);
            }
            qry += " group by usr.usrid";//order by log.LOGIN_TIME desc
            MySqlParameter[] p = { countryidP, stateidP, districtidP, blockidP, UserNameP, TWNameP, mobilenoP, emailP, insannoP, whatsappmobileP, genderP };
            DataTable dt = MySqlHelper.ExecuteDataset(common.GetConnectionString(), qry, p).Tables[0];
            return dt;
        }
        catch
        {
            Exception ex = new Exception("Get User Details Error:");
            throw ex;
        }
    }
    public static string GenerateRandomString(int length)
    {
        //Removed O, o, 0, l, 1
        string allowedLetterChars = "abcdefghijkmnpqrstuvwxyzABCDEFGHJKLMNPQRSTUVWXYZ";
        string allowedNumberChars = "23456789";
        char[] chars = new char[length];
        Random rd = new Random();
        bool useLetter = true;
        for (int i = 0; i < length; i++)
        {
            if (useLetter)
            {
                chars[i] = allowedLetterChars[rd.Next(0, allowedLetterChars.Length)];
                useLetter = false;
            }
            else
            {
                chars[i] = allowedNumberChars[rd.Next(0, allowedNumberChars.Length)];
                useLetter = true;
            }
        }
        return new string(chars);
    }
    public bool ApproveUser()
    {
        try
        {
            RandomPassword = GenerateRandomString(10);
            //DBMD5Password = common.CreateMD5Hash(RandomPassword, common.GetPasswordSalt());
            //refstamemberid=@refstamemberid
            string qry = "update tbusr set refstamemberid=@refstamemberid,refcomments=@refcomments,refdmname=@refdmname,refdmmobno=@refdmmobno,wgrpnam=@wgrpnam,usrpwd=@usrpwd,isapproved=@isapproved,modby=@modby,modon=now() where usrid=@usrid";
            MySqlParameter refstamemberidP = new MySqlParameter("@refstamemberid", AssignedSMID);
            MySqlParameter refcommentsP = new MySqlParameter("@refcomments", AssignedSMComments);
            MySqlParameter refdmnameP = new MySqlParameter("@refdmname", AssignedDMName);
            MySqlParameter usridP = new MySqlParameter("@usrid", UserId);
            MySqlParameter refdmmobnoP = new MySqlParameter("@refdmmobno", AssignedDmContactNo);
            MySqlParameter wgrpnamP = new MySqlParameter("@wgrpnam", WhatsGroupName);
            MySqlParameter isapprovedP = new MySqlParameter("@isapproved", true);
            MySqlParameter usrpwdP = new MySqlParameter("@usrpwd", DBMD5Password);
            MySqlParameter modbyP = new MySqlParameter("@modby", ModifiedBy);
            MySqlParameter[] p = { refstamemberidP, refcommentsP, refdmnameP, refdmmobnoP, wgrpnamP, usridP, isapprovedP, usrpwdP, modbyP };
            int r = MySqlHelper.ExecuteNonQuery(common.GetConnectionString(), qry, p);
            if (r > 0)
            {
                qry = "insert into assignedsmlist(smusrid,assigneduserid)values(@refstamemberid,@usrid)";
                MySqlHelper.ExecuteNonQuery(common.GetConnectionString(), qry, p);
                //qry = "select * from tbusr where usrid=@usrid";
                //DataRow dr = MySqlHelper.ExecuteDataRow(common.GetConnectionString(), qry, usridP);
                //if (common.GetMailSendConfirmation() == "1")
                //{
                //    ExceptionHandlingClass.SendUserApproveMail(Name, PrimaryEmailId, RandomPassword);
                //}
                return true;
            }
            else
            {
                return false;
            }
        }
        catch
        {
            Exception ex = new Exception("Error in Approve User!!");
            throw ex;
        }
    }
    //public void SendPasswordMailAgain(string userid)
    //{
    //    try
    //    {
    //        RandomPassword = GenerateRandomString(10);
    //        DBMD5Password = common.CreateMD5Hash(RandomPassword, common.GetPasswordSalt());
    //        string qry = "update tbusr set usrpwd=@usrpwd where usrid=@usrid";
    //        MySqlParameter usrpwdP = new MySqlParameter("@usrpwd", DBMD5Password);
    //        MySqlParameter usridP = new MySqlParameter("@usrid", userid);
    //        MySqlParameter[] p = { usrpwdP, usridP };
    //        int r = MySqlHelper.ExecuteNonQuery(common.GetConnectionString(), qry, p);
    //        if (r > 0)
    //        {
    //            qry = "select usrname,usrpriemail from tbusr where usrid=" + userid;
    //            DataRow dr = MySqlHelper.ExecuteDataRow(common.GetConnectionString(), qry);
    //            Name = dr["usrname"].ToString();
    //            PrimaryEmailId = dr["usrpriemail"].ToString();
    //            if (common.GetMailSendConfirmation() == "1")
    //            {
    //                ExceptionHandlingClass.SendUserApproveMail(Name, PrimaryEmailId, RandomPassword);
    //            }
    //        }
           
    //    }
    //    catch
    //    {
    //        Exception ex = new Exception("Send User Password Error!!");
    //        throw ex;
    //    }
    //}

    public bool ApprovalStatus(int userid,bool status)
    {
        bool result;
        try
        {
            string qry = "select * from tbusr where isapproved=@isapproved";
            MySqlParameter isapprovedP = new MySqlParameter("@isapproved", status);
            result = Convert.ToBoolean(MySqlHelper.ExecuteScalar(common.GetConnectionString(), qry, isapprovedP));
            return result;
        }
        catch
        {
            Exception ex = new Exception("Error in Check Approve Status!!");
            throw ex;
        }
    }
   
   
    public DataRow AuthenticateUser(string username, string password)
    {
        try
        {//isauthorized
            string qry = "select usrname,usrcouid,usrstaid,usrdstid,usrblkid,isapproved,usrid,usrpriemail,usrcategory,twhandle,usrgen from tbusr where usrpwd=@usrpwd and usrpriemail=@usrpriemail and isapproved=@isapproved limit 1";
            MySqlParameter usrnamP = new MySqlParameter("@usrpriemail", username);//useremail
            //MySqlParameter userpwdP = new MySqlParameter("@usrpwd", common.CreateMD5Hash(password, common.GetPasswordSalt()));//userpwd
            MySqlParameter userpwdP = new MySqlParameter("@usrpwd", password);
            MySqlParameter isapprovedP = new MySqlParameter("@isapproved", true);//IsApproved
            MySqlParameter[] p = { usrnamP, userpwdP, isapprovedP };
            DataRow dr = MySqlHelper.ExecuteDataRow(common.GetConnectionString(), qry, p);
            return dr;
           
        }
        catch
        {
            Exception ex = new Exception("Error in Authenticate User!!");
            throw ex;
        }
    }
   
    public string GetTodaySewaPonits(string userid)
    {
        string total = "0";
        try
        {
            string qry = "select sum(sewapoints) from tbreport where sewadate BETWEEN CURDATE() AND DATE_ADD(CURDATE(), INTERVAL 1 day) and userid=" + userid;
            total = MySqlHelper.ExecuteScalar(common.GetConnectionString(), qry).ToString();
            return total;
        }
        catch
        {
            return total;
        }
    }
    public string GetTotalAlltimeSewaPonits(string userid)
    {
        string total = "0";
        try
        {
            string qry = "select sum(sewapoints) from tbreport where userid=" + userid;
            total = MySqlHelper.ExecuteScalar(common.GetConnectionString(), qry).ToString();
            return total;
        }
        catch
        {
            return total;
        }
    }

    public string MemberReferred(string userid)
    {
        string count = "";
        try
        {
            string qry = "select count(*) from assignedsmlist where smusrid=" + userid;
            count = MySqlHelper.ExecuteScalar(common.GetConnectionString(), qry).ToString();
            return count;
        }
        catch
        {
            return count;
        }
    }
    public DataRow GetUserData(int userid)
    {
        try
        {
            string qry = "select * from tbusr usr left join tbcountry cou on usr.usrcouid=cou.countryid left join tbstate sta on usr.usrstaid=sta.stateid left join tbdistrict dst on usr.usrdstid=dst.districtid left join tbblock blk on usr.usrblkid=blk.blockid left join tbqualification qua on usr.usreduid=qua.qualificationid left join tbprofession prof on usr.usrprofessionid=prof.professionid left join tbstamember sm on usr.refstamemberid=sm.stamemberid left join tbcategory cate on usr.usrcategory=cate.categoryid where usr.usrid=@userid";
            MySqlParameter useridP = new MySqlParameter("@userid", userid);
            MySqlParameter[] p = { useridP };
            DataRow dr = MySqlHelper.ExecuteDataRow(common.GetConnectionString(), qry, p);
            return dr;
        }
        catch
        {
            Exception ex = new Exception("Get User Details Error!");
            throw ex;
        }
    }
    public string GetUserColumnByID(string columnname, string userid)
    {
        string result = "";
        try
        {
            string qry = "select " + columnname + " from tbusr where usrid=" + userid;
            return result = MySqlHelper.ExecuteScalar(common.GetConnectionString(), qry).ToString();
        }
        catch
        {
            return result;
        }
    }
    public DataTable GetReportData(string counntryid,string stateid,string districtid,string blockid,string categoryid,string subcategoryid,string startdate,string enddate,string createdby,string referredsm,string name,string mobileno)
    {
        try
        {
            //left join tbqualification qua on usr.usreduid=qua.qualificationid left join tbprofession prof on usr.usrprofessionid=prof.professionid
            string qry = "select countryname,statename,districtname,blockname,socialname,subname,counting,twitterhandle,comments,sewadate,usrname,usrmob,membername,rep.createdon,sewapoints,reportid from tbreport rep left join tbusr usr on rep.userid=usr.usrid left join tbsubsocial subso on rep.subcategoryid=subso.subsocialidpk left join tbsocial social on subso.subid=social.socialid left join tbcountry cou on usr.usrcouid=cou.countryid left join tbstate sta on usr.usrstaid=sta.stateid left join tbdistrict dst on usr.usrdstid=dst.districtid left join tbblock blk on usr.usrblkid=blk.blockid left join tbstamember stamem on usr.refstamemberid=stamem.stamemberid where 1=1";
            MySqlParameter countryidP, stateidP, districtidP, blockidP, categoryidP, subcategoryidP, startdateP, enddateP, createdbyP, refstamemberidP, nameP, mobilenoP, genderP;
            MySqlParameter[] p = { };
            if (counntryid != "")
            {
                qry += " and cou.countryid=@countryid";
                Array.Resize<MySqlParameter>(ref p, p.Length + 1);
                countryidP = new MySqlParameter("@countryid", counntryid);
                p[p.Length - 1] = countryidP;
            }
            if (stateid != "")
            {
                qry += " and sta.stateid=@stateid";
                Array.Resize<MySqlParameter>(ref p, p.Length + 1);
                stateidP = new MySqlParameter("@stateid", stateid);
                p[p.Length - 1] = stateidP;
            }
            if (districtid != "")
            {
                qry += " and dst.districtid=@districtid";
                Array.Resize<MySqlParameter>(ref p, p.Length + 1);
                districtidP = new MySqlParameter("@districtid", districtid);
                p[p.Length - 1] = districtidP;
            }
            if (blockid != "")
            {
                qry += " and blk.blockid=@blockid";
                Array.Resize<MySqlParameter>(ref p, p.Length + 1);
                blockidP = new MySqlParameter("@blockid", blockid);
                p[p.Length - 1] = blockidP;
            }
            if (categoryid != "")
            {
                qry += " and social.socialid=@socialid";
                Array.Resize<MySqlParameter>(ref p, p.Length + 1);
                categoryidP = new MySqlParameter("@socialid", categoryid);
                p[p.Length - 1] = categoryidP;
            }
            if (subcategoryid != "")
            {
                qry += " and rep.subcategoryid=@subcategoryid";
                Array.Resize<MySqlParameter>(ref p, p.Length + 1);
                subcategoryidP = new MySqlParameter("@subcategoryid", subcategoryid);
                p[p.Length - 1] = subcategoryidP;
            }
            if (startdate != "")
            {
                DateTime dt = Convert.ToDateTime(startdate);
                qry += " and rep.createdon>=@startdate";
                Array.Resize<MySqlParameter>(ref p, p.Length + 1);
                startdateP = new MySqlParameter("@startdate", dt);
                p[p.Length - 1] = startdateP;
            }
            if (enddate != "")
            {
                DateTime dt = Convert.ToDateTime(enddate);
                qry += " and rep.createdon<=@enddate";
                Array.Resize<MySqlParameter>(ref p, p.Length + 1);
                enddateP = new MySqlParameter("@enddate", dt.AddDays(1).AddSeconds(-1));
                p[p.Length - 1] = enddateP;
            }
            if (createdby != "")
            {
                qry += " and rep.createdby=@createdby";
                Array.Resize<MySqlParameter>(ref p, p.Length + 1);
                createdbyP = new MySqlParameter("@createdby", createdby);
                p[p.Length - 1] = createdbyP;
            }
            if (referredsm != "")
            {
                //
                qry += " and usr.refstamemberid=@refstamemberid";
                Array.Resize<MySqlParameter>(ref p, p.Length + 1);
                refstamemberidP = new MySqlParameter("@refstamemberid", referredsm);
                p[p.Length - 1] = refstamemberidP;
            }
            if (name != "")
            {
                qry += " and usr.usrname like @username";
                Array.Resize<MySqlParameter>(ref p, p.Length + 1);
                nameP = new MySqlParameter("@username", name + "%");
                p[p.Length - 1] = nameP;
            }
            if (mobileno != "")
            {
                qry += " and usr.usrmob=@usrmob";
                Array.Resize<MySqlParameter>(ref p, p.Length + 1);
                mobilenoP = new MySqlParameter("@usrmob", mobileno);
                p[p.Length - 1] = mobilenoP;
            }
            //if (!Global.IsAdmin)
            //{
            //    qry += " and usr.usrgen=@Gender";
            //    Array.Resize<MySqlParameter>(ref p, p.Length + 1);
            //    genderP = new MySqlParameter("@Gender", Global.GenderData);
            //    p[p.Length - 1] = genderP;
            //}
            DataTable row = MySqlHelper.ExecuteDataset(common.GetConnectionString(), qry, p).Tables[0];
            return row;
        }
        catch
        {
            Exception ex = new Exception("Get Report Error!!");
            throw ex;
        }
    }
    public DataTable GetSubSocial(int socialid)
    {
        try
        {
            string qry = "select * from tbsubsocial where subid=" + socialid;
            return MySqlHelper.ExecuteDataset(common.GetConnectionString(), qry).Tables[0];
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataTable GetAllState()
    {
        try
        {
            //stateid=2 and statecountryid=" + countryid + " and 
            string qry = "select * from tbstate where staactsts=1 order by statename"; ;
            return MySqlHelper.ExecuteDataset(common.GetConnectionString(), qry).Tables[0];
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataTable GetAllStateMember()
    {
        try
        {
            //stateid=2 and statecountryid=" + countryid + " and 
            string qry = "select * from tbstamember where isactive=1 order by membername";
            return MySqlHelper.ExecuteDataset(common.GetConnectionString(), qry).Tables[0];
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataTable GetAllDistrict(int stateid)
    {
        try
        {
            //districtid=32
            string qry = "select * from tbdistrict where districtstateid=" + stateid + " and dstactsts=1 order by districtname";
            return MySqlHelper.ExecuteDataset(common.GetConnectionString(), qry).Tables[0];
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataTable GetAllBlock(int districtid)
    {
        try
        {
            //districtid=32
            string qry = "select * from tbblock where blockdistrictid=" + districtid + " and blkactsts=1 order by blockname";
            return MySqlHelper.ExecuteDataset(common.GetConnectionString(), qry).Tables[0];
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataRow CheckEmailForForgetPwd(string emailid)
    {
        //bool result = false;
        try
        {
            string qry = "select usrpriemail,isapproved,ispwdchanged from tbusr where isapproved=1 and usrpriemail=@usrpriemail and ispwdchanged=@changed";
            MySqlParameter changedP = new MySqlParameter("@changed", false);
            MySqlParameter usrpriemailP = new MySqlParameter("@usrpriemail", emailid);
            MySqlParameter[] p = { changedP, usrpriemailP };
            DataRow dr = MySqlHelper.ExecuteDataRow(common.GetConnectionString(), qry, p);
            return dr;
        }
        catch
        {
            Exception ex = new Exception("Error In Check Email ID");
            throw ex;
        }

    }
    public bool CheckEmailAndCodeForgetPwd(string emailid,string code)
    {
        bool result = false;
        try
        {
            string qry = "select usrpriemail from tbusr where isapproved=1 and usrpriemail=@usrpriemail and ispwdchanged=@changed and pwdcode=@code";
            MySqlParameter pwdcodeP = new MySqlParameter("@code", code);
            MySqlParameter changedP = new MySqlParameter("@changed", true);
            MySqlParameter usrpriemailP = new MySqlParameter("@usrpriemail", emailid);
            MySqlParameter[] p = { changedP, usrpriemailP, pwdcodeP };
            DataRow dr = MySqlHelper.ExecuteDataRow(common.GetConnectionString(), qry, p);
            if (dr != null)
            {
                result = true;
            }
            return result;
        }
        catch
        {
            Exception ex = new Exception("Error In Check Email And Code ForgetPwd");
            throw ex;
        }

    }
    public bool UpdateUserForgotPwd(string emailid,string code,string password)
    {
        try
        {
            string qry = "update tbusr set usrpwd=@usrpwd,ispwdchanged=@changed,pwdcode='',lastforgetpwd=now() where usrpriemail=@usrpriemail and pwdcode=@pwdcode";
          //  MySqlParameter usrpwdP = new MySqlParameter("@usrpwd", common.CreateMD5Hash(password, common.GetPasswordSalt()));
            MySqlParameter pwdcodeP = new MySqlParameter("@pwdcode", code);
            MySqlParameter changedP = new MySqlParameter("@changed", false);
            MySqlParameter usrpriemailP = new MySqlParameter("@usrpriemail", emailid);
            MySqlParameter[] p = {  pwdcodeP, changedP, usrpriemailP };
            int r = MySqlHelper.ExecuteNonQuery(common.GetConnectionString(), qry, p);
            if (r > 0)
            {
                return true;
            }
            else
            {
                Exception ex = new Exception("Error In Update User ForgotPwd");
                throw ex;
            }
        }
        catch
        {
            Exception ex = new Exception("Error In Update User ForgotPwd");
            throw ex;
        }
    }
    public bool UpdateCodeResetPwd(string code, string emailid)
    {
        try
        {
            //update tb_employee_with_code set code=@code where  email=@email or uname=@uname
            string qry = "update tbusr set pwdcode=@code,ispwdchanged=@changed where usrpriemail=@usrpriemail";
            MySqlParameter pwdocdeP = new MySqlParameter("@code", code);
            MySqlParameter ispwdchangedP = new MySqlParameter("@changed",true);
            MySqlParameter usrpriemailP = new MySqlParameter("@usrpriemail", emailid);
            MySqlParameter[] p = { pwdocdeP, ispwdchangedP, usrpriemailP };
            int r = MySqlHelper.ExecuteNonQuery(common.GetConnectionString(), qry, p);
            if (r > 0)
            {
                return true;
            }
            else
            {
                Exception ex = new Exception("Error In Update Code Reset Pwd");
                throw ex;
            }

        }
        catch
        {
            Exception ex = new Exception("Error In Update Code Reset Pwd");
            throw ex;
        }
    }
    
    public DataTable GetReportCategoryWise(string stateid, string districtid, string blockid, string categoryid, string startdate, string enddate,string memberid)
    {
        try
        {
            string qry = "select sum(rep.counting) as count,rep.subcategoryid,usr.usrstaid,usr.usrdstid,usr.usrblkid,usr.refstamemberid,sm.membername,sta.statename,dst.districtname,blk.blockname from tbreport rep left join tbusr usr on rep.createdby=usr.usrid left join tbstate sta on usr.usrstaid=sta.stateid left join tbdistrict dst on usr.usrdstid=dst.districtid left join tbblock blk on usr.usrblkid=blk.blockid left join tbsubsocial subso on rep.subcategoryid=subso.subsocialidpk left join tbstamember sm on usr.refstamemberid=sm.stamemberid where 1=1";
            MySqlParameter stateidP, districtidP, blockidP, categoryidP, memberidP, startdateP, enddateP, createdbyP, genderP;
            MySqlParameter[] p = { };
            if (stateid != "")
            {
                qry += " and usr.usrstaid=@stateid";
                Array.Resize<MySqlParameter>(ref p, p.Length + 1);
                stateidP = new MySqlParameter("@stateid", stateid);
                p[p.Length - 1] = stateidP;
            }
            if (districtid != "")
            {
                qry += " and usr.usrdstid=@districtid";
                Array.Resize<MySqlParameter>(ref p, p.Length + 1);
                districtidP = new MySqlParameter("@districtid", districtid);
                p[p.Length - 1] = districtidP;
            }
            if (blockid != "")
            {
                qry += " and usr.usrblkid=@blockid";
                Array.Resize<MySqlParameter>(ref p, p.Length + 1);
                blockidP = new MySqlParameter("@blockid", blockid);
                p[p.Length - 1] = blockidP;
            }
            if (categoryid != "")
            {
                qry += " and subso.subid=@socialid";
                Array.Resize<MySqlParameter>(ref p, p.Length + 1);
                categoryidP = new MySqlParameter("@socialid", categoryid);
                p[p.Length - 1] = categoryidP;
            }
            if (memberid != "")
            {
                qry += " and usr.refstamemberid=@refstamemberid";
                Array.Resize<MySqlParameter>(ref p, p.Length + 1);
                memberidP = new MySqlParameter("@refstamemberid", memberid);
                p[p.Length - 1] = memberidP;
            }
            if (startdate != "")
            {
                DateTime dt = Convert.ToDateTime(startdate);
                qry += " and rep.sewadate>=@startdate";
                Array.Resize<MySqlParameter>(ref p, p.Length + 1);
                startdateP = new MySqlParameter("@startdate", dt);
                p[p.Length - 1] = startdateP;
            }
            if (enddate != "")
            {
                DateTime dt = Convert.ToDateTime(enddate);
                qry += " and rep.sewadate<=@enddate";
                Array.Resize<MySqlParameter>(ref p, p.Length + 1);
                enddateP = new MySqlParameter("@enddate", dt.AddDays(1).AddSeconds(-1));
                p[p.Length - 1] = enddateP;
            }
          
            qry += " group by rep.subcategoryid";
            DataTable row = MySqlHelper.ExecuteDataset(common.GetConnectionString(), qry, p).Tables[0];
            return row;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
   
    
   
    public DataTable GetSubCategoryRows(int socialid)
    {
        try
        {
            string qry = "select * from tbsubsocial where subid=@subid and isactive=1";
            MySqlParameter subidP = new MySqlParameter("@subid", socialid);
            DataTable dt = MySqlHelper.ExecuteDataset(common.GetConnectionString(), qry, subidP).Tables[0];
            int count = dt.Rows.Count;
            return dt;
        }
        catch
        {
            Exception ex = new Exception("Get Sub Category Rows");
            throw ex;
        }
    }
    public void DeleteReportRow(int reportid)
    {
        try
        {
            string qry = "delete from tbreport where reportid=" + reportid;
            MySqlHelper.ExecuteNonQuery(common.GetConnectionString(), qry);
        }
        catch
        {
            Exception ex = new Exception("Delete Report Error!!");
            throw ex;
        }

    }

}