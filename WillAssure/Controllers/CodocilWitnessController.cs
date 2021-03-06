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
using System.Collections;

namespace WillAssure.Controllers
{


    public class CodocilWitnessController : Controller
    {

        public static string connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        SqlConnection con = new SqlConnection(connectionString);

        // GET: CodocilWitness
        public ActionResult CodocilWitnessIndex()
        {

            if (TempData["Message"] != null)
            {
                if (TempData["Message"].ToString() == "true")
                {
                    ViewBag.Message = "Verified";
                }
            }

            codocilwitnessmodel CWM = new codocilwitnessmodel();

            return View("~/Views/CodocilWitness/CodocilWitnessPageContent.cshtml", CWM);
        }


        public ActionResult InsertAppointeesFormData(AppointeesModel AM)
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



            AM.documentId = 0;
            int appid = 0;
            // latest appointees
            int apid = 0;


           
             
                    ViewBag.disablefield = "true";
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SP_CRUDAppointees", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@condition", "insert");
                    cmd.Parameters.AddWithValue("@documentId", AM.documentId);
                    cmd.Parameters.AddWithValue("@Type", "Witness");

                    if (AM.subTypetxt != null || AM.subTypetxt != "")
                    {
                        cmd.Parameters.AddWithValue("@subType", AM.subTypetxt);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@subType", "None");
                    }






                    cmd.Parameters.AddWithValue("@Name", AM.Firstname);
                    cmd.Parameters.AddWithValue("@middleName", AM.middleName);
                    cmd.Parameters.AddWithValue("@Surname", AM.Surname);
                    cmd.Parameters.AddWithValue("@Identity_proof", AM.Identity_Proof);
                    cmd.Parameters.AddWithValue("@Identity_proof_value", AM.Identity_Proof_Value);


                    if (AM.Alt_Identity_Proof != null)
                    {
                        cmd.Parameters.AddWithValue("@Alt_Identity_proof", AM.Alt_Identity_Proof);
                    }
                    else
                    {
                        AM.Alt_Identity_Proof = "None";
                        cmd.Parameters.AddWithValue("@Alt_Identity_proof", AM.Alt_Identity_Proof);
                    }


                    if (AM.Alt_Identity_Proof_Value != null)
                    {
                        cmd.Parameters.AddWithValue("@Alt_Identity_proof_value", AM.Alt_Identity_Proof_Value);
                    }
                    else
                    {
                        AM.Alt_Identity_Proof_Value = "None";
                        cmd.Parameters.AddWithValue("@Alt_Identity_proof_value", AM.Alt_Identity_Proof_Value);
                    }









                    //DateTime dat = DateTime.ParseExact(AM.Dob, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                    //cmd.Parameters.AddWithValue("@DOB", "None");
                    cmd.Parameters.AddWithValue("@Gender", AM.Gender);
                    cmd.Parameters.AddWithValue("@Occupation", "None");
                    cmd.Parameters.AddWithValue("@Relationship", "None");
                    cmd.Parameters.AddWithValue("@Address1", AM.Address1);
                    if (AM.Address2 != null || AM.Address2 == "")
                    {
                        cmd.Parameters.AddWithValue("@Address2", AM.Address2);
                    }
                    else
                    {
                        AM.Address2 = "None";
                        cmd.Parameters.AddWithValue("@Address2", AM.Address2);
                    }


                    if (AM.Address3 != null || AM.Address3 == "")
                    {
                        cmd.Parameters.AddWithValue("@Address3", AM.Address3);
                    }
                    else
                    {
                        AM.Address3 = "None";
                        cmd.Parameters.AddWithValue("@Address3", AM.Address3);
                    }


                    cmd.Parameters.AddWithValue("@City", AM.citytext);
                    cmd.Parameters.AddWithValue("@State", AM.statetext);
                    cmd.Parameters.AddWithValue("@Pin", AM.Pin);
                    cmd.Parameters.AddWithValue("@tid", AM.ddltid);
                    cmd.Parameters.AddWithValue("@ExecutorType", "Single");
                    cmd.ExecuteNonQuery();
                    con.Close();



                    con.Open();
                    string query = "select top 1 * from Appointees order by apId desc";
                    SqlDataAdapter da2 = new SqlDataAdapter(query, con);
                    DataTable dt2 = new DataTable();
                    da2.Fill(dt2);
                    if (dt2.Rows.Count > 0)
                    {
                        appid = Convert.ToInt32(dt2.Rows[0]["apId"]);
                        apid = 1; // for yes
                    }
                    else
                    {
                        apid = 2; //for no
                    }
                    con.Close();



                    //end

                    // update document status

                    con.Open();
                    string qte = "update Appointees set documentstatus = 'Incompleted' , WillType='" + Session["WillType"].ToString() + "' where apId =" + appid + " ";
                    SqlCommand cmdte = new SqlCommand(qte, con);
                    cmdte.ExecuteNonQuery();
                    con.Close();


                    //end


                    con.Open();
                    string qt = "update Appointees set doctype = 'Will'  where  apId = " + Convert.ToInt32(dt2.Rows[0]["apId"]) + "";
                    SqlCommand cmdt = new SqlCommand(qt, con);
                    cmdt.ExecuteNonQuery();
                    con.Close();






                    ////////////////////////////////////alternate witness //////////////////////////////////////////////////




                    con.Open();
                    SqlCommand cmdw = new SqlCommand("SP_CRUDAppointees", con);
                    cmdw.CommandType = CommandType.StoredProcedure;
                    cmdw.Parameters.AddWithValue("@condition", "insert");
                    cmdw.Parameters.AddWithValue("@documentId", AM.wdocumentId);
                    cmdw.Parameters.AddWithValue("@Type", "Witness");

                    if (AM.wsubTypetxt != null || AM.wsubTypetxt != "")
                    {
                        cmdw.Parameters.AddWithValue("@subType", AM.wsubTypetxt);
                    }
                    else
                    {
                        cmdw.Parameters.AddWithValue("@subType", "None");
                    }






                    cmdw.Parameters.AddWithValue("@Name", AM.wFirstname);
                    cmdw.Parameters.AddWithValue("@middleName", AM.wmiddleName);
                    cmdw.Parameters.AddWithValue("@Surname", AM.wSurname);
                    cmdw.Parameters.AddWithValue("@Identity_proof", AM.wIdentity_Proof);
                    cmdw.Parameters.AddWithValue("@Identity_proof_value", AM.wIdentity_Proof_Value);


                    if (AM.wAlt_Identity_Proof != null)
                    {
                        cmdw.Parameters.AddWithValue("@Alt_Identity_proof", AM.wAlt_Identity_Proof);
                    }
                    else
                    {
                        AM.wAlt_Identity_Proof = "None";
                        cmdw.Parameters.AddWithValue("@Alt_Identity_proof", AM.wAlt_Identity_Proof);
                    }


