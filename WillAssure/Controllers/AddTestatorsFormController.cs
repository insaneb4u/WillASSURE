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
using System.Net.Mail;
using System.Net;

namespace WillAssure.Controllers
{
    public class AddTestatorsFormController : Controller
    {
        public static string connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        SqlConnection con = new SqlConnection(connectionString);


        // GET: AddTestatorsForm
        public ActionResult AddTestatorsFormIndex()
        {



            return View("~/Views/AddTestatorsForm/AddTestatorPageContent.cshtml");
        }

        public ActionResult InsertTestatorFormData(TestatorFormModel TFM)
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


            if (Session["uid"] != null)
            {
                TFM.uId = Convert.ToInt32(Session["uid"]);

                con.Open();
                SqlCommand cmd = new SqlCommand("SP_CRUDTestatorDetails", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@condition", "insert");
                cmd.Parameters.AddWithValue("@First_Name", TFM.First_Name);
                cmd.Parameters.AddWithValue("@Last_Name", TFM.Last_Name);
                cmd.Parameters.AddWithValue("@Middle_Name", TFM.Middle_Name);
                cmd.Parameters.AddWithValue("@DOB", TFM.DOB);
                cmd.Parameters.AddWithValue("@Occupation", TFM.Occupation);
                cmd.Parameters.AddWithValue("@Mobile", TFM.Mobile);
                cmd.Parameters.AddWithValue("@Email", TFM.Email);
                cmd.Parameters.AddWithValue("@maritalStatus", TFM.material_status_txt);
                cmd.Parameters.AddWithValue("@Religion", TFM.Religiontext);
                cmd.Parameters.AddWithValue("@Identity_Proof", TFM.Identity_Proof_txt);
                cmd.Parameters.AddWithValue("@Identity_proof_Value", TFM.Identity_proof_Value);
                cmd.Parameters.AddWithValue("@Alt_Identity_Proof", TFM.Alt_Identity_Proof);
                cmd.Parameters.AddWithValue("@Alt_Identity_proof_Value", TFM.Alt_Identity_proof_Value);
                cmd.Parameters.AddWithValue("@Gender", TFM.Gendertext);
                cmd.Parameters.AddWithValue("@Address1", TFM.Address1);
                cmd.Parameters.AddWithValue("@Address2", TFM.Address2);
                cmd.Parameters.AddWithValue("@Address3", TFM.Address3);
                cmd.Parameters.AddWithValue("@City", TFM.citytext);
                cmd.Parameters.AddWithValue("@State", TFM.statetext);
                cmd.Parameters.AddWithValue("@Country", TFM.countrytext);
                cmd.Parameters.AddWithValue("@Pin", TFM.Pin);
                cmd.Parameters.AddWithValue("@active", TFM.active);
                cmd.Parameters.AddWithValue("@Contact_Verification", "0");
                cmd.Parameters.AddWithValue("@Email_Verification", "0");
                cmd.Parameters.AddWithValue("@Mobile_Verification_Status", "0");
                cmd.Parameters.AddWithValue("@Email_OTP", TFM.EmailOTP);
                cmd.Parameters.AddWithValue("@Mobile_OTP", TFM.MobileOTP);
                cmd.Parameters.AddWithValue("@uid", TFM.uId);
                cmd.ExecuteNonQuery();
                con.Close();

                int testatorid = 0;
                int templateid = 0;
                string testatortype = "";

                // for storing testator id and created by in document master
                con.Open();
                string query = "select top 1 * from TestatorDetails order by tId desc ";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    testatorid = Convert.ToInt32(dt.Rows[0]["tId"]);
                    Session["tid"] = "";
                    Session["tid"] = testatorid;
                }
                con.Close();
                //end



                // for storing templateMaster templateid and  testator id in document master
                con.Open();
                string q2 = "select * from templateMaster";
                SqlDataAdapter da2 = new SqlDataAdapter(q2, con);
                DataTable dt2 = new DataTable();
                da2.Fill(dt2);
                if (dt2.Rows.Count > 0)
                {
                    templateid = Convert.ToInt32(dt2.Rows[0]["templateid"]);

                    Session["templateid"] = templateid;
                    testatortype = dt2.Rows[0]["testator_type"].ToString();
                    Session["Document_Created_By"] = "";
                    Session["Document_Created_By"] = TFM.Document_Created_By;
                }
                con.Close();
                //end


                // document generation 

              
                con.Open();
                string q = "insert into documentMaster (tId,templateId,IsUpdatetable,uId,pId,created_by,testator_type) values (" + testatorid + " , " + templateid + " ,  'Yes' ,   "+Convert.ToInt32(Session["uid"]) +" , 1 , '" + TFM.Document_Created_By_txt + "' , '" + testatortype + "')";
                SqlCommand c = new SqlCommand(q, con);
                c.ExecuteNonQuery();
                con.Close();



                //end



                // DOCUMENT RULES
                int typeid = 0;
                int typecat = 0;

                if (TFM.documenttype == "Will")
                {
                    typeid = 1;
                }
                if (TFM.documentcategory == "Quick")
                {
                    typecat = 1;
                }
                if (TFM.documentcategory == "Detailed")
                {
                    typecat = 2;
                }


                con.Open();
                string q1 = "insert into documentRules (documentType,category,templateId) values (" + typeid +" , "+typecat+" , "+templateid+" )";
                SqlCommand c1 = new SqlCommand(q1, con);
                c1.ExecuteNonQuery();
                con.Close();



                //


                //1st condition
                if (TFM.Amt_Paid_By_txt == "Distributor" && TFM.Document_Created_By_txt == "Distributor")
                {
                    TFM.Authentication_Required = 0;
                    TFM.Link_Required = 0;
                    TFM.Login_Required = 0;

                    con.Open();
                    string query1 = "insert into Authorization_Rules (Document_Created_By,Distributor_Id,Amt_Paid_By,Testator_Id,Authentication_Required,Link_Required,Login_Required) values ('" + TFM.Document_Created_By_txt + "' , '" + TFM.Document_Created_By_ID + "' , '" + TFM.Amt_Paid_By_txt + "' , '" + TFM.Amt_Paid_By + "'  , '" + TFM.Authentication_Required + "' , '" + TFM.Link_Required + "' , '" + TFM.Login_Required + "') ";
                    SqlCommand cmd2 = new SqlCommand(query1, con);
                    cmd2.ExecuteNonQuery();
                    con.Close();


                }
                //end
                //2nd condition 
                if (TFM.Amt_Paid_By_txt == "Distributor" && TFM.Document_Created_By_txt == "Testator")
                {
                    TFM.Authentication_Required = 1;
                    TFM.Link_Required = 1;
                    TFM.Login_Required = 1;

                    con.Open();
                    string query2 = "insert into Authorization_Rules (Document_Created_By,Distributor_Id,Amt_Paid_By,Testator_Id,Authentication_Required,Link_Required,Login_Required) values ('" + TFM.Document_Created_By_txt + "' , '" + TFM.Document_Created_By_ID + "' , '" + TFM.Amt_Paid_By_txt + "' , '" + TFM.Amt_Paid_By + "'  , '" + TFM.Authentication_Required + "' , '" + TFM.Link_Required + "' , '" + TFM.Login_Required + "') ";
                    SqlCommand cmd2 = new SqlCommand(query2, con);
                    cmd2.ExecuteNonQuery();
                    con.Close();





                    //generate Mail

                    string mailid = "imransayyed528@gmail.com";
                    string mailto = TFM.Email;
                    //string mailto = "willtestmail@mailprotech.com";
                    string subject = "Testing Mail Sending";
                    string OTP = "<font color='Green' style='font-size=3em;'>" + TFM.EmailOTP + "</font>";
                    string text = "Your OTP for Verification Is " + OTP + "";
                    string body = "<font color='red'>" + text + "</font>";
                    using (MailMessage mm = new MailMessage(mailid, mailto))
                    {
                        mm.Subject = subject;
                        mm.Body = body;

                        mm.IsBodyHtml = true;
                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = "smtp.gmail.com";
                        smtp.EnableSsl = true;
                        NetworkCredential NetworkCred = new NetworkCredential(mailid, TFM.password);
                        smtp.UseDefaultCredentials = true;
                        smtp.Credentials = NetworkCred;
                        smtp.Port = 587;
                        smtp.Send(mm);

                    }

                    //end


                }
                //end
                // 3rd condtion
                if (TFM.Amt_Paid_By_txt == "Testator" && TFM.Document_Created_By_txt == "Distributor")
                {
                    TFM.Authentication_Required = 1;
                    TFM.Link_Required = 1;
                    TFM.Login_Required = 1;

                    con.Open();
                    string query3 = "insert into Authorization_Rules (Document_Created_By,Distributor_Id,Amt_Paid_By,Testator_Id,Authentication_Required,Link_Required,Login_Required) values ('" + TFM.Document_Created_By_txt + "' , '" + TFM.Document_Created_By_ID + "' , '" + TFM.Amt_Paid_By_txt + "' , '" + TFM.Amt_Paid_By + "'  , '" + TFM.Authentication_Required + "' , '" + TFM.Link_Required + "' , '" + TFM.Login_Required + "') ";
                    SqlCommand cmd2 = new SqlCommand(query3, con);
                    cmd2.ExecuteNonQuery();
                    con.Close();





                    //generate Mail

                    string mailid = "imransayyed528@gmail.com";
                    string mailto = TFM.Email;
                    //string mailto = "willtestmail@mailprotech.com";
                    string subject = "Testing Mail Sending";
                    string OTP = "<font color='Green' style='font-size=3em;'>" + TFM.EmailOTP + "</font>";
                    string text = "Your OTP for Verification Is " + OTP + "";
                    string body = "<font color='red'>" + text + "</font>";
                    using (MailMessage mm = new MailMessage(mailid, mailto))
                    {
                        mm.Subject = subject;
                        mm.Body = body;

                        mm.IsBodyHtml = true;
                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = "smtp.gmail.com";
                        smtp.EnableSsl = true;
                        NetworkCredential NetworkCred = new NetworkCredential(mailid, TFM.password);
                        smtp.UseDefaultCredentials = true;
                        smtp.Credentials = NetworkCred;
                        smtp.Port = 587;
                        smtp.Send(mm);

                    }

                    //end
                }
                //end
                //4th condition
                if (TFM.Amt_Paid_By_txt == "Testator" && TFM.Document_Created_By_txt == "Testator")
                {
                    TFM.Authentication_Required = 1;
                    TFM.Link_Required = 1;
                    TFM.Login_Required = 1;

                    con.Open();
                    string query4 = "insert into Authorization_Rules (Document_Created_By,Distributor_Id,Amt_Paid_By,Testator_Id,Authentication_Required,Link_Required,Login_Required) values ('" + TFM.Document_Created_By_txt + "' , '" + TFM.Document_Created_By_ID + "' , '" + TFM.Amt_Paid_By_txt + "' , '" + TFM.Amt_Paid_By + "'  , '" + TFM.Authentication_Required + "' , '" + TFM.Link_Required + "' , '" + TFM.Login_Required + "') ";
                    SqlCommand cmd2 = new SqlCommand(query4, con);
                    cmd2.ExecuteNonQuery();
                    con.Close();






                    //generate Mail

                    string mailid = "imransayyed528@gmail.com";
                    string mailto = TFM.Email;
                    //string mailto = "willtestmail@mailprotech.com";
                    string subject = "Testing Mail Sending";
                    string OTP = "<font color='Green' style='font-size=3em;'>" + TFM.EmailOTP + "</font>";
                    string text = "Your OTP for Verification Is " + OTP + "";
                    string body = "<font color='red'>" + text + "</font>";
                    using (MailMessage mm = new MailMessage(mailid, mailto))
                    {
                        mm.Subject = subject;
                        mm.Body = body;

                        mm.IsBodyHtml = true;
                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = "smtp.gmail.com";
                        smtp.EnableSsl = true;
                        NetworkCredential NetworkCred = new NetworkCredential(mailid, TFM.password);
                        smtp.UseDefaultCredentials = true;
                        smtp.Credentials = NetworkCred;
                        smtp.Port = 587;
                        smtp.Send(mm);

                    }

                    //end
                }
                //end





                string v1 = Eramake.eCryptography.Encrypt(TFM.EmailOTP);

                ViewBag.Message = "Verified";

                return RedirectToAction("EmailVerificationIndex", "EmailVerification", new { v2 = v1 });
            }
            else
            {
                Response.Write("Please Fill Up Company Details First");
            }

            return View("~/Views/AddTestatorsForm/AddTestatorPageContent.cshtml");
          

        }





        public String BindCountryDDL()
        {

            con.Open();
            string query = "select * from country_tbl";
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
            string query = "select * from tbl_state";
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
            string query = "select * from tbl_city";
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
            string query = "select * from tbl_state where country_id = '"+ response + "'";
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
            string query = "select * from tbl_city where state_id = '" + response + "'";
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








    }
}