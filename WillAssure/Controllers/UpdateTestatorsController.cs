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
using System.Net.Mail;
using System.Net;

namespace WillAssure.Controllers
{
    public class UpdateTestatorsController : Controller
    {
        public static string connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        SqlConnection con = new SqlConnection(connectionString);


        // GET: UpdateTestators
        public ActionResult UpdateTestatorsIndex(int NestId,string documenttype)
        {





            string queryc1 = "";
            string queryc2 = "";
            string queryc3 = "";
            string queryc4 = "";
            string queryc5 = "";
            string queryc6 = "";
            string queryc7 = "";
            string queryc22 = "";







            Session["testatorID"] = NestId;

            ViewBag.collapse = "true";
            ViewBag.cod = "true";

            if (TempData["Message"] != null)
            {
                if (TempData["Message"].ToString() == "true")
                {
                    ViewBag.Message = "Verified";
                }
            }


            if (Session["Type"] != null)
            {

                if (Session["Type"].ToString() == "SuperAdmin")
                {
                    if (documenttype != null && documenttype != "")
                    {
                        if (documenttype == "'Quick'")
                        {
                            Session["doctype"] = "Will";
                            documenttype = documenttype.Substring(1);
                            documenttype = documenttype.Substring(0,documenttype.Length - 1);
                            Session["WillType"] = documenttype;
                        }

                        if (documenttype == "Detailed")
                        {

                            Session["doctype"] = "'Will'";
                            Session["WillType"] = documenttype;
                        }

                     
                   
                    }



                    ViewBag.enablebtnremaining = "true";
                }
            }
            else
            {
                RedirectToAction("LoginPageIndex", "LoginPage");
            }



            con.Close();





            //end




            // enable remaining form button

            con.Open();


            //// latest tid

            string queryte = "select top 1 tId from TestatorDetails where WillType = '" + Session["WillType"] + "' order by tId desc";
            SqlDataAdapter date = new SqlDataAdapter(queryte, con);
            DataTable dtte = new DataTable();
            date.Fill(dtte);
            int test = 0;
            if (dtte.Rows.Count > 0)
            {
                test = Convert.ToInt32(dtte.Rows[0]["tId"]);
            }



            string querycou = "select Country from TestatorDetails where tId = " + test + "";
            SqlDataAdapter dacou = new SqlDataAdapter(querycou, con);
            DataTable dtcou = new DataTable();
            dacou.Fill(dtcou);

            if (dtcou.Rows.Count > 0)
            {
                if (dtcou.Rows[0]["Country"].ToString() != "none")
                {
                    ViewBag.enablebtnremaining = "true";
                }



            }

            con.Close();
            //end





            //if (NestId == 0)
            //{
            int getid = 0;
            if (Session["uuid"] != null)
            {
                getid = Convert.ToInt32(Session["uuid"]);
            }
            else
            {
                RedirectToAction("LoginPageIndex", "LoginPage");
            }


            string qq26 = "";



            con.Open();

            if (Session["WillType"] != null && Session["doctype"] != null)
            {
                if (Session["WillType"].ToString() == "Quick" && Session["doctype"].ToString() == "Will")
                {
                    qq26 = "select top 1 tId from TestatorDetails where uId = " + getid + " order by  tid desc ";

                    SqlDataAdapter daa26 = new SqlDataAdapter(qq26, con);
                    DataTable dtt26 = new DataTable();
                    daa26.Fill(dtt26);
                    if (dtt26.Rows.Count > 0)
                    {
                        NestId = Convert.ToInt32(dtt26.Rows[0]["tId"]);
                    }
                }
                if (Session["WillType"].ToString() == "Detailed" && Session["doctype"].ToString() == "Will")
                {
                    qq26 = "select top 1 tId from TestatorDetails where uId = " + getid + " order by  tid desc ";

                    SqlDataAdapter daa26 = new SqlDataAdapter(qq26, con);
                    DataTable dtt26 = new DataTable();
                    daa26.Fill(dtt26);
                    if (dtt26.Rows.Count > 0)
                    {
                        NestId = Convert.ToInt32(dtt26.Rows[0]["tId"]);
                    }
                }
                if (Session["doctype"].ToString() == "POA")
                {
                    qq26 = "select top 1 tId from TestatorDetails where uId = " + getid + " order by  tid desc ";

                    SqlDataAdapter daa26 = new SqlDataAdapter(qq26, con);
                    DataTable dtt26 = new DataTable();
                    daa26.Fill(dtt26);
                    if (dtt26.Rows.Count > 0)
                    {
                        NestId = Convert.ToInt32(dtt26.Rows[0]["tId"]);
                    }
                }
                if (Session["doctype"].ToString() == "GiftDeeds")
                {
                    qq26 = "select top 1 tId from TestatorDetails where uId = " + getid + " order by  tid desc ";

                    SqlDataAdapter daa26 = new SqlDataAdapter(qq26, con);
                    DataTable dtt26 = new DataTable();
                    daa26.Fill(dtt26);
                    if (dtt26.Rows.Count > 0)
                    {
                        NestId = Convert.ToInt32(dtt26.Rows[0]["tId"]);
                    }
                }
            }
            else
            {
                RedirectToAction("LoginPageIndex", "LoginPage");
            }

            

           




            




            
           


            con.Close();
            //}

            // check type 
            string typ = "";
            string upquery = "";
            con.Open();

            if (Session["doctype"] != null)
            {
                upquery = "update TestatorDetails set WillType='" + Session["WillType"].ToString() + "' , doctype='" + Session["doctype"].ToString() + "' where tId = " + NestId + "";
                SqlCommand upcmd = new SqlCommand(upquery, con);
                upcmd.ExecuteNonQuery();
            }
            else
            {
                RedirectToAction("LoginPageIndex", "LoginPage");
            }

             


           


            string qq1 = "select Type  from users where uId = " + getid + " ";
            SqlDataAdapter daa = new SqlDataAdapter(qq1, con);
            DataTable dtt = new DataTable();
            daa.Fill(dtt);
            con.Close();
            string willtype = "";
            if (dtt.Rows.Count > 0)
            {
                typ = dtt.Rows[0]["Type"].ToString();
                
            }



            //end


         






            if (typ == "Testator")
            {


                con.Open();
                string qq12 = "select Type from users where uId = " + getid + " and designation = 1 ";
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
            TestatorFormModel TFM = new TestatorFormModel();

            con.Open();
            string query = "select * from TestatorDetails where tId = '" + NestId + "' ";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();


            if (dt.Rows.Count > 0)
            {

                ViewBag.hasdata = "true";
                TFM.tId = NestId;


                TFM.First_Name = dt.Rows[0]["First_Name"].ToString();
                if (Session["Type"] != null)
                {
                    if (Session["Type"].ToString() == "DistributorAdmin" || Session["Type"].ToString() == "SuperAdmin" || Session["Type"].ToString() == "Testator")
                    {
                        Session["distid"] = NestId;
                        Session["disttestator"] = TFM.First_Name;
                    }
                }
                else
                {
                    RedirectToAction("LoginPageIndex", "LoginPage");
                }

                TFM.Last_Name = dt.Rows[0]["Last_Name"].ToString();
                TFM.Dob = Convert.ToDateTime(dt.Rows[0]["DOB"]).ToString("dd/MM/yyyy");

                if (dt.Rows[0]["Middle_Name"].ToString() != "None")
                {
                    TFM.Middle_Name = dt.Rows[0]["Middle_Name"].ToString();
                }

                

                if (dt.Rows[0]["Occupation"].ToString() != "none")
                {
                    TFM.Occupation = dt.Rows[0]["Occupation"].ToString();
                }

                TFM.Mobile = dt.Rows[0]["Mobile"].ToString();
                TFM.Email = dt.Rows[0]["Email"].ToString();

                if (dt.Rows[0]["maritalStatus"].ToString() != "none")
                {
                    TFM.material_status_txt = dt.Rows[0]["maritalStatus"].ToString();
                }

                if (dt.Rows[0]["Religion"].ToString() != "none")
                {
                    TFM.Religiontext = dt.Rows[0]["Religion"].ToString();
                }

                if (dt.Rows[0]["RelationShip"].ToString() != "none")
                {
                    TFM.RelationshipTxt = dt.Rows[0]["RelationShip"].ToString();
                }


                if (dt.Rows[0]["Identity_Proof"].ToString() != "none")
                {
                    TFM.Identity_Proof = dt.Rows[0]["Identity_Proof"].ToString();
                }


                if (dt.Rows[0]["Identity_proof_Value"].ToString() != "none")
                {
                    TFM.Identity_proof_Value = dt.Rows[0]["Identity_proof_Value"].ToString();
                }

                if (dt.Rows[0]["Alt_Identity_Proof"].ToString() != "none")
                {
                    TFM.Alt_Identity_Proof = dt.Rows[0]["Alt_Identity_Proof"].ToString();
                }


                if (dt.Rows[0]["Alt_Identity_proof_Value"].ToString() != "none")
                {
                    TFM.Alt_Identity_proof_Value = dt.Rows[0]["Alt_Identity_proof_Value"].ToString();
                }

                if (dt.Rows[0]["Gender"].ToString() != "none                ")
                {
                    TFM.Gendertxt = dt.Rows[0]["Gender"].ToString();

                    if (TFM.Gendertxt == "Male                ")
                    {
                        ViewBag.gender = "Father";
                    }


                    if (TFM.Gendertxt == "Female              ")
                    {
                        ViewBag.gender = "Spouse";
                    }

                }
            

                if (dt.Rows[0]["Address1"].ToString() != "none")
                {
                    TFM.Address1 = dt.Rows[0]["Address1"].ToString();
                }

                if (dt.Rows[0]["Address2"].ToString() != "none")
                {
                    TFM.Address2 = dt.Rows[0]["Address2"].ToString();
                }

                if (dt.Rows[0]["Address3"].ToString() != "none")
                {
                    TFM.Address3 = dt.Rows[0]["Address3"].ToString();
                }

                if (dt.Rows[0]["City"].ToString() != "none")
                {
                    TFM.citytext = dt.Rows[0]["City"].ToString();
                }

                if (dt.Rows[0]["State"].ToString() != "none")
                {
                    TFM.statetext = dt.Rows[0]["State"].ToString();
                }

                if (dt.Rows[0]["Country"].ToString() != "none")
                {
                    TFM.countrytext = dt.Rows[0]["Country"].ToString();
                }

                if (dt.Rows[0]["Pin"].ToString() != "none")
                {
                    TFM.Pin = dt.Rows[0]["Pin"].ToString();
                }

                if (dt.Rows[0]["active"].ToString() != "none")
                {
                    TFM.active = dt.Rows[0]["active"].ToString();
                }


               
                 TFM.Spousetxt = dt.Rows[0]["SpouseName"].ToString();
                


                TFM.uId = Convert.ToInt32(dt.Rows[0]["uId"]);


                TFM.tempemailotp = dt.Rows[0]["Email_OTP"].ToString();
                TFM.tempmobileotp = dt.Rows[0]["Mobile_OTP"].ToString();




            }







            if (Session["doctype"] != null)
            {

                if (Session["doctype"].ToString() == "POA")
                {
                    Session["WillType"] = "POA";
                }

                if (Session["doctype"].ToString() == "GiftDeeds")
                {
                    Session["WillType"] = "GiftDeedss";
                }

            }
            else
            {
                return RedirectToAction("LoginPageIndex", "LoginPage");
            }







            //// check data for next page link if available active links

            if (Session["distid"] != null && Session["willtype"] != null && Session["doctype"] != null)
            {
                con.Open();




                //////// check TesttaorFamily 


                if (Session["WillType"].ToString() == "Quick" && Session["doctype"].ToString() == "Will")
                {
                    queryc22 = "select * from Appointees where WillType = 'Quick'  and tId = " + Convert.ToInt32(Session["distid"]) + " and Type = 'Guardian'   ";
                }
                if (Session["WillType"].ToString() == "Detailed" && Session["doctype"].ToString() == "Will")
                {
                    queryc22 = "select * from Appointees where WillType = 'Detailed'  and tId = " + Convert.ToInt32(Session["distid"]) + "  and Type = 'Guardian'  ";
                }
          


                SqlDataAdapter dac22 = new SqlDataAdapter(queryc22, con);
                DataTable dtc22 = new DataTable();
                dac22.Fill(dtc22);

                if (dtc22.Rows.Count > 0)
                {
                    ViewBag.beneactive = "true";
                }


                /////end















                //////// check beneficiary institution


                if (Session["WillType"].ToString() == "Quick" && Session["doctype"].ToString() == "Will")
                {
                    queryc1 = "select * from BeneficiaryInstitutions where tId = " + Convert.ToInt32(Session["distid"]) + "  and WillType = 'Quick'     ";
                }
                if (Session["WillType"].ToString() == "Detailed" && Session["doctype"].ToString() == "Will")
                {
                    queryc1 = "select * from BeneficiaryInstitutions where tId = " + Convert.ToInt32(Session["distid"]) + "  and WillType = 'Detailed'     ";
                }
                //if (Session["doctype"].ToString() == "POA")
                //{
                //    queryc1 = "select * from BeneficiaryInstitutions where tId = " + Convert.ToInt32(Session["distid"]) + "  and doctype = 'POA'    ";
                //}
                //if (Session["doctype"].ToString() == "Giftdeeds")
                //{
                //    queryc1 = "select * from BeneficiaryInstitutions where tId = " + Convert.ToInt32(Session["distid"]) + "  and doctype = 'Giftdeeds'    ";
                //}




                SqlDataAdapter dac1 = new SqlDataAdapter(queryc1, con);
                DataTable dtc1 = new DataTable();
                dac1.Fill(dtc1);

                if (dtc1.Rows.Count > 0)
                {
                    ViewBag.beneinstitureactive = "true";
                }


                /////end






                //////// check beneficiary 


                if (Session["WillType"].ToString() == "Quick" && Session["doctype"].ToString() == "Will")
                {
                    queryc2 = "select * from BeneficiaryDetails where tId = " + Convert.ToInt32(Session["distid"]) + " and doctype = 'Will' and WillType = 'Quick'  and tId = " + Convert.ToInt32(Session["distid"]) + "  and beneficiary_type='Beneficiary'  ";
                }
                if (Session["WillType"].ToString() == "Detailed" && Session["doctype"].ToString() == "Will")
                {
                    queryc2 = "select * from BeneficiaryDetails where tId = " + Convert.ToInt32(Session["distid"]) + " and doctype = 'Will' and WillType = 'Detailed'  and tId = " + Convert.ToInt32(Session["distid"]) + "  and beneficiary_type='Beneficiary'  ";
                }
                if (Session["doctype"].ToString() == "POA")
                {
                    queryc2 = "select * from BeneficiaryDetails where tId = " + Convert.ToInt32(Session["distid"]) + " and doctype = 'POA'   and tId = " + Convert.ToInt32(Session["distid"]) + "  and beneficiary_type='Beneficiary'  ";
                }
                if (Session["doctype"].ToString() == "Giftdeeds")
                {
                    queryc2 = "select * from BeneficiaryDetails where tId = " + Convert.ToInt32(Session["distid"]) + " and doctype = 'Giftdeeds'   and tId = " + Convert.ToInt32(Session["distid"]) + "  and beneficiary_type='Beneficiary'  ";
                }


                SqlDataAdapter dac2 = new SqlDataAdapter(queryc2, con);
                DataTable dtc2 = new DataTable();
                dac2.Fill(dtc2);

                if (dtc2.Rows.Count > 0)
                {
                    ViewBag.beneactive = "true";
                }


                /////end







                //////// check Executor 


                if (Session["WillType"].ToString() == "Quick" && Session["doctype"].ToString() == "Will")
                {
                    queryc3 = "select * from Appointees where Type = 'Executor' and doctype = 'Will' and WillType = 'Quick'  and tId = " + Convert.ToInt32(Session["distid"]) + " ";
                }
                if (Session["WillType"].ToString() == "Detailed" && Session["doctype"].ToString() == "Will")
                {
                    queryc3 = "select * from Appointees where Type = 'Executor' and doctype = 'Will' and WillType = 'Detailed'  and tId = " + Convert.ToInt32(Session["distid"]) + " ";
                }
                if (Session["doctype"].ToString() == "POA")
                {
                    queryc3 = "select * from Appointees where Type = 'Executor' and doctype = 'POA'  and tId = " + Convert.ToInt32(Session["distid"]) + " ";
                }
                if (Session["doctype"].ToString() == "Giftdeeds")
                {
                    queryc3 = "select * from Appointees where Type = 'Executor' and doctype = 'Giftdeeds' and  tId = " + Convert.ToInt32(Session["distid"]) + " ";
                }




                SqlDataAdapter dac3 = new SqlDataAdapter(queryc3, con);
                DataTable dtc3 = new DataTable();
                dac3.Fill(dtc3);

                if (dtc3.Rows.Count > 0)
                {
                    ViewBag.executoractive = "true";
                }


                /////end



                //////// check Witness 

                if (Session["WillType"].ToString() == "Quick" && Session["doctype"].ToString() == "Will")
                {
                    queryc4 = "select * from Appointees where Type = 'Witness' and doctype = 'Will' and WillType='Quick'   and tId = " + Convert.ToInt32(Session["distid"]) + " ";
                }
                if (Session["WillType"].ToString() == "Detailed" && Session["doctype"].ToString() == "Will")
                {
                    queryc4 = "select * from Appointees where Type = 'Witness' and doctype = 'Will' and WillType='Detailed'   and tId = " + Convert.ToInt32(Session["distid"]) + " ";
                }
                if (Session["doctype"].ToString() == "POA")
                {
                    queryc4 = "select * from Appointees where Type = 'Witness' and doctype = 'POA'  and tId = " + Convert.ToInt32(Session["distid"]) + " ";
                }
                if (Session["doctype"].ToString() == "Giftdeeds")
                {
                    queryc4 = "select * from Appointees where Type = 'Witness' and doctype = 'Giftdeeds'   and tId = " + Convert.ToInt32(Session["distid"]) + " ";
                }



                SqlDataAdapter dac4 = new SqlDataAdapter(queryc4, con);
                DataTable dtc4 = new DataTable();
                dac4.Fill(dtc4);

                if (dtc4.Rows.Count > 0)
                {
                    ViewBag.witnessactive = "true";
                }


                /////end








                /////////////////////////mAPPING

                if (Session["WillType"].ToString() == "Quick" && Session["doctype"].ToString() == "Will")
                {
                    queryc5 = "select * from BeneficiaryAssets where WillType = 'Quick' and doctype = 'Will' and tid = " + Convert.ToInt32(Session["distid"]) + " ";
                }
                if (Session["WillType"].ToString() == "Detailed" && Session["doctype"].ToString() == "Will")
                {
                    queryc5 = "select * from BeneficiaryAssets where WillType = 'Detailed' and doctype = 'Will' and tid = " + Convert.ToInt32(Session["distid"]) + " ";
                }
                if (Session["doctype"].ToString() == "POA")
                {
                    queryc5 = "select * from BeneficiaryAssets where  doctype = 'POA' and tid = " + Convert.ToInt32(Session["distid"]) + " ";
                }
                if (Session["doctype"].ToString() == "Giftdeeds")
                {
                    queryc5 = "select * from BeneficiaryAssets where doctype = 'Giftdeeds' and tid = " + Convert.ToInt32(Session["distid"]) + " ";
                }



                SqlDataAdapter dac5 = new SqlDataAdapter(queryc5, con);
                DataTable dtc5 = new DataTable();
                dac5.Fill(dtc5);

                if (dtc5.Rows.Count > 0)
                {
                    ViewBag.mappingactive = "true";
                }














                /////////////////////////Testator Details

                if (Session["WillType"].ToString() == "Quick" && Session["doctype"].ToString() == "Will")
                {
                    queryc6 = "select * from testatorFamily where  WillType = 'Quick' and tid = "+ Convert.ToInt32(Session["distid"]) +"";
                }
                if (Session["WillType"].ToString() == "Detailed" && Session["doctype"].ToString() == "Will")
                {
                    queryc6 = "select * from testatorFamily where  WillType = 'Detailed' and tid = " + Convert.ToInt32(Session["distid"]) + "";
                }
              



                SqlDataAdapter dac6 = new SqlDataAdapter(queryc6, con);
                DataTable dtc6 = new DataTable();
                dac6.Fill(dtc6);

                if (dtc6.Rows.Count > 0)
                {
                    ViewBag.tfactive = "true";
                }






                /////////////////////////assetinformation

                if (Session["WillType"].ToString() == "Quick" && Session["doctype"].ToString() == "Will")
                {

                    queryc7 = "select * from AssetInformation where doctype = 'Will' and WillType = 'Quick' and tid = " + Convert.ToInt32(Session["distid"]) + " ";

                }


                if (Session["WillType"].ToString() == "Detailed" && Session["doctype"].ToString() == "Will")
                {

                    queryc7 = "select * from AssetInformation where doctype = 'Will' and WillType = 'Detailed' and tid = " + Convert.ToInt32(Session["distid"]) + " ";

                }
                if (Session["doctype"].ToString() == "POA")
                {
                    queryc7 = "select * from AssetInformation where   doctype = 'POA' and tid = " + Convert.ToInt32(Session["distid"]) + " ";
                }
                if (Session["doctype"].ToString() == "Giftdeeds")
                {
                    queryc7 = "select * from AssetInformation where  doctype = 'Giftdeeds' and tid = " + Convert.ToInt32(Session["distid"]) + " ";
                }



                SqlDataAdapter dac7 = new SqlDataAdapter(queryc7, con);
                DataTable dtc7 = new DataTable();
                dac7.Fill(dtc7);

                if (dtc7.Rows.Count > 0)
                {
                    ViewBag.assetinformationactive = "true";
                }




                ////////END











                con.Close();






                //end



            }
            else
            {
                return RedirectToAction("LoginPageIndex", "LoginPage");
            }






            con.Open();

            // get tid
            if (Session["Type"].ToString() != "SuperAdmin")
            {
               


                //end





                // check testator family
                string query1 = "select beneficiary_type from BeneficiaryDetails where  tid=" + Session["distid"] + "";
                SqlDataAdapter da1 = new SqlDataAdapter(query1, con);
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);
                if (dt1.Rows.Count > 0)
                {
                    if (dt1.Rows[0]["beneficiary_type"].ToString() == "TestatorFamily" || dt1.Rows[0]["beneficiary_type"].ToString() == "Beneficiary" || dt1.Rows[0]["beneficiary_type"].ToString() == "Institution")
                    {
                        ViewBag.beneactive = "true";
                        
                    }



                }
               





            }
            




            return View("~/Views/UpdateTestators/UpdateTestatorPageContent.cshtml", TFM);
        }




