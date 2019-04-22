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
            return View("~/Views/EditMainAssets/EditMainAssetsPageContent.cshtml");
        }



        public string BindMappedFormData(int value)
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
                testString = Lmlist[15].Action;

            }




            string final = "";
            con.Open();
            string query = "select a.aiid , c.AssetsType , d.AssetsCategory , a.tid , a.docid , a.Json from AssetInformation a  inner join TestatorDetails b on a.tid=b.tId inner join AssetsType c on a.atId = c.atId inner join AssetsCategory d on a.amId=d.amId where a.tid =" + value + "   ";
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
                testString = Lmlist[15].Action;

            }




           // string final = "";
            con.Open();
            string query = "select a.aiid , c.AssetsType , d.AssetsCategory , a.tid , a.docid , a.Json from AssetInformation a  inner join TestatorDetails b on a.tid=b.tId inner join AssetsType c on a.atId = c.atId inner join AssetsCategory d on a.amId=d.amId inner join users e on e.uId=b.uId  where e.Linked_user = " + Convert.ToInt32(Session["uuid"]) + "   ";
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


        public string LoadData()
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
                testString = Lmlist[15].Action;

            }

           

            con.Open();
            string checkuid = "select * from TestatorDetails a inner join users b on a.tId=b.uId  where b.uId = " + Convert.ToInt32(Session["uuid"]) + " ";
            SqlDataAdapter checkda = new SqlDataAdapter(checkuid, con);
            DataTable checkdt = new DataTable();
            checkda.Fill(checkdt);
            int chktid = 0;
            if (checkdt.Rows.Count > 0)
            {
                chktid = Convert.ToInt32(checkdt.Rows[0]["tId"]);
            }
            con.Close();

            if (Convert.ToInt32(Session["uuid"]) != 1)
            {
                string final = "";
                con.Open();
                string query = "select a.aiid , c.AssetsType , d.AssetsCategory , a.tid , a.docid , a.Json from AssetInformation a  inner join TestatorDetails b on a.tid=b.tId inner join AssetsType c on a.atId = c.atId inner join AssetsCategory d on a.amId=d.amId where a.tid = " + chktid + "   ";
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
            else
            {
                string final = "";
                con.Open();
                string query = "select a.aiid , c.AssetsType , d.AssetsCategory , a.tid , a.docid , a.Json from AssetInformation a  inner join TestatorDetails b on a.tid=b.tId inner join AssetsType c on a.atId = c.atId inner join AssetsCategory d on a.amId=d.amId    ";
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
          
        }




        public string BindTestatorDDL()
        {

            if (Convert.ToInt32(Session["uuid"]) != 1)
            {
                string ck = "select type from users where uId =" + Convert.ToInt32(Session["uuid"]) + "";
                SqlDataAdapter cda = new SqlDataAdapter(ck, con);
                DataTable cdt = new DataTable();
                cda.Fill(cdt);
                string type = "";
                if (cdt.Rows.Count > 0)
                {
                    type = cdt.Rows[0]["type"].ToString();

                }

                if (type != "Testator")
                {
                    con.Open();
                    string query = "select * from TestatorDetails a   inner join users b on a.uId = b.uId  where b.Linked_user = " + Convert.ToInt32(Session["uuid"]) + " ";
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
                else
                {
                    con.Open();
                    string query = "select * from TestatorDetails a   inner join users b on a.uId = b.uId  where b.uId = " + Convert.ToInt32(Session["uuid"]) + " ";
                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();
                    string data = "";




                    if (dt.Rows.Count > 0)
                    {


                        for (int i = 0; i < dt.Rows.Count; i++)
                        {




                            data = data + "<option value=" + dt.Rows[i]["tId"].ToString() + " >" + dt.Rows[i]["First_Name"].ToString() + "</option>";



                        }




                    }

                    return data;


                }



            }
            else
            {
                con.Open();
                string query = "select * from TestatorDetails";
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


        }


    }
}