                    if (AM.wAlt_Identity_Proof_Value != null)
                    {
                        cmdw.Parameters.AddWithValue("@Alt_Identity_proof_value", AM.wAlt_Identity_Proof_Value);
                    }
                    else
                    {
                        AM.wAlt_Identity_Proof_Value = "None";
                        cmdw.Parameters.AddWithValue("@Alt_Identity_proof_value", AM.wAlt_Identity_Proof_Value);
                    }









                    //DateTime dat = DateTime.ParseExact(AM.Dob, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                    //cmd.Parameters.AddWithValue("@DOB", "None");
                    cmdw.Parameters.AddWithValue("@Gender", AM.wGender);
                    cmdw.Parameters.AddWithValue("@Occupation", "None");
                    cmdw.Parameters.AddWithValue("@Relationship", "None");
                    cmdw.Parameters.AddWithValue("@Address1", AM.wAddress1);
                    if (AM.wAddress2 != null || AM.wAddress2 == "")
                    {
                        cmdw.Parameters.AddWithValue("@Address2", AM.wAddress2);
                    }
                    else
                    {
                        AM.wAddress2 = "None";
                        cmdw.Parameters.AddWithValue("@Address2", AM.wAddress2);
                    }


                    if (AM.wAddress3 != null || AM.wAddress3 == "")
                    {
                        cmdw.Parameters.AddWithValue("@Address3", AM.wAddress3);
                    }
                    else
                    {
                        AM.wAddress3 = "None";
                        cmdw.Parameters.AddWithValue("@Address3", AM.wAddress3);
                    }


                    cmdw.Parameters.AddWithValue("@City", AM.wcitytext);
                    cmdw.Parameters.AddWithValue("@State", AM.wstatetext);
                    cmdw.Parameters.AddWithValue("@Pin", AM.wPin);
                    cmdw.Parameters.AddWithValue("@tid", AM.ddltid);
                    cmdw.Parameters.AddWithValue("@ExecutorType", "Single");
                    cmdw.ExecuteNonQuery();
                    con.Close();


                    int appid22 = 0;
                    con.Open();
                    string query22 = "select top 1 * from Appointees order by apId desc";
                    SqlDataAdapter da22 = new SqlDataAdapter(query22, con);
                    DataTable dt22 = new DataTable();
                    da22.Fill(dt22);
                    if (dt22.Rows.Count > 0)
                    {
                        appid22 = Convert.ToInt32(dt22.Rows[0]["apId"]);
                        apid = 1; // for yes
                    }
                    else
                    {
                        apid = 2; //for no
                    }
                    con.Close();



                    //end

                    // update document status

                    con.Open();
                    string qte22 = "update Appointees set documentstatus = 'Incompleted' , WillType='" + Session["WillType"].ToString() + "' where apId =" + appid22 + " ";
                    SqlCommand cmdte22 = new SqlCommand(qte22, con);
                    cmdte22.ExecuteNonQuery();
                    con.Close();


                    //end


                    con.Open();
                    string qt22 = "update Appointees set doctype = 'Will'  where  apId = " + appid22 + "";
                    SqlCommand cmdt22 = new SqlCommand(qt22, con);
                    cmdt22.ExecuteNonQuery();
                    con.Close();









                    ////////////////////////////////////////end//////////////////////////////////////////////////////////////


                
            
          






         

            TempData["Message"] = "true";

            // dropdown selection
            int AppointmentofGuardian = 0;
            if (AM.Typetxt == "Guardian")
            {
                AppointmentofGuardian = 1;    //yes
            }
            else
            {
                AppointmentofGuardian = 2;    // no
            }

            int Numberofexecutors = 0;
            if (AM.subTypetxt == "Single")
            {
                Numberofexecutors = 1;
            }
            if (AM.subTypetxt == "Many Joint")
            {
                Numberofexecutors = 2;
            }
            if (AM.subTypetxt == "Many Independent")
            {
                Numberofexecutors = 3;
            }

            //end

            // Document Rules

            //get latest id first
            con.Open();
            string getquery = "select top 1 * from documentRules order by wdId desc";
            SqlDataAdapter da = new SqlDataAdapter(getquery, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            int getruleid = 0;
            if (dt.Rows.Count > 0)
            {
                getruleid = Convert.ToInt32(dt.Rows[0]["wdId"]);
            }
            con.Close();

            //end



            con.Open();
            string rulequery = "update documentRules set guardian = " + AppointmentofGuardian + " ,executors_category = " + Numberofexecutors + " where tid = " + AM.ddltid + " ";
            SqlCommand cmd2 = new SqlCommand(rulequery, con);
            cmd2.ExecuteNonQuery();
            con.Close();
            //end














            ViewBag.Message = "Verified";


            // latest appointees
            int altapid = 0;
            con.Open();
            string query4 = "select top 1 * from alternate_Appointees order by apId desc";
            SqlDataAdapter da4 = new SqlDataAdapter(query4, con);
            DataTable dt4 = new DataTable();
            da4.Fill(dt4);
            if (dt4.Rows.Count > 0)
            {

                altapid = 1; // for yes
            }
            else
            {
                altapid = 2; //for no
            }
            con.Close();



            //end



            // dropdown selection
            int AppointmentofaltGuardian = 0;
            if (AM.altguardian == "Guardian")
            {
                AppointmentofaltGuardian = 1;    //yes
            }
            else
            {
                AppointmentofaltGuardian = 2;    // no
            }

            int altNumberofexecutors = 2;
            if (AM.altexecutor == "Single")
            {
                altNumberofexecutors = 1;
            }
            if (AM.altexecutor == "Many Joint")
            {
                altNumberofexecutors = 1;
            }
            if (AM.altexecutor == "Many Independent")
            {
                altNumberofexecutors = 1;
            }

            //end

            // Document Rules

            //get latest id first
            con.Open();
            string getquery4 = "select top 1 * from documentRules order by wdId desc";
            SqlDataAdapter da5 = new SqlDataAdapter(getquery4, con);
            DataTable dt5 = new DataTable();
            da5.Fill(dt5);
            int getruleid2 = 0;
            if (dt.Rows.Count > 0)
            {
                getruleid2 = Convert.ToInt32(dt5.Rows[0]["wdId"]);
            }
            con.Close();

            //end



            con.Open();
            string rulequery2 = "update documentRules set AlternateGaurdian = " + AppointmentofaltGuardian + " , AlternateExecutors = " + altNumberofexecutors + " where tid = " + AM.ddltid + " ";
            SqlCommand cmd6 = new SqlCommand(rulequery2, con);
            cmd6.ExecuteNonQuery();
            con.Close();
            //end


            // update document master with latest rule id

            con.Open();
            string rquery2 = "update documentMaster set wdId = " + getruleid2 + " where tId =  " + AM.ddltid + "  ";
            SqlCommand rcmd2 = new SqlCommand(rquery2, con);
            rcmd2.ExecuteNonQuery();
            con.Close();




            //end







           

       


            TempData["Message"] = "true";
            ModelState.Clear();

            return RedirectToAction("CodocilWitnessIndex", "CodocilWitness");
        }



