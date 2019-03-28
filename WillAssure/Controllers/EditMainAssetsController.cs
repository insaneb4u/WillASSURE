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
using Newtonsoft.Json;

namespace WillAssure.Controllers
{
    public class EditMainAssetsController : Controller
    {
        public static string connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        SqlConnection con = new SqlConnection(connectionString);
        // GET: EditMainAssets
        public ActionResult EditMainAssetsIndex()
        {
            if (Session.SessionID == null)
            {
                return View("~/Views/LoginPage/LoginPageContent.cshtml");
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
            return View("~/Views/EditMainAssets/EditMainAssetsPageContent.cshtml");
        }



        public string BindMappedFormData()
        {

            // check roles
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








            }

            con.Close();





            //end
            string testString = "";

            for (int i = 0; i < Lmlist.Count(); i++)
            {
                testString = Lmlist[9].Action;

            }




            string final = "";
            con.Open();
            string query = "select b.aiid , a.AssetsType , c.AssetsCategory , b.Json from AssetsType a inner join  AssetInformation b on a.atId = b.atId inner join AssetsCategory  c on b.amId = c.amId order by b.aiid asc";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            string data = "";

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string getjson = dt.Rows[i]["Json"].ToString();


                var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(getjson);
                foreach (var kv in dict)
                {
                    final = final + kv.Key + ":" + kv.Value;
                }



                if (testString == "1,2,0" || testString == "0,2,0" || testString == "0,2,3" || testString == "0,2,3" || testString == "0,2,0")
                {
                    data = data + "<tr class='nr'><td>" + dt.Rows[i]["aiid"].ToString() + "</td>";
                    data += "<td>" + dt.Rows[i]["AssetsType"].ToString() + "</td>";
                    data += "<td>" + dt.Rows[i]["AssetsCategory"].ToString() + "</td>";
                    data += "<td>" + final + "</td>";
                    data += "<td><button type='button'   id='" + dt.Rows[i]["aiid"].ToString() + "' onClick='Edit(this.id)'   class='btn btn-primary'>Edit</button></td></tr>";

                }

                if (testString == "1,0,3" || testString == "0,0,3" || testString == "0,2,3" || testString == "1,0,3" || testString == "0,0,3")
                {
                    data = data + "<tr class='nr'><td>" + dt.Rows[i]["aiid"].ToString() + "</td>";
                    data += "<td>" + dt.Rows[i]["AssetsType"].ToString() + "</td>";
                    data += "<td>" + dt.Rows[i]["AssetsCategory"].ToString() + "</td>";
                    data += "<td>" + final + "</td>";
                    data += "<td><button type='button'   id='" + dt.Rows[i]["aiid"].ToString() + "'    class='btn btn-danger deletenotification'>Delete</button>  </td></tr>";

                }


                if (testString == "1,2,3" || testString == "0,2,3")
                {
                    data = data + "<tr class='nr'><td>" + dt.Rows[i]["aiid"].ToString() + "</td>";
                    data += "<td>" + dt.Rows[i]["AssetsType"].ToString() + "</td>";
                    data += "<td>" + dt.Rows[i]["AssetsCategory"].ToString() + "</td>";
                    data += "<td>" + final + "</td>";
                    data += "<td><button type='button'   id='" + dt.Rows[i]["aiid"].ToString() + "' onClick='Edit(this.id)'   class='btn btn-primary'>Edit</button><button type='button'   id='" + dt.Rows[i]["aiid"].ToString() + "'    class='btn btn-danger deletenotification'>Delete</button>  </td></tr>";


                }


                if (testString == "0,0,0")
                {
                    data = data + "<tr class='nr'><td>" + dt.Rows[i]["aiid"].ToString() + "</td>";
                    data += "<td>" + dt.Rows[i]["AssetsType"].ToString() + "</td>";
                    data += "<td>" + dt.Rows[i]["AssetsCategory"].ToString() + "</td>";
                    data += "<td>" + final + "</td>";
                   

                }





            }










        
    

