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
    public class UpdateEditFormController : Controller
    {
        public static string connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        SqlConnection con = new SqlConnection(connectionString);

        // GET: UpdateEditForm
        public ActionResult UpdateEditFormIndex(int NestId)
        {


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
                UFM.Dob = dt.Rows[0]["DOB"].ToString();
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


            return View("~/Views/UpdateEditForm/UpdateEditFormContent.cshtml", UFM);
        }


        public string BindDesignationDDL()
        {

            con.Open();
            string query = "select * from Roles";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            string data = "";

            if (dt.Rows.Count > 0)
            {


                for (int i = 0; i < dt.Rows.Count; i++)
                {




                    data = data + "<option value=" + dt.Rows[i]["rid"].ToString() + " >" + dt.Rows[i]["Role"].ToString() + "</option>";



                }




            }

            return data;


        }



        public ActionResult UpdatingUserform(UserFormModel UFM)
        {


            con.Open();
            SqlCommand cmd = new SqlCommand("SP_Users", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@condition", "update");
            cmd.Parameters.AddWithValue("@uid",UFM.uid);
            cmd.Parameters.AddWithValue("@FirstName", UFM.FirstName);
            cmd.Parameters.AddWithValue("@LastName", UFM.LastName);
            cmd.Parameters.AddWithValue("@MiddleName", UFM.MiddleName);
            cmd.Parameters.AddWithValue("@Dob", UFM.Dob);
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

            Response.Write("<script>alert('Your Records Have Been Updated...!')</script>");


            return View("~/Views/UpdateEditForm/UpdateEditFormContent.cshtml");
        }




        public String BindRoleDDL()
        {

            con.Open();
            string query = "select * from Roles";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            string data = "";

            if (dt.Rows.Count > 0)
            {


                for (int i = 0; i < dt.Rows.Count; i++)
                {




                    data = data + "<option value=" + dt.Rows[i]["rid"].ToString() + " >" + dt.Rows[i]["Role"].ToString() + "</option>";



                }




            }

            return data;

        }






    }
}