        public string dateofbirth()
        {

            string data = "<option Value=''>--Select--</option>";
            string date = Request["send"].ToString();
            date = date.Substring(6, date.Length - 6);
            var today = DateTime.Now.Year;
            int age = today - int.Parse(date);

            string msg = "NoData";
            if (date != null)
            {
                // identify if guardian or not


                if (age != 0)
                {
                    if (age <= 18)
                    {
                       
                        msg = "true";


                        

                    }
                    else
                    {
                        msg = "false";

                        


                    }

                }




                //end

            }


            return msg;

        }



        public ActionResult index()
        {

            ViewBag.collapse = "true";
            ViewBag.cod = "true";











            int NestId = 0;
            //if (NestId == 0)
            //{
            int getid = 0;
            if (Session["uuid"] != null)
            {
                  getid = Convert.ToInt32(Session["uuid"]);
            }
            else
            {
                RedirectToAction("LoginPageIndex", "LoginPage");
            }

             
            con.Open();
            string qq26 = "select tId from TestatorDetails where uId = " + Convert.ToInt32(Session["uuid"]) + " ";
            SqlDataAdapter daa26 = new SqlDataAdapter(qq26, con);
            DataTable dtt26 = new DataTable();
            daa26.Fill(dtt26);
            if (dtt26.Rows.Count > 0)
            {
                NestId = Convert.ToInt32(dtt26.Rows[0]["tId"]);
            }
            con.Close();
            //}

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
            TestatorFormModel TFM = new TestatorFormModel();

            con.Open();
            string query = "select * from TestatorDetails where tId = '" + NestId + "' ";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();


            if (dt.Rows.Count > 0)
            {
                TFM.tId = NestId;


                TFM.First_Name = dt.Rows[0]["First_Name"].ToString();
                if (Session["Type"] != null)
                {
                    if (Session["Type"].ToString() == "DistributorAdmin" || Session["Type"].ToString() == "SuperAdmin" || Session["Type"].ToString() == "Testator")
                    {
                        Session["distid"] = NestId;
                        Session["disttestator"] = TFM.First_Name;
                    }
                }
                else
                {
                    RedirectToAction("LoginPageIndex", "LoginPage");
                }

                TFM.Last_Name = dt.Rows[0]["Last_Name"].ToString();
                TFM.Dob = Convert.ToDateTime(dt.Rows[0]["DOB"]).ToString("dd-MM-yyyy");
                TFM.Middle_Name = dt.Rows[0]["Middle_Name"].ToString();

                if (dt.Rows[0]["Occupation"].ToString() != "none")
                {
                    TFM.Occupation = dt.Rows[0]["Occupation"].ToString();
                }

                TFM.Mobile = dt.Rows[0]["Mobile"].ToString();
                TFM.Email = dt.Rows[0]["Email"].ToString();

                if (dt.Rows[0]["maritalStatus"].ToString() != "none")
                {
                    TFM.material_status_txt = dt.Rows[0]["maritalStatus"].ToString();
                }

                if (dt.Rows[0]["Religion"].ToString() != "none")
                {
                    TFM.Religiontext = dt.Rows[0]["Religion"].ToString();
                }

                if (dt.Rows[0]["RelationShip"].ToString() != "none")
                {
                    TFM.RelationshipTxt = dt.Rows[0]["RelationShip"].ToString();
                }


                if (dt.Rows[0]["Identity_Proof"].ToString() != "none")
                {
                    TFM.Identity_Proof = dt.Rows[0]["Identity_Proof"].ToString();
                }


                if (dt.Rows[0]["Identity_proof_Value"].ToString() != "none")
                {
                    TFM.Identity_proof_Value = dt.Rows[0]["Identity_proof_Value"].ToString();
                }

                if (dt.Rows[0]["Alt_Identity_Proof"].ToString() != "none")
                {
                    TFM.Alt_Identity_Proof = dt.Rows[0]["Alt_Identity_Proof"].ToString();
                }


                if (dt.Rows[0]["Alt_Identity_proof_Value"].ToString() != "none")
                {
                    TFM.Alt_Identity_proof_Value = dt.Rows[0]["Alt_Identity_proof_Value"].ToString();
                }

                if (dt.Rows[0]["Gender"].ToString() != "none")
                {
                    TFM.Gendertxt = dt.Rows[0]["Gender"].ToString();
                }

                if (dt.Rows[0]["Address1"].ToString() != "none")
                {
                    TFM.Address1 = dt.Rows[0]["Address1"].ToString();
                }

                if (dt.Rows[0]["Address2"].ToString() != "none")
                {
                    TFM.Address2 = dt.Rows[0]["Address2"].ToString();
                }

                if (dt.Rows[0]["Address3"].ToString() != "none")
                {
                    TFM.Address3 = dt.Rows[0]["Address3"].ToString();
                }

                if (dt.Rows[0]["City"].ToString() != "none")
                {
                    TFM.citytext = dt.Rows[0]["City"].ToString();
                }

                if (dt.Rows[0]["State"].ToString() != "none")
                {
                    TFM.statetext = dt.Rows[0]["State"].ToString();
                }

                if (dt.Rows[0]["Country"].ToString() != "none")
                {
                    TFM.countrytext = dt.Rows[0]["Country"].ToString();
                }

                if (dt.Rows[0]["Pin"].ToString() != "none")
                {
                    TFM.Pin = dt.Rows[0]["Pin"].ToString();
                }

                if (dt.Rows[0]["active"].ToString() != "none")
                {
                    TFM.active = dt.Rows[0]["active"].ToString();
                }


                TFM.uId = Convert.ToInt32(dt.Rows[0]["uId"]);


                TFM.tempemailotp = dt.Rows[0]["Email_OTP"].ToString();
                TFM.tempmobileotp = dt.Rows[0]["Mobile_OTP"].ToString();




            }






            return View("~/Views/UpdateTestators/UpdateTestatorPageContent.cshtml", TFM);
        }


