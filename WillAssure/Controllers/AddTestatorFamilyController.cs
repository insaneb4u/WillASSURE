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


namespace WillAssure.Controllers
{
    public class AddTestatorFamilyController : Controller
    {


        public static string connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        SqlConnection con = new SqlConnection(connectionString);

        // GET: AddTestatorFamily
        public ActionResult AddTestatorFamilyIndex()
        {
            if (Session.SessionID == null)
            {
                return View("~/Views/LoginPage/LoginPageContent.cshtml");
            }
            if (Session["tid"] == null)
            {
                ViewBag.message = "link";
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
            return View("~/Views/AddTestatorFamily/AddTestatorFamilyPageContent.cshtml");
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




        public String BindStateDDL()
        {

            con.Open();
            string query = "select distinct * from tbl_state order by statename asc   ";
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
            string query = "select distinct * from tbl_city where state_id = '" + response + "' order by city_name asc ";
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



        public ActionResult InsertTestatorFamilyFormData(TestatorFamilyModel TFM)
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


            if (Session["tid"] != null)
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SP_CRUDtestatorfamily", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@condition", "insert");
                cmd.Parameters.AddWithValue("@First_Name", TFM.First_Name);
                cmd.Parameters.AddWithValue("@Last_Name", TFM.Last_Name);
                cmd.Parameters.AddWithValue("@Middle_Name", TFM.Middle_Name);
                cmd.Parameters.AddWithValue("@DOB", TFM.DOB);
                cmd.Parameters.AddWithValue("@Marital_Status", TFM.Marital_Status);
                cmd.Parameters.AddWithValue("@Religion", TFM.Religion);
                cmd.Parameters.AddWithValue("@Relationship", TFM.RelationshipTxt);
                cmd.Parameters.AddWithValue("@Address1", TFM.Address1);
                cmd.Parameters.AddWithValue("@Address2", TFM.Address2);
                cmd.Parameters.AddWithValue("@Address3", TFM.Address3);
                cmd.Parameters.AddWithValue("@City", TFM.City_txt);
                cmd.Parameters.AddWithValue("@State", TFM.State_txt);
                cmd.Parameters.AddWithValue("@Pin", TFM.Pin);
                cmd.Parameters.AddWithValue("@tId", Convert.ToInt32(Session["tid"]));
                cmd.Parameters.AddWithValue("@active", TFM.active);
                cmd.Parameters.AddWithValue("@Identity_Proof", TFM.Identity_Proof);
                cmd.Parameters.AddWithValue("@Identity_Proof_Value", TFM.Identity_Proof_Value);
                cmd.Parameters.AddWithValue("@Alt_Identity_Proof", TFM.Alt_Identity_Proof);
                cmd.Parameters.AddWithValue("@Alt_Identity_Proof_Value", TFM.Alt_Identity_Proof_Value);
                cmd.Parameters.AddWithValue("@Is_Informed_Person", TFM.Is_Informed_Person);
                cmd.ExecuteNonQuery();
                con.Close();

                ViewBag.message = "Verified";
            }
            else
            {
                ViewBag.message = "link";
            }



            return View("~/Views/AddTestatorFamily/AddTestatorFamilyPageContent.cshtml");
        }


    }
}