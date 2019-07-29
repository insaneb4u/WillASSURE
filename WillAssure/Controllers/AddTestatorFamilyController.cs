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
        public ActionResult AddTestatorFamilyIndex(string success , string NestId)
        {


       


         




            if (TempData["Message"] != null)
            {
                if (TempData["Message"].ToString() == "true")
                {
                    ViewBag.Message = "Verified";
                }
            }


            ViewBag.view = "Will";
            ViewBag.collapse = "true";
            // check type 
            string typ = "";
            con.Open();
            string qq1 = "select Type from users where uId = " + Convert.ToInt32(Session["uuid"]) + " ";
            SqlDataAdapter daa = new SqlDataAdapter(qq1, con);
            DataTable dtt = new DataTable();
            daa.Fill(dtt);
            con.Close();

            if (dtt.Rows.Count > 0)
            {
                typ = dtt.Rows[0]["Type"].ToString();
            }



            //end



            if (typ == "Testator")
            {

                
                con.Open();
                string qq12 = "select Type from users where uId = " + Convert.ToInt32(Session["uuid"]) + " and designation = 1 ";
                SqlDataAdapter da42 = new SqlDataAdapter(qq12, con);
                DataTable d4t2 = new DataTable();
                da42.Fill(d4t2);
                con.Close();

                if (d4t2.Rows.Count > 0)
                {
                    ViewBag.documentlink = "true";
                }


                // check will status
                con.Open();
                string qry1 = "select Will  from users where Will = 1 ";
                SqlDataAdapter daa1 = new SqlDataAdapter(qry1, con);
                DataTable dtt1 = new DataTable();
                daa1.Fill(dtt1);
                if (dtt1.Rows.Count > 0)
                {
                    ViewBag.documentbtn1 = "true";
                }
                con.Close();
                //end


                // check codocil status
                con.Open();
                string qry2 = "select Codocil  from users where Codocil = 1 ";
                SqlDataAdapter daa2 = new SqlDataAdapter(qry2, con);
                DataTable dtt2 = new DataTable();
                daa2.Fill(dtt2);
                if (dtt2.Rows.Count > 0)
                {
                    ViewBag.documentbtn2 = "true";
                }
                con.Close();

                //end


                // check Poa status
                con.Open();
                string qry4 = "select POA  from users where POA = 1 ";
                SqlDataAdapter daa4 = new SqlDataAdapter(qry4, con);
                DataTable dtt4 = new DataTable();
                daa4.Fill(dtt4);
                if (dtt4.Rows.Count > 0)
                {
                    ViewBag.documentbtn3 = "true";
                }
                con.Close();
                //end


                // check gift deeds status
                con.Open();
                string qry3 = "select Giftdeeds  from users where Giftdeeds = 1 ";
                SqlDataAdapter daa3 = new SqlDataAdapter(qry3, con);
                DataTable dtt3 = new DataTable();
                daa3.Fill(dtt3);
                if (dtt3.Rows.Count > 0)
                {
                    ViewBag.documentbtn4 = "true";
                }
                con.Close();
                //end
            }
            else
            {

                ViewBag.showtitle = "true";
                ViewBag.documentlink = "true";

            }






            if (Session["rId"] == null || Session["uuid"] == null)
            {

               RedirectToAction("LoginPageIndex", "LoginPage");

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











            TestatorFamilyModel TFM = new TestatorFamilyModel();

            string query = "";


            con.Open();

            if (NestId != null)
            {
                query = "select * from testatorFamily where fId = " + NestId + "";
            }
            else
            {
                query = "select * from testatorFamily where tId = " + Convert.ToInt32(Session["distid"]) + "";
            }

           



            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            string data = "";
            int alternatetfid = 0;

            if (dt.Rows.Count > 0)
            {
                ViewBag.disablefield = "true";

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    TFM.fId = Convert.ToInt32(dt.Rows[i]["fId"]);
                    alternatetfid = Convert.ToInt32(dt.Rows[i]["fId"]);
                    TFM.First_Name = dt.Rows[i]["First_Name"].ToString();
                    TFM.Last_Name = dt.Rows[i]["Last_Name"].ToString();
                    TFM.Middle_Name = dt.Rows[i]["Middle_Name"].ToString();
                    TFM.Dob = Convert.ToDateTime(dt.Rows[0]["DOB"]).ToString("dd-MM-yyyy");
                    TFM.Marital_Status = dt.Rows[i]["Marital_Status"].ToString();
                    TFM.Religion = dt.Rows[i]["Religion"].ToString();
                    if (dt.Rows[i]["Relationship"].ToString() != "None")
                    {
                        TFM.RelationshipTxt = dt.Rows[i]["Relationship"].ToString();
                    }
                   
                    TFM.Address1 = dt.Rows[i]["Address1"].ToString();
                    TFM.Address2 = dt.Rows[i]["Address2"].ToString();
                    TFM.Address3 = dt.Rows[i]["Address3"].ToString();
                    TFM.City_txt = dt.Rows[i]["City"].ToString();
                    TFM.State_txt = dt.Rows[i]["State"].ToString();
                    TFM.Pin = dt.Rows[i]["Pin"].ToString();

                    TFM.active = dt.Rows[i]["active"].ToString();
                    TFM.Identity_Proof = dt.Rows[i]["Identity_Proof"].ToString();
                    TFM.Identity_Proof_Value = dt.Rows[i]["Identity_Proof_Value"].ToString();
                    TFM.Alt_Identity_Proof = dt.Rows[i]["Alt_Identity_Proof"].ToString();
                    TFM.Alt_Identity_Proof_Value = dt.Rows[i]["Alt_Identity_Proof_Value"].ToString();
                    TFM.Is_Informed_Person = dt.Rows[i]["Is_Informed_Person"].ToString();



                }
            }







            string query4 = "";

            con.Open();

      
            query4 = "select * from alttestatorFamily where testatorfamilyid = " + alternatetfid + "";
        
            





            SqlDataAdapter da4 = new SqlDataAdapter(query4, con);
            DataTable dt4 = new DataTable();
            da4.Fill(dt4);
            con.Close();
            string data4 = "";

            if (dt4.Rows.Count > 0)
            {
                ViewBag.disablefield = "true";
                ViewBag.alternate = "true";

                for (int i = 0; i < dt4.Rows.Count; i++)
                {
                    TFM.altfId = Convert.ToInt32(dt4.Rows[i]["altfId"]);
                    TFM.altFirst_Name = dt4.Rows[i]["altFirst_Name"].ToString();
                    TFM.altLast_Name = dt4.Rows[i]["altLast_Name"].ToString();
                    TFM.altMiddle_Name = dt4.Rows[i]["altMiddle_Name"].ToString();
                    TFM.altDob = Convert.ToDateTime(dt4.Rows[0]["altDOB"]).ToString("dd-MM-yyyy");
                    TFM.altMarital_Status = dt4.Rows[i]["altMarital_Status"].ToString();
                    TFM.altReligion = dt4.Rows[i]["altReligion"].ToString();
                    TFM.altRelationshipTxt = dt4.Rows[i]["altRelationship"].ToString();
                    TFM.altAddress1 = dt4.Rows[i]["altAddress1"].ToString();
                    TFM.altAddress2 = dt4.Rows[i]["altAddress2"].ToString();
                    TFM.altAddress3 = dt4.Rows[i]["altAddress3"].ToString();
                    TFM.altCity_txt = dt4.Rows[i]["altCity"].ToString();
                    TFM.altState_txt = dt4.Rows[i]["altState"].ToString();
                    TFM.altPin = dt4.Rows[i]["altPin"].ToString();

                    
                    TFM.altIdentity_Proof = dt4.Rows[i]["altIdentity_Proof"].ToString();
                    TFM.altIdentity_Proof_Value = dt4.Rows[i]["altIdentity_Proof_Value"].ToString();
                    TFM.Alt_Identity_Proof = dt4.Rows[i]["altAlt_Identity_Proof"].ToString();
                    TFM.Alt_Identity_Proof_Value = dt4.Rows[i]["altAlt_Identity_Proof_Value"].ToString();
                    TFM.Is_Informed_Person = dt4.Rows[i]["altIs_Informed_Person"].ToString();



                }
            }








            

            string query33 = "select * from Appointees where tfId = " + Convert.ToInt32(Session["distid"]) + "";
        





        SqlDataAdapter da33 = new SqlDataAdapter(query33, con);
        DataTable dt33 = new DataTable();
        da33.Fill(dt33);
                    con.Close();
                   

                    if (dt33.Rows.Count > 0)
                    {


                        for (int i = 0; i< dt33.Rows.Count; i++)
                        {
                            ViewBag.guardian = "true";
                            TFM.guaapId = Convert.ToInt32(dt33.Rows[i]["apId"]);
                            TFM.guaTypetxt = dt33.Rows[i]["Type"].ToString();
        TFM.guasubTypetxt = dt33.Rows[i]["subType"].ToString();
        TFM.guaName = dt33.Rows[i]["Name"].ToString();
        TFM.guamiddleName = dt33.Rows[i]["middleName"].ToString();
        TFM.guaSurname = dt33.Rows[i]["Surname"].ToString();
        TFM.guaIdentity_Proof = dt33.Rows[i]["Identity_Proof"].ToString();
        TFM.guaIdentity_Proof_Value = dt33.Rows[i]["Identity_Proof_Value"].ToString();
        TFM.guaAlt_Identity_Proof = dt33.Rows[i]["Alt_Identity_Proof"].ToString();
        TFM.guaAlt_Identity_Proof_Value = dt33.Rows[i]["Alt_Identity_Proof_Value"].ToString();
        //TFM.Dob = Convert.ToDateTime(dt33.Rows[0]["DOB"]).ToString("dd-MM-yyyy");
        TFM.guaGender = dt33.Rows[i]["Gender"].ToString();
        TFM.guaOccupation = dt33.Rows[i]["Occupation"].ToString();
        TFM.guaRelationshipTxt = dt33.Rows[i]["Relationship"].ToString();
        TFM.guaAddress1 = dt33.Rows[i]["Address1"].ToString();
        TFM.guaAddress2 = dt33.Rows[i]["Address2"].ToString();
        TFM.guaAddress3 = dt33.Rows[i]["Address3"].ToString();
        TFM.guacitytext = dt33.Rows[i]["City"].ToString();
        TFM.guastatetext = dt33.Rows[i]["State"].ToString();
        TFM.guaPin = dt33.Rows[i]["Pin"].ToString();




    }
}




            return View("~/Views/AddTestatorFamily/AddTestatorFamilyPageContent.cshtml", TFM);
        }


        public String guaBindCountryDDL()
        {

            con.Open();
            string query = "select distinct * from country_tbl order by CountryName asc  ";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            string data = "";

            if (dt.Rows.Count > 0)
            {


                for (int i = 0; i < dt.Rows.Count; i++)
                {




                    data = data + "<option value=" + dt.Rows[i]["CountryID"].ToString() + " >" + dt.Rows[i]["CountryName"].ToString() + "</option>";



                }




            }

            return data;

        }



        public String BindCountryDDL()
        {

            con.Open();
            string query = "select distinct * from country_tbl order by CountryName asc  ";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            string data = "";

            if (dt.Rows.Count > 0)
            {


                for (int i = 0; i < dt.Rows.Count; i++)
                {




                    data = data + "<option value=" + dt.Rows[i]["CountryID"].ToString() + " >" + dt.Rows[i]["CountryName"].ToString() + "</option>";



                }




            }

            return data;

        }


        public string Onnamebindtfcity()
        {
            string response = Request["send"];
            con.Open();
            string query = "select distinct b.id  , b.city_name  from  tbl_state a inner join tbl_city b on  a.state_id=b.state_id where a.statename = '" + response + "' order by b.city_name asc";
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


        public string dateofbirth()
        {

            string data = "<option Value=''>--Select--</option>";
            string date = Request["send"].ToString();
            date = date.Substring(6, date.Length - 6);
            var today = DateTime.Now.Year;
            int age = today - int.Parse(date);
          
            string msg = "";
            if (date != null)
            {
                // identify if guardian or not

              
                if (age != 0)
                {
                    if (age <= 18)
                    {
                        ViewBag.guardianform = "true";
                        msg = "true";


                        con.Open();
                        string query1 = "select ab.Rid , ab.MemberName , year(getdate())-year(ab.DOB) as age  from RelationShip ab where ( year(getdate())-year(ab.DOB)) <= 18";
                        SqlDataAdapter da1 = new SqlDataAdapter(query1, con);
                        DataTable dt1 = new DataTable();
                        da1.Fill(dt1);
                        con.Close();


                        if (dt1.Rows.Count > 0)
                        {


                            for (int i = 0; i < dt1.Rows.Count; i++)
                            {




                                data = data + "<option value=" + dt1.Rows[i]["Rid"].ToString() + " >" + dt1.Rows[i]["MemberName"].ToString() + "</option>";



                            }




                        }

                        
                    }
                    else
                    {
                        msg = "false";

                        con.Open();
                        string query2 = "select ab.Rid , ab.MemberName , year(getdate())-year(ab.DOB) as age  from RelationShip ab where ( year(getdate())-year(ab.DOB)) >= 18";
                        SqlDataAdapter da2 = new SqlDataAdapter(query2, con);
                        DataTable dt2 = new DataTable();
                        da2.Fill(dt2);
                        con.Close();


                        if (dt2.Rows.Count > 0)
                        {


                            for (int i = 0; i < dt2.Rows.Count; i++)
                            {




                                data = data + "<option value=" + dt2.Rows[i]["Rid"].ToString() + " >" + dt2.Rows[i]["MemberName"].ToString() + "</option>";



                            }




                        }

                        

                    }

                }
                else
                {
                    con.Open();
                    string query1 = "select ab.Rid , ab.MemberName , year(getdate())-year(ab.DOB) as age  from RelationShip ab where ( year(getdate())-year(ab.DOB)) <= 18";
                    SqlDataAdapter da1 = new SqlDataAdapter(query1, con);
                    DataTable dt1 = new DataTable();
                    da1.Fill(dt1);
                    con.Close();


                    if (dt1.Rows.Count > 0)
                    {


                        for (int i = 0; i < dt1.Rows.Count; i++)
                        {




                            data = data + "<option value=" + dt1.Rows[i]["Rid"].ToString() + " >" + dt1.Rows[i]["MemberName"].ToString() + "</option>";



                        }




                    }
                }
               



                //end

            }


            return msg + "~" + data;

        }
           





              
        



        //public String BindRelationDDL()
        //{

        //    con.Open();
        //    string query = "select * from relationship";
        //    SqlDataAdapter da = new SqlDataAdapter(query, con);
        //    DataTable dt = new DataTable();
        //    da.Fill(dt);
        //    con.Close();
        //    string data = "";

        //    if (dt.Rows.Count > 0)
        //    {


        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {




        //            data = data + "<option value=" + dt.Rows[i]["Rid"].ToString() + " >" + dt.Rows[i]["MemberName"].ToString() + "</option>";



        //        }




        //    }

        //    return data;

        //}




        public String AltBindRelationDDL()
        {

            con.Open();
            string query = "select * from relationship";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            string data = "";

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
            string query = "select distinct * from tbl_state where country_id = '108' order by statename asc   ";
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



        public string Onnamebindguacity()
        {
            string response = Request["send"];
            con.Open();
            string query = "select distinct b.id  , b.city_name  from  tbl_state a inner join tbl_city b on  a.state_id=b.state_id where a.statename = '"+response+"' order by b.city_name asc";
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

            ViewBag.collapse = "true";
            // check type 
            string typ = "";
            con.Open();
            string qq1 = "select Type from users where uId = " + Convert.ToInt32(Session["uuid"]) + " ";
            SqlDataAdapter daa = new SqlDataAdapter(qq1, con);
            DataTable dtt = new DataTable();
            daa.Fill(dtt);
            con.Close();

            if (dtt.Rows.Count > 0)
            {
                typ = dtt.Rows[0]["Type"].ToString();
            }



            //end



            if (typ == "Testator")
            {
                // check will status
                con.Open();
                string qry1 = "select Will  from users where Will = 1 ";
                SqlDataAdapter daa1 = new SqlDataAdapter(qry1, con);
                DataTable dtt1 = new DataTable();
                daa1.Fill(dtt1);
                if (dtt1.Rows.Count > 0)
                {
                    ViewBag.documentbtn1 = "true";
                }
                con.Close();
                //end


                // check codocil status
                con.Open();
                string qry2 = "select Codocil  from users where Codocil = 1 ";
                SqlDataAdapter daa2 = new SqlDataAdapter(qry2, con);
                DataTable dtt2 = new DataTable();
                daa2.Fill(dtt2);
                if (dtt2.Rows.Count > 0)
                {
                    ViewBag.documentbtn2 = "true";
                }
                con.Close();

                //end


                // check Poa status
                con.Open();
                string qry4 = "select POA  from users where POA = 1 ";
                SqlDataAdapter daa4 = new SqlDataAdapter(qry4, con);
                DataTable dtt4 = new DataTable();
                daa4.Fill(dtt4);
                if (dtt4.Rows.Count > 0)
                {
                    ViewBag.documentbtn3 = "true";
                }
                con.Close();
                //end


                // check gift deeds status
                con.Open();
                string qry3 = "select Giftdeeds  from users where Giftdeeds = 1 ";
                SqlDataAdapter daa3 = new SqlDataAdapter(qry3, con);
                DataTable dtt3 = new DataTable();
                daa3.Fill(dtt3);
                if (dtt3.Rows.Count > 0)
                {
                    ViewBag.documentbtn4 = "true";
                }
                con.Close();
                //end
            }
            else
            {
                ViewBag.showtitle = "true";
                ViewBag.documentlink = "true";

            }

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
           
            string dd = Convert.ToDateTime(TFM.Dob).ToString("dd-MM-yyyy");
            var d = DateTime.ParseExact(dd, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            //DateTime fromdat = DateTime.ParseExact(TFM.Dob, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            cmd.Parameters.AddWithValue("@DOB", d);
                cmd.Parameters.AddWithValue("@Marital_Status", "none");
                cmd.Parameters.AddWithValue("@Religion", "none");
            if (TFM.RelationshipTxt != "")
            {
                
                cmd.Parameters.AddWithValue("@Relationship", TFM.RelationshipTxt);
            }
            else
            {
                TFM.RelationshipTxt = "None";
                cmd.Parameters.AddWithValue("@Relationship", TFM.RelationshipTxt);
            }
               
                
                cmd.Parameters.AddWithValue("@Address1", TFM.Address1);
                cmd.Parameters.AddWithValue("@Address2", TFM.Address2);
                cmd.Parameters.AddWithValue("@Address3", TFM.Address3);
                cmd.Parameters.AddWithValue("@City", TFM.City_txt);
                cmd.Parameters.AddWithValue("@State", TFM.State_txt);
                cmd.Parameters.AddWithValue("@Pin", TFM.Pin);
                cmd.Parameters.AddWithValue("@tId", TFM.ddltid);

            if (TFM.active != null && TFM.active != "")
            {
                cmd.Parameters.AddWithValue("@active", TFM.active);
            }
            else
            {
                TFM.active = "Active";
                cmd.Parameters.AddWithValue("@active", TFM.active);
            }
                




            cmd.Parameters.AddWithValue("@Identity_Proof", TFM.Identity_Proof);
                cmd.Parameters.AddWithValue("@Identity_Proof_Value", TFM.Identity_Proof_Value);
                cmd.Parameters.AddWithValue("@Alt_Identity_Proof", TFM.Alt_Identity_Proof);

            if (TFM.Alt_Identity_Proof_Value != null)
            {
                cmd.Parameters.AddWithValue("@Alt_Identity_Proof_Value", TFM.Alt_Identity_Proof_Value);
            }
            else
            {
                TFM.Alt_Identity_Proof_Value = "None";
                cmd.Parameters.AddWithValue("@Alt_Identity_Proof_Value", TFM.Alt_Identity_Proof_Value);
            }

               



            cmd.Parameters.AddWithValue("@Is_Informed_Person", "none");
                cmd.ExecuteNonQuery();
                con.Close();


            // lastest tfid 
            int fid = 0;
            con.Open();
            string qchecke = "select max(fId) as fId from testatorFamily ";
            SqlDataAdapter dachke = new SqlDataAdapter(qchecke, con);
            DataTable dtchke = new DataTable();
            dachke.Fill(dtchke);
            if (dtchke.Rows.Count > 0)
            {
                fid = Convert.ToInt32(dtchke.Rows[0]["fId"]);
            }


            string qt2 = "update testatorFamily set Country='"+TFM.country_txt+"' ,  WillType='"+Session["WillType"].ToString()+ "' where fId=" + fid + "";
            SqlCommand cmd33e2 = new SqlCommand(qt2, con);
            cmd33e2.ExecuteNonQuery();
            con.Close();


            ViewBag.message = "Verified";
            con.Close();


            // guardian form details
            if (TFM.guardiancheck == "true")
            {

                con.Open();
                SqlCommand cmd3 = new SqlCommand("SP_CRUDAppointees", con);
                cmd3.CommandType = CommandType.StoredProcedure;
                cmd3.Parameters.AddWithValue("@condition", "insert");
                cmd3.Parameters.AddWithValue("@documentId", TFM.guadocumentId);
                cmd3.Parameters.AddWithValue("@Type", "Guardian");

                if (TFM.guasubTypetxt != null || TFM.guasubTypetxt != "")
                {
                    cmd3.Parameters.AddWithValue("@subType", TFM.guasubTypetxt);
                }
                else
                {
                    cmd3.Parameters.AddWithValue("@subType", "None");
                }






                cmd3.Parameters.AddWithValue("@Name", TFM.guaName);
                cmd3.Parameters.AddWithValue("@middleName", TFM.guamiddleName);
                cmd3.Parameters.AddWithValue("@Surname", TFM.guaSurname);
                cmd3.Parameters.AddWithValue("@Identity_proof", TFM.guaIdentity_Proof);
                cmd3.Parameters.AddWithValue("@Identity_proof_value", TFM.guaIdentity_Proof_Value);


                if (TFM.Alt_Identity_Proof != null)
                {
                    cmd3.Parameters.AddWithValue("@Alt_Identity_proof", TFM.guaAlt_Identity_Proof);
                }
                else
                {
                    TFM.Alt_Identity_Proof = "None";
                    cmd3.Parameters.AddWithValue("@Alt_Identity_proof", TFM.guaAlt_Identity_Proof);
                }


                if (TFM.Alt_Identity_Proof_Value != null)
                {
                    cmd3.Parameters.AddWithValue("@Alt_Identity_proof_value", TFM.guaAlt_Identity_Proof_Value);
                }
                else
                {
                    TFM.Alt_Identity_Proof_Value = "None";
                    cmd3.Parameters.AddWithValue("@Alt_Identity_proof_value", TFM.guaAlt_Identity_Proof_Value);
                }









                //DateTime dat = DateTime.ParseExact(TFM.Dob, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                //cmd.Parameters.AddWithValue("@DOB", "None");
                cmd3.Parameters.AddWithValue("@Gender", TFM.guaGender);
                cmd3.Parameters.AddWithValue("@Occupation", "None");
                cmd3.Parameters.AddWithValue("@Relationship", "None");
                cmd3.Parameters.AddWithValue("@Address1", TFM.guaAddress1);
                if (TFM.guaAddress2 != null || TFM.guaAddress2 == "")
                {
                    cmd3.Parameters.AddWithValue("@Address2", TFM.guaAddress2);
                }
                else
                {
                    TFM.guaAddress2 = "None";
                    cmd3.Parameters.AddWithValue("@Address2", TFM.guaAddress2);
                }


                if (TFM.guaAddress3 != null || TFM.guaAddress3 == "")
                {
                    cmd3.Parameters.AddWithValue("@Address3", TFM.guaAddress3);
                }
                else
                {
                    TFM.guaAddress3 = "None";
                    cmd3.Parameters.AddWithValue("@Address3", TFM.guaAddress3);
                }


                cmd3.Parameters.AddWithValue("@City", TFM.guacitytext);
                cmd3.Parameters.AddWithValue("@State", TFM.guastatetext);
                cmd3.Parameters.AddWithValue("@Pin", TFM.guaPin);
                cmd3.Parameters.AddWithValue("@tid", Convert.ToInt32(Session["distid"]));
                cmd3.Parameters.AddWithValue("@ExecutorType", "None");
                cmd3.ExecuteNonQuery();
                con.Close();

                // lastest app id
                int appid = 0;
                con.Open();
                string qcheckee = "select max(apId) as apId from Appointees ";
                SqlDataAdapter dachkee = new SqlDataAdapter(qcheckee, con);
                DataTable dtchkee = new DataTable();
                dachkee.Fill(dtchkee);
                if (dtchkee.Rows.Count > 0)
                {
                    appid = Convert.ToInt32(dtchkee.Rows[0]["apId"]);
                }


                string qt = "update Appointees set Country='" + TFM.guacountrytext + "' ,  documentstatus='incompleted' , tfid=" + fid + " where apId = " + appid + "";
                SqlCommand cmd33e = new SqlCommand(qt, con);
                cmd33e.ExecuteNonQuery();
                con.Close();



            }




            //end





            con.Open();
            SqlCommand cmd2 = new SqlCommand("SP_CRUDBeneficiaryDetails", con);
            cmd2.CommandType = System.Data.CommandType.StoredProcedure;
            cmd2.Parameters.AddWithValue("@condition", "insert");
            cmd2.Parameters.AddWithValue("@First_Name ", TFM.First_Name);
            cmd2.Parameters.AddWithValue("@Last_Name", TFM.Last_Name);
            cmd2.Parameters.AddWithValue("@Middle_Name", TFM.Middle_Name);
            
            cmd2.Parameters.AddWithValue("@DOB", Convert.ToDateTime(TFM.Dob));
            cmd2.Parameters.AddWithValue("@Mobile", "None");
            cmd2.Parameters.AddWithValue("@Relationship", "none");
            cmd2.Parameters.AddWithValue("@Marital_Status", "none");
            cmd2.Parameters.AddWithValue("@Religion", "none");
            cmd2.Parameters.AddWithValue("@Identity_proof", TFM.Identity_Proof);
            cmd2.Parameters.AddWithValue("@Identity_proof_value", TFM.Identity_Proof_Value);

            if (TFM.Alt_Identity_Proof != null)
            {
                cmd2.Parameters.AddWithValue("@Alt_Identity_proof", TFM.Alt_Identity_Proof);
            }
            else
            {
                TFM.Alt_Identity_Proof = "None";
                cmd2.Parameters.AddWithValue("@Alt_Identity_proof", TFM.Alt_Identity_Proof);
            }

           

            if (TFM.Alt_Identity_Proof_Value != null)
            {
                cmd2.Parameters.AddWithValue("@Alt_Identity_proof_value", TFM.Alt_Identity_Proof_Value);
            }
            else
            {
                TFM.Alt_Identity_Proof_Value = "none";
                cmd2.Parameters.AddWithValue("@Alt_Identity_proof_value", TFM.Alt_Identity_Proof_Value);
            }

            

            cmd2.Parameters.AddWithValue("@Address1", TFM.Address1);
            if (TFM.Address2 != null || TFM.Address2 == "")
            {
                cmd2.Parameters.AddWithValue("@Address2", TFM.Address2);

            }
            else
            {
                TFM.Address2 = "None";
                cmd2.Parameters.AddWithValue("@Address2", TFM.Address2);
            }


            if (TFM.Address3 != null || TFM.Address3 == "")
            {
                cmd2.Parameters.AddWithValue("@Address3", TFM.Address3);

            }
            else
            {
                TFM.Address3 = "None";
                cmd2.Parameters.AddWithValue("@Address3", TFM.Address3);
            }


            cmd2.Parameters.AddWithValue("@City", TFM.City_txt);
            cmd2.Parameters.AddWithValue("@State", TFM.State_txt);
            cmd2.Parameters.AddWithValue("@Pin", TFM.Pin);
            cmd2.Parameters.AddWithValue("@aid", 0);
            cmd2.Parameters.AddWithValue("@tid", TFM.ddltid);
            cmd2.Parameters.AddWithValue("@beneficiary_type", "TestatorFamily");
            cmd2.ExecuteNonQuery();
            con.Close();



            con.Open();
            string QUERY = "select top 1 fId from testatorfamily order by fId desc";
            SqlDataAdapter DA = new SqlDataAdapter(QUERY,con);
            DataTable DT = new DataTable();
            DA.Fill(DT);
            int tfid = 0;
            if (DT.Rows.Count > 0)
            {
                tfid = Convert.ToInt32(DT.Rows[0]["fId"]);
            }

            con.Close();



            con.Open();
            string QUERY2 = "select top 1 bpId from BeneficiaryDetails order by bpId desc";
            SqlDataAdapter DA2 = new SqlDataAdapter(QUERY2, con);
            DataTable DT2 = new DataTable();
            DA2.Fill(DT2);
            int bpId = 0;
            if (DT2.Rows.Count > 0)
            {
                bpId = Convert.ToInt32(DT2.Rows[0]["bpId"]);
            }

            con.Close();



            // set document status
            con.Open();
            string querye = "update testatorfamily set documentstatus = 'incompleted'  where  fId = "+tfid+"";
            SqlCommand cmd3e = new SqlCommand(querye, con);
            cmd3e.ExecuteNonQuery();
            con.Close();

            con.Open();
            string querye2 = "update BeneficiaryDetails set doctype = '"+Session["doctype"].ToString()+ "'  where  bpId = " + bpId + "";
            SqlCommand cmd3e2 = new SqlCommand(querye2, con);
            cmd3e2.ExecuteNonQuery();
            con.Close();

            //end



            //  for beneficiary details


            con.Open();
            string QUERYe = "select top 1 bpId from BeneficiaryDetails order by bpId desc";
            SqlDataAdapter DAe = new SqlDataAdapter(QUERYe, con);
            DataTable DTe = new DataTable();
            DAe.Fill(DTe);
            int tfide = 0;
            if (DTe.Rows.Count > 0)
            {
                tfide = Convert.ToInt32(DTe.Rows[0]["bpId"]);
            }

            con.Close();



            // set document status
            con.Open();
            string queryee = "update BeneficiaryDetails set documentstatus = 'incompleted' , WillType='"+Session["WillType"].ToString()+"'  where  bpId = " + tfide + "";
            SqlCommand cmd3ee = new SqlCommand(queryee, con);
            cmd3ee.ExecuteNonQuery();
            con.Close();



            //end













            // alternate testator family
            if (TFM.altchek == "true")
            {

               


                if (TFM.altMarital_Status == null)
                {
                    TFM.altMarital_Status = "None";
                }


                if (TFM.altReligion == null)
                {
                    TFM.altReligion = "None";
                }


                if (TFM.altAddress2 == null)
                {
                    TFM.altAddress2 = "None";
                }

                if (TFM.altAddress3 == null)
                {
                    TFM.altAddress3 = "None";
                }


                if (TFM.altactive == null)
                {
                    TFM.altactive = "None";
                }


                if (TFM.altIdentity_Proof == null)
                {
                    TFM.altIdentity_Proof = "None";
                }




                if (TFM.altIdentity_Proof_Value == null)
                {
                    TFM.altIdentity_Proof_Value = "None";
                }



                if (TFM.altAlt_Identity_Proof == null)
                {
                    TFM.altAlt_Identity_Proof = "None";
                }




                if (TFM.altAlt_Identity_Proof_Value == null)
                {
                    TFM.altAlt_Identity_Proof_Value = "None";
                }


                if (TFM.altIs_Informed_Person == null)
                {
                    TFM.altIs_Informed_Person = "None";
                }




                con.Open();
                string query = "insert into alttestatorFamily (altFirst_Name , altLast_Name , altMiddle_Name , altDOB , altMarital_Status , altReligion , altRelationship , altAddress1 , altAddress2 , altAddress3 , altCity , altState , altPin  , altactive , altIdentity_Proof , altIdentity_Proof_Value , altAlt_Identity_Proof , altAlt_Identity_Proof_Value , altIs_Informed_Person , testatorfamilyid , alttId) values ('" + TFM.altFirst_Name+"' , '"+TFM.altLast_Name+"' , '"+TFM.Middle_Name+"' , '"+ Convert.ToDateTime(TFM.altDob).ToString("yyyy-MM-dd") + "' , '"+TFM.altMarital_Status+"' , '"+TFM.altReligion+"' , '"+TFM.RelationshipTxt+"' , '"+TFM.altAddress1+ "' , '" + TFM.altAddress2 + "' , '"+TFM.altAddress3+"' , '"+TFM.City_txt+"' , '"+TFM.altState_txt+"' , '"+TFM.altPin+"'  ,  '"+TFM.altactive+ "' , '"+TFM.altIdentity_Proof + "' , '"+TFM.altIdentity_Proof_Value + "' , '"+TFM.altAlt_Identity_Proof + "' , '"+TFM.altAlt_Identity_Proof_Value + "' , '"+TFM.altIs_Informed_Person + "' , "+tfid+ " ,  "+TFM.ddltid+"  )  ";
                SqlCommand cmd3 = new SqlCommand(query,con);
                cmd3.ExecuteNonQuery();
                con.Close();



                // alternate beneficiary 
                con.Open();
                SqlCommand altcmd = new SqlCommand("SP_CRUD_alternate_Beneficiary", con);
                altcmd.CommandType = CommandType.StoredProcedure;
                altcmd.Parameters.AddWithValue("@condition", "insert");
            
                    altcmd.Parameters.AddWithValue("@bpId", "0");
                

                altcmd.Parameters.AddWithValue("@First_Name", TFM.altFirst_Name);
                altcmd.Parameters.AddWithValue("@Last_Name", TFM.altLast_Name);
                altcmd.Parameters.AddWithValue("@Middle_Name", TFM.altMiddle_Name);
                DateTime altdat = DateTime.ParseExact(TFM.altDob, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                altcmd.Parameters.AddWithValue("@DOB", altdat);
                altcmd.Parameters.AddWithValue("@Mobile", "None");
                altcmd.Parameters.AddWithValue("@Relationship", "None");
                altcmd.Parameters.AddWithValue("@Marital_Status", TFM.altMarital_Status);
                altcmd.Parameters.AddWithValue("@Religion", "none");
                altcmd.Parameters.AddWithValue("@Identity_Proof", TFM.altIdentity_Proof);
                altcmd.Parameters.AddWithValue("@Identity_Proof_Value", TFM.altIdentity_Proof_Value);





                if (TFM.altAlt_Identity_Proof != null)
                {
                    altcmd.Parameters.AddWithValue("@Alt_Identity_Proof", TFM.altAlt_Identity_Proof);
                }
                else
                {
                    TFM.altAlt_Identity_Proof = "None";
                    altcmd.Parameters.AddWithValue("@Alt_Identity_Proof", TFM.altAlt_Identity_Proof);
                }

                if (TFM.altAlt_Identity_Proof_Value != null)
                {
                    altcmd.Parameters.AddWithValue("@Alt_Identity_Proof_Value", TFM.altAlt_Identity_Proof_Value);
                }
                else
                {
                    TFM.altAlt_Identity_Proof_Value = "None";
                    altcmd.Parameters.AddWithValue("@Alt_Identity_Proof_Value", TFM.altAlt_Identity_Proof_Value);
                }



                altcmd.Parameters.AddWithValue("@Address1", TFM.altAddress1);
                if (TFM.altAddress2 != null || TFM.altAddress2 == "")
                {
                    altcmd.Parameters.AddWithValue("@Address2", TFM.altAddress2);

                }
                else
                {
                    TFM.altAddress2 = "None";
                    altcmd.Parameters.AddWithValue("@Address2", TFM.altAddress2);
                }


                if (TFM.altAddress3 != null || TFM.altAddress3 == "")
                {
                    altcmd.Parameters.AddWithValue("@Address3", TFM.altAddress3);

                }
                else
                {
                    TFM.altAddress3 = "None";
                    altcmd.Parameters.AddWithValue("@Address3", TFM.altAddress3);
                }
                altcmd.Parameters.AddWithValue("@City", TFM.altCity_txt);
                altcmd.Parameters.AddWithValue("@State", TFM.altState_txt);
                altcmd.Parameters.AddWithValue("@Pin", TFM.altPin);
                altcmd.Parameters.AddWithValue("@tid", Convert.ToInt32(Session["distid"]));

                altcmd.ExecuteNonQuery();
                con.Close();




                //end



            }





            //end



            int bpid = 0;
            con.Open();
            string qcheck = "select max(bpId) as bpId from BeneficiaryDetails ";
            SqlDataAdapter dachk = new SqlDataAdapter(qcheck,con);
            DataTable dtchk = new DataTable();
            dachk.Fill(dtchk);
            if (dtchk.Rows.Count > 0)
            {
                bpid = Convert.ToInt32(dtchk.Rows[0]["bpId"]);
            }

            con.Close();


            con.Open();
            string qu = "update BeneficiaryDetails set fetchid = 'TF' where  bpId = "+ bpid.ToString() + "";
            SqlCommand cmdu = new SqlCommand(qu,con);
            cmdu.ExecuteNonQuery();

            con.Close();



            //}
            //else
            //{
            //    ViewBag.message = "link";
            //}


            //con.Open();
            //string lastvisit = "insert into PageActive () values ";
            //SqlCommand cmdv = new SqlCommand(lastvisit,con);
            //cmdv.ExecuteNonQuery();
            //con.Close();


            string date = TFM.Dob;
            TempData["Message"] = "true";




            ModelState.Clear();

            return RedirectToAction("AddTestatorFamilyIndex", "AddTestatorFamily" , new {date= date });
        }



        public string BindTestatorDDL()
        {

            if (Convert.ToInt32(Session["uuid"]) != 1)
            {
                string ck = "select type from users where uId ="+ Convert.ToInt32(Session["uuid"]) + "";
                SqlDataAdapter cda = new SqlDataAdapter(ck,con);
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


                    con.Open();
                    string query2 = "select * from testatorFamily where tId =  " + Convert.ToInt32(dt.Rows[0]["tId"]) + " ";
                    SqlDataAdapter da2 = new SqlDataAdapter(query2, con);
                    DataTable dt2 = new DataTable();
                    da2.Fill(dt2);
                    con.Close();
                    string popup = "";
                    if (dt2.Rows.Count > 0)
                    {
                        popup = "true";

                    }


                    if (dt.Rows.Count > 0)
                    {
                       
                  

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {




                            data = data + "<option value=" + dt.Rows[i]["tId"].ToString() + " >" + dt.Rows[i]["First_Name"].ToString() + "</option> " +"~"+ dt.Rows[i]["tId"].ToString() + "~" + popup;



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


        public string gettestatoraddress()
        {
            string tid = Request["send"].ToString();

            con.Open();
            string query = "select Address1 , Address2 , Address3 from TestatorDetails where tId = "+ tid + " ";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            string data = "";
            if (dt.Rows.Count > 0)
            {
                data = dt.Rows[0]["Address1"].ToString() + "~" + dt.Rows[0]["Address2"].ToString() + "~" + dt.Rows[0]["Address3"].ToString();

            }


            return data;
        }






        public int CheckTestatorUsers(string value , string checkstatus)
        {




            int check = 0;
            if (checkstatus != "true")
            {
                
                int Response = Convert.ToInt32(Request["send"]);

                if (Request["send"] != "")
                {
                    // check for data exists or not for testato family
                    con.Open();
                    string query1 = "select a.fId , a.First_Name , a.Last_Name , a.Middle_Name , a.DOB , a.Marital_Status , a.Religion , a.Relationship , a.Address1 , a.Address2 , a.Address3 , a.City , a.State , a.Pin , a.tId , a.active , a.Identity_Proof , a.Identity_Proof_Value , a.Alt_Identity_Proof , a.Alt_Identity_Proof_Value , a.Is_Informed_Person from testatorFamily a inner join TestatorDetails b on a.tId=b.tId where b.tId =   " + value + " ";
                    SqlDataAdapter da = new SqlDataAdapter(query1, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    //end

                    if (dt.Rows.Count > 0)
                    {
                        string query2 = "Update PageActivity set ActID=1 , Tid=" + value + " , PageStatus=2  ";
                        SqlCommand cmd = new SqlCommand(query2, con);
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        string query2 = "Update PageActivity set ActID=1 , Tid=" + value + " , PageStatus=1  ";
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

            return check;

        }



        public ActionResult updatetestatorfamily(TestatorFamilyModel TFM)
        {



            con.Open();
            SqlCommand cmd = new SqlCommand("SP_CRUDtestatorfamily", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@condition", "update");
            cmd.Parameters.AddWithValue("@fId", TFM.fId);
            cmd.Parameters.AddWithValue("@First_Name", TFM.First_Name);
            cmd.Parameters.AddWithValue("@Last_Name", TFM.Last_Name);
            cmd.Parameters.AddWithValue("@Middle_Name", TFM.Middle_Name);
            DateTime dat = DateTime.ParseExact(TFM.Dob, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            cmd.Parameters.AddWithValue("@DOB", dat);
            cmd.Parameters.AddWithValue("@Marital_Status", "none");
            cmd.Parameters.AddWithValue("@Religion", "none");
            cmd.Parameters.AddWithValue("@Relationship", TFM.RelationshipTxt);
            cmd.Parameters.AddWithValue("@Address1", TFM.Address1);
            cmd.Parameters.AddWithValue("@Address2", TFM.Address2);
            cmd.Parameters.AddWithValue("@Address3", TFM.Address3);
            cmd.Parameters.AddWithValue("@City", TFM.City_txt);
            cmd.Parameters.AddWithValue("@State", TFM.State_txt);
            cmd.Parameters.AddWithValue("@tId", TFM.ddltid);
            cmd.Parameters.AddWithValue("@Pin", TFM.Pin);
            cmd.Parameters.AddWithValue("@active", "1");
            cmd.Parameters.AddWithValue("@Identity_Proof", TFM.Identity_Proof);
            cmd.Parameters.AddWithValue("@Identity_Proof_Value", TFM.Identity_Proof_Value);
            cmd.Parameters.AddWithValue("@Alt_Identity_Proof", TFM.Alt_Identity_Proof);
            cmd.Parameters.AddWithValue("@Alt_Identity_Proof_Value", TFM.Alt_Identity_Proof_Value);
            cmd.Parameters.AddWithValue("@Is_Informed_Person", "none");
            cmd.ExecuteNonQuery();






            string upaltquery = "update alttestatorFamily set altFirst_Name = '"+TFM.altFirst_Name+"'  , altLast_Name = '"+TFM.altLast_Name+"'  , altMiddle_Name = '"+TFM.altMiddle_Name+"'  , altDOB = '"+Convert.ToDateTime(TFM.altDob).ToString("yyyy-MM-dd")+"' , altMarital_Status = '"+TFM.altMarital_Status+"'   , altReligion = '"+TFM.altReligion+"' , altRelationship = '"+TFM.altRelationshipTxt+"' , altAddress1 = '"+TFM.altAddress1+"' , altAddress2 = '"+TFM.altAddress2+"' , altAddress3 = '"+TFM.altAddress3+"' , altCity = '"+TFM.altCity_txt+"'  , altState = '"+TFM.altState_txt+"'  , altPin = '"+TFM.altPin+"'  , altIdentity_Proof = '"+TFM.altIdentity_Proof+"'  , altIdentity_Proof_Value = '"+TFM.altIdentity_Proof_Value+"' , altAlt_Identity_Proof = '"+TFM.altAlt_Identity_Proof+"'  , altAlt_Identity_Proof_Value = '"+TFM.altAlt_Identity_Proof_Value+ "'  where testatorfamilyid = "+ TFM.fId + " ";
            SqlCommand altcmd = new SqlCommand(upaltquery,con);
            altcmd.ExecuteNonQuery();



            con.Close();




           





            ViewBag.message = "Verified";
            ViewBag.disablefield = "true";

            return RedirectToAction("AddTestatorFamilyIndex", "AddTestatorFamily");
        }








        public String altBindStateDDL()
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



        public string altOnChangeBindCity()
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





    




        public String guaBindStateDDL()
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



        public string guaOnChangeBindCity()
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





        public string Validateidentity()
        {
            string response = Request["send"].ToString();
            string msg = "";
            con.Open();
            string query = "select Alt_Identity_proof_Value , Identity_proof_Value from TestatorDetails where uId = '" + Convert.ToInt32(Session["uuid"])+"'";
            SqlDataAdapter da = new SqlDataAdapter(query,con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            
            if (dt.Rows.Count > 0)
            {

                if (dt.Rows[0]["Identity_proof_Value"].ToString() == response || dt.Rows[0]["Alt_Identity_proof_Value"].ToString() == response)
                {
                    msg = "false";
                }
                else
                {
                    msg = "true";
                }

            }


            con.Close();




            return msg;
        }





        public string altValidateidentity()
        {
            string response = Request["send"].ToString();
            string msg = "";
            con.Open();
            string query = "select  Identity_proof_Value , Alt_Identity_proof_Value   from TestatorDetails where uId = '" + Convert.ToInt32(Session["uuid"]) + "'";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {

                if (dt.Rows[0]["Alt_Identity_proof_Value"].ToString() == response || dt.Rows[0]["Identity_proof_Value"].ToString() == response)
                {
                    msg = "false";
                }
                else
                {
                    msg = "true";
                }

            }


            con.Close();




            return msg;
        }






        public string Validateidentity2()
        {
            string response = Request["send"].ToString();
            string msg = "";






            con.Open();



            string query = "select tId from TestatorDetails where uId = " + Convert.ToInt32(Session["uuid"]) + "   ";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            int tid = 0;
            if (dt.Rows.Count > 0)
            {
                tid = Convert.ToInt32(dt.Rows[0]["tId"]);
            }




            string query2 = "select Alt_Identity_proof_Value , Identity_proof_Value from TestatorDetails  where tId = " + tid + " ";
            SqlDataAdapter da2 = new SqlDataAdapter(query2, con);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);

            if (dt2.Rows.Count > 0)
            {

                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    if (dt2.Rows[i]["Identity_proof_Value"].ToString() == response || dt2.Rows[0]["Alt_Identity_proof_Value"].ToString() == response)
                    {
                        msg = "false";
                    }
                    else
                    {
                        msg = "true";
                    }


                }


            }


            con.Close();




            return msg;
        }





        public string altValidateidentity2()
        {
            string response = Request["send"].ToString();
            string msg = "";






            con.Open();



            string query = "select tId from TestatorDetails where uId = " + Convert.ToInt32(Session["uuid"]) + "";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            int tid = 0;
            if (dt.Rows.Count > 0)
            {
                tid = Convert.ToInt32(dt.Rows[0]["tId"]);
            }




            string query2 = "select Identity_proof_Value , Alt_Identity_proof_value from TestatorDetails  where tId = " + tid + "  ";
            SqlDataAdapter da2 = new SqlDataAdapter(query2, con);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);

            if (dt2.Rows.Count > 0)
            {

                if (dt2.Rows[0]["Alt_Identity_proof_value"].ToString() == response || dt2.Rows[0]["Identity_proof_Value"].ToString() == response)
                {
                    msg = "false";
                }
                else
                {
                    msg = "true";
                }

            }


            con.Close();




            return msg;
        }



    }
}