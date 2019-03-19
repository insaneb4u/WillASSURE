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
    public class UpdateAppointeesController : Controller
    {
        public static string connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        SqlConnection con = new SqlConnection(connectionString);
        // GET: UpdateAppointees
        public ActionResult UpdateAppointeesIndex(int NestId)
        {
            AppointeesModel Am = new AppointeesModel();
            Am.apId = NestId;

            con.Open();
            string query = "select * from Appointees where apId = "+NestId+"";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            string data = "";

            if (dt.Rows.Count > 0)
            {


                for (int i = 0; i < dt.Rows.Count; i++)
                {
                      
                       
                        Am.Type =dt.Rows[i]["Type"].ToString();
                        Am.subType=dt.Rows[i]["subType"].ToString();
                        Am.Name =dt.Rows[i]["Name"].ToString();
                        Am.middleName=dt.Rows[i]["middleName"].ToString();
                        Am.Surname=dt.Rows[i]["Surname"].ToString();
                        Am.Identity_Proof=dt.Rows[i]["Identity_Proof"].ToString();
                        Am.Identity_Proof_Value =dt.Rows[i]["Identity_Proof_Value"].ToString();
                        Am.Alt_Identity_Proof =dt.Rows[i]["Alt_Identity_Proof"].ToString();
                        Am.Alt_Identity_Proof_Value =dt.Rows[i]["Alt_Identity_Proof_Value"].ToString();
                        Am.DOB = Convert.ToDateTime(dt.Rows[i]["DOB"]);
                        Am.Gender =dt.Rows[i]["Gender"].ToString();
                        Am.Occupation =dt.Rows[i]["Occupation"].ToString();
                        Am.RelationshipTxt =dt.Rows[i]["Relationship"].ToString();
                        Am.Address1 =dt.Rows[i]["Address1"].ToString();
                        Am.Address2 =dt.Rows[i]["Address2"].ToString();
                        Am.Address3 =dt.Rows[i]["Address3"].ToString();
                        Am.citytext =dt.Rows[i]["City"].ToString();
                        Am.statetext=dt.Rows[i]["State"].ToString();
                        Am.Pin =dt.Rows[i]["Pin"].ToString();
                        

                               

                }
            }

            return View("~/Views/UpdateAppointees/UpdateAppointeesPageContent.cshtml", Am);
        }

        public String BindStateDDL()
        {

            con.Open();
            string query = "select * from tbl_state";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            string data = "<option value=''>--Select--</option>";

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
            string data = "<option value='0'>--Select--</option>";

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
            string data = "<option value='0' >--Select--</option>";

            if (dt.Rows.Count > 0)
            {


                for (int i = 0; i < dt.Rows.Count; i++)
                {




                    data = data + "<option value=" + dt.Rows[i]["Rid"].ToString() + " >" + dt.Rows[i]["MemberName"].ToString() + "</option>";



                }




            }

            return data;

        }



        public ActionResult UpdatingAppointeesFormData(AppointeesModel AM)
        {
            AM.documentId = 0;

            con.Open();
            SqlCommand cmd = new SqlCommand("SP_CRUDAppointees", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@condition", "update");
            cmd.Parameters.AddWithValue("@apId", AM.apId);
            cmd.Parameters.AddWithValue("@documentId", AM.documentId);
            cmd.Parameters.AddWithValue("@Type", AM.Typetxt);
            cmd.Parameters.AddWithValue("@subType", AM.subTypetxt);
            cmd.Parameters.AddWithValue("@Name", AM.Name);
            cmd.Parameters.AddWithValue("@middleName", AM.middleName);
            cmd.Parameters.AddWithValue("@Surname", AM.Surname);
            cmd.Parameters.AddWithValue("@Identity_proof", AM.Identity_Proof);
            cmd.Parameters.AddWithValue("@Identity_proof_value", AM.Identity_Proof_Value);
            cmd.Parameters.AddWithValue("@Alt_Identity_proof", AM.Alt_Identity_Proof);
            cmd.Parameters.AddWithValue("@Alt_Identity_proof_value", AM.Alt_Identity_Proof_Value);
            cmd.Parameters.AddWithValue("@DOB", AM.DOB.ToString("dd/MM/yyyy"));
            cmd.Parameters.AddWithValue("@Gender", AM.Gender);
            cmd.Parameters.AddWithValue("@Occupation", AM.Occupation);
            cmd.Parameters.AddWithValue("@Relationship", AM.RelationshipTxt);
            cmd.Parameters.AddWithValue("@Address1", AM.Address1);
            cmd.Parameters.AddWithValue("@Address2", AM.Address2);
            cmd.Parameters.AddWithValue("@Address3", AM.Address3);
            cmd.Parameters.AddWithValue("@City", AM.citytext);
            cmd.Parameters.AddWithValue("@State", AM.statetext);
            cmd.Parameters.AddWithValue("@Pin", AM.Pin);
            cmd.ExecuteNonQuery();
            con.Close();

            return View("~/Views/UpdateAppointees/UpdateAppointeesPageContent.cshtml");
        }
    }
}