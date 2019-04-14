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


namespace WillAssure.Controllers
{
    public class UpdateDistributorEmployeeController : Controller
    {
        public static string connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        SqlConnection con = new SqlConnection(connectionString);

        // GET: UpdateDistributorEmployee
        public ActionResult UpdateDistributorEmployeeIndex(int NestId)
        {
            if (Session["rId"] == null || Session["uuid"] == null)
            {

               RedirectToAction("LoginPageIndex", "LoginPage");

            }
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


            UserFormModel UFM = new UserFormModel();

            con.Open();
            string query = "select * from users where uId = '" + NestId + "' ";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();


            if (dt.Rows.Count > 0)
            {
                UFM.uid = NestId;
                UFM.FirstName = dt.Rows[0]["First_Name"].ToString();
                UFM.LastName = dt.Rows[0]["Last_Name"].ToString();
                UFM.MiddleName = dt.Rows[0]["Middle_Name"].ToString();
                UFM.Dob = Convert.ToDateTime(dt.Rows[0]["DOB"]).ToString("dd-MM-yyyy");
                UFM.Mobile = dt.Rows[0]["Mobile"].ToString();
                UFM.Email = dt.Rows[0]["eMail"].ToString();
                UFM.Address1 = dt.Rows[0]["Address1"].ToString();
                UFM.Address2 = dt.Rows[0]["Address2"].ToString();
                UFM.Address3 = dt.Rows[0]["Address3"].ToString();
                UFM.citytext = dt.Rows[0]["City"].ToString();
                UFM.statetext = dt.Rows[0]["State"].ToString();
                UFM.Pin = dt.Rows[0]["Pin"].ToString();
                UFM.UserId = dt.Rows[0]["userID"].ToString();
                UFM.UserPassword = dt.Rows[0]["userPwd"].ToString();

                UFM.Designation = dt.Rows[0]["Designation"].ToString();
                UFM.rid = Convert.ToInt32(dt.Rows[0]["rId"]);
                UFM.Active = dt.Rows[0]["active"].ToString();



            }

            return View("~/Views/UpdateDistributorEmployee/UpdateDistributorEmployeePageContent.cshtml",UFM);

        }




       



        public ActionResult UpdatingUserform(UserFormModel UFM)
        {
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

            con.Open();
            SqlCommand cmd = new SqlCommand("SP_Users", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@condition", "update");
            cmd.Parameters.AddWithValue("@uid", UFM.uid);
            cmd.Parameters.AddWithValue("@FirstName", UFM.FirstName);
            cmd.Parameters.AddWithValue("@LastName", UFM.LastName);
            cmd.Parameters.AddWithValue("@MiddleName", UFM.MiddleName);
            DateTime dat = DateTime.ParseExact(UFM.Dob, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            cmd.Parameters.AddWithValue("@Dob", dat);
            cmd.Parameters.AddWithValue("@Mobile", UFM.Mobile);
            cmd.Parameters.AddWithValue("@Email", UFM.Email);
            cmd.Parameters.AddWithValue("@Address1", UFM.Address1);
            cmd.Parameters.AddWithValue("@Address2", UFM.Address2);
            cmd.Parameters.AddWithValue("@Address3", UFM.Address3);
            cmd.Parameters.AddWithValue("@City", UFM.citytext);
            cmd.Parameters.AddWithValue("@State ", UFM.statetext);
            cmd.Parameters.AddWithValue("@Pin", UFM.Pin);
            cmd.Parameters.AddWithValue("@UserId", UFM.UserId);
            cmd.Parameters.AddWithValue("@UserPassword", UFM.UserPassword);
            cmd.Parameters.AddWithValue("@Designation", UFM.Designation);
            cmd.Parameters.AddWithValue("@Active", UFM.Active);
            cmd.Parameters.AddWithValue("@rid", UFM.rid);

            cmd.Parameters.AddWithValue("@compId", UFM.CompId);
            cmd.Parameters.AddWithValue("@Linked_user", UFM.rid);
            cmd.ExecuteNonQuery();
            con.Close();



            ViewBag.Message = "Verified";
            return View("~/Views/UpdateDistributorEmployee/UpdateDistributorEmployeePageContent.cshtml");
        }

        public String BindStateDDL()
        {

            con.Open();
            string query = "select distinct * from tbl_state where country_id = 101 order by statename asc";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            string data = "<option value=''>--Select State--</option>";

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
            string data = "<option value=''>--Select City--</option>";

            if (dt.Rows.Count > 0)
            {


                for (int i = 0; i < dt.Rows.Count; i++)
                {




                    data = data + "<option value=" + dt.Rows[i]["id"].ToString() + " >" + dt.Rows[i]["city_name"].ToString() + "</option>";



                }




            }

            return data;
        }




        public String BindRoleDDL()
        {
            int index = Convert.ToInt32(Request["send"]);
            string data = "";


            data = "<option value=''>--Select Role--</option>";



            if (Session["rId"] != null)
            {
                int roles = 0;
                roles = Convert.ToInt32(Session["rId"]);


                con.Open();
                string query = "select * from Roles   where  Pid = " + index + "";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();


                if (dt.Rows.Count > 0)
                {


                    for (int i = 0; i < dt.Rows.Count; i++)
                    {




                        data = data + "<option value=" + dt.Rows[i]["rid"].ToString() + " >" + dt.Rows[i]["Role"].ToString() + "</option>";



                    }




                }









            }

            return data;
        }




        public String BindCompanyDDL()
        {
            string data = "<option value=''>--Select --</option>";
            if (Convert.ToInt32(Session["rId"]) == 1)
            {
                con.Open();
                string query = "select uId , First_Name from users";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();


                if (dt.Rows.Count > 0)
                {


                    for (int i = 0; i < dt.Rows.Count; i++)
                    {




                        data = data + "<option value=" + dt.Rows[i]["uId"].ToString() + " >" + dt.Rows[i]["First_Name"].ToString() + "</option>";



                    }




                }



            }
            else
            {

                con.Open();
                string query = "select a.uId , a.First_Name  from users a inner join roles b on a.rId=b.rId where a.rId = " + Convert.ToInt32(Session["rId"]) + " and a.uId = " + Convert.ToInt32(Session["uuid"]) + " ";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();


                if (dt.Rows.Count > 0)
                {


                    for (int i = 0; i < dt.Rows.Count; i++)
                    {




                        data = data + "<option value=" + dt.Rows[i]["uId"].ToString() + " >" + dt.Rows[i]["First_Name"].ToString() + "</option>";



                    }




                }

            }

            return data;

        }







        public string BindDistributorDDL()
        {
            con.Open();
            string query = "select uId , First_Name from users";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            string data = "<option value=''>--Select --</option>";

            if (dt.Rows.Count > 0)
            {


                for (int i = 0; i < dt.Rows.Count; i++)
                {




                    data = data + "<option value=" + dt.Rows[i]["uId"].ToString() + " >" + dt.Rows[i]["First_Name"].ToString() + "</option>";



                }




            }

            return data;
        }




        public string checktype()
        {

            string buttonname = "";

            if (Convert.ToInt32(Session["rId"]) == 1)
            {











                buttonname = "Update Distributor";
                ViewBag.type = buttonname;








            }


            //bindbutton


            return buttonname;
        }









    }




}