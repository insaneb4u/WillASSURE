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
    public class AddBeneficiaryController : Controller
    {

        public static string connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        SqlConnection con = new SqlConnection(connectionString);

        // GET: AddBeneficiary
        public ActionResult AddBeneficiaryIndex()
        {
            if (Session.SessionID == null)
            {
                return View("~/Views/LoginPage/LoginPageContent.cshtml");
            }

            if (Session["aiid"] == null && Session["tid"] == null)
            {
                ViewBag.Message = "link";
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
            return View("~/Views/AddBeneficiary/AddBeneficiaryPageContent.cshtml");
        }



        public String BindStateDDL()
        {

            con.Open();
            string query = "select distinct * from tbl_state where country_id = 101 order by statename asc  ";
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




        public ActionResult InsertBeneficiaryData(BeneficiaryModel BM)
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

            if (Session["aiid"].ToString() != "" && Session["tid"].ToString() != "")
            {
                BM.aid = Convert.ToInt32(Session["aiid"]);
                BM.tid = Convert.ToInt32(Session["tid"]);


                con.Open();
                SqlCommand cmd = new SqlCommand("SP_CRUDBeneficiaryDetails", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@condition", "insert");
                cmd.Parameters.AddWithValue("@First_Name ", BM.First_Name);
                cmd.Parameters.AddWithValue("@Last_Name", BM.Last_Name);
                cmd.Parameters.AddWithValue("@Middle_Name", BM.Middle_Name);
                DateTime dat = DateTime.ParseExact(BM.Dob, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                cmd.Parameters.AddWithValue("@DOB", dat);
                cmd.Parameters.AddWithValue("@Mobile", BM.Mobile);
                cmd.Parameters.AddWithValue("@Relationship", BM.RelationshipTxt);
                cmd.Parameters.AddWithValue("@Marital_Status", BM.Marital_Status);
                cmd.Parameters.AddWithValue("@Religion", BM.Religion);
                cmd.Parameters.AddWithValue("@Identity_proof", BM.Identity_proof);
                cmd.Parameters.AddWithValue("@Identity_proof_value", BM.Identity_proof_value);
                cmd.Parameters.AddWithValue("@Alt_Identity_proof", BM.Alt_Identity_proof);
                cmd.Parameters.AddWithValue("@Alt_Identity_proof_value", BM.Alt_Identity_proof_value);
                cmd.Parameters.AddWithValue("@Address1", BM.Address1);
                cmd.Parameters.AddWithValue("@Address2", BM.Address2);
                cmd.Parameters.AddWithValue("@Address3", BM.Address3);
                cmd.Parameters.AddWithValue("@City", BM.City_txt);
                cmd.Parameters.AddWithValue("@State", BM.State_txt);
                cmd.Parameters.AddWithValue("@Pin", BM.Pin);
                cmd.Parameters.AddWithValue("@aid", BM.aid);
                cmd.Parameters.AddWithValue("@tid", BM.tid);
                cmd.Parameters.AddWithValue("@beneficiary_type", BM.beneficiary_type);
                cmd.ExecuteNonQuery();
                con.Close();


                con.Open();
                string query2 = "select top 1 * from BeneficiaryDetails order by bpId desc";
                SqlDataAdapter da = new SqlDataAdapter(query2,con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    
                    Session["bpId"] = Convert.ToInt32(dt.Rows[0]["bpId"]);
                }
                con.Close();




                ViewBag.Message = "RecordsInsert";





            }
            else
            {
                ViewBag.Message = "link";
            }




            ModelState.Clear();
            return View("~/Views/AddBeneficiary/AddBeneficiaryPageContent.cshtml");
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