        public ActionResult UpdatingTestatorFormData(TestatorFormModel TFM)
        {
            ViewBag.collapse = "true";

            // check type 
            string typ5 = "";
            con.Open();
            string qq15 = "select Type from users where uId = " + Convert.ToInt32(Session["uuid"]) + " ";
            SqlDataAdapter daa5 = new SqlDataAdapter(qq15, con);
            DataTable dtt5 = new DataTable();
            daa5.Fill(dtt5);
            con.Close();

            if (dtt5.Rows.Count > 0)
            {
                typ5 = dtt5.Rows[0]["Type"].ToString();
            }



            //end



            if (typ5 == "Testator")
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



            // update email and password 
            con.Open();

            string chkmail = "select Email from testatordetails where uId = " + TFM.uId + " ";
            SqlDataAdapter chkmailadp = new SqlDataAdapter(chkmail, con);
            DataTable chkmaildt = new DataTable();
            chkmailadp.Fill(chkmaildt);
            if (chkmaildt.Rows.Count > 0)
            {

                if (chkmaildt.Rows[0]["Email"].ToString() != TFM.Email)
                {


                    //generate MOBILE OTP
                    TFM.MobileOTP = String.Empty;
                    string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
                    int iOTPLength = 5;

                    string sTempChars = String.Empty;
                    Random rand = new Random();

                    for (int i = 0; i < iOTPLength; i++)

                    {

                        int p = rand.Next(0, saAllowedCharacters.Length);

                        sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];

                        TFM.MobileOTP += sTempChars;

                    }
                    //END




                    //generate EMAIL OTP
                    TFM.EmailOTP = String.Empty;
                    string[] saAllowedCharacters2 = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
                    int iOTPLength2 = 5;

                    string sTempChars2 = String.Empty;
                    Random rand2 = new Random();

                    for (int i = 0; i < iOTPLength2; i++)

                    {

                        int p = rand.Next(0, saAllowedCharacters2.Length);

                        sTempChars2 = saAllowedCharacters2[rand.Next(0, saAllowedCharacters2.Length)];

                        TFM.EmailOTP += sTempChars2;

                    }
                    //END


                    //generate Password OTP
                    TFM.userPassword = String.Empty;
                    string[] saAllowedCharacters3 = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
                    int iOTPLength3 = 5;

                    string sTempChars3 = String.Empty;
                    Random rand3 = new Random();

                    for (int i = 0; i < iOTPLength3; i++)

                    {

                        int p = rand3.Next(0, saAllowedCharacters3.Length);

                        sTempChars3 = saAllowedCharacters3[rand3.Next(0, saAllowedCharacters3.Length)];

                        TFM.userPassword += sTempChars3;

                    }
                    //END

                    con.Close();




                    //con.Open();
                    //string query33 = "update users set  userID= '" + TFM.Email + "' , userPwd='" + TFM.userPassword + "'   where uId = " + TFM.uId + "";
                    //SqlCommand cmd33 = new SqlCommand(query33, con);
                    //cmd33.ExecuteNonQuery();
                    //con.Close();


                    // update otp for email and mobile





                    con.Open();
                    string qq = "update TestatorDetails set Contact_Verification = 0 ,Email_Verification = 0 , Mobile_Verification_Status = 0 , Email_OTP = '" + TFM.EmailOTP + "' , Mobile_OTP = '" + TFM.MobileOTP + "' where  uId = " + TFM.uId + " ";
                    SqlCommand cmddd = new SqlCommand(qq, con);
                    cmddd.ExecuteNonQuery();
                    con.Close();





                    //end


                    if (TFM.Email != "")
                    {
                        //generate Mail
                        string mailto2 = TFM.Email;
                        string userlogin = TFM.Email;


                        string subject2 = "Will Assure Login Credentials";

                        string text2 = "<font color='Green' style='font-size=3em;'>Your UserId And Password For Logging In Is <br> UserID : " + TFM.Email + " <br> Password : " + TFM.userPassword + "</font>";
                        string body2 = "<font color='red'>" + text2 + "</font>";


                        MailMessage msg3 = new MailMessage();
                        msg3.From = new MailAddress("info@drinco.in");
                        msg3.To.Add(mailto2);
                        msg3.Subject = subject2;
                        msg3.Body = body2;

                        msg3.IsBodyHtml = true;
                        SmtpClient smtp2 = new SmtpClient("216.10.240.149", 25);
                        smtp2.Credentials = new NetworkCredential("info@drinco.in", "95Bzf%s7");
                        smtp2.EnableSsl = false;
                        smtp2.Send(msg3);
                        smtp2.Dispose();


                        //end

                    }




                    // email otp
                    string mailto = TFM.Email;
                    string Userid = TFM.Identity_proof_Value;

                    Session["userid"] = Userid;
                    string subject = "Testing Mail Sending";
                    string OTP = "<font color='Green' style='font-size=3em;'>" + TFM.EmailOTP + "</font>";
                    string text = "Your OTP for Verification Is " + OTP + "";
                    string body = "<font color='red'>" + text + "</font>";


                    MailMessage msg = new MailMessage();
                    msg.From = new MailAddress("info@drinco.in");
                    msg.To.Add(mailto);
                    msg.Subject = subject;
                    msg.Body = body;

                    msg.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient("216.10.240.149", 25);
                    smtp.Credentials = new NetworkCredential("info@drinco.in", "95Bzf%s7");
                    smtp.EnableSsl = false;
                    smtp.Send(msg);
                    smtp.Dispose();



                    //end




                    con.Close();
                }

            }
            con.Close();








