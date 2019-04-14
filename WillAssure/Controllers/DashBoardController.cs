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
    public class DashBoardController : Controller
    {
        public static string connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        SqlConnection con = new SqlConnection(connectionString);
        // GET: DashBoard
        public ActionResult DashBoardIndex()
        {
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



            // total count Dashboard

            string q1 = "select count(*) as TotalDistributorAdmin from users  where Linked_user = " + Convert.ToInt32(Session["uuid"]) + " and Type = 'DistributorAdmin'";
            SqlDataAdapter da1 = new SqlDataAdapter(q1, con);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            ViewBag.TotalDistributorAdmin = Convert.ToInt32(dt1.Rows[0]["TotalDistributorAdmin"]);




            string q2 = "select count(*) as TotalWillEmployee from users  where Linked_user = " + Convert.ToInt32(Session["uuid"]) + " and Type = 'WillEmployee'";
            SqlDataAdapter da22 = new SqlDataAdapter(q2, con);
            DataTable dt22 = new DataTable();
            da22.Fill(dt22);
            ViewBag.TotalWillEmployee = Convert.ToInt32(dt22.Rows[0]["TotalWillEmployee"]);



            string q4 = "select count(*) as TotalDistributorEmployee from users  where Linked_user = " + Convert.ToInt32(Session["uuid"]) + " and Type = 'DistributorEmployee'";
            SqlDataAdapter da4 = new SqlDataAdapter(q4, con);
            DataTable dt4 = new DataTable();
            da4.Fill(dt4);
            ViewBag.TotalDistributorEmployee = Convert.ToInt32(dt4.Rows[0]["TotalDistributorEmployee"]);



            string q5 = "select count(*) as TotalTestator  from TestatorDetails a inner join users b on a.uId=b.uId where b.Linked_user = " + Convert.ToInt32(Session["uuid"]) + "";
            SqlDataAdapter da5 = new SqlDataAdapter(q5, con);
            DataTable dt5 = new DataTable();
            da5.Fill(dt5);
            ViewBag.TotalTestator = Convert.ToInt32(dt5.Rows[0]["TotalTestator"]);



            string q6 = "select count(*) as TotalWillEmployee from users  where Linked_user = " + Convert.ToInt32(Session["uuid"]) + " and Type = 'WillEmployee'";
            SqlDataAdapter da6 = new SqlDataAdapter(q6, con);
            DataTable dt6 = new DataTable();
            da6.Fill(dt6);
            ViewBag.TotalWillEmployee = Convert.ToInt32(dt6.Rows[0]["TotalWillEmployee"]);




            string q7 = "select count(*) as TotalFamily from testatorFamily a inner join TestatorDetails b on a.tId=b.tId inner join users c on b.uId = c.uId where c.Linked_user = " + Convert.ToInt32(Session["uuid"]) + "";
            SqlDataAdapter da7 = new SqlDataAdapter(q7, con);
            DataTable dt7 = new DataTable();
            da7.Fill(dt7);
            ViewBag.TotalFamily = Convert.ToInt32(dt7.Rows[0]["TotalFamily"]);



            string q8 = "select count(*) as TotalAssetInformation from AssetInformation a  inner join TestatorDetails b on a.tid=b.tId inner join AssetsType c on a.atId = c.atId inner join AssetsCategory d on a.amId=d.amId inner join users e on e.uId=b.uId  where e.Linked_user = " + Convert.ToInt32(Session["uuid"]) + "";
            SqlDataAdapter da8 = new SqlDataAdapter(q8, con);
            DataTable dt8 = new DataTable();
            da8.Fill(dt8);
            ViewBag.TotalAssetInformation = Convert.ToInt32(dt8.Rows[0]["TotalAssetInformation"]);



            string q9 = "select count(*) as TotalBeneficiary from BeneficiaryDetails a inner join TestatorDetails b on a.tId=b.tId inner join users c on b.uId = c.uId where c.Linked_user = " + Convert.ToInt32(Session["uuid"]) + "";
            SqlDataAdapter da9 = new SqlDataAdapter(q9, con);
            DataTable dt9 = new DataTable();
            da9.Fill(dt9);
            ViewBag.TotalBeneficiary = Convert.ToInt32(dt9.Rows[0]["TotalBeneficiary"]);



            string q10 = "select count(*) as TotalNominee from Nominee a inner join TestatorDetails b on a.tId=b.tId inner join users c on b.uId=c.uId where c.Linked_user = " + Convert.ToInt32(Session["uuid"]) + "";
            SqlDataAdapter da10 = new SqlDataAdapter(q10, con);
            DataTable dt10 = new DataTable();
            da10.Fill(dt10);
            ViewBag.TotalNominee = Convert.ToInt32(dt10.Rows[0]["TotalNominee"]);




            string q11 = "select count(*) as TotalAppointees  from Appointees a inner join  TestatorDetails b on a.tid=b.tid inner join users c on b.uId=c.uId where c.Linked_user = " + Convert.ToInt32(Session["uuid"]) + "";
            SqlDataAdapter da11 = new SqlDataAdapter(q11, con);
            DataTable dt11 = new DataTable();
            da11.Fill(dt11);
            ViewBag.TotalAppointees = Convert.ToInt32(dt11.Rows[0]["TotalAppointees"]);





            con.Close();



            //end





            return View("~/Views/DashBoard/DashBoardPageContent.cshtml");
        }
    }
}