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
    public class AddTestatorFamilyController : Controller
    {


        public static string connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        SqlConnection con = new SqlConnection(connectionString);

        // GET: AddTestatorFamily
        public ActionResult AddTestatorFamilyIndex()
        {
            if (Session.SessionID == null)
            {

                return RedirectToAction("LoginPageIndex", "LoginPage");

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
            string query = "select distinct * from tbl_state where country_id = 101 order by statename asc   ";
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


            //if (Session["tid"] != null)
            //{
                con.Open();
                SqlCommand cmd = new SqlCommand("SP_CRUDtestatorfamily", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@condition", "insert");
                cmd.Parameters.AddWithValue("@First_Name", TFM.First_Name);
                cmd.Parameters.AddWithValue("@Last_Name", TFM.Last_Name);
                cmd.Parameters.AddWithValue("@Middle_Name", TFM.Middle_Name);
                DateTime dat = DateTime.ParseExact(TFM.Dob, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                cmd.Parameters.AddWithValue("@DOB", dat);
                cmd.Parameters.AddWithValue("@Marital_Status", TFM.Marital_Status);
                cmd.Parameters.AddWithValue("@Religion", TFM.Religion);
                cmd.Parameters.AddWithValue("@Relationship", TFM.RelationshipTxt);
                cmd.Parameters.AddWithValue("@Address1", TFM.Address1);
                cmd.Parameters.AddWithValue("@Address2", TFM.Address2);
                cmd.Parameters.AddWithValue("@Address3", TFM.Address3);
                cmd.Parameters.AddWithValue("@City", TFM.City_txt);
                cmd.Parameters.AddWithValue("@State", TFM.State_txt);
                cmd.Parameters.AddWithValue("@Pin", TFM.Pin);
                cmd.Parameters.AddWithValue("@tId", TFM.ddltid);
                cmd.Parameters.AddWithValue("@active", TFM.active);
                cmd.Parameters.AddWithValue("@Identity_Proof", TFM.Identity_Proof);
                cmd.Parameters.AddWithValue("@Identity_Proof_Value", TFM.Identity_Proof_Value);
                cmd.Parameters.AddWithValue("@Alt_Identity_Proof", TFM.Alt_Identity_Proof);
                cmd.Parameters.AddWithValue("@Alt_Identity_Proof_Value", TFM.Alt_Identity_Proof_Value);
                cmd.Parameters.AddWithValue("@Is_Informed_Person", TFM.Is_Informed_Person);
                cmd.ExecuteNonQuery();
                con.Close();

                ViewBag.message = "Verified";
            //}
            //else
            //{
            //    ViewBag.message = "link";
            //}

            ModelState.Clear();

            return View("~/Views/AddTestatorFamily/AddTestatorFamilyPageContent.cshtml");
        }



        public string BindTestatorDDL()
        {
            con.Open();
            string query = "select a.tId , a.First_Name from TestatorDetails a inner join users b on a.uId=b.uId where b.Linked_user  = " + Convert.ToInt32(Session["uuid"])+" ";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            string data = "<option value='' >--Select--</option>";




            if (dt.Rows.Count > 0)
            {


                for (int i = 0; i < dt.Rows.Count; i++)
                {




                    data = data + "<option value=" + dt.Rows[i]["tId"].ToString() + " >" + dt.Rows[i]["First_Name"].ToString() + "</option>";



                }




            }

            return data;
        }






        public int CheckTestatorUsers()
        {
            int check = 0;
            int Response = Convert.ToInt32(Request["send"]);

            if (Request["send"] != "")
            {
                // check for data exists or not for testato family
                con.Open();
                string query1 = "select a.fId , a.First_Name , a.Last_Name , a.Middle_Name , a.DOB , a.Marital_Status , a.Religion , a.Relationship , a.Address1 , a.Address2 , a.Address3 , a.City , a.State , a.Pin , a.tId , a.active , a.Identity_Proof , a.Identity_Proof_Value , a.Alt_Identity_Proof , a.Alt_Identity_Proof_Value , a.Is_Informed_Person from testatorFamily a inner join TestatorDetails b on a.tId=b.tId inner join users c on b.uId = c.uId where c.Linked_user =  " + Convert.ToInt32(Session["uuid"]) + " ";
                SqlDataAdapter da = new SqlDataAdapter(query1, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                //end

                if (dt.Rows.Count > 0)
                {
                    string query2 = "Update PageActivity set ActID=1 , Tid=" + Response + " , PageStatus=2  ";
                    SqlCommand cmd = new SqlCommand(query2, con);
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    string query2 = "Update PageActivity set ActID=1 , Tid=" + Response + " , PageStatus=1  ";
                    SqlCommand cmd = new SqlCommand(query2, con);
                    cmd.ExecuteNonQuery();
                }




                // if already exits page status 2 else 1

                string query3 = "select * from PageActivity";
                SqlDataAdapter da3 = new SqlDataAdapter(query3, con);
                DataTable dt3 = new DataTable();
                da3.Fill(dt3);
                
                if (dt3.Rows.Count > 0)
                {
                    check = Convert.ToInt32(dt3.Rows[0]["PageStatus"]);




                }


                //end






                con.Close();

            }





            return check;

        }





    }
}