        public String BindStateDDL()
        {

            con.Open();
            string query = "select distinct * from tbl_state where country_id = 101 order by statename asc  ";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            string data = "<select value='0'>--Select State--</select>";

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
            string query = "select distinct * from tbl_city where state_id ='" + response + "' order by city_name asc ";
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




        public String altBindStateDDL()
        {

            con.Open();
            string query = "select distinct * from tbl_state where country_id = 101 order by statename asc  ";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            string data = "<select value='0'>--Select State--</select>";

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
            string query = "select distinct * from tbl_city where state_id ='" + response + "' order by city_name asc ";
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


        public ActionResult InsertWitnessFormData(AppointeesModel AM)
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



            AM.documentId = 0;
            int appid = 0;
            // latest appointees
            int apid = 0;


            if (Session["doctype"] != null)
            {
                if (Session["doctype"].ToString() == "Will")
                {
                    ViewBag.disablefield = "true";
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SP_CRUDAppointees", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@condition", "insert");
                    cmd.Parameters.AddWithValue("@documentId", AM.documentId);
                    cmd.Parameters.AddWithValue("@Type", "Witness");

                    if (AM.subTypetxt != null || AM.subTypetxt != "")
                    {
                        cmd.Parameters.AddWithValue("@subType", AM.subTypetxt);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@subType", "None");
                    }






                    cmd.Parameters.AddWithValue("@Name", AM.Firstname);
                    cmd.Parameters.AddWithValue("@middleName", AM.middleName);
                    cmd.Parameters.AddWithValue("@Surname", AM.Surname);
                    cmd.Parameters.AddWithValue("@Identity_proof", AM.Identity_Proof);
                    cmd.Parameters.AddWithValue("@Identity_proof_value", AM.Identity_Proof_Value);


                    if (AM.Alt_Identity_Proof != null)
                    {
                        cmd.Parameters.AddWithValue("@Alt_Identity_proof", AM.Alt_Identity_Proof);
                    }
                    else
                    {
                        AM.Alt_Identity_Proof = "None";
                        cmd.Parameters.AddWithValue("@Alt_Identity_proof", AM.Alt_Identity_Proof);
                    }


                    if (AM.Alt_Identity_Proof_Value != null)
                    {
                        cmd.Parameters.AddWithValue("@Alt_Identity_proof_value", AM.Alt_Identity_Proof_Value);
                    }
                    else
                    {
                        AM.Alt_Identity_Proof_Value = "None";
                        cmd.Parameters.AddWithValue("@Alt_Identity_proof_value", AM.Alt_Identity_Proof_Value);
                    }









                    //DateTime dat = DateTime.ParseExact(AM.Dob, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                    //cmd.Parameters.AddWithValue("@DOB", "None");
                    cmd.Parameters.AddWithValue("@Gender", AM.Gender);
                    cmd.Parameters.AddWithValue("@Occupation", "None");
                    cmd.Parameters.AddWithValue("@Relationship", "None");
                    cmd.Parameters.AddWithValue("@Address1", AM.Address1);
                    if (AM.Address2 != null || AM.Address2 == "")
                    {
                        cmd.Parameters.AddWithValue("@Address2", AM.Address2);
                    }
                    else
                    {
                        AM.Address2 = "None";
                        cmd.Parameters.AddWithValue("@Address2", AM.Address2);
                    }


                    if (AM.Address3 != null || AM.Address3 == "")
                    {
                        cmd.Parameters.AddWithValue("@Address3", AM.Address3);
                    }
                    else
                    {
                        AM.Address3 = "None";
                        cmd.Parameters.AddWithValue("@Address3", AM.Address3);
                    }


                    cmd.Parameters.AddWithValue("@City", AM.citytext);
                    cmd.Parameters.AddWithValue("@State", AM.statetext);
                    cmd.Parameters.AddWithValue("@Pin", AM.Pin);
                    cmd.Parameters.AddWithValue("@tid", AM.ddltid);
                    cmd.Parameters.AddWithValue("@ExecutorType", "Single");
                    cmd.ExecuteNonQuery();
                    con.Close();



                    con.Open();
                    string query = "select top 1 * from Appointees order by apId desc";
                    SqlDataAdapter da2 = new SqlDataAdapter(query, con);
                    DataTable dt2 = new DataTable();
                    da2.Fill(dt2);
                    if (dt2.Rows.Count > 0)
                    {
                        appid = Convert.ToInt32(dt2.Rows[0]["apId"]);
                        apid = 1; // for yes
                    }
                    else
                    {
                        apid = 2; //for no
                    }
                    con.Close();



                    //end

                    // update document status

                    con.Open();
                    string qte = "update Appointees set documentstatus = 'Incompleted' , WillType='" + Session["WillType"].ToString() + "' where apId =" + appid + " ";
                    SqlCommand cmdte = new SqlCommand(qte, con);
                    cmdte.ExecuteNonQuery();
                    con.Close();


                    //end


                    con.Open();
                    string qt = "update Appointees set doctype = 'Will'  where  apId = " + Convert.ToInt32(dt2.Rows[0]["apId"]) + "";
                    SqlCommand cmdt = new SqlCommand(qt, con);
                    cmdt.ExecuteNonQuery();
                    con.Close();






                    ////////////////////////////////////alternate witness //////////////////////////////////////////////////




                    con.Open();
                    SqlCommand cmdw = new SqlCommand("SP_CRUDAppointees", con);
                    cmdw.CommandType = CommandType.StoredProcedure;
                    cmdw.Parameters.AddWithValue("@condition", "insert");
                    cmdw.Parameters.AddWithValue("@documentId", AM.wdocumentId);
                    cmdw.Parameters.AddWithValue("@Type", "Witness");

                    if (AM.wsubTypetxt != null || AM.wsubTypetxt != "")
                    {
                        cmdw.Parameters.AddWithValue("@subType", AM.wsubTypetxt);
                    }
                    else
                    {
                        cmdw.Parameters.AddWithValue("@subType", "None");
                    }






                    cmdw.Parameters.AddWithValue("@Name", AM.wFirstname);
                    cmdw.Parameters.AddWithValue("@middleName", AM.wmiddleName);
                    cmdw.Parameters.AddWithValue("@Surname", AM.wSurname);
                    cmdw.Parameters.AddWithValue("@Identity_proof", AM.wIdentity_Proof);
                    cmdw.Parameters.AddWithValue("@Identity_proof_value", AM.wIdentity_Proof_Value);


                    if (AM.wAlt_Identity_Proof != null)
                    {
                        cmdw.Parameters.AddWithValue("@Alt_Identity_proof", AM.wAlt_Identity_Proof);
                    }
                    else
                    {
                        AM.wAlt_Identity_Proof = "None";
                        cmdw.Parameters.AddWithValue("@Alt_Identity_proof", AM.wAlt_Identity_Proof);
                    }


                    if (AM.wAlt_Identity_Proof_Value != null)
                    {
                        cmdw.Parameters.AddWithValue("@Alt_Identity_proof_value", AM.wAlt_Identity_Proof_Value);
                    }
                    else
                    {
                        AM.wAlt_Identity_Proof_Value = "None";
                        cmdw.Parameters.AddWithValue("@Alt_Identity_proof_value", AM.wAlt_Identity_Proof_Value);
                    }









                    //DateTime dat = DateTime.ParseExact(AM.Dob, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                    //cmd.Parameters.AddWithValue("@DOB", "None");
                    cmdw.Parameters.AddWithValue("@Gender", AM.wGender);
                    cmdw.Parameters.AddWithValue("@Occupation", "None");
                    cmdw.Parameters.AddWithValue("@Relationship", "None");
                    cmdw.Parameters.AddWithValue("@Address1", AM.wAddress1);
                    if (AM.wAddress2 != null || AM.wAddress2 == "")
                    {
                        cmdw.Parameters.AddWithValue("@Address2", AM.wAddress2);
                    }
                    else
                    {
                        AM.wAddress2 = "None";
                        cmdw.Parameters.AddWithValue("@Address2", AM.wAddress2);
                    }


                    if (AM.wAddress3 != null || AM.wAddress3 == "")
                    {
                        cmdw.Parameters.AddWithValue("@Address3", AM.wAddress3);
                    }
                    else
                    {
                        AM.wAddress3 = "None";
                        cmdw.Parameters.AddWithValue("@Address3", AM.wAddress3);
                    }


                    cmdw.Parameters.AddWithValue("@City", AM.wcitytext);
                    cmdw.Parameters.AddWithValue("@State", AM.wstatetext);
                    cmdw.Parameters.AddWithValue("@Pin", AM.wPin);
                    cmdw.Parameters.AddWithValue("@tid", AM.ddltid);
                    cmdw.Parameters.AddWithValue("@ExecutorType", "Single");
                    cmdw.ExecuteNonQuery();
                    con.Close();


                    int appid22 = 0;
                    con.Open();
                    string query22 = "select top 1 * from Appointees order by apId desc";
                    SqlDataAdapter da22 = new SqlDataAdapter(query22, con);
                    DataTable dt22 = new DataTable();
                    da22.Fill(dt22);
                    if (dt22.Rows.Count > 0)
                    {
                        appid22 = Convert.ToInt32(dt22.Rows[0]["apId"]);
                        apid = 1; // for yes
                    }
                    else
                    {
                        apid = 2; //for no
                    }
                    con.Close();



                    //end

                    // update document status

                    con.Open();
                    string qte22 = "update Appointees set documentstatus = 'Incompleted' , WillType='" + Session["WillType"].ToString() + "' where apId =" + appid22 + " ";
                    SqlCommand cmdte22 = new SqlCommand(qte22, con);
                    cmdte22.ExecuteNonQuery();
                    con.Close();


                    //end


                    con.Open();
                    string qt22 = "update Appointees set doctype = 'Will'  where  apId = " + appid22 + "";
                    SqlCommand cmdt22 = new SqlCommand(qt22, con);
                    cmdt22.ExecuteNonQuery();
                    con.Close();









                    ////////////////////////////////////////end//////////////////////////////////////////////////////////////


                }
            }
            else
            {
                RedirectToAction("LoginPageIndex", "LoginPage");
            }






            if (Session["doctype"] != null)
            {
                if (Session["doctype"].ToString() == "POA")
                {
                    ViewBag.disablefield = "true";
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SP_CRUDAppointees", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@condition", "insert");
                    cmd.Parameters.AddWithValue("@documentId", AM.documentId);
                    cmd.Parameters.AddWithValue("@Type", "Witness");
                    cmd.Parameters.AddWithValue("@subType", "None");
                    cmd.Parameters.AddWithValue("@Name", AM.Firstname);
                    cmd.Parameters.AddWithValue("@middleName", AM.middleName);
                    cmd.Parameters.AddWithValue("@Surname", AM.Surname);
                    cmd.Parameters.AddWithValue("@Identity_proof", AM.Identity_Proof);
                    cmd.Parameters.AddWithValue("@Identity_proof_value", AM.Identity_Proof_Value);


                    if (AM.Alt_Identity_Proof != null)
                    {
                        cmd.Parameters.AddWithValue("@Alt_Identity_proof", AM.Alt_Identity_Proof);
                    }
                    else
                    {
                        AM.Alt_Identity_Proof = "None";
                        cmd.Parameters.AddWithValue("@Alt_Identity_proof", AM.Alt_Identity_Proof);
                    }


                    if (AM.Alt_Identity_Proof_Value != null)
                    {
                        cmd.Parameters.AddWithValue("@Alt_Identity_proof_value", AM.Alt_Identity_Proof_Value);
                    }
                    else
                    {
                        AM.Alt_Identity_Proof_Value = "None";
                        cmd.Parameters.AddWithValue("@Alt_Identity_proof_value", AM.Alt_Identity_Proof_Value);
                    }









                    //DateTime dat = DateTime.ParseExact(AM.Dob, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                    //cmd.Parameters.AddWithValue("@DOB", "None");
                    cmd.Parameters.AddWithValue("@Gender", AM.Gender);
                    cmd.Parameters.AddWithValue("@Occupation", "None");
                    cmd.Parameters.AddWithValue("@Relationship", "None");
                    cmd.Parameters.AddWithValue("@Address1", AM.Address1);
                    if (AM.Address2 != null || AM.Address2 == "")
                    {
                        cmd.Parameters.AddWithValue("@Address2", AM.Address2);
                    }
                    else
                    {
                        AM.Address2 = "None";
                        cmd.Parameters.AddWithValue("@Address2", AM.Address2);
                    }


                    if (AM.Address3 != null || AM.Address3 == "")
                    {
                        cmd.Parameters.AddWithValue("@Address3", AM.Address3);
                    }
                    else
                    {
                        AM.Address3 = "None";
                        cmd.Parameters.AddWithValue("@Address3", AM.Address3);
                    }


                    cmd.Parameters.AddWithValue("@City", AM.citytext);
                    cmd.Parameters.AddWithValue("@State", AM.statetext);
                    cmd.Parameters.AddWithValue("@Pin", AM.Pin);
                    cmd.Parameters.AddWithValue("@tid", AM.ddltid);
                    cmd.Parameters.AddWithValue("@ExecutorType", "None");
                    cmd.ExecuteNonQuery();
                    con.Close();



                    con.Open();
                    string query = "select top 1 * from Appointees order by apId desc";
                    SqlDataAdapter da2 = new SqlDataAdapter(query, con);
                    DataTable dt2 = new DataTable();
                    da2.Fill(dt2);
                    if (dt2.Rows.Count > 0)
                    {
                        appid = Convert.ToInt32(dt2.Rows[0]["apId"]);
                        apid = 1; // for yes
                    }
                    else
                    {
                        apid = 2; //for no
                    }
                    con.Close();



                    //end
                    con.Open();
                    string qt = "update Appointees set doctype = 'POA'  where  apId = " + apid + "";
                    SqlCommand cmdt = new SqlCommand(qt, con);
                    cmdt.ExecuteNonQuery();
                    con.Close();







                    ////////////////////////////////////alternate witness //////////////////////////////////////////////////




                    con.Open();
                    SqlCommand cmdw = new SqlCommand("SP_CRUDAppointees", con);
                    cmdw.CommandType = CommandType.StoredProcedure;
                    cmdw.Parameters.AddWithValue("@condition", "insert");
                    cmdw.Parameters.AddWithValue("@documentId", AM.wdocumentId);
                    cmdw.Parameters.AddWithValue("@Type", "Witness");

                    if (AM.wsubTypetxt != null || AM.wsubTypetxt != "")
                    {
                        cmdw.Parameters.AddWithValue("@subType", AM.wsubTypetxt);
                    }
                    else
                    {
                        cmdw.Parameters.AddWithValue("@subType", "None");
                    }






                    cmdw.Parameters.AddWithValue("@Name", AM.wFirstname);
                    cmdw.Parameters.AddWithValue("@middleName", AM.wmiddleName);
                    cmdw.Parameters.AddWithValue("@Surname", AM.wSurname);
                    cmdw.Parameters.AddWithValue("@Identity_proof", AM.wIdentity_Proof);
                    cmdw.Parameters.AddWithValue("@Identity_proof_value", AM.wIdentity_Proof_Value);


                    if (AM.wAlt_Identity_Proof != null)
                    {
                        cmdw.Parameters.AddWithValue("@Alt_Identity_proof", AM.wAlt_Identity_Proof);
                    }
                    else
                    {
                        AM.wAlt_Identity_Proof = "None";
                        cmdw.Parameters.AddWithValue("@Alt_Identity_proof", AM.wAlt_Identity_Proof);
                    }


                    if (AM.wAlt_Identity_Proof_Value != null)
                    {
                        cmdw.Parameters.AddWithValue("@Alt_Identity_proof_value", AM.wAlt_Identity_Proof_Value);
                    }
                    else
                    {
                        AM.wAlt_Identity_Proof_Value = "None";
                        cmdw.Parameters.AddWithValue("@Alt_Identity_proof_value", AM.wAlt_Identity_Proof_Value);
                    }









                    //DateTime dat = DateTime.ParseExact(AM.Dob, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                    //cmd.Parameters.AddWithValue("@DOB", "None");
                    cmdw.Parameters.AddWithValue("@Gender", AM.wGender);
                    cmdw.Parameters.AddWithValue("@Occupation", "None");
                    cmdw.Parameters.AddWithValue("@Relationship", "None");
                    cmdw.Parameters.AddWithValue("@Address1", AM.wAddress1);
                    if (AM.wAddress2 != null || AM.wAddress2 == "")
                    {
                        cmdw.Parameters.AddWithValue("@Address2", AM.wAddress2);
                    }
                    else
                    {
                        AM.wAddress2 = "None";
                        cmdw.Parameters.AddWithValue("@Address2", AM.wAddress2);
                    }


                    if (AM.wAddress3 != null || AM.wAddress3 == "")
                    {
                        cmdw.Parameters.AddWithValue("@Address3", AM.wAddress3);
                    }
                    else
                    {
                        AM.wAddress3 = "None";
                        cmdw.Parameters.AddWithValue("@Address3", AM.wAddress3);
                    }


                    cmdw.Parameters.AddWithValue("@City", AM.wcitytext);
                    cmdw.Parameters.AddWithValue("@State", AM.wstatetext);
                    cmdw.Parameters.AddWithValue("@Pin", AM.wPin);
                    cmdw.Parameters.AddWithValue("@tid", AM.ddltid);
                    cmdw.Parameters.AddWithValue("@ExecutorType", "Single");
                    cmdw.ExecuteNonQuery();
                    con.Close();


                    int appid22 = 0;
                    con.Open();
                    string query22 = "select top 1 * from Appointees order by apId desc";
                    SqlDataAdapter da22 = new SqlDataAdapter(query22, con);
                    DataTable dt22 = new DataTable();
                    da22.Fill(dt22);
                    if (dt22.Rows.Count > 0)
                    {
                        appid22 = Convert.ToInt32(dt22.Rows[0]["apId"]);
                        apid = 1; // for yes
                    }
                    else
                    {
                        apid = 2; //for no
                    }
                    con.Close();



                    //end

                    // update document status

                    con.Open();
                    string qte22 = "update Appointees set documentstatus = 'Incompleted' , WillType='" + Session["WillType"].ToString() + "' where apId =" + appid22 + " ";
                    SqlCommand cmdte22 = new SqlCommand(qte22, con);
                    cmdte22.ExecuteNonQuery();
                    con.Close();


                    //end


                    con.Open();
                    string qt22 = "update Appointees set doctype = 'Will'  where  apId = " + Convert.ToInt32(dt22.Rows[0]["apId"]) + "";
                    SqlCommand cmdt22 = new SqlCommand(qt22, con);
                    cmdt.ExecuteNonQuery();
                    con.Close();









                    ////////////////////////////////////////end//////////////////////////////////////////////////////////////








                }
            }
            else
            {
                RedirectToAction("LoginPageIndex", "LoginPage");
            }

