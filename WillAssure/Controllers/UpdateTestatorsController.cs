﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WillAssure.Models;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Net.Mail;
using System.Net;

namespace WillAssure.Controllers
{
    public class UpdateTestatorsController : Controller
    {
        public static string connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        SqlConnection con = new SqlConnection(connectionString);


        // GET: UpdateTestators
        public ActionResult UpdateTestatorsIndex(int NestId)
        {
            ViewBag.collapse = "true";
            ViewBag.cod = "true";



            //if (NestId == 0)
            //{
            int getid = Convert.ToInt32(Session["uuid"]);
            con.Open();
            string qq26 = "select tId from TestatorDetails where uId = " + Convert.ToInt32(Session["uuid"]) + " ";
            SqlDataAdapter daa26 = new SqlDataAdapter(qq26, con);
            DataTable dtt26 = new DataTable();
            daa26.Fill(dtt26);
            if (dtt26.Rows.Count > 0)
            {
                NestId = Convert.ToInt32(dtt26.Rows[0]["tId"]);
            }
            con.Close();
            //}

            // check type 
            string typ = "";
            con.Open();
            string qq1 = "select Type from users where uId = " + Convert.ToInt32(Session["uuid"]) + " ";
            SqlDataAdapter daa = new SqlDataAdapter(qq1, con);
            DataTable dtt = new DataTable();
            daa.Fill(dtt);
            con.Close();

            if (dtt.Rows.Count > 0)
            {
                typ = dtt.Rows[0]["Type"].ToString();
            }



            //end



            if (typ == "Testator")
            {


                con.Open();
                string qq12 = "select Type from users where uId = " + Convert.ToInt32(Session["uuid"]) + " and designation = 1 ";
                SqlDataAdapter da42 = new SqlDataAdapter(qq12, con);
                DataTable d4t2 = new DataTable();
                da42.Fill(d4t2);
                con.Close();

                if (d4t2.Rows.Count > 0)
                {
                    ViewBag.documentlink = "true";
                }


                // check will status
                con.Open();
                string qry1 = "select Will  from users where Will = 1 ";
                SqlDataAdapter daa1 = new SqlDataAdapter(qry1, con);
                DataTable dtt1 = new DataTable();
                daa1.Fill(dtt1);
                if (dtt1.Rows.Count > 0)
                {
                    ViewBag.documentbtn1 = "true";
                }
                con.Close();
                //end


                // check codocil status
                con.Open();
                string qry2 = "select Codocil  from users where Codocil = 1 ";
                SqlDataAdapter daa2 = new SqlDataAdapter(qry2, con);
                DataTable dtt2 = new DataTable();
                daa2.Fill(dtt2);
                if (dtt2.Rows.Count > 0)
                {
                    ViewBag.documentbtn2 = "true";
                }
                con.Close();

                //end


                // check Poa status
                con.Open();
                string qry4 = "select POA  from users where POA = 1 ";
                SqlDataAdapter daa4 = new SqlDataAdapter(qry4, con);
                DataTable dtt4 = new DataTable();
                daa4.Fill(dtt4);
                if (dtt4.Rows.Count > 0)
                {
                    ViewBag.documentbtn3 = "true";
                }
                con.Close();
                //end


                // check gift deeds status
                con.Open();
                string qry3 = "select Giftdeeds  from users where Giftdeeds = 1 ";
                SqlDataAdapter daa3 = new SqlDataAdapter(qry3, con);
                DataTable dtt3 = new DataTable();
                daa3.Fill(dtt3);
                if (dtt3.Rows.Count > 0)
                {
                    ViewBag.documentbtn4 = "true";
                }
                con.Close();
                //end


            }
            else
            {

                ViewBag.showtitle = "true";
                ViewBag.documentlink = "true";

            }






            if (Session["rId"] == null || Session["uuid"] == null)
            {

                RedirectToAction("LoginPageIndex", "LoginPage");

            }
            //if (Session["tid"]== null)
            //{
            //    ViewBag.message = "link";
            //}


            List<LoginModel> Lmlist = new List<LoginModel>();
            con.Open();
            string q = "select * from Assignment_Roles where RoleId = " + Convert.ToInt32(Session["rId"]) + "";
            SqlDataAdapter da3 = new SqlDataAdapter(q, con);
            DataTable dt3 = new DataTable();
            da3.Fill(dt3);
            if (dt3.Rows.Count > 0)
            {

                for (int i = 0; i < dt3.Rows.Count; i++)
                {
                    LoginModel lm = new LoginModel();
                    lm.PageName = dt3.Rows[i]["PageName"].ToString();
                    lm.PageStatus = dt3.Rows[i]["PageStatus"].ToString();
                    lm.Action = dt3.Rows[i]["Action"].ToString();
                    lm.Nav1 = dt3.Rows[i]["Nav1"].ToString();
                    lm.Nav2 = dt3.Rows[i]["Nav2"].ToString();

                    Lmlist.Add(lm);
                }



                ViewBag.PageName = Lmlist;




            }

            con.Close();
            TestatorFormModel TFM = new TestatorFormModel();

            con.Open();
            string query = "select * from TestatorDetails where tId = '" + NestId + "' ";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();


            if (dt.Rows.Count > 0)
            {
                TFM.tId = NestId;


                TFM.First_Name = dt.Rows[0]["First_Name"].ToString();
                if (Session["Type"] != null)
                {
                    if (Session["Type"].ToString() == "DistributorAdmin" || Session["Type"].ToString() == "SuperAdmin" || Session["Type"].ToString() == "Testator")
                    {
                        Session["distid"] = NestId;
                        Session["disttestator"] = TFM.First_Name;
                    }
                }
                else
                {
                    RedirectToAction("LoginPageIndex", "LoginPage");
                }

                TFM.Last_Name = dt.Rows[0]["Last_Name"].ToString();
                TFM.Dob = Convert.ToDateTime(dt.Rows[0]["DOB"]).ToString("dd-MM-yyyy");
                TFM.Middle_Name = dt.Rows[0]["Middle_Name"].ToString();


                TFM.Occupation = dt.Rows[0]["Occupation"].ToString();
                TFM.Mobile = dt.Rows[0]["Mobile"].ToString();
                TFM.Email = dt.Rows[0]["Email"].ToString();
                TFM.material_status_txt = dt.Rows[0]["maritalStatus"].ToString();
                TFM.Religiontext = dt.Rows[0]["Religion"].ToString();
                TFM.RelationshipTxt = dt.Rows[0]["RelationShip"].ToString();
                TFM.Identity_Proof = dt.Rows[0]["Identity_Proof"].ToString();
                TFM.Identity_proof_Value = dt.Rows[0]["Identity_proof_Value"].ToString();
                TFM.Alt_Identity_Proof = dt.Rows[0]["Alt_Identity_Proof"].ToString();
                TFM.Alt_Identity_proof_Value = dt.Rows[0]["Alt_Identity_proof_Value"].ToString();
                TFM.Gendertext = dt.Rows[0]["Gender"].ToString();
                TFM.Address1 = dt.Rows[0]["Address1"].ToString();
                TFM.Address2 = dt.Rows[0]["Address2"].ToString();
                TFM.Address3 = dt.Rows[0]["Address3"].ToString();
                TFM.citytext = dt.Rows[0]["City"].ToString();
                TFM.statetext = dt.Rows[0]["State"].ToString();
                TFM.countrytext = dt.Rows[0]["Country"].ToString();
                TFM.Pin = dt.Rows[0]["Pin"].ToString();
                TFM.active = dt.Rows[0]["active"].ToString();
                TFM.uId = Convert.ToInt32(dt.Rows[0]["uId"]);


                TFM.tempemailotp = dt.Rows[0]["Email_OTP"].ToString();
                TFM.tempmobileotp = dt.Rows[0]["Mobile_OTP"].ToString();




            }


            return View("~/Views/UpdateTestators/UpdateTestatorPageContent.cshtml", TFM);
        }