            return data;
        }



        public string DeleteMappedRecords(RoleFormModel RFM)
        {
            string final = "";
            int index = Convert.ToInt32(Request["send"]);

            con.Open();
            string query1 = "delete from AssetInformation where aiid = "+index+" ";
            SqlCommand cmd = new SqlCommand(query1,con);
            cmd.ExecuteNonQuery();
            con.Close();


            // check roles
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








            }

            con.Close();





            //end
            string testString = "";

            for (int i = 0; i < Lmlist.Count(); i++)
            {
                testString = Lmlist[9].Action;

            }




           // string final = "";
            con.Open();
            string query = "select b.aiid , a.AssetsType , c.AssetsCategory , b.Json from AssetsType a inner join  AssetInformation b on a.atId = b.atId inner join AssetsCategory  c on b.amId = c.amId order by b.aiid asc";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            string data = "";

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string getjson = dt.Rows[i]["Json"].ToString();


                var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(getjson);
                foreach (var kv in dict)
                {
                    final = final + kv.Key + ":" + kv.Value;
                }



                if (testString == "1,2,0" || testString == "0,2,0" || testString == "0,2,3" || testString == "0,2,3" || testString == "0,2,0")
                {
                    data = data + "<tr class='nr'><td>" + dt.Rows[i]["aiid"].ToString() + "</td>";
                    data += "<td>" + dt.Rows[i]["AssetsType"].ToString() + "</td>";
                    data += "<td>" + dt.Rows[i]["AssetsCategory"].ToString() + "</td>";
                    data += "<td>" + final + "</td>";
                    data += "<td><button type='button'   id='" + dt.Rows[i]["aiid"].ToString() + "' onClick='Edit(this.id)'   class='btn btn-primary'>Edit</button></td></tr>";

                }

                if (testString == "1,0,3" || testString == "0,0,3" || testString == "0,2,3" || testString == "1,0,3" || testString == "0,0,3")
                {
                    data = data + "<tr class='nr'><td>" + dt.Rows[i]["aiid"].ToString() + "</td>";
                    data += "<td>" + dt.Rows[i]["AssetsType"].ToString() + "</td>";
                    data += "<td>" + dt.Rows[i]["AssetsCategory"].ToString() + "</td>";
                    data += "<td>" + final + "</td>";
                    data += "<td><button type='button'   id='" + dt.Rows[i]["aiid"].ToString() + "'   class='btn btn-danger deletenotification'>Delete</button>  </td></tr>";

                }


                if (testString == "1,2,3" || testString == "0,2,3")
                {
                    data = data + "<tr class='nr'><td>" + dt.Rows[i]["aiid"].ToString() + "</td>";
                    data += "<td>" + dt.Rows[i]["AssetsType"].ToString() + "</td>";
                    data += "<td>" + dt.Rows[i]["AssetsCategory"].ToString() + "</td>";
                    data += "<td>" + final + "</td>";
                    data += "<td><button type='button'   id='" + dt.Rows[i]["aiid"].ToString() + "' onClick='Edit(this.id)'   class='btn btn-primary'>Edit</button><button type='button'   id='" + dt.Rows[i]["aiid"].ToString() + "'    class='btn btn-danger deletenotification'>Delete</button>  </td></tr>";


                }


                if (testString == "0,0,0")
                {
                    data = data + "<tr class='nr'><td>" + dt.Rows[i]["aiid"].ToString() + "</td>";
                    data += "<td>" + dt.Rows[i]["AssetsType"].ToString() + "</td>";
                    data += "<td>" + dt.Rows[i]["AssetsCategory"].ToString() + "</td>";
                    data += "<td>" + final + "</td>";


                }





            }













            return data;
        }





        public int UpdateMappedData()
        {
            int index = Convert.ToInt32(Request["send"]);




            return index;
        }
    }
}