            if (Session["doctype"] != null)
            {
                if (Session["doctype"].ToString() == "Giftdeeds")
                {
                    ViewBag.disablefield = "true";
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SP_CRUDAppointees", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@condition", "insert");
                    cmd.Parameters.AddWithValue("@documentId", AM.documentId);
                    cmd.Parameters.AddWithValue("@Type", "Witness");
                    cmd.Parameters.AddWithValue("@subType", AM.subTypetxt);
                    cmd.Parameters.AddWithValue("@Name", AM.Firstname);
                    cmd.Parameters.AddWithValue("@middleName", AM.middleName);
                    cmd.Parameters.AddWithValue("@Surname", AM.Surname);
                    cmd.Parameters.AddWithValue("@Identity_proof", AM.Identity_Proof);
                    cmd.Parameters.AddWithValue("@Identity_proof_value", AM.Identity_Proof_Value);


                    if (AM.Alt_Identity_Proof != null)
                    {
                        cmd.Parameters.AddWithValue("@Alt_Identity_proof", AM.Alt_Identity_Proof);
                    }
                    else
                    {
                        AM.Alt_Identity_Proof = "None";
                        cmd.Parameters.AddWithValue("@Alt_Identity_proof", AM.Alt_Identity_Proof);
                    }


                    if (AM.Alt_Identity_Proof_Value != null)
                    {
                        cmd.Parameters.AddWithValue("@Alt_Identity_proof_value", AM.Alt_Identity_Proof_Value);
                    }
                    else
                    {
                        AM.Alt_Identity_Proof_Value = "None";
                        cmd.Parameters.AddWithValue("@Alt_Identity_proof_value", AM.Alt_Identity_Proof_Value);
                    }









                    //DateTime dat = DateTime.ParseExact(AM.Dob, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                    //cmd.Parameters.AddWithValue("@DOB", "None");
                    cmd.Parameters.AddWithValue("@Gender", AM.Gender);
                    cmd.Parameters.AddWithValue("@Occupation", "None");
                    cmd.Parameters.AddWithValue("@Relationship", "None");
                    cmd.Parameters.AddWithValue("@Address1", AM.Address1);
                    if (AM.Address2 != null || AM.Address2 == "")
                    {
                        cmd.Parameters.AddWithValue("@Address2", AM.Address2);
                    }
                    else
                    {
                        AM.Address2 = "None";
                        cmd.Parameters.AddWithValue("@Address2", AM.Address2);
                    }


                    if (AM.Address3 != null || AM.Address3 == "")
                    {
                        cmd.Parameters.AddWithValue("@Address3", AM.Address3);
                    }
                    else
                    {
                        AM.Address3 = "None";
                        cmd.Parameters.AddWithValue("@Address3", AM.Address3);
                    }


                    cmd.Parameters.AddWithValue("@City", AM.citytext);
                    cmd.Parameters.AddWithValue("@State", AM.statetext);
                    cmd.Parameters.AddWithValue("@Pin", AM.Pin);
                    cmd.Parameters.AddWithValue("@tid", AM.ddltid);
                    cmd.ExecuteNonQuery();
                    con.Close();



                    con.Open();
                    string query = "select top 1 * from Appointees order by apId desc";
                    SqlDataAdapter da2 = new SqlDataAdapter(query, con);
                    DataTable dt2 = new DataTable();
                    da2.Fill(dt2);
                    if (dt2.Rows.Count > 0)
                    {
                        appid = Convert.ToInt32(dt2.Rows[0]["apId"]);
                        apid = 1; // for yes
                    }
                    else
                    {
                        apid = 2; //for no
                    }
                    con.Close();



                    //end
                    con.Open();
                    string qt = "update Appointees set doctype = 'Giftdeeds'  where  apId = " + apid + "";
                    SqlCommand cmdt = new SqlCommand(qt, con);
                    cmdt.ExecuteNonQuery();
                    con.Close();






                    ////////////////////////////////////alternate witness //////////////////////////////////////////////////




                    con.Open();
                    SqlCommand cmdw = new SqlCommand("SP_CRUDAppointees", con);
                    cmdw.CommandType = CommandType.StoredProcedure;
                    cmdw.Parameters.AddWithValue("@condition", "insert");
                    cmdw.Parameters.AddWithValue("@documentId", AM.wdocumentId);
                    cmdw.Parameters.AddWithValue("@Type", "Witness");

                    if (AM.wsubTypetxt != null || AM.wsubTypetxt != "")
                    {
                        cmdw.Parameters.AddWithValue("@subType", AM.wsubTypetxt);
                    }
                    else
                    {
                        cmdw.Parameters.AddWithValue("@subType", "None");
                    }






                    cmdw.Parameters.AddWithValue("@Name", AM.wFirstname);
                    cmdw.Parameters.AddWithValue("@middleName", AM.wmiddleName);
                    cmdw.Parameters.AddWithValue("@Surname", AM.wSurname);
                    cmdw.Parameters.AddWithValue("@Identity_proof", AM.wIdentity_Proof);
                    cmdw.Parameters.AddWithValue("@Identity_proof_value", AM.wIdentity_Proof_Value);


                    if (AM.wAlt_Identity_Proof != null)
                    {
                        cmdw.Parameters.AddWithValue("@Alt_Identity_proof", AM.wAlt_Identity_Proof);
                    }
                    else
                    {
                        AM.wAlt_Identity_Proof = "None";
                        cmdw.Parameters.AddWithValue("@Alt_Identity_proof", AM.wAlt_Identity_Proof);
                    }


                    if (AM.wAlt_Identity_Proof_Value != null)
                    {
                        cmdw.Parameters.AddWithValue("@Alt_Identity_proof_value", AM.wAlt_Identity_Proof_Value);
                    }
                    else
                    {
                        AM.wAlt_Identity_Proof_Value = "None";
                        cmdw.Parameters.AddWithValue("@Alt_Identity_proof_value", AM.wAlt_Identity_Proof_Value);
                    }









                    //DateTime dat = DateTime.ParseExact(AM.Dob, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                    //cmd.Parameters.AddWithValue("@DOB", "None");
                    cmdw.Parameters.AddWithValue("@Gender", AM.wGender);
                    cmdw.Parameters.AddWithValue("@Occupation", "None");
                    cmdw.Parameters.AddWithValue("@Relationship", "None");
                    cmdw.Parameters.AddWithValue("@Address1", AM.wAddress1);
                    if (AM.wAddress2 != null || AM.wAddress2 == "")
                    {
                        cmdw.Parameters.AddWithValue("@Address2", AM.wAddress2);
                    }
                    else
                    {
                        AM.wAddress2 = "None";
                        cmdw.Parameters.AddWithValue("@Address2", AM.wAddress2);
                    }


                    if (AM.wAddress3 != null || AM.wAddress3 == "")
                    {
                        cmdw.Parameters.AddWithValue("@Address3", AM.wAddress3);
                    }
                    else
                    {
                        AM.wAddress3 = "None";
                        cmdw.Parameters.AddWithValue("@Address3", AM.wAddress3);
                    }


                    cmdw.Parameters.AddWithValue("@City", AM.wcitytext);
                    cmdw.Parameters.AddWithValue("@State", AM.wstatetext);
                    cmdw.Parameters.AddWithValue("@Pin", AM.wPin);
                    cmdw.Parameters.AddWithValue("@tid", AM.ddltid);
                    cmdw.Parameters.AddWithValue("@ExecutorType", "Single");
                    cmdw.ExecuteNonQuery();
                    con.Close();


                    int appid22 = 0;
                    con.Open();
                    string query22 = "select top 1 * from Appointees order by apId desc";
                    SqlDataAdapter da22 = new SqlDataAdapter(query22, con);
                    DataTable dt22 = new DataTable();
                    da22.Fill(dt22);
                    if (dt22.Rows.Count > 0)
                    {
                        appid22 = Convert.ToInt32(dt22.Rows[0]["apId"]);
                        apid = 1; // for yes
                    }
                    else
                    {
                        apid = 2; //for no
                    }
                    con.Close();



                    //end

                    // update document status

                    con.Open();
                    string qte22 = "update Appointees set documentstatus = 'Incompleted' , WillType='" + Session["WillType"].ToString() + "' where apId =" + appid22 + " ";
                    SqlCommand cmdte22 = new SqlCommand(qte22, con);
                    cmdte22.ExecuteNonQuery();
                    con.Close();


                    //end


                    con.Open();
                    string qt22 = "update Appointees set doctype = 'Will'  where  apId = " + Convert.ToInt32(dt22.Rows[0]["apId"]) + "";
                    SqlCommand cmdt22 = new SqlCommand(qt22, con);
                    cmdt.ExecuteNonQuery();
                    con.Close();









                    ////////////////////////////////////////end//////////////////////////////////////////////////////////////



                }
            }
            else
            {
                RedirectToAction("LoginPageIndex", "LoginPage");
            }

            TempData["Message"] = "true";

            // dropdown selection
            int AppointmentofGuardian = 0;
            if (AM.Typetxt == "Guardian")
            {
                AppointmentofGuardian = 1;    //yes
            }
            else
            {
                AppointmentofGuardian = 2;    // no
            }

            int Numberofexecutors = 0;
            if (AM.subTypetxt == "Single")
            {
                Numberofexecutors = 1;
            }
            if (AM.subTypetxt == "Many Joint")
            {
                Numberofexecutors = 2;
            }
            if (AM.subTypetxt == "Many Independent")
            {
                Numberofexecutors = 3;
            }

            //end

            // Document Rules

            //get latest id first
            con.Open();
            string getquery = "select top 1 * from documentRules order by wdId desc";
            SqlDataAdapter da = new SqlDataAdapter(getquery, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            int getruleid = 0;
            if (dt.Rows.Count > 0)
            {
                getruleid = Convert.ToInt32(dt.Rows[0]["wdId"]);
            }
            con.Close();

            //end



            con.Open();
            string rulequery = "update documentRules set guardian = " + AppointmentofGuardian + " ,executors_category = " + Numberofexecutors + " where tid = " + AM.ddltid + " ";
            SqlCommand cmd2 = new SqlCommand(rulequery, con);
            cmd2.ExecuteNonQuery();
            con.Close();
            //end














            ViewBag.Message = "Verified";


            // latest appointees
            int altapid = 0;
            con.Open();
            string query4 = "select top 1 * from alternate_Appointees order by apId desc";
            SqlDataAdapter da4 = new SqlDataAdapter(query4, con);
            DataTable dt4 = new DataTable();
            da4.Fill(dt4);
            if (dt4.Rows.Count > 0)
            {

                altapid = 1; // for yes
            }
            else
            {
                altapid = 2; //for no
            }
            con.Close();



            //end



            // dropdown selection
            int AppointmentofaltGuardian = 0;
            if (AM.altguardian == "Guardian")
            {
                AppointmentofaltGuardian = 1;    //yes
            }
            else
            {
                AppointmentofaltGuardian = 2;    // no
            }

            int altNumberofexecutors = 2;
            if (AM.altexecutor == "Single")
            {
                altNumberofexecutors = 1;
            }
            if (AM.altexecutor == "Many Joint")
            {
                altNumberofexecutors = 1;
            }
            if (AM.altexecutor == "Many Independent")
            {
                altNumberofexecutors = 1;
            }

            //end

            // Document Rules

            //get latest id first
            con.Open();
            string getquery4 = "select top 1 * from documentRules order by wdId desc";
            SqlDataAdapter da5 = new SqlDataAdapter(getquery4, con);
            DataTable dt5 = new DataTable();
            da5.Fill(dt5);
            int getruleid2 = 0;
            if (dt.Rows.Count > 0)
            {
                getruleid2 = Convert.ToInt32(dt5.Rows[0]["wdId"]);
            }
            con.Close();

            //end



            con.Open();
            string rulequery2 = "update documentRules set AlternateGaurdian = " + AppointmentofaltGuardian + " , AlternateExecutors = " + altNumberofexecutors + " where tid = " + AM.ddltid + " ";
            SqlCommand cmd6 = new SqlCommand(rulequery2, con);
            cmd6.ExecuteNonQuery();
            con.Close();
            //end


            // update document master with latest rule id

            con.Open();
            string rquery2 = "update documentMaster set wdId = " + getruleid2 + " where tId =  " + AM.ddltid + "  ";
            SqlCommand rcmd2 = new SqlCommand(rquery2, con);
            rcmd2.ExecuteNonQuery();
            con.Close();




            //end







            

           


            TempData["Message"] = "true";
            ModelState.Clear();

            return RedirectToAction("AddwitnessIndex", "Addwitness");
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


                    con.Open();
                    string query2 = "select * from Appointees where tId =  " + Convert.ToInt32(dt.Rows[0]["tId"]) + " ";
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




                            data = data + "<option value=" + dt.Rows[i]["tId"].ToString() + " >" + dt.Rows[i]["First_Name"].ToString() + "</option> " + "~" + dt.Rows[i]["tId"].ToString() + "~" + popup;



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



        public int CheckTestatorUsers(string value, string checkstatus)
        {
            int check = 0;
            if (checkstatus != "true")
            {

                int Response = Convert.ToInt32(Request["send"]);

                if (Request["send"] != "")
                {
                    // check for data exists or not for testato family
                    if (value != null)
                    {
                        con.Open();
                        string query1 = "select * from Appointees where tid = " + value + " ";
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





        public ActionResult UpdateAppointees(AppointeesModel AM)
        {

            con.Open();
            SqlCommand cmd = new SqlCommand("SP_CRUDAppointees", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@condition", "update");
            cmd.Parameters.AddWithValue("@apId", AM.apId);
            cmd.Parameters.AddWithValue("@documentId", AM.documentId);
            cmd.Parameters.AddWithValue("@Type", "Witness");
            cmd.Parameters.AddWithValue("@subType", AM.wTypetxt);
            cmd.Parameters.AddWithValue("@Name", AM.Firstname);
            cmd.Parameters.AddWithValue("@middleName", AM.middleName);
            cmd.Parameters.AddWithValue("@Surname", AM.Surname);
            cmd.Parameters.AddWithValue("@Identity_proof", AM.Identity_Proof);
            cmd.Parameters.AddWithValue("@Identity_proof_value", AM.Identity_Proof_Value);
            cmd.Parameters.AddWithValue("@Alt_Identity_proof", AM.Alt_Identity_Proof);
            cmd.Parameters.AddWithValue("@Alt_Identity_proof_value", AM.Alt_Identity_Proof_Value);
            //DateTime dat = DateTime.ParseExact(AM.Dob, "dd-MM-yyyy", CultureInfo.InvariantCulture);


            cmd.Parameters.AddWithValue("@DOB", Convert.ToDateTime(AM.Dob).ToString("yyyy-MM-dd"));
            cmd.Parameters.AddWithValue("@Gender", AM.Gender);
            cmd.Parameters.AddWithValue("@Occupation", "None");
            cmd.Parameters.AddWithValue("@Relationship", "None");
            cmd.Parameters.AddWithValue("@Address1", AM.Address1);
            cmd.Parameters.AddWithValue("@Address2", AM.Address2);
            cmd.Parameters.AddWithValue("@Address3", AM.Address3);
            cmd.Parameters.AddWithValue("@City", AM.citytext);
            cmd.Parameters.AddWithValue("@State", AM.statetext);
            cmd.Parameters.AddWithValue("@Pin", AM.Pin);
            cmd.Parameters.AddWithValue("@tid", AM.ddltid);
            cmd.Parameters.AddWithValue("@ExecutorType", "Single");


            cmd.ExecuteNonQuery();
            con.Close();




            con.Open();

            SqlCommand cmd2 = new SqlCommand("SP_CRUDAppointees", con);
            cmd2.CommandType = CommandType.StoredProcedure;
            cmd2.Parameters.AddWithValue("@condition", "update");
            cmd2.Parameters.AddWithValue("@apId", AM.wapId);
            cmd2.Parameters.AddWithValue("@documentId", AM.wdocumentId);
            cmd2.Parameters.AddWithValue("@Type", "Witness");
            cmd2.Parameters.AddWithValue("@subType", AM.wsubTypetxt);
            cmd2.Parameters.AddWithValue("@Name", AM.wFirstname);
            cmd2.Parameters.AddWithValue("@middleName", AM.wmiddleName);
            cmd2.Parameters.AddWithValue("@Surname", AM.wSurname);
            cmd2.Parameters.AddWithValue("@Identity_proof", AM.wIdentity_Proof);
            cmd2.Parameters.AddWithValue("@Identity_proof_value", AM.wIdentity_Proof_Value);
            cmd2.Parameters.AddWithValue("@Alt_Identity_proof", AM.wAlt_Identity_Proof);
            cmd2.Parameters.AddWithValue("@Alt_Identity_proof_value", AM.wAlt_Identity_Proof_Value);
            //DateTime dat = DateTime.ParseExact(AM.Dob, "dd-MM-yyyy", CultureInfo.InvariantCulture);


            cmd2.Parameters.AddWithValue("@DOB", Convert.ToDateTime(AM.Dob).ToString("yyyy-MM-dd"));
            cmd2.Parameters.AddWithValue("@Gender", AM.wGender);
            cmd2.Parameters.AddWithValue("@Occupation", "None");
            cmd2.Parameters.AddWithValue("@Relationship", "None");
            cmd2.Parameters.AddWithValue("@Address1", AM.wAddress1);
            cmd2.Parameters.AddWithValue("@Address2", AM.wAddress2);
            cmd2.Parameters.AddWithValue("@Address3", AM.wAddress3);
            cmd2.Parameters.AddWithValue("@City", AM.wcitytext);
            cmd2.Parameters.AddWithValue("@State", AM.wstatetext);
            cmd2.Parameters.AddWithValue("@Pin", AM.wPin);
            cmd2.Parameters.AddWithValue("@tid", AM.ddltid);
            cmd2.Parameters.AddWithValue("@ExecutorType", "Single");
            cmd2.ExecuteNonQuery();

            con.Close();



            if (Convert.ToInt32(Session["upappointeesid"]) != 0)
            {
                AM.check = "true";
            }


            if (AM.check == "true")
            {
                con.Open();
                SqlCommand cmdd = new SqlCommand("SP_CRUDAlternateAppointees", con);
                cmdd.CommandType = CommandType.StoredProcedure;
                cmdd.Parameters.AddWithValue("@condition", "update");
                cmdd.Parameters.AddWithValue("@id", AM.altapId);
                cmdd.Parameters.AddWithValue("@apId", AM.apId);


                cmdd.Parameters.AddWithValue("@Name", AM.altName);
                cmdd.Parameters.AddWithValue("@middleName", AM.altmiddleName);
                cmdd.Parameters.AddWithValue("@Surname", AM.altSurname);
                cmdd.Parameters.AddWithValue("@Identity_proof", AM.altIdentity_Proof);
                cmdd.Parameters.AddWithValue("@Identity_proof_value", AM.altIdentity_Proof_Value);

                if (AM.altAlt_Identity_Proof != null)
                {
                    cmdd.Parameters.AddWithValue("@Alt_Identity_proof", AM.altAlt_Identity_Proof);
                }
                else
                {
                    AM.altAlt_Identity_Proof = "None";
                    cmdd.Parameters.AddWithValue("@Alt_Identity_proof", AM.altAlt_Identity_Proof);
                }


                if (AM.altAlt_Identity_Proof_Value != null)
                {
                    cmdd.Parameters.AddWithValue("@Alt_Identity_proof_value", AM.altAlt_Identity_Proof_Value);
                }
                else
                {
                    AM.altAlt_Identity_Proof_Value = "None";
                    cmdd.Parameters.AddWithValue("@Alt_Identity_proof_value", AM.altAlt_Identity_Proof_Value);
                }



                //DateTime dat2 = DateTime.ParseExact(AM.altDob, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                cmdd.Parameters.AddWithValue("@DOB", Convert.ToDateTime(AM.altDob).ToString("dd-MM-yyyy"));
                cmdd.Parameters.AddWithValue("@Gender", AM.altGender);
                cmdd.Parameters.AddWithValue("@Occupation", AM.altOccupation);
                cmdd.Parameters.AddWithValue("@Relationship", AM.altRelationshipTxt);
                cmdd.Parameters.AddWithValue("@Address1", AM.altAddress1);
                cmdd.Parameters.AddWithValue("@Address2", AM.altAddress2);
                cmdd.Parameters.AddWithValue("@Address3", AM.altAddress3);
                cmdd.Parameters.AddWithValue("@City", AM.altcitytext);
                cmdd.Parameters.AddWithValue("@State", AM.altstatetext);
                cmdd.Parameters.AddWithValue("@Pin", AM.altPin);
                cmdd.Parameters.AddWithValue("@tid", AM.ddltid);
                cmdd.ExecuteNonQuery();
                con.Close();
            }






            ViewBag.Message = "Verified";
            return RedirectToAction("AddwitnessIndex", "Addwitness");



        }






        public string Validateidentity()
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




            string query2 = "select Identity_proof_Value from TestatorDetails  where tId = " + tid + " ";
            SqlDataAdapter da2 = new SqlDataAdapter(query2, con);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);

            if (dt2.Rows.Count > 0)
            {

                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    if (dt2.Rows[i]["Identity_proof_Value"].ToString() == response)
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





        public string altValidateidentity()
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




            string query2 = "select Alt_Identity_proof_value from TestatorDetails  where tId = " + tid + "  ";
            SqlDataAdapter da2 = new SqlDataAdapter(query2, con);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);

            if (dt2.Rows.Count > 0)
            {

                if (dt2.Rows[0]["Alt_Identity_proof_value"].ToString() == response)
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