        public ActionResult UpdatingTestatorFormData(TestatorFormModel TFM)
        {
            ViewBag.collapse = "true";

            // check type 
            string typ5 = "";
            con.Open();
            string qq15 = "select Type from users where uId = " + Convert.ToInt32(Session["uuid"]) + " ";
            SqlDataAdapter daa5 = new SqlDataAdapter(qq15, con);
            DataTable dtt5 = new DataTable();
            daa5.Fill(dtt5);
            con.Close();

            if (dtt5.Rows.Count > 0)
            {
                typ5 = dtt5.Rows[0]["Type"].ToString();
            }



            //end



            if (typ5 == "Testator")
            {
                // check will status
                con.Open();
                string qry1 = "select Will  from users where Will = 1 ";
                SqlDataAdapter daa1 = new SqlDataAdapter(qry1, con);
                DataTable dtt1 = new DataTable();
                daa1.Fill(dtt1);
                if (dtt1.Rows.Count > 0)
                {
                    ViewBag.documentbtn1 = "true";
                }
                con.Close();
                //end


                // check codocil status
                con.Open();
                string qry2 = "select Codocil  from users where Codocil = 1 ";
                SqlDataAdapter daa2 = new SqlDataAdapter(qry2, con);
                DataTable dtt2 = new DataTable();
                daa2.Fill(dtt2);
                if (dtt2.Rows.Count > 0)
                {
                    ViewBag.documentbtn2 = "true";
                }
                con.Close();

                //end


                // check Poa status
                con.Open();
                string qry4 = "select POA  from users where POA = 1 ";
                SqlDataAdapter daa4 = new SqlDataAdapter(qry4, con);
                DataTable dtt4 = new DataTable();
                daa4.Fill(dtt4);
                if (dtt4.Rows.Count > 0)
                {
                    ViewBag.documentbtn3 = "true";
                }
                con.Close();
                //end


                // check gift deeds status
                con.Open();
                string qry3 = "select Giftdeeds  from users where Giftdeeds = 1 ";
                SqlDataAdapter daa3 = new SqlDataAdapter(qry3, con);
                DataTable dtt3 = new DataTable();
                daa3.Fill(dtt3);
                if (dtt3.Rows.Count > 0)
                {
                    ViewBag.documentbtn4 = "true";
                }
                con.Close();
                //end
            }
            else
            {
                ViewBag.showtitle = "true";
                ViewBag.documentlink = "true";

            }
            // roleassignment
            List<LoginModel> Lmlist = new List<LoginModel>();
            con.Open();
            string q = "select * from Assignment_Roles where RoleId = " + Convert.ToInt32(Session["rId"]) + "";
            SqlDataAdapter da3 = new SqlDataAdapter(q, con);
            DataTable dt3 = new DataTable();
            da3.Fill(dt3);
            if (dt3.Rows.Count > 0)
            {

                for (int i = 0; i < dt3.Rows.Count; i++)
                {
                    LoginModel lm = new LoginModel();
                    lm.PageName = dt3.Rows[i]["PageName"].ToString();
                    lm.PageStatus = dt3.Rows[i]["PageStatus"].ToString();
                    lm.Action = dt3.Rows[i]["Action"].ToString();
                    lm.Nav1 = dt3.Rows[i]["Nav1"].ToString();
                    lm.Nav2 = dt3.Rows[i]["Nav2"].ToString();

                    Lmlist.Add(lm);
                }



                ViewBag.PageName = Lmlist;




            }

            con.Close();










            //end



            // update email and password 
            con.Open();

            string chkmail = "select Email from testatordetails where uId = " + TFM.uId + " ";
            SqlDataAdapter chkmailadp = new SqlDataAdapter(chkmail, con);
            DataTable chkmaildt = new DataTable();
            chkmailadp.Fill(chkmaildt);
            if (chkmaildt.Rows.Count > 0)
            {

                if (chkmaildt.Rows[0]["Email"].ToString() != TFM.Email)
                {


                    //generate MOBILE OTP
                    TFM.MobileOTP = String.Empty;
                    string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
                    int iOTPLength = 5;

                    string sTempChars = String.Empty;
                    Random rand = new Random();

                    for (int i = 0; i < iOTPLength; i++)

                    {

                        int p = rand.Next(0, saAllowedCharacters.Length);

                        sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];

                        TFM.MobileOTP += sTempChars;

                    }
                    //END




                    //generate EMAIL OTP
                    TFM.EmailOTP = String.Empty;
                    string[] saAllowedCharacters2 = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
                    int iOTPLength2 = 5;

                    string sTempChars2 = String.Empty;
                    Random rand2 = new Random();

                    for (int i = 0; i < iOTPLength2; i++)

                    {

                        int p = rand.Next(0, saAllowedCharacters2.Length);

                        sTempChars2 = saAllowedCharacters2[rand.Next(0, saAllowedCharacters2.Length)];

                        TFM.EmailOTP += sTempChars2;

                    }
                    //END


                    //generate Password OTP
                    TFM.userPassword = String.Empty;
                    string[] saAllowedCharacters3 = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
                    int iOTPLength3 = 5;

                    string sTempChars3 = String.Empty;
                    Random rand3 = new Random();

                    for (int i = 0; i < iOTPLength3; i++)

                    {

                        int p = rand3.Next(0, saAllowedCharacters3.Length);

                        sTempChars3 = saAllowedCharacters3[rand3.Next(0, saAllowedCharacters3.Length)];

                        TFM.userPassword += sTempChars3;

                    }
                    //END

                    con.Close();




                    con.Open();
                    string query33 = "update users set  userID= '" + TFM.Email + "' , userPwd='" + TFM.userPassword + "'   where uId = " + TFM.uId + "";
                    SqlCommand cmd33 = new SqlCommand(query33, con);
                    cmd33.ExecuteNonQuery();
                    con.Close();


                    // update otp for email and mobile





                    con.Open();
                    string qq = "update TestatorDetails set Contact_Verification = 0 ,Email_Verification = 0 , Mobile_Verification_Status = 0 , Email_OTP = '" + TFM.EmailOTP + "' , Mobile_OTP = '" + TFM.MobileOTP + "' where  uId = " + TFM.uId + " ";
                    SqlCommand cmddd = new SqlCommand(qq, con);
                    cmddd.ExecuteNonQuery();
                    con.Close();





                    //end


                    if (TFM.Email != "")
                    {
                        //generate Mail
                        string mailto2 = TFM.Email;
                        string userlogin = TFM.Email;


                        string subject2 = "Will Assure Login Credentials";

                        string text2 = "<font color='Green' style='font-size=3em;'>Your UserId And Password For Logging In Is <br> UserID : " + TFM.Email + " <br> Password : " + TFM.userPassword + "</font>";
                        string body2 = "<font color='red'>" + text2 + "</font>";


                        MailMessage msg3 = new MailMessage();
                        msg3.From = new MailAddress("info@drinco.in");
                        msg3.To.Add(mailto2);
                        msg3.Subject = subject2;
                        msg3.Body = body2;

                        msg3.IsBodyHtml = true;
                        SmtpClient smtp2 = new SmtpClient("216.10.240.149", 25);
                        smtp2.Credentials = new NetworkCredential("info@drinco.in", "95Bzf%s7");
                        smtp2.EnableSsl = false;
                        smtp2.Send(msg3);
                        smtp2.Dispose();


                        //end

                    }




                    // email otp
                    string mailto = TFM.Email;
                    string Userid = TFM.Identity_proof_Value;

                    Session["userid"] = Userid;
                    string subject = "Testing Mail Sending";
                    string OTP = "<font color='Green' style='font-size=3em;'>" + TFM.EmailOTP + "</font>";
                    string text = "Your OTP for Verification Is " + OTP + "";
                    string body = "<font color='red'>" + text + "</font>";


                    MailMessage msg = new MailMessage();
                    msg.From = new MailAddress("info@drinco.in");
                    msg.To.Add(mailto);
                    msg.Subject = subject;
                    msg.Body = body;

                    msg.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient("216.10.240.149", 25);
                    smtp.Credentials = new NetworkCredential("info@drinco.in", "95Bzf%s7");
                    smtp.EnableSsl = false;
                    smtp.Send(msg);
                    smtp.Dispose();



                    //end




                    con.Close();
                }

            }
            con.Close();








