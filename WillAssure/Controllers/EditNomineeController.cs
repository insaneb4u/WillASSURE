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
    public class EditNomineeController : Controller
    {
        public static string connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        SqlConnection con = new SqlConnection(connectionString);

        // GET: EditNominee
        public ActionResult EditNomineeIndex()
        {
            return View("~/Views/EditNominee/EditNomineePageContent.cshtml");
        }


        public string BindNomineeFormData()
        {
            con.Open();
            string query = "select * from Nominee";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            string data = "";

            if (dt.Rows.Count > 0)
            {


                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    data = data + "<tr class='nr'><td>" + dt.Rows[i]["nId"].ToString() + "</td>"
                                + "<td>" + dt.Rows[i]["First_Name"].ToString() + "</td>"
                                + "<td>" + dt.Rows[i]["Last_Name"].ToString() + "</td>"
                                + "<td>" + dt.Rows[i]["Middle_Name"].ToString() + "</td>"
                                + "<td>" + dt.Rows[i]["DOB"].ToString() + "</td>"
                                + "<td>" + dt.Rows[i]["Mobile"].ToString() + "</td>"
                                + "<td>" + dt.Rows[i]["Relationship"].ToString() + "</td>"
                                + "<td>" + dt.Rows[i]["Marital_Status"].ToString() + "</td>"
                                + "<td>" + dt.Rows[i]["Religion"].ToString() + "</td>"
                                + "<td>" + dt.Rows[i]["Identity_Proof"].ToString() + "</td>"
                                + "<td>" + dt.Rows[i]["Identity_Proof_Value"].ToString() + "</td>"
                                + "<td>" + dt.Rows[i]["Alt_Identity_Proof"].ToString() + "</td>"
                                + "<td>" + dt.Rows[i]["Alt_Identity_Proof_Value"].ToString() + "</td>"
                                + "<td>" + dt.Rows[i]["Address1"].ToString() + "</td>"
                                + "<td>" + dt.Rows[i]["Address2"].ToString() + "</td>"
                                + "<td>" + dt.Rows[i]["Address3"].ToString() + "</td>"
                                + "<td>" + dt.Rows[i]["City"].ToString() + "</td>"
                                + "<td>" + dt.Rows[i]["State"].ToString() + "</td>"
                                + "<td>" + dt.Rows[i]["Pin"].ToString() + "</td>"
                                + "<td>" + dt.Rows[i]["aiid"].ToString() + "</td>"
                                + "<td>" + dt.Rows[i]["tId"].ToString() + "</td>"
                                + "<td>" + dt.Rows[i]["dateCreated"].ToString() + "</td>"
                                + "<td>" + dt.Rows[i]["createdBy"].ToString() + "</td>"
                                + "<td>" + dt.Rows[i]["documentId"].ToString() + "</td>"
                                + "<td>" + dt.Rows[i]["Description_of_Assets"].ToString() + "</td>"

                                + "<td><button type='button'   id='" + dt.Rows[i]["nId"].ToString() + "' onClick='Edit(this.id)'   class='btn btn-primary'>Edit</button><button type='button'   id='" + dt.Rows[i]["nId"].ToString() + "' onClick='Delete(this.id)'   class='btn btn-danger '>Delete</button></td>    </tr>";

                }
            }

            return data;
        }



        public string DeleteNomineeRecords(RoleFormModel RFM)
        {
            int index = Convert.ToInt32(Request["send"]);

            con.Open();
            SqlCommand cmd = new SqlCommand("SP_CRUDNominee", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@action", "delete");
            cmd.Parameters.AddWithValue("@nId",index);
            cmd.Parameters.AddWithValue("@First_Name", "");
            cmd.Parameters.AddWithValue("@Last_Name", "");
            cmd.Parameters.AddWithValue("@Middle_Name", "");
            cmd.Parameters.AddWithValue("@DOB", "");
            cmd.Parameters.AddWithValue("@Mobile","");
            cmd.Parameters.AddWithValue("@Relationship", "");
            cmd.Parameters.AddWithValue("@Marital_Status", "");
            cmd.Parameters.AddWithValue("@Religion", "");
            cmd.Parameters.AddWithValue("@Identity_Proof", "");
            cmd.Parameters.AddWithValue("@Identity_Proof_Value", "");
            cmd.Parameters.AddWithValue("@Alt_Identity_Proof", "");
            cmd.Parameters.AddWithValue("@Alt_Identity_Proof_Value", "");
            cmd.Parameters.AddWithValue("@Address1", "");
            cmd.Parameters.AddWithValue("@Address2", "");
            cmd.Parameters.AddWithValue("@Address3", "");
            cmd.Parameters.AddWithValue("@City", "");
            cmd.Parameters.AddWithValue("@State", "");
            cmd.Parameters.AddWithValue("@Pin","");
            cmd.Parameters.AddWithValue("@aid", "");
            cmd.Parameters.AddWithValue("@tId", "");
            cmd.Parameters.AddWithValue("@createdBy", "");
            cmd.Parameters.AddWithValue("@documentId", "");
            cmd.Parameters.AddWithValue("@Description_of_Assets", "");
            cmd.ExecuteNonQuery();
            con.Close();



            con.Open();
            string query = "select * from Nominee";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            string data = "";

            if (dt.Rows.Count > 0)
            {


                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    data = data + "<tr class='nr'><td>" + dt.Rows[i]["nId"].ToString() + "</td>"
                                + "<td>" + dt.Rows[i]["First_Name"].ToString() + "</td>"
                                + "<td>" + dt.Rows[i]["Last_Name"].ToString() + "</td>"
                                + "<td>" + dt.Rows[i]["Middle_Name"].ToString() + "</td>"
                                + "<td>" + dt.Rows[i]["DOB"].ToString() + "</td>"
                                + "<td>" + dt.Rows[i]["Mobile"].ToString() + "</td>"
                                + "<td>" + dt.Rows[i]["Relationship"].ToString() + "</td>"
                                + "<td>" + dt.Rows[i]["Marital_Status"].ToString() + "</td>"
                                + "<td>" + dt.Rows[i]["Religion"].ToString() + "</td>"
                                + "<td>" + dt.Rows[i]["Identity_Proof"].ToString() + "</td>"
                                + "<td>" + dt.Rows[i]["Identity_Proof_Value"].ToString() + "</td>"
                                + "<td>" + dt.Rows[i]["Alt_Identity_Proof"].ToString() + "</td>"
                                + "<td>" + dt.Rows[i]["Alt_Identity_Proof_Value"].ToString() + "</td>"
                                + "<td>" + dt.Rows[i]["Address1"].ToString() + "</td>"
                                + "<td>" + dt.Rows[i]["Address2"].ToString() + "</td>"
                                + "<td>" + dt.Rows[i]["Address3"].ToString() + "</td>"
                                + "<td>" + dt.Rows[i]["City"].ToString() + "</td>"
                                + "<td>" + dt.Rows[i]["State"].ToString() + "</td>"
                                + "<td>" + dt.Rows[i]["Pin"].ToString() + "</td>"
                                + "<td>" + dt.Rows[i]["aiid"].ToString() + "</td>"
                                + "<td>" + dt.Rows[i]["tId"].ToString() + "</td>"
                                + "<td>" + dt.Rows[i]["dateCreated"].ToString() + "</td>"
                                + "<td>" + dt.Rows[i]["createdBy"].ToString() + "</td>"
                                + "<td>" + dt.Rows[i]["documentId"].ToString() + "</td>"
                                + "<td>" + dt.Rows[i]["Description_of_Assets"].ToString() + "</td>"

                                + "<td><button type='button'   id='" + dt.Rows[i]["nId"].ToString() + "' onClick='Edit(this.id)'   class='btn btn-primary'>Edit</button><button type='button'   id='" + dt.Rows[i]["nId"].ToString() + "' onClick='Delete(this.id)'   class='btn btn-danger '>Delete</button></td>    </tr>";

                }


            }

            return data;
        }





        public int UpdateNominee()
        {
            int index = Convert.ToInt32(Request["send"]);




            return index;
        }




    }
}