            //end







            con.Open();
            //DateTime dat;
            //if (TFM.Dobb != null || TFM.Dobb == "")
            //{
            //    dat = DateTime.ParseExact(TFM.Dobb, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            //}
            //else
            //{
            //    dat = DateTime.ParseExact(TFM.Dob, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            //}
            string dateString = TFM.Dob;
            DateTime dd = Convert.ToDateTime(dateString,
                System.Globalization.CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat);
     
            
            SqlCommand cmd = new SqlCommand("SP_CRUDTestatorDetails", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@condition", "update");
            cmd.Parameters.AddWithValue("@tId", TFM.tId);
            cmd.Parameters.AddWithValue("@First_Name", TFM.First_Name);
            cmd.Parameters.AddWithValue("@Last_Name", TFM.Last_Name);

            if (TFM.Middle_Name != null)
            {
                cmd.Parameters.AddWithValue("@Middle_Name", TFM.Middle_Name);
            }
            else
            {
                TFM.Middle_Name = "None";
               cmd.Parameters.AddWithValue("@Middle_Name", TFM.Middle_Name);
            }
            

            cmd.Parameters.AddWithValue("@DOB", dd);
            cmd.Parameters.AddWithValue("@Occupation", TFM.Occupation);
            cmd.Parameters.AddWithValue("@Mobile", TFM.Mobile);
            cmd.Parameters.AddWithValue("@Email", TFM.Email);
            cmd.Parameters.AddWithValue("@maritalStatus", TFM.material_status_txt);
            cmd.Parameters.AddWithValue("@Religion", TFM.Religiontext);
            cmd.Parameters.AddWithValue("@Relationship", "none");
            cmd.Parameters.AddWithValue("@Identity_Proof", TFM.Identity_Proof);
            cmd.Parameters.AddWithValue("@Identity_proof_Value", TFM.Identity_proof_Value);
            cmd.Parameters.AddWithValue("@Alt_Identity_Proof", TFM.Alt_Identity_Proof);
            cmd.Parameters.AddWithValue("@Alt_Identity_proof_Value", TFM.Alt_Identity_proof_Value);


            if (TFM.Gendertext != null)
            {
                cmd.Parameters.AddWithValue("@Gender", TFM.Gendertext);

            }
            else
            {
                cmd.Parameters.AddWithValue("@Gender", TFM.Gendertxt);
            }

                
            


            



            cmd.Parameters.AddWithValue("@Address1", TFM.Address1);
            if (TFM.Address2 != null)
            {
                cmd.Parameters.AddWithValue("@Address2", TFM.Address2);
            }
            else
            {
                TFM.Address2 = "None";
                cmd.Parameters.AddWithValue("@Address2", TFM.Address2);
            }

            if (TFM.Address3 != null)
            {
                cmd.Parameters.AddWithValue("@Address3", TFM.Address3);
            }
            else
            {
                TFM.Address3 = "None";
                cmd.Parameters.AddWithValue("@Address3", TFM.Address3);
            }
            cmd.Parameters.AddWithValue("@City", TFM.citytext);
            cmd.Parameters.AddWithValue("@State", TFM.statetext);
            cmd.Parameters.AddWithValue("@Country", TFM.countrytext);
            cmd.Parameters.AddWithValue("@Pin", TFM.Pin);
            if (TFM.active != null)
            {
                cmd.Parameters.AddWithValue("@active", TFM.active);
            }
            else
            {
                cmd.Parameters.AddWithValue("@active", "Active");
            }

            cmd.Parameters.AddWithValue("@Contact_Verification", "");
            cmd.Parameters.AddWithValue("@Email_Verification", "");
            cmd.Parameters.AddWithValue("@Mobile_Verification_Status", "");
            if (TFM.EmailOTP != null)
            {
                cmd.Parameters.AddWithValue("@Email_OTP", TFM.EmailOTP);
            }
            else
            {

                cmd.Parameters.AddWithValue("@Email_OTP", TFM.tempemailotp);

            }
            if (TFM.MobileOTP != null)
            {
                cmd.Parameters.AddWithValue("@Mobile_OTP", TFM.MobileOTP);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Mobile_OTP", TFM.tempmobileotp);
            }


            cmd.Parameters.AddWithValue("@uid", "");

            cmd.ExecuteNonQuery();


            string spname = "";

            if (TFM.SpouseName != null)
            {
                spname = TFM.SpouseName; 
            }
            else
            {
                spname = TFM.Spousetxt;
            }


            string queryu = "update testatordetails set SpouseName='"+ spname + "' where tId = "+TFM.tId+" ";
            SqlCommand cddu = new SqlCommand(queryu, con);
            cddu.ExecuteNonQuery();







           // eMail = '" + TFM.Email + "' ,

            if (TFM.EmailOTP != null)
            {
                string query2 = "update users set First_Name= '" + TFM.First_Name + "' , Last_Name='" + TFM.Last_Name + "' ,  Middle_Name='" + TFM.Middle_Name + "' , DOB = '" + dd + "' , Mobile = '" + TFM.Mobile + "' , Address1='" + TFM.Address1 + "' , Address2='" + TFM.Address2 + "' , Address3 = '" + TFM.Address3 + "' , City='" + TFM.citytext + "' ,State= '" + TFM.statetext + "' , Pin='" + TFM.Pin + "' , Designation = '2'   where uId = " + TFM.uId + "     ";
                SqlCommand cdd = new SqlCommand(query2, con);
                cdd.ExecuteNonQuery();
                con.Close();
            }
            else
            {
           
                string query2 = "update users set First_Name= '" + TFM.First_Name + "' , Last_Name='" + TFM.Last_Name + "' ,  Middle_Name='" + TFM.Middle_Name + "' , DOB = '" + dd + "' , Mobile = '" + TFM.Mobile + "' ,   Address1='" + TFM.Address1 + "' , Address2='" + TFM.Address2 + "' , Address3 = '" + TFM.Address3 + "' , City='" + TFM.citytext + "' ,State= '" + TFM.statetext + "' , Pin='" + TFM.Pin + "' , Designation = '1'   where uId = " + TFM.uId + "     ";
                SqlCommand cdd = new SqlCommand(query2, con);
                cdd.ExecuteNonQuery();
                con.Close();

            }














            TempData["Message"] = "true";


            return RedirectToAction("UpdateTestatorsIndex", "UpdateTestators", new { NestId = TFM.uId });
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



       


        public string UpdateStateBind()
        {

            string response = Request["sender"].ToString();

            // country id
            string query2 = "select distinct  CountryID , CountryName from country_tbl where CountryName = '" + response + "'  ";
            SqlDataAdapter da2 = new SqlDataAdapter(query2, con);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            int countryid = 0;
            if (dt2.Rows.Count > 0)
            {
                countryid = Convert.ToInt32(dt2.Rows[0]["CountryID"]);
            }


            //end





            con.Open();
            string query = "select distinct * from tbl_state where country_id = " + countryid + "  ";
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







        public String BindCityDDL()
        {

            con.Open();
            string query = "select distinct * from tbl_city  order by city_name asc ";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            string data = "<option value=''>--Select--</option>";

            if (dt.Rows.Count > 0)
            {


                for (int i = 0; i < dt.Rows.Count; i++)
                {




                    data = data + "<option value=" + dt.Rows[i]["id"].ToString() + " >" + dt.Rows[i]["city_name"].ToString() + "</option>";



                }




            }

            return data;

        }


        public string OnChangeBindState()
        {
            string response = Request["send"];
            con.Open();
            string query = "select distinct * from tbl_state where country_id = '" + response + "' order by statename asc";
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
            string query = "select distinct * from tbl_city where state_id = '" + response + "' order by city_name asc";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            string data = "<option value=''>--Select--</option>";

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





        public ActionResult Checkforcompletedpage()
        {
            string qtest001 = "";
          


            con.Open();
            if (Session["Type"].ToString() == "SuperAdmin")
            {
                qtest001 = "select uId , WillType from users where Linked_user = " + Convert.ToInt32(Session["uuid"]) + " and Type = 'Testator'";
            }
            if (Session["Type"].ToString() == "Distributor")
            {
                qtest001 = "select uId , WillType from users where Linked_user = " + Convert.ToInt32(Session["uuid"]) + " and Type = 'DistributorAdmin'";
            }
            if (Session["Type"].ToString() == "Testator")
            {
                qtest001 = "select top 1 tId , WillType from TestatorDetails where uId = " + Convert.ToInt32(Session["uuid"]) + " order by tid desc ";
            }
            
            SqlDataAdapter test001da = new SqlDataAdapter(qtest001, con);
            DataTable test001dt = new DataTable();
            test001da.Fill(test001dt);
            con.Close();
            int NestId = 0;
            if (test001dt.Rows.Count > 0)
            {

                NestId = Convert.ToInt32(test001dt.Rows[0]["tId"]);


            }




            if (Session["Type"].ToString() != "SuperAdmin")
            {
                con.Open();
                string qtest0012 = "select WillType from users where uId = " + Convert.ToInt32(Session["uuid"]) + "";
                SqlDataAdapter test001da2 = new SqlDataAdapter(qtest0012, con);
                DataTable test001dt2 = new DataTable();
                test001da2.Fill(test001dt2);

                if (test001dt2.Rows.Count > 0)
                {
                    Session["WillType"] = test001dt2.Rows[0]["WillType"].ToString();
                }
                con.Close();
            }

          



       


            if (Session["doctype"] != null && Session["WillType"] != null && Session["Type"] != null)
            {

              




                if (Session["doctype"].ToString() == "Will" && Session["WillType"].ToString() == "Detailed")
                {
                    //////////// check document completion /////////////



                    // for testator family
                    con.Open();
                    string qchk001 = "select a.fId , a.First_Name , a.Last_Name , a.Middle_Name , a.DOB , a.Marital_Status , a.Religion , a.Relationship , a.Address1 , a.Address2 , a.Address3 , a.City , a.State , a.Pin , a.tId , a.active , a.Identity_Proof , a.Identity_Proof_Value , a.Alt_Identity_Proof , a.Alt_Identity_Proof_Value , a.Is_Informed_Person from testatorFamily a inner join TestatorDetails b on a.tId=b.tId where b.tId =   " + NestId + " and a.WillType='Detailed' ";
                    SqlDataAdapter chk001da = new SqlDataAdapter(qchk001, con);
                    DataTable chk001dt = new DataTable();
                    chk001da.Fill(chk001dt);
                    con.Close();

                    if (chk001dt.Rows.Count > 0)
                    {

                    }
                    else
                    {
                        return RedirectToAction("AddTestatorFamilyIndex", "AddTestatorFamily");
                    }
                    //end




                    // for beneficiary institution
                    con.Open();
                    string qchk0020 = "select * from [BeneficiaryInstitutions] where tid = " + NestId + "  and WillType='Detailed'";
                    SqlDataAdapter chk002da0 = new SqlDataAdapter(qchk0020, con);
                    DataTable chk002dt0 = new DataTable();
                    chk002da0.Fill(chk002dt0);
                    con.Close();
                    if (chk002dt0.Rows.Count > 0)
                    {

                    }
                    else
                    {
                        return RedirectToAction("AddBeneficiaryInstituteIndex", "AddBeneficiaryInstitute");
                    }
                    //end





                    // for beneficiary
                    con.Open();
                    string qchk002 = "select a.bpId , a.First_Name , a.Last_Name , a.Middle_Name , a.DOB , a.Mobile , a.Relationship , a.Marital_Status , a.Religion , a.Identity_proof , a.Identity_proof_value , a.Alt_Identity_proof , a.Alt_Identity_proof_value , a.Address1 , a.Address2 , a.Address3 , a.City , a.State , a.Pin , a.aiid , a.tId , a.dateCreated , a.createdBy , a.documentId , a.beneficiary_type from BeneficiaryDetails a inner join TestatorDetails b on a.tId=b.tId and a.fetchid not in ('TF') where b.tId = " + NestId + " and a.beneficiary_type = 'Beneficiary'  and a.WillType='Detailed' ";
                    SqlDataAdapter chk002da = new SqlDataAdapter(qchk002, con);
                    DataTable chk002dt = new DataTable();
                    chk002da.Fill(chk002dt);
                    con.Close();
                    if (chk002dt.Rows.Count > 0)
                    {

                    }
                    else
                    {
                        return RedirectToAction("AddBeneficiaryIndex", "AddBeneficiary");
                    }
                    //end











                    // for assetinformation
                    con.Open();
                    string qchk003 = "select a.aiid , a.atId , a.amId , a.tid , a.docid , a.Json from AssetInformation a inner join TestatorDetails b on a.tid=b.tId where b.tId = " + NestId + "  and a.WillType='Detailed'   ";
                    SqlDataAdapter chk003da = new SqlDataAdapter(qchk003, con);
                    DataTable chk003dt = new DataTable();
                    chk003da.Fill(chk003dt);
                    con.Close();
                    if (chk003dt.Rows.Count > 0)
                    {

                    }
                    else
                    {
                        return RedirectToAction("AddMainAssetsIndex", "AddMainAssets");
                    }

                    //end









                    // for asset mapping 

                    con.Open();
                    string qchk006 = "select a.Beneficiary_Asset_ID , a.AssetType_ID , a.AssetCategory_ID ,  a.Beneficiary_ID , a.Proportion , a.tid from BeneficiaryAssets a inner join TestatorDetails b on a.tid=b.tId where b.tId = " + NestId + "  and a.WillType='Detailed'";
                    SqlDataAdapter chk006da = new SqlDataAdapter(qchk006, con);
                    DataTable chk006dt = new DataTable();
                    chk006da.Fill(chk006dt);
                    con.Close();

                    if (chk006dt.Rows.Count > 0)
                    {

                    }
                    else
                    {
                        return RedirectToAction("AddAssetMappingIndex", "AddAssetMapping");
                    }

                    //end


                    // for appointees 

                    con.Open();
                    string qchk008 = "select * from Appointees where tid = " + NestId + " and Type='Executor' and WillType='Detailed' ";
                    SqlDataAdapter chk008da = new SqlDataAdapter(qchk008, con);
                    DataTable chk008dt = new DataTable();
                    chk008da.Fill(chk008dt);
                    con.Close();

                    if (chk008dt.Rows.Count > 0)
                    {

                    }
                    else
                    {
                        return RedirectToAction("AddAppointeesIndex", "AddAppointees");
                    }

                    //end






                    // for Addwitness 

                    con.Open();
                    string qchk0082 = "select * from Appointees where tid = " + NestId + " and Type='Witness' and WillType='Detailed' ";
                    SqlDataAdapter chk008da2 = new SqlDataAdapter(qchk0082, con);
                    DataTable chk008dt2 = new DataTable();
                    chk008da2.Fill(chk008dt2);
                    con.Close();

                    if (chk008dt2.Rows.Count > 0)
                    {

                    }
                    else
                    {
                        return RedirectToAction("AddwitnessIndex", "Addwitness");
                    }

                    //end



                }


                if (Session["doctype"].ToString() == "Will" && Session["WillType"].ToString() == "Quick")
                {
                    //////////// check document completion /////////////



                    // for testator family
                    con.Open();
                    string qchk001 = "select a.fId , a.First_Name , a.Last_Name , a.Middle_Name , a.DOB , a.Marital_Status , a.Religion , a.Relationship , a.Address1 , a.Address2 , a.Address3 , a.City , a.State , a.Pin , a.tId , a.active , a.Identity_Proof , a.Identity_Proof_Value , a.Alt_Identity_Proof , a.Alt_Identity_Proof_Value , a.Is_Informed_Person from testatorFamily a inner join TestatorDetails b on a.tId=b.tId where b.tId =   " + NestId + " and a.WillType='Detailed' ";
                    SqlDataAdapter chk001da = new SqlDataAdapter(qchk001, con);
                    DataTable chk001dt = new DataTable();
                    chk001da.Fill(chk001dt);
                    con.Close();

                    if (chk001dt.Rows.Count > 0)
                    {

                    }
                    else
                    {
                        return RedirectToAction("AddTestatorFamilyIndex", "AddTestatorFamily");
                    }
                    //end




                    // for beneficiary institution
                    con.Open();
                    string qchk0020 = "select * from [BeneficiaryInstitutions] where tid = " + NestId + "  and WillType='Detailed'";
                    SqlDataAdapter chk002da0 = new SqlDataAdapter(qchk0020, con);
                    DataTable chk002dt0 = new DataTable();
                    chk002da0.Fill(chk002dt0);
                    con.Close();
                    if (chk002dt0.Rows.Count > 0)
                    {

                    }
                    else
                    {
                        return RedirectToAction("AddBeneficiaryInstituteIndex", "AddBeneficiaryInstitute");
                    }
                    //end





                    // for beneficiary
                    con.Open();
                    string qchk002 = "select a.bpId , a.First_Name , a.Last_Name , a.Middle_Name , a.DOB , a.Mobile , a.Relationship , a.Marital_Status , a.Religion , a.Identity_proof , a.Identity_proof_value , a.Alt_Identity_proof , a.Alt_Identity_proof_value , a.Address1 , a.Address2 , a.Address3 , a.City , a.State , a.Pin , a.aiid , a.tId , a.dateCreated , a.createdBy , a.documentId , a.beneficiary_type from BeneficiaryDetails a inner join TestatorDetails b on a.tId=b.tId and a.fetchid not in ('TF') where b.tId = " + NestId + " and a.beneficiary_type = 'Beneficiary'  and a.WillType='Detailed' ";
                    SqlDataAdapter chk002da = new SqlDataAdapter(qchk002, con);
                    DataTable chk002dt = new DataTable();
                    chk002da.Fill(chk002dt);
                    con.Close();
                    if (chk002dt.Rows.Count > 0)
                    {

                    }
                    else
                    {
                        return RedirectToAction("AddBeneficiaryIndex", "AddBeneficiary");
                    }
                    //end



                    // for asset mapping 

                    con.Open();
                    string qchk006 = "select a.Beneficiary_Asset_ID , a.AssetType_ID , a.AssetCategory_ID ,  a.Beneficiary_ID , a.Proportion , a.tid from BeneficiaryAssets a inner join TestatorDetails b on a.tid=b.tId where b.tId = " + NestId + "  and a.WillType='Detailed'";
                    SqlDataAdapter chk006da = new SqlDataAdapter(qchk006, con);
                    DataTable chk006dt = new DataTable();
                    chk006da.Fill(chk006dt);
                    con.Close();

                    if (chk006dt.Rows.Count > 0)
                    {

                    }
                    else
                    {
                        return RedirectToAction("AddAssetMappingIndex", "AddAssetMapping");
                    }

                    //end


                    // for appointees 

                    con.Open();
                    string qchk008 = "select * from Appointees where tid = " + NestId + " and Type='Executor' and WillType='Detailed' ";
                    SqlDataAdapter chk008da = new SqlDataAdapter(qchk008, con);
                    DataTable chk008dt = new DataTable();
                    chk008da.Fill(chk008dt);
                    con.Close();

                    if (chk008dt.Rows.Count > 0)
                    {

                    }
                    else
                    {
                        return RedirectToAction("AddAppointeesIndex", "AddAppointees");
                    }

                    //end






                    // for Addwitness 

                    con.Open();
                    string qchk0082 = "select * from Appointees where tid = " + NestId + " and Type='Witness' and WillType='Detailed' ";
                    SqlDataAdapter chk008da2 = new SqlDataAdapter(qchk0082, con);
                    DataTable chk008dt2 = new DataTable();
                    chk008da2.Fill(chk008dt2);
                    con.Close();

                    if (chk008dt2.Rows.Count > 0)
                    {

                    }
                    else
                    {
                        return RedirectToAction("AddwitnessIndex", "Addwitness");
                    }

                    //end



                }



                if (Session["doctype"].ToString() == "POA")
                {





                    // for beneficiary
                    con.Open();
                    string qchk002 = "select a.bpId , a.First_Name , a.Last_Name , a.Middle_Name , a.DOB , a.Mobile , a.Relationship , a.Marital_Status , a.Religion , a.Identity_proof , a.Identity_proof_value , a.Alt_Identity_proof , a.Alt_Identity_proof_value , a.Address1 , a.Address2 , a.Address3 , a.City , a.State , a.Pin , a.aiid , a.tId , a.dateCreated , a.createdBy , a.documentId , a.beneficiary_type from BeneficiaryDetails a inner join TestatorDetails b on a.tId=b.tId and a.fetchid not in ('TF') where b.tId = " + NestId + "  and a.doctype = 'POA'";
                    SqlDataAdapter chk002da = new SqlDataAdapter(qchk002, con);
                    DataTable chk002dt = new DataTable();
                    chk002da.Fill(chk002dt);
                    con.Close();
                    if (chk002dt.Rows.Count > 0)
                    {

                    }
                    else
                    {
                        return RedirectToAction("AddBeneficiaryIndex", "AddBeneficiary");
                    }
                    //end












                    // for assetinformation
                    con.Open();
                    string qchk003 = "select a.aiid , a.atId , a.amId , a.tid , a.docid , a.Json from AssetInformation a inner join TestatorDetails b on a.tid=b.tId where b.tId = " + NestId + " and a.doctype = 'POA'  ";
                    SqlDataAdapter chk003da = new SqlDataAdapter(qchk003, con);
                    DataTable chk003dt = new DataTable();
                    chk003da.Fill(chk003dt);
                    con.Close();
                    if (chk003dt.Rows.Count > 0)
                    {

                    }
                    else
                    {
                        return RedirectToAction("AddMainAssetsIndex", "AddMainAssets");
                    }

                    //end









                    // for asset mapping 

                    con.Open();
                    string qchk006 = "select a.Beneficiary_Asset_ID , a.AssetType_ID , a.AssetCategory_ID , a.SchemeName , a.InstrumentName , a.Beneficiary_ID , a.Proportion , a.tid from BeneficiaryAssets a inner join TestatorDetails b on a.tid=b.tId where b.tId = " + NestId + "  and a.doctype = 'POA'";
                    SqlDataAdapter chk006da = new SqlDataAdapter(qchk006, con);
                    DataTable chk006dt = new DataTable();
                    chk006da.Fill(chk006dt);
                    con.Close();

                    if (chk006dt.Rows.Count > 0)
                    {

                    }
                    else
                    {
                        return RedirectToAction("AddAssetMappingIndex", "AddAssetMapping");
                    }

                    //end









                    // for Addwitness 

                    con.Open();
                    string qchk0082 = "select * from Appointees where tid = " + NestId + " and Type='Witness' and doctype='POA' ";
                    SqlDataAdapter chk008da2 = new SqlDataAdapter(qchk0082, con);
                    DataTable chk008dt2 = new DataTable();
                    chk008da2.Fill(chk008dt2);
                    con.Close();

                    if (chk008dt2.Rows.Count > 0)
                    {

                    }
                    else
                    {
                        return RedirectToAction("AddwitnessIndex", "Addwitness");
                    }

                    //end






                }



                if (Session["doctype"].ToString() == "GiftDeeds")
                {



                    // for beneficiary
                    con.Open();
                    string qchk002 = "select a.bpId , a.First_Name , a.Last_Name , a.Middle_Name , a.DOB , a.Mobile , a.Relationship , a.Marital_Status , a.Religion , a.Identity_proof , a.Identity_proof_value , a.Alt_Identity_proof , a.Alt_Identity_proof_value , a.Address1 , a.Address2 , a.Address3 , a.City , a.State , a.Pin , a.aiid , a.tId , a.dateCreated , a.createdBy , a.documentId , a.beneficiary_type from BeneficiaryDetails a inner join TestatorDetails b on a.tId=b.tId and a.fetchid not in ('TF') where b.tId = " + NestId + "  and a.doctype = 'Giftdeeds'";
                    SqlDataAdapter chk002da = new SqlDataAdapter(qchk002, con);
                    DataTable chk002dt = new DataTable();
                    chk002da.Fill(chk002dt);
                    con.Close();
                    if (chk002dt.Rows.Count > 0)
                    {

                    }
                    else
                    {
                        return RedirectToAction("AddBeneficiaryIndex", "AddBeneficiary");
                    }
                    //end












                    // for assetinformation
                    con.Open();
                    string qchk003 = "select a.aiid , a.atId , a.amId , a.tid , a.docid , a.Json from AssetInformation a inner join TestatorDetails b on a.tid=b.tId where b.tId = " + NestId + " and a.doctype = 'Giftdeeds'  ";
                    SqlDataAdapter chk003da = new SqlDataAdapter(qchk003, con);
                    DataTable chk003dt = new DataTable();
                    chk003da.Fill(chk003dt);
                    con.Close();
                    if (chk003dt.Rows.Count > 0)
                    {

                    }
                    else
                    {
                        return RedirectToAction("AddMainAssetsIndex", "AddMainAssets");
                    }

                    //end









                    // for asset mapping 

                    con.Open();
                    string qchk006 = "select a.Beneficiary_Asset_ID , a.AssetType_ID , a.AssetCategory_ID , a.SchemeName , a.InstrumentName , a.Beneficiary_ID , a.Proportion , a.tid from BeneficiaryAssets a inner join TestatorDetails b on a.tid=b.tId where b.tId = " + NestId + "  and a.doctype = 'Giftdeeds'";
                    SqlDataAdapter chk006da = new SqlDataAdapter(qchk006, con);
                    DataTable chk006dt = new DataTable();
                    chk006da.Fill(chk006dt);
                    con.Close();

                    if (chk006dt.Rows.Count > 0)
                    {

                    }
                    else
                    {
                        return RedirectToAction("AddAssetMappingIndex", "AddAssetMapping");
                    }

                    //end









                    // for Addwitness 

                    con.Open();
                    string qchk0082 = "select * from Appointees where tid = " + NestId + " and Type='Witness' and doctype='Giftdeeds' ";
                    SqlDataAdapter chk008da2 = new SqlDataAdapter(qchk0082, con);
                    DataTable chk008dt2 = new DataTable();
                    chk008da2.Fill(chk008dt2);
                    con.Close();

                    if (chk008dt2.Rows.Count > 0)
                    {

                    }
                    else
                    {
                        return RedirectToAction("AddwitnessIndex", "Addwitness");
                    }

                    //end

                }

            }
            else
            {

                return RedirectToAction("LoginPageIndex", "LoginPage");


            }












            return RedirectToAction("WillDetailsIndex", "WillDetails", new { NestId = NestId, doctype = Session["doctype"].ToString() });
        }


        public string Validateidentity()
        {
            string response = Request["send"].ToString();
            string msg = "";

            string testatorchk = "";
            string testatorfamilychk = "";
            string guardianchk = "";
            string benechk = "";
            string witchk = "";

            con.Open();


            string query = "select tId from TestatorDetails where tId = " + Convert.ToInt32(Session["distid"]) + "";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            int tid = 0;
            if (dt.Rows.Count > 0)
            {
                tid = Convert.ToInt32(dt.Rows[0]["tId"]);
            }




            string query2 = "select Identity_proof_Value , Alt_Identity_proof_value from TestatorDetails  where tId = " + Convert.ToInt32(Session["distid"]) + "  ";
            SqlDataAdapter da2 = new SqlDataAdapter(query2, con);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);

            if (dt2.Rows.Count > 0)
            {

                if (dt2.Rows[0]["Alt_Identity_proof_value"].ToString() == response || dt2.Rows[0]["Identity_proof_Value"].ToString() == response)
                {
                    testatorchk = "false";
                }


            }







            //////////////////////////////// check in testator family  ///////////////////////////////////////







            string query3 = "select a.Alt_Identity_proof_Value , a.Identity_proof_Value from testatorFamily a inner join TestatorDetails b on a.tId=b.tId where  b.tId = '" + Convert.ToInt32(Session["distid"]) + "'";
            SqlDataAdapter da3 = new SqlDataAdapter(query3, con);
            DataTable dt3 = new DataTable();
            da3.Fill(dt3);

            if (dt3.Rows.Count > 0)
            {
                for (int i = 0; i < dt3.Rows.Count; i++)
                {
                    if (dt3.Rows[i]["Identity_proof_Value"].ToString() == response || dt3.Rows[i]["Alt_Identity_proof_Value"].ToString() == response)
                    {
                        testatorfamilychk = "false";
                    }







                }



            }


            ////////////////////////////////////////end////////////////////////////////////////////


            //////////////////////////////// check in Appointees guardian  ///////////////////////////////////////





            string query4 = "select a.Alt_Identity_proof_Value , a.Identity_proof_Value from Appointees a inner join TestatorDetails b on a.tId=b.tId where a.Type='Guardian' and b.tid = '" + Convert.ToInt32(Session["distid"]) + "' ";
            SqlDataAdapter da4 = new SqlDataAdapter(query4, con);
            DataTable dt4 = new DataTable();
            da4.Fill(dt4);

            if (dt4.Rows.Count > 0)
            {
                for (int i = 0; i < dt4.Rows.Count; i++)
                {
                    if (dt4.Rows[i]["Identity_proof_Value"].ToString() == response || dt4.Rows[i]["Alt_Identity_proof_Value"].ToString() == response)
                    {
                        guardianchk = "false";
                    }







                }



            }


            ////////////////////////////////////////end////////////////////////////////////////////



            //////////////////////////////// check in beneficiary  ///////////////////////////////////////





            string query5 = "select a.Alt_Identity_proof_Value , a.Identity_proof_Value from BeneficiaryDetails a inner join TestatorDetails b on a.tId=b.tId  where  b.tId = '" + Convert.ToInt32(Session["distid"]) + "' ";
            SqlDataAdapter da5 = new SqlDataAdapter(query5, con);
            DataTable dt5 = new DataTable();
            da5.Fill(dt5);

            if (dt5.Rows.Count > 0)
            {
                for (int i = 0; i < dt5.Rows.Count; i++)
                {
                    if (dt5.Rows[i]["Identity_proof_Value"].ToString() == response || dt5.Rows[i]["Alt_Identity_proof_Value"].ToString() == response)
                    {
                        benechk = "false";
                    }






                }



            }


            ////////////////////////////////////////end////////////////////////////////////////////



            //////////////////////////////// check in Witness  ///////////////////////////////////////





            string query6 = "select a.Alt_Identity_proof_Value , a.Identity_proof_Value from Appointees a inner join TestatorDetails b on a.tId=b.tId where  b.tId = " + Convert.ToInt32(Session["distid"]) + "   and a.Type = 'Witness'";
            SqlDataAdapter da6 = new SqlDataAdapter(query6, con);
            DataTable dt6 = new DataTable();
            da6.Fill(dt6);

            if (dt6.Rows.Count > 0)
            {
                for (int i = 0; i < dt6.Rows.Count; i++)
                {
                    if (dt6.Rows[i]["Identity_proof_Value"].ToString() == response || dt6.Rows[i]["Alt_Identity_proof_Value"].ToString() == response)
                    {
                        witchk = "false";
                    }







                }



            }


            ////////////////////////////////////////end////////////////////////////////////////////





            con.Close();

            msg = testatorchk + "~" + testatorfamilychk + "~" + guardianchk + "~" + benechk + "~" + witchk;


            return msg;
        }





        public string altValidateidentity()
        {
            string response = Request["send"].ToString();
            string msg = "";

            string testatorchk = "";
            string testatorfamilychk = "";
            string guardianchk = "";
            string benechk = "";
            string witchk = "";

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




            string query2 = "select Identity_proof_Value , Alt_Identity_proof_value from TestatorDetails  where tId = " + Convert.ToInt32(Session["distid"]) + "  ";
            SqlDataAdapter da2 = new SqlDataAdapter(query2, con);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);

            if (dt2.Rows.Count > 0)
            {

                if (dt2.Rows[0]["Alt_Identity_proof_value"].ToString() == response || dt2.Rows[0]["Identity_proof_Value"].ToString() == response)
                {
                    testatorchk = "false";
                }


            }







            //////////////////////////////// check in testator family  ///////////////////////////////////////







            string query3 = "select a.Alt_Identity_proof_Value , a.Identity_proof_Value from testatorFamily a inner join TestatorDetails b on a.tId=b.tId where  b.tId = '" + Convert.ToInt32(Session["distid"]) + "'";
            SqlDataAdapter da3 = new SqlDataAdapter(query3, con);
            DataTable dt3 = new DataTable();
            da3.Fill(dt3);

            if (dt3.Rows.Count > 0)
            {
                for (int i = 0; i < dt3.Rows.Count; i++)
                {
                    if (dt3.Rows[i]["Identity_proof_Value"].ToString() == response || dt3.Rows[i]["Alt_Identity_proof_Value"].ToString() == response)
                    {
                        testatorfamilychk = "false";
                    }







                }



            }


            ////////////////////////////////////////end////////////////////////////////////////////


            //////////////////////////////// check in Appointees guardian  ///////////////////////////////////////





            string query4 = "select a.Alt_Identity_proof_Value , a.Identity_proof_Value from Appointees a inner join TestatorDetails b on a.tId=b.tId where a.Type='Guardian' and b.tid = '" + Convert.ToInt32(Session["distid"]) + "' ";
            SqlDataAdapter da4 = new SqlDataAdapter(query4, con);
            DataTable dt4 = new DataTable();
            da4.Fill(dt4);

            if (dt4.Rows.Count > 0)
            {
                for (int i = 0; i < dt4.Rows.Count; i++)
                {
                    if (dt4.Rows[i]["Identity_proof_Value"].ToString() == response || dt4.Rows[i]["Alt_Identity_proof_Value"].ToString() == response)
                    {
                        guardianchk = "false";
                    }







                }



            }


            ////////////////////////////////////////end////////////////////////////////////////////



            //////////////////////////////// check in beneficiary  ///////////////////////////////////////





            string query5 = "select a.Alt_Identity_proof_Value , a.Identity_proof_Value from BeneficiaryDetails a inner join TestatorDetails b on a.tId=b.tId  where  b.tId = '" + Convert.ToInt32(Session["distid"]) + "' ";
            SqlDataAdapter da5 = new SqlDataAdapter(query5, con);
            DataTable dt5 = new DataTable();
            da5.Fill(dt5);

            if (dt5.Rows.Count > 0)
            {
                for (int i = 0; i < dt5.Rows.Count; i++)
                {
                    if (dt5.Rows[i]["Identity_proof_Value"].ToString() == response || dt5.Rows[i]["Alt_Identity_proof_Value"].ToString() == response)
                    {
                        benechk = "false";
                    }






                }



            }


            ////////////////////////////////////////end////////////////////////////////////////////



            //////////////////////////////// check in Witness  ///////////////////////////////////////





            string query6 = "select a.Alt_Identity_proof_Value , a.Identity_proof_Value from Appointees a inner join TestatorDetails b on a.tId=b.tId where  b.tId = " + Convert.ToInt32(Session["distid"]) + "   and a.Type = 'Witness'";
            SqlDataAdapter da6 = new SqlDataAdapter(query6, con);
            DataTable dt6 = new DataTable();
            da6.Fill(dt6);

            if (dt6.Rows.Count > 0)
            {
                for (int i = 0; i < dt6.Rows.Count; i++)
                {
                    if (dt6.Rows[i]["Identity_proof_Value"].ToString() == response || dt6.Rows[i]["Alt_Identity_proof_Value"].ToString() == response)
                    {
                        witchk = "false";
                    }







                }



            }


            ////////////////////////////////////////end////////////////////////////////////////////





            con.Close();

            msg = testatorchk + "~" + testatorfamilychk + "~" + guardianchk + "~" + benechk + "~" + witchk;


            return msg;
        }



    }
}