            //end







            con.Open();
            DateTime dat;
            if (TFM.Dobb != null || TFM.Dobb == "")
            {
                 dat = DateTime.ParseExact(TFM.Dobb, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            }
            else
            {
                 dat = DateTime.ParseExact(TFM.Dob, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            }
            

            SqlCommand cmd = new SqlCommand("SP_CRUDTestatorDetails", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@condition", "update");
            cmd.Parameters.AddWithValue("@tId", TFM.tId);
            cmd.Parameters.AddWithValue("@First_Name", TFM.First_Name);
            cmd.Parameters.AddWithValue("@Last_Name", TFM.Last_Name);
            cmd.Parameters.AddWithValue("@Middle_Name", TFM.Middle_Name);
            cmd.Parameters.AddWithValue("@DOB", dat);
            cmd.Parameters.AddWithValue("@Occupation", TFM.Occupation);
            cmd.Parameters.AddWithValue("@Mobile", TFM.Mobile);
            cmd.Parameters.AddWithValue("@Email", TFM.Email);
            cmd.Parameters.AddWithValue("@maritalStatus", TFM.material_status_txt);
            cmd.Parameters.AddWithValue("@Religion", TFM.Religiontext);
            cmd.Parameters.AddWithValue("@Relationship", "none");
            cmd.Parameters.AddWithValue("@Identity_Proof", TFM.Identity_Proof);
            cmd.Parameters.AddWithValue("@Identity_proof_Value", TFM.Identity_proof_Value);
            cmd.Parameters.AddWithValue("@Alt_Identity_Proof", TFM.Alt_Identity_Proof);
            cmd.Parameters.AddWithValue("@Alt_Identity_proof_Value", TFM.Alt_Identity_proof_Value);
            cmd.Parameters.AddWithValue("@Gender", TFM.Gendertext);
            cmd.Parameters.AddWithValue("@Address1", TFM.Address1);
            if (TFM.Address2 != null)
            {
                cmd.Parameters.AddWithValue("@Address2", TFM.Address2);
            }
            else
            {
                TFM.Address2 = "None";
                cmd.Parameters.AddWithValue("@Address2", TFM.Address2);
            }

            if (TFM.Address3 != null)
            {
                cmd.Parameters.AddWithValue("@Address3", TFM.Address3);
            }
            else
            {
                TFM.Address3 = "None";
                cmd.Parameters.AddWithValue("@Address3", TFM.Address3);
            }
            cmd.Parameters.AddWithValue("@City", TFM.citytext);
            cmd.Parameters.AddWithValue("@State", TFM.statetext);
            cmd.Parameters.AddWithValue("@Country", TFM.countrytext);
            cmd.Parameters.AddWithValue("@Pin", TFM.Pin);
            if (TFM.active != null)
            {
                cmd.Parameters.AddWithValue("@active", TFM.active);
            }
            else
            {
                cmd.Parameters.AddWithValue("@active", "Active");
            }

            cmd.Parameters.AddWithValue("@Contact_Verification", "");
            cmd.Parameters.AddWithValue("@Email_Verification", "");
            cmd.Parameters.AddWithValue("@Mobile_Verification_Status", "");
            if (TFM.EmailOTP != null)
            {
                cmd.Parameters.AddWithValue("@Email_OTP", TFM.EmailOTP);
            }
            else
            {

                cmd.Parameters.AddWithValue("@Email_OTP", TFM.tempemailotp);

            }
            if (TFM.MobileOTP != null)
            {
                cmd.Parameters.AddWithValue("@Mobile_OTP", TFM.MobileOTP);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Mobile_OTP", TFM.tempmobileotp);
            }


            cmd.Parameters.AddWithValue("@uid", "");

            cmd.ExecuteNonQuery();

            //string query = "update TestatorDetails set First_Name = '"+TFM.First_Name+"' , Last_Name='"+TFM.Last_Name+"' ,Middle_Name= '"+TFM.Middle_Name+"' , DOB = '"+ sqlFormattedDate + "' ,Occupation='"+TFM.Occupation+"' ,Mobile='"+TFM.Mobile+"' ,Email = '"+TFM.Email+"' ,maritalStatus='"+TFM.material_status +"' , Religion='"+TFM.Religiontext+ "' ,  Relationship = '"+TFM.RelationshipTxt + "'   ,Identity_Proof='" + TFM.Identity_Proof+"' ,Identity_proof_Value='"+TFM.Identity_proof_Value+"',Alt_Identity_Proof='"+TFM.Alt_Identity_Proof+"',Alt_Identity_proof_Value='"+TFM.Alt_Identity_proof_Value+"',Gender='"+TFM.Gendertext+"',Address1='"+TFM.Address1+"',Address2='"+TFM.Address2+"',Address3='"+TFM.Address3+"',Country='"+TFM.countrytext+"',State='"+TFM.statetext+"',City='"+TFM.citytext+"',Pin='"+TFM.Pin+"',active='"+TFM.active+ "'  where  tId = " + TFM.tId+"";
            //SqlCommand cmd = new SqlCommand(query,con);
            //cmd.ExecuteNonQuery();




            if (TFM.EmailOTP != null)
            {
                string query2 = "update users set First_Name= '" + TFM.First_Name + "' , Last_Name='" + TFM.Last_Name + "' ,  Middle_Name='" + TFM.Middle_Name + "' , DOB = '" + Convert.ToDateTime(dat).ToString("dd-MM-yyyy") + "' , Mobile = '" + TFM.Mobile + "' ,  eMail = '" + TFM.Email + "' , Address1='" + TFM.Address1 + "' , Address2='" + TFM.Address2 + "' , Address3 = '" + TFM.Address3 + "' , City='" + TFM.citytext + "' ,State= '" + TFM.statetext + "' , Pin='" + TFM.Pin + "' , Designation = '2'   where uId = " + TFM.uId + "     ";
                SqlCommand cdd = new SqlCommand(query2, con);
                cdd.ExecuteNonQuery();
                con.Close();
            }
            else
            {

                string query2 = "update users set First_Name= '" + TFM.First_Name + "' , Last_Name='" + TFM.Last_Name + "' ,  Middle_Name='" + TFM.Middle_Name + "' , DOB = '" + Convert.ToDateTime(dat).ToString("dd-MM-yyyy") + "' , Mobile = '" + TFM.Mobile + "' ,  eMail = '" + TFM.Email + "' , Address1='" + TFM.Address1 + "' , Address2='" + TFM.Address2 + "' , Address3 = '" + TFM.Address3 + "' , City='" + TFM.citytext + "' ,State= '" + TFM.statetext + "' , Pin='" + TFM.Pin + "' , Designation = '1'   where uId = " + TFM.uId + "     ";
                SqlCommand cdd = new SqlCommand(query2, con);
                cdd.ExecuteNonQuery();
                con.Close();

            }














            ViewBag.Message = "Verified";


            return View("~/Views/UpdateTestators/UpdateTestatorPageContent.cshtml");
        }


        public String BindCountryDDL()
        {

            con.Open();
            string query = "select distinct * from country_tbl order by CountryName asc  ";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            string data = "";

            if (dt.Rows.Count > 0)
            {


                for (int i = 0; i < dt.Rows.Count; i++)
                {




                    data = data + "<option value=" + dt.Rows[i]["CountryID"].ToString() + " >" + dt.Rows[i]["CountryName"].ToString() + "</option>";



                }




            }

            return data;

        }



        public String BindStateDDL()
        {

            con.Open();
            string query = "select distinct * from tbl_state order by statename asc  ";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            string data = "";

            if (dt.Rows.Count > 0)
            {


                for (int i = 0; i < dt.Rows.Count; i++)
                {




                    data = data + "<option value=" + dt.Rows[i]["state_id"].ToString() + " >" + dt.Rows[i]["statename"].ToString() + "</option>";



                }




            }

            return data;

        }






        public String BindCityDDL()
        {

            con.Open();
            string query = "select distinct * from tbl_city  order by city_name asc ";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            string data = "";

            if (dt.Rows.Count > 0)
            {


                for (int i = 0; i < dt.Rows.Count; i++)
                {




                    data = data + "<option value=" + dt.Rows[i]["id"].ToString() + " >" + dt.Rows[i]["city_name"].ToString() + "</option>";



                }




            }

            return data;

        }


        public string OnChangeBindState()
        {
            string response = Request["send"];
            con.Open();
            string query = "select distinct * from tbl_state where country_id = '" + response + "' order by statename asc";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            string data = "";

            if (dt.Rows.Count > 0)
            {


                for (int i = 0; i < dt.Rows.Count; i++)
                {




                    data = data + "<option value=" + dt.Rows[i]["state_id"].ToString() + " >" + dt.Rows[i]["statename"].ToString() + "</option>";



                }




            }

            return data;
        }




        public string OnChangeBindCity()
        {
            string response = Request["send"];
            con.Open();
            string query = "select distinct * from tbl_city where state_id = '" + response + "' order by city_name asc";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            string data = "";

            if (dt.Rows.Count > 0)
            {


                for (int i = 0; i < dt.Rows.Count; i++)
                {




                    data = data + "<option value=" + dt.Rows[i]["id"].ToString() + " >" + dt.Rows[i]["city_name"].ToString() + "</option>";



                }




            }

            return data;
        }

        public String BindRelationDDL()
        {

            con.Open();
            string query = "select * from relationship";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            string data = "<option value='' >--Select--</option>";

            if (dt.Rows.Count > 0)
            {


                for (int i = 0; i < dt.Rows.Count; i++)
                {




                    data = data + "<option value=" + dt.Rows[i]["Rid"].ToString() + " >" + dt.Rows[i]["MemberName"].ToString() + "</option>";



                }




            }

            return data;

        }






    }
}