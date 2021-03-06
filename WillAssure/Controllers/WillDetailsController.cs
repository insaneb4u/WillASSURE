﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WillAssure.Models;

namespace WillAssure.Controllers
{
    public class WillDetailsController : Controller
    {
        public static string connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        SqlConnection con = new SqlConnection(connectionString);
        // GET: WillDetails
        public ActionResult WillDetailsIndex(int NestId , string doctype , string willtype)
        {
            ViewBag.Collapse = "true";
            //if (Session["Type"].ToString() != "DistributorAdmin")
            //{
            //    if (Session["doctype"].ToString() == "Will")
            //    {
            //        ViewBag.view = "Will";
            //    }


            //    if (Session["doctype"].ToString() == "POA" || Session["doctype"].ToString() == "GiftDeeds")
            //    {
            //        ViewBag.view = "POA";
            //        ViewBag.view = "GiftDeeds";
            //    }
            //}




            string queryc1 = "";
            string queryc2 = "";
            string queryc3 = "";
            string queryc4 = "";
            string queryc5 = "";
            string queryc7 = "";
            string queryc22 = "";
            string queryc44 = "";
            //// check data for next page link if available active links

            if (Session["distid"] != null && Session["willtype"] != null && Session["doctype"] != null)
            {
                con.Open();



                //////// check TesttaorFamily 


                if (Session["WillType"].ToString() == "Quick" && Session["doctype"].ToString() == "Will")
                {
                    queryc22 = "select * from Appointees where WillType = 'Quick'  and tId = " + Convert.ToInt32(Session["distid"]) + "  and Type = 'Guardian'  ";
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
                    queryc1 = "select * from BeneficiaryInstitutions where tId = " + Convert.ToInt32(Session["distid"]) + "  and WillType = 'Quick'    ";
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
                    ViewBag.beneactive = "true";
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





                ////////END











                con.Close();



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



                //end





            }
            else
            {
                return RedirectToAction("LoginPageIndex", "LoginPage");
            }







            











            if (Session["doctype"] != null)
            {

                Session["doctype"] = doctype;

            }
            else
            {
                RedirectToAction("LoginPageIndex", "LoginPage");
            }
            


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

            WillDetailModel WDM = new WillDetailModel();
            List<WillDetailModel> list1 = new List<WillDetailModel>();
            List<WillDetailModel> list2 = new List<WillDetailModel>();
            List<WillDetailModel> list3 = new List<WillDetailModel>();
            List<WillDetailModel> list4 = new List<WillDetailModel>();
            List<WillDetailModel> list5 = new List<WillDetailModel>();
            List<WillDetailModel> list6 = new List<WillDetailModel>();
            List<WillDetailModel> list7 = new List<WillDetailModel>();
            List<WillDetailModel> list8 = new List<WillDetailModel>();
            List<WillDetailModel> list9 = new List<WillDetailModel>();
            List<WillDetailModel> list10 = new List<WillDetailModel>();


            con.Open();



            string query11 = "select * from BeneficiaryInstitutions where tid = " + NestId + " ";
            SqlDataAdapter da11 = new SqlDataAdapter(query11, con);
            DataTable dt11 = new DataTable();
            da11.Fill(dt11);
            con.Close();


            //WDM.VerifyId = NestId;
            if (dt11.Rows.Count > 0)
            {

                // testator details
                for (int i = 0; i < dt11.Rows.Count; i++)
                {
                    WillDetailModel WDM1 = new WillDetailModel();

                    ViewBag.tid = Convert.ToInt32(dt11.Rows[i]["tId"]);
                    WDM1.insFirstName = dt11.Rows[i]["Name"].ToString();
                    WDM1.insTypeText = dt11.Rows[i]["Type"].ToString();
                    WDM1.insRegistrationNo = dt11.Rows[i]["registrationNo"].ToString();
                    WDM1.insAddress = dt11.Rows[i]["Address"].ToString();
                    WDM1.insStateText = dt11.Rows[i]["State"].ToString();
                    WDM1.insCityText = dt11.Rows[i]["City"].ToString();
                    WDM1.inscountry_txt = dt11.Rows[i]["Country"].ToString();

                    //end

                    list10.Add(WDM1);

                }


                ViewBag.institution = list10;

            }














            string query = "select * from TestatorDetails where tId = "+NestId+" ";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();


                //WDM.VerifyId = NestId;
                if (dt.Rows.Count > 0)
                {

                // testator details
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    WillDetailModel WDM1 = new WillDetailModel();

                        ViewBag.tid = Convert.ToInt32(dt.Rows[i]["tId"]);
                        WDM1.TestatorName = dt.Rows[i]["First_Name"].ToString();
                        WDM1.TestatorLastName = dt.Rows[i]["Last_Name"].ToString();
                        WDM1.TestatorMiddleName = dt.Rows[i]["Middle_Name"].ToString();
                        WDM1.TestatorDOB = Convert.ToDateTime(dt.Rows[i]["DOB"]).ToString("dd-MM-yyyy");
                        WDM1.TestatorOccupation = dt.Rows[i]["Occupation"].ToString();
                        WDM1.TestatorMobile = dt.Rows[i]["Mobile"].ToString();
                        WDM1.TestatorEmail = dt.Rows[i]["Email"].ToString();
                        WDM1.TestatorMaritalStatus = dt.Rows[i]["maritalStatus"].ToString();
                        WDM1.TestatorRelationship = dt.Rows[i]["RelationShip"].ToString();
                        WDM1.TestatorReligion = dt.Rows[i]["Religion"].ToString();
                        WDM1.TestatorIdentityProof = dt.Rows[i]["Identity_Proof"].ToString();
                        WDM1.TestatorIdentityProofValue = dt.Rows[i]["Identity_proof_Value"].ToString();
                        WDM1.TestatorAltIdentityProof = dt.Rows[i]["Alt_Identity_Proof"].ToString();
                        WDM1.TestatorAltIdentityProofValue = dt.Rows[i]["Alt_Identity_proof_Value"].ToString();
                        WDM1.TestatorGender = dt.Rows[i]["Gender"].ToString();
                        WDM1.TestatorAddress1 = dt.Rows[i]["Address1"].ToString();
                        WDM1.TestatorAddress2 = dt.Rows[i]["Address2"].ToString();
                        WDM1.TestatorAddress3 = dt.Rows[i]["Address3"].ToString();
                        WDM1.TestatorCity = dt.Rows[i]["City"].ToString();
                        WDM1.TestatorState = dt.Rows[i]["State"].ToString();
                        WDM1.TestatorCountry = dt.Rows[i]["Country"].ToString();
                        WDM1.TestatorPin = dt.Rows[i]["Pin"].ToString();
                        //end

                        list1.Add(WDM1);

                }


                ViewBag.TestatorDetails = list1;

                }
           



                // beneficiary 

          

                con.Open();
                string query2 = "";

            if (Session["doctype"] != null)
            {

                if (Session["doctype"].ToString() == "Will")
                {
                    query2 = "select a.bpId , a.First_Name , a.Last_Name , a.Middle_Name , a.DOB , a.Mobile , a.Relationship , a.Marital_Status , a.Religion , a.Identity_proof , a.Identity_proof_value , a.Alt_Identity_proof , a.Alt_Identity_proof_value , a.Address1 , a.Address2 , a.Address3 , a.City , a.State , a.Pin , a.aiid , a.tId , a.dateCreated , a.createdBy , a.documentId , a.beneficiary_type from BeneficiaryDetails a inner join TestatorDetails b on a.tId=b.tId where b.tId = " + NestId + " and a.doctype = 'Will'  and a.beneficiary_type='Beneficiary' ";
                }

                if (Session["doctype"].ToString() == "POA")
                {
                    query2 = "select a.bpId , a.First_Name , a.Last_Name , a.Middle_Name , a.DOB , a.Mobile , a.Relationship , a.Marital_Status , a.Religion , a.Identity_proof , a.Identity_proof_value , a.Alt_Identity_proof , a.Alt_Identity_proof_value , a.Address1 , a.Address2 , a.Address3 , a.City , a.State , a.Pin , a.aiid , a.tId , a.dateCreated , a.createdBy , a.documentId , a.beneficiary_type from BeneficiaryDetails a inner join TestatorDetails b on a.tId=b.tId where b.tId = " + NestId + " and a.doctype = 'POA'  and a.beneficiary_type='Beneficiary'    ";
                }


                if (Session["doctype"].ToString() == "GiftDeeds")
                {
                    query2 = "select a.bpId , a.First_Name , a.Last_Name , a.Middle_Name , a.DOB , a.Mobile , a.Relationship , a.Marital_Status , a.Religion , a.Identity_proof , a.Identity_proof_value , a.Alt_Identity_proof , a.Alt_Identity_proof_value , a.Address1 , a.Address2 , a.Address3 , a.City , a.State , a.Pin , a.aiid , a.tId , a.dateCreated , a.createdBy , a.documentId , a.beneficiary_type from BeneficiaryDetails a inner join TestatorDetails b on a.tId=b.tId where b.tId = " + NestId + " and a.doctype = 'GiftDeeds'  and a.beneficiary_type='Beneficiary'   ";
                }





            }
            else
            {
                return RedirectToAction("LoginPageIndex", "LoginPage");
            }



            SqlDataAdapter da2 = new SqlDataAdapter(query2, con);
                DataTable dt2 = new DataTable();
                da2.Fill(dt2);
                con.Close();



                if (dt2.Rows.Count > 0)
                {

                   
                       
                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    WillDetailModel WDM2 = new WillDetailModel();
                    WDM2.BeneficiaryName = dt2.Rows[i]["First_Name"].ToString();

                    WDM2.BeneficiaryLastName = dt2.Rows[i]["Last_Name"].ToString();
                    WDM2.BeneficiaryMiddleName = dt2.Rows[i]["Middle_Name"].ToString();
                    WDM2.BeneficiaryDOB = Convert.ToDateTime(dt2.Rows[i]["DOB"]).ToString("dd-MM-yyyy");
                    WDM2.BeneficiaryMobile = dt2.Rows[i]["Mobile"].ToString();
                    WDM2.BeneficiaryRelationship = dt2.Rows[i]["Relationship"].ToString();
                    WDM2.BeneficiaryMartialStatus = dt2.Rows[i]["Marital_Status"].ToString();
                    WDM2.BeneficiaryReligion = dt2.Rows[i]["Religion"].ToString();
                    WDM2.BeneficiaryIdentityProof = dt2.Rows[i]["Identity_proof"].ToString();
                    WDM2.BeneficiaryIdentityProofValue = dt2.Rows[i]["Identity_proof_value"].ToString();
                    WDM2.BeneficiaryaltidentityProof = dt2.Rows[i]["Alt_Identity_proof"].ToString();
                    WDM2.beneficiaryaltidentityproofvalue = dt2.Rows[i]["Alt_Identity_proof_value"].ToString();
                    WDM2.beneficiaryaddress1 = dt2.Rows[i]["Address1"].ToString();
                    WDM2.beneficiaryaddress2 = dt2.Rows[i]["Address2"].ToString();
                    WDM2.beneficiaryaddress3 = dt2.Rows[i]["Address3"].ToString();
                    WDM2.beneficiarycity = dt2.Rows[i]["City"].ToString();
                    WDM2.beneficiarystate = dt2.Rows[i]["State"].ToString();
                    WDM2.beneficiarypin = dt2.Rows[i]["Pin"].ToString();

                    list2.Add(WDM2);
                }
                    

                    ViewBag.beneficiary = list2;



                }
              



                //end






                // appointees 




                con.Open();
                string query3 = "";


            if (Session["doctype"] != null)
            {

                if (Session["doctype"].ToString() == "Will" && Session["WillType"].ToString() == "Quick")
                {
                    query3 = "select a.apId , a.documentId , a.Type , a.subType , a.Name , a.middleName  , a.Surname , a.Identity_Proof , a.Identity_Proof_Value , a.Alt_Identity_Proof , a.Alt_Identity_Proof_Value , a.DOB , a.Gender , a.Occupation , a.Relationship , a.Address1 , a.Address2 , a.Address3 , a.City , a.State , a.Pin , a.dateCreated, a.tid  from Appointees a inner join  TestatorDetails b on a.tid=b.tid where b.tId = "+NestId+"   and a.Type='Executor' and a.WillType = 'Quick'";
                }

                if (Session["doctype"].ToString() == "Will" && Session["WillType"].ToString() == "Detailed")
                {
                    query3 = "select a.apId , a.documentId , a.Type , a.subType , a.Name , a.middleName  , a.Surname , a.Identity_Proof , a.Identity_Proof_Value , a.Alt_Identity_Proof , a.Alt_Identity_Proof_Value , a.DOB , a.Gender , a.Occupation , a.Relationship , a.Address1 , a.Address2 , a.Address3 , a.City , a.State , a.Pin , a.dateCreated, a.tid  from Appointees a inner join  TestatorDetails b on a.tid=b.tid where b.tId = "+NestId+"   and a.Type='Executor' and a.WillType = 'Detailed'";
                }

                if (Session["doctype"].ToString() == "POA")
                {
                    query3 = "select a.apId , a.documentId , a.Type , a.subType , a.Name , a.middleName  , a.Surname , a.Identity_Proof , a.Identity_Proof_Value , a.Alt_Identity_Proof , a.Alt_Identity_Proof_Value , a.DOB , a.Gender , a.Occupation , a.Relationship , a.Address1 , a.Address2 , a.Address3 , a.City , a.State , a.Pin , a.dateCreated, a.tid  from Appointees a inner join  TestatorDetails b on a.tid=b.tid where b.tId = " + NestId + " and a.doctype = 'POA' and a.ExecutorType='Executor' ";
                }


                if (Session["doctype"].ToString() == "GiftDeeds")
                {
                    query3 = "select a.apId , a.documentId , a.Type , a.subType , a.Name , a.middleName  , a.Surname , a.Identity_Proof , a.Identity_Proof_Value , a.Alt_Identity_Proof , a.Alt_Identity_Proof_Value , a.DOB , a.Gender , a.Occupation , a.Relationship , a.Address1 , a.Address2 , a.Address3 , a.City , a.State , a.Pin , a.dateCreated, a.tid  from Appointees a inner join  TestatorDetails b on a.tid=b.tid where b.tId = " + NestId + " and a.doctype = 'Giftdeeds' and a.ExecutorType='Executor' ";
                }





            }
            else
            {
                return RedirectToAction("LoginPageIndex", "LoginPage");
            }




            SqlDataAdapter da4 = new SqlDataAdapter(query3, con);
                DataTable dt4 = new DataTable();
                da4.Fill(dt4);
                con.Close();



                if (dt4.Rows.Count > 0)
                {
                for (int i = 0; i < dt4.Rows.Count; i++)
                {
                    WillDetailModel WDM4 = new WillDetailModel();


                    WDM4.appointeesType = dt4.Rows[i]["Type"].ToString();

                    WDM4.appointeesSubtype = dt4.Rows[i]["subType"].ToString();
                    WDM4.appointeesName = dt4.Rows[i]["Name"].ToString();
                    WDM4.appointeesmiddle = dt4.Rows[i]["middleName"].ToString();
                    WDM4.appointeessurname = dt4.Rows[i]["Surname"].ToString();
                    WDM4.appointeesidentityproof = dt4.Rows[i]["Identity_Proof"].ToString();
                    WDM4.appointeesIdentityproofvalue = dt4.Rows[i]["Identity_Proof_Value"].ToString();
                    WDM4.appointeesaltidentityproof = dt4.Rows[i]["Alt_Identity_Proof"].ToString();
                    WDM4.appointeesaltidentityproofvalue = dt4.Rows[i]["Alt_Identity_Proof_Value"].ToString();
                    //WDM4.appointeesDOB = Convert.ToDateTime(dt4.Rows[i]["DOB"]).ToString("dd-MM-yyyy");
                    WDM4.appointeesGender = dt4.Rows[i]["Gender"].ToString();
                    WDM4.appointeesOccupation = dt4.Rows[i]["Occupation"].ToString();
                    WDM4.appointeesrelationship = dt4.Rows[i]["Relationship"].ToString();
                    WDM4.appointeesaddress1 = dt4.Rows[i]["Address1"].ToString();
                    WDM4.appointeesaddress2 = dt4.Rows[i]["Address2"].ToString();
                    WDM4.appointeesaddress3 = dt4.Rows[i]["Address3"].ToString();
                    WDM4.appointeescity = dt4.Rows[i]["City"].ToString();
                    WDM4.appointeesstate = dt4.Rows[i]["State"].ToString();
                    WDM4.appointeespin = dt4.Rows[i]["Pin"].ToString();

                    list5.Add(WDM4);
                }
                 }
                    ViewBag.appointees = list5;

                
                



                //end






                // Testator Family 
                con.Open();
                string query5 = "select a.fId , a.First_Name , a.Last_Name , a.Middle_Name , a.DOB , a.Marital_Status , a.Religion , a.Relationship , a.Address1 , a.Address2 , a.Address3 , a.City , a.State , a.Pin , a.tId , a.active , a.Identity_Proof , a.Identity_Proof_Value , a.Alt_Identity_Proof , a.Alt_Identity_Proof_Value , a.Is_Informed_Person from testatorFamily a inner join TestatorDetails b on a.tId=b.tId where b.tId = " + NestId + " ";
                SqlDataAdapter da5 = new SqlDataAdapter(query5, con);
                DataTable dt5 = new DataTable();
                da5.Fill(dt5);
                con.Close();



                if (dt5.Rows.Count > 0)
                {

                for (int i = 0; i < dt5.Rows.Count; i++)
                {
                    WillDetailModel WDM3 = new WillDetailModel();
                    WDM3.tffirstname = dt5.Rows[i]["First_Name"].ToString();

                    WDM3.tflastname = dt5.Rows[i]["Last_Name"].ToString();
                    WDM3.tfmiddlename = dt5.Rows[i]["Middle_Name"].ToString();
                    WDM3.tfdob = Convert.ToDateTime(dt5.Rows[i]["DOB"]).ToString("dd-MM-yyyy");
                    WDM3.tfmaritalstatus = dt5.Rows[i]["Marital_Status"].ToString();
                    WDM3.tfreligion = dt5.Rows[i]["Religion"].ToString();
                    WDM3.tfrelationship = dt5.Rows[i]["Relationship"].ToString();
                    WDM3.tfaddress1 = dt5.Rows[i]["Address1"].ToString();
                    WDM3.tfaddress2 = dt5.Rows[i]["Address2"].ToString();
                    WDM3.tfaddress3 = dt5.Rows[i]["Address3"].ToString();
                    WDM3.tfcity = dt5.Rows[i]["City"].ToString();
                    WDM3.tfstate = dt5.Rows[i]["State"].ToString();
                    WDM3.tfpin = dt5.Rows[i]["Pin"].ToString();
                    WDM3.tfidentityproof = dt5.Rows[i]["Identity_Proof"].ToString();
                    WDM3.tfidentityproofvalue = dt5.Rows[i]["Identity_Proof_Value"].ToString();
                    WDM3.tfaltidentityproof = dt5.Rows[i]["Alt_Identity_Proof"].ToString();
                    WDM3.tfaltidentityproofvalue = dt5.Rows[i]["Alt_Identity_Proof_Value"].ToString();
                    WDM3.tfisinformedperson = dt5.Rows[i]["Is_Informed_Person"].ToString();

                    list3.Add(WDM3);
                }
                    
                    ViewBag.testatorFamily = list3;



                }
              

                // Beneficiary Mapping
                con.Open();


                string query6 = "";

            if (Session["doctype"] != null)
            {

                if (Session["WillType"].ToString() == "Quick")
                {
                    query6 = "select b.First_Name , a.Proportion from BeneficiaryAssets a inner join BeneficiaryDetails b on a.tid=b.tId where a.WillType = 'Quick' and a.tid = "+ NestId + "";
                }

                if (Session["WillType"].ToString() == "Detailed")
                {
                    query6 = "select e.First_Name , c.AssetsType , b.AssetsCategory ,  a.Proportion from BeneficiaryAssets a inner join AssetsCategory b on a.AssetCategory_ID=b.amId inner join AssetsType c on b.atId = c.atId inner join TestatorDetails d on a.tid = d.tId  inner join BeneficiaryDetails e on e.tId=a.tid where a.tid =  " + NestId + " and a.doctype = 'Will' and a.WillType='Detailed'";
                }

                if (Session["doctype"].ToString() == "POA")
                {
                    query6 = "select e.First_Name , c.AssetsType , b.AssetsCategory ,  a.Proportion from BeneficiaryAssets a inner join AssetsCategory b on a.AssetCategory_ID=b.amId inner join AssetsType c on b.atId = c.atId inner join TestatorDetails d on a.tid = d.tId  inner join BeneficiaryDetails e on e.tId=a.tid where a.tid =  " + NestId + " and a.doctype = 'POA'";
                }


                if (Session["doctype"].ToString() == "GiftDeeds")
                {
                    query6 = "select e.First_Name , c.AssetsType , b.AssetsCategory ,  a.Proportion from BeneficiaryAssets a inner join AssetsCategory b on a.AssetCategory_ID=b.amId inner join AssetsType c on b.atId = c.atId inner join TestatorDetails d on a.tid = d.tId  inner join BeneficiaryDetails e on e.tId=a.tid where a.tid =  " + NestId + " and a.doctype = 'GiftDeeds'";
                }





            }
            else
            {
                return RedirectToAction("LoginPageIndex", "LoginPage");
            }





            SqlDataAdapter da6 = new SqlDataAdapter(query6, con);
                DataTable dt6 = new DataTable();
                da6.Fill(dt6);
                con.Close();



                if (dt6.Rows.Count > 0)
                {

                for (int i = 0; i < dt6.Rows.Count; i++)
                {
                    WillDetailModel WDM5 = new WillDetailModel();
                    WDM5.BeneficiaryName = dt6.Rows[i]["First_Name"].ToString();

                    if (Session["WillType"].ToString() != "Quick")
                    {
                        WDM5.bmassettype = dt6.Rows[i]["AssetsType"].ToString();

                        WDM5.bmassetcat = dt6.Rows[i]["AssetsCategory"].ToString();
                    }
                  
             
                    WDM5.bmproportion = dt6.Rows[i]["Proportion"].ToString();
                    list6.Add(WDM5);
                }
                
                     


                        

                    
                    ViewBag.BeneficiaryMapping = list6;



                }
                





                //end



                



                con.Open();
                string query8 = "select a.nId , a.First_Name , a.Last_Name , a.Middle_Name , a.DOB , a.Mobile , a.Relationship , a.Marital_Status , a.Religion , a.Identity_Proof , a.Identity_Proof_Value , a.Alt_Identity_Proof , a.Alt_Identity_Proof_Value , a.Address1 , a.Address2 , a.Address3 , a.City , a.State , a.Pin , a.aiid , a.tId , a.dateCreated , a.createdBy , a.documentId , a.Description_of_Assets from Nominee a inner join TestatorDetails b on a.tId=b.tId where b.tId = " + NestId + "";
                SqlDataAdapter da8 = new SqlDataAdapter(query8, con);
                DataTable dt8 = new DataTable();
                da8.Fill(dt8);
                con.Close();


                if (dt8.Rows.Count > 0)
                {

                for (int i = 0; i < dt8.Rows.Count; i++)
                {

                    WillDetailModel NM = new WillDetailModel();

                    NM.nomfirstname = dt8.Rows[i]["First_Name"].ToString();
                    NM.nomlastname = dt8.Rows[i]["Last_Name"].ToString();
                    NM.nommiddlename = dt8.Rows[i]["Middle_Name"].ToString();
                    NM.nomdob = Convert.ToDateTime(dt8.Rows[i]["DOB"]).ToString("dd-MM-yyyy");
                    NM.nommobile = dt8.Rows[i]["Mobile"].ToString();
                    NM.nomrelationship = dt8.Rows[i]["Relationship"].ToString();
                    NM.nommaritalstatus = dt8.Rows[i]["Marital_Status"].ToString();
                    NM.nomreligion = dt8.Rows[i]["Religion"].ToString();
                    NM.nomidentityproof = dt8.Rows[i]["Identity_Proof"].ToString();
                    NM.nomidentityproofvalue = dt8.Rows[i]["Identity_Proof_Value"].ToString();
                    NM.nomaltidentityproof = dt8.Rows[i]["Alt_Identity_Proof"].ToString();
                    NM.nomaltidentityproofvalue = dt8.Rows[i]["Alt_Identity_Proof_Value"].ToString();
                    NM.nomaddress1 = dt8.Rows[i]["Address1"].ToString();
                    NM.nomaddress2 = dt8.Rows[i]["Address2"].ToString();
                    NM.nomaddress3 = dt8.Rows[i]["Address3"].ToString();
                    NM.nomcity = dt8.Rows[i]["City"].ToString();
                    NM.nomstate = dt8.Rows[i]["State"].ToString();
                    NM.nompin = dt8.Rows[i]["Pin"].ToString();



                    list8.Add(NM);

                }

                    ViewBag.nominee = list8;
                }




            //end







            // Witness 




            con.Open();
            string query33 = "";


            if (Session["doctype"] != null)
            {

                if (Session["doctype"].ToString() == "Will" && Session["WillType"].ToString() == "Quick")
                {
                    query33 = "select a.apId , a.documentId , a.Type , a.subType , a.Name , a.middleName  , a.Surname , a.Identity_Proof , a.Identity_Proof_Value , a.Alt_Identity_Proof , a.Alt_Identity_Proof_Value , a.DOB , a.Gender , a.Occupation , a.Relationship , a.Address1 , a.Address2 , a.Address3 , a.City , a.State , a.Pin , a.dateCreated, a.tid  from Appointees a inner join  TestatorDetails b on a.tid=b.tid where b.tId = " + NestId + "   and a.Type='Witness' and a.WillType = 'Quick'";
                }

                if (Session["doctype"].ToString() == "Will" && Session["WillType"].ToString() == "Detailed")
                {
                    query33 = "select a.apId , a.documentId , a.Type , a.subType , a.Name , a.middleName  , a.Surname , a.Identity_Proof , a.Identity_Proof_Value , a.Alt_Identity_Proof , a.Alt_Identity_Proof_Value , a.DOB , a.Gender , a.Occupation , a.Relationship , a.Address1 , a.Address2 , a.Address3 , a.City , a.State , a.Pin , a.dateCreated, a.tid  from Appointees a inner join  TestatorDetails b on a.tid=b.tid where b.tId = " + NestId + "   and a.Type='Witness' and a.WillType = 'Detailed'";
                }

                if (Session["doctype"].ToString() == "POA")
                {
                    query33 = "select a.apId , a.documentId , a.Type , a.subType , a.Name , a.middleName  , a.Surname , a.Identity_Proof , a.Identity_Proof_Value , a.Alt_Identity_Proof , a.Alt_Identity_Proof_Value , a.DOB , a.Gender , a.Occupation , a.Relationship , a.Address1 , a.Address2 , a.Address3 , a.City , a.State , a.Pin , a.dateCreated, a.tid  from Appointees a inner join  TestatorDetails b on a.tid=b.tid where b.tId = " + NestId + " and a.doctype = 'POA' and a.ExecutorType='Executor' ";
                }


                if (Session["doctype"].ToString() == "GiftDeeds")
                {
                    query33 = "select a.apId , a.documentId , a.Type , a.subType , a.Name , a.middleName  , a.Surname , a.Identity_Proof , a.Identity_Proof_Value , a.Alt_Identity_Proof , a.Alt_Identity_Proof_Value , a.DOB , a.Gender , a.Occupation , a.Relationship , a.Address1 , a.Address2 , a.Address3 , a.City , a.State , a.Pin , a.dateCreated, a.tid  from Appointees a inner join  TestatorDetails b on a.tid=b.tid where b.tId = " + NestId + " and a.doctype = 'Giftdeeds' and a.ExecutorType='Executor' ";
                }





            }
            else
            {
                return RedirectToAction("LoginPageIndex", "LoginPage");
            }




            SqlDataAdapter da43 = new SqlDataAdapter(query33, con);
            DataTable dt43 = new DataTable();
            da43.Fill(dt43);
            con.Close();



            if (dt43.Rows.Count > 0)
            {
                for (int i = 0; i < dt43.Rows.Count; i++)
                {
                    WillDetailModel WDM4 = new WillDetailModel();


                    WDM4.waltappointeesType = dt43.Rows[i]["Type"].ToString();

                    WDM4.waltappointeesSubtype = dt43.Rows[i]["subType"].ToString();
                    WDM4.waltappname = dt43.Rows[i]["Name"].ToString();
                    WDM4.waltappmiddlename = dt43.Rows[i]["middleName"].ToString();
                    WDM4.waltappsurname = dt43.Rows[i]["Surname"].ToString();
                    WDM4.waltappidentityproof = dt43.Rows[i]["Identity_Proof"].ToString();
                    WDM4.waltappidentityproofvalue = dt43.Rows[i]["Identity_Proof_Value"].ToString();
                    WDM4.waltappaltidentityproof = dt43.Rows[i]["Alt_Identity_Proof"].ToString();
                    WDM4.waltappaltidentityproofvalue = dt43.Rows[i]["Alt_Identity_Proof_Value"].ToString();
                    //WDM4.appointeesDOB = Convert.ToDateTime(dt44.Rows[i]["DOB"]).ToString("dd-MM-yyyy");
                    WDM4.waltappgender = dt43.Rows[i]["Gender"].ToString();
                    WDM4.waltappoccupation = dt43.Rows[i]["Occupation"].ToString();
                    WDM4.waltapprelationship = dt43.Rows[i]["Relationship"].ToString();
                    WDM4.waltappaddress1 = dt43.Rows[i]["Address1"].ToString();
                    WDM4.waltappaddress2 = dt43.Rows[i]["Address2"].ToString();
                    WDM4.waltappaddress3 = dt43.Rows[i]["Address3"].ToString();
                    WDM4.waltappcity = dt43.Rows[i]["City"].ToString();
                    WDM4.waltappstate = dt43.Rows[i]["State"].ToString();
                    WDM4.wltapppin = dt43.Rows[i]["Pin"].ToString();

                    list5.Add(WDM4);
                }
            }
            ViewBag.Witness = list5;






            //end














            return View("~/Views/WillDetails/WillDetailsPageContent.cshtml", WDM);
        }



        public ActionResult GetTidForVerification(WillDetailModel WDM)
        {

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



            int Response = Convert.ToInt32(Request["send"]);

            con.Open();

            string query = "insert into DocumentVerification (Uid,Tid,Verification_Status) values ("+Convert.ToInt32(Session["uuid"])+" ," + Response + " , 'InActive' )";
            SqlCommand cmd = new SqlCommand(query,con);
            cmd.ExecuteNonQuery();


            con.Close();



            //check document matches with rules
            con.Open();
            string checkquery = "select b.templateId , b.tId , a.documentType , a.category , a.guardian , a.executors_category , a.AlternateBenficiaries , a.AlternateGaurdian , a.AlternateExecutors from documentRules a inner join documentMaster b on a.wdId=b.wdId where a.tid =" + Response + "";
            SqlDataAdapter checkda = new SqlDataAdapter(checkquery, con);
            DataTable checkdt = new DataTable();
            checkda.Fill(checkdt);

            int documentType1 = 0;
            int category = 0;
            int guardian = 0;
            int executors_category = 0;
            int AlternateBenficiaries = 0;
            int AlternateGaurdian = 0;
            int AlternateExecutors = 0;
            int Dmtemplateid = 0;

            if (checkdt.Rows.Count > 0)
            {

                Dmtemplateid = Convert.ToInt32(checkdt.Rows[0]["templateId"]);
                documentType1 = Convert.ToInt32(checkdt.Rows[0]["documentType"]);
                category = Convert.ToInt32(checkdt.Rows[0]["category"]);
                guardian = Convert.ToInt32(checkdt.Rows[0]["guardian"]);
                executors_category = Convert.ToInt32(checkdt.Rows[0]["executors_category"]);
                AlternateBenficiaries = Convert.ToInt32(checkdt.Rows[0]["AlternateBenficiaries"]);
                AlternateGaurdian = Convert.ToInt32(checkdt.Rows[0]["AlternateGaurdian"]);
                AlternateExecutors = Convert.ToInt32(checkdt.Rows[0]["AlternateExecutors"]);

            }




            con.Close();




            // find match

            con.Open();
            string matchquery = "select TemplateID from DocumentIdentifier where DocumentType = " + documentType1 + " and TypeOfWill = " + category + " and AppointmentOfGuardian = " + guardian + " and NumberOfExecutors = " + executors_category + "  and AppointmentOfAltBeneficiary = " + AlternateBenficiaries + " and AppointmentOfAltGuardian = " + AlternateGaurdian + "  and AppointmentOfAltExecutor = " + AlternateExecutors + " ";
            SqlDataAdapter matchda = new SqlDataAdapter(matchquery, con);
            DataTable matchdt = new DataTable();
            matchda.Fill(matchdt);
            int TemplateID =0;
            if (matchdt.Rows.Count > 0)
            {
                // update documentmaster with match template id 

                TemplateID = Convert.ToInt32(matchdt.Rows[0]["TemplateID"]);
                string query3 = "update documentMaster set templateId = " + Convert.ToInt32(matchdt.Rows[0]["TemplateID"]) + " where tId= " + TemplateID + "  ";
                SqlCommand cmd3 = new SqlCommand(query3, con);
                cmd3.ExecuteNonQuery();

                //
            }


            ViewBag.Message = "Verified";

            return View("~/Views/WillDetails/WillDetailsPageContent.cshtml");
        }






        public ActionResult SetDocumentStatusCompleted()
        {

            con.Open();
       
            string  qtest001 = "select top 1 tId from TestatorDetails where uId = " + Convert.ToInt32(Session["uuid"]) + " order by tid desc ";
            SqlDataAdapter test001da = new SqlDataAdapter(qtest001, con);
            DataTable test001dt = new DataTable();
            test001da.Fill(test001dt);
          

            int NestId = 0;
            if (test001dt.Rows.Count > 0)
            {

                NestId = Convert.ToInt32(test001dt.Rows[0]["tId"]);

            }


            if (Session["WillType"].ToString() == "Detailed")
            {

                // update doctype for testator
               
                string query77 = "update TestatorDetails set WillType = 'Detailed'  , doctype='" + Session["doctype"].ToString() + "' , documentstatus = 'Completed'   where  tId = " + NestId + "";
                SqlCommand cmd77 = new SqlCommand(query77, con);
                cmd77.ExecuteNonQuery();
               

                //end


                // for testator family

                string qchk001 = "update testatorFamily set documentstatus = 'Completed' where tId = "+NestId+" and WillType = 'Detailed'";
                SqlCommand cmd1 = new SqlCommand(qchk001,con);
                cmd1.ExecuteNonQuery();
                //end




                // for beneficiary institution
               
                string qchk0020 = "update BeneficiaryInstitutions set documentstatus = 'Completed' where tId = "+NestId+" and WillType = 'Detailed'";
                SqlCommand cmd2 = new SqlCommand(qchk0020, con);
                cmd2.ExecuteNonQuery();

                //end





                // for beneficiary

                string qchk002 = "update BeneficiaryDetails set documentstatus = 'Completed' where tId = "+NestId+" and WillType = 'Detailed' ";
                SqlCommand cmd3 = new SqlCommand(qchk002, con);
                cmd3.ExecuteNonQuery();

                //end











                // for assetinformation

                string qchk003 = "update AssetInformation set documentstatus = 'Completed' where tId = "+NestId+" and WillType = 'Detailed'  ";
                SqlCommand cmd4 = new SqlCommand(qchk003, con);
                cmd4.ExecuteNonQuery();


                //end









                // for asset mapping 


                string qchk006 = "update BeneficiaryAssets set documentstatus = 'Completed' where tId = "+NestId+" and WillType = 'Detailed'";
                SqlCommand cmd5 = new SqlCommand(qchk006, con);
                cmd5.ExecuteNonQuery();


                //end


                // for appointees 


                string qchk008 = "update Appointees set documentstatus = 'Completed' where tId = "+NestId+" and WillType = 'Detailed' and Type='Executor' ";
                SqlCommand cmd6 = new SqlCommand(qchk008, con);
                cmd6.ExecuteNonQuery();



                //end






                // for Addwitness 


                string qchk0082 = "update Appointees set documentstatus = 'Completed' where tId = " + NestId + " and WillType = 'Detailed' and Type='Witness' ";
                SqlCommand cmd7 = new SqlCommand(qchk0082, con);
                cmd7.ExecuteNonQuery();

                //end



            



                // Reset users table with 0

                string qchk0083 = "update users set Will = 0 where uId="+Convert.ToInt32(Session["uuid"])+" ";
                SqlCommand cmd8 = new SqlCommand(qchk0083, con);
                cmd8.ExecuteNonQuery();



                //end







            }


            if (Session["WillType"].ToString() == "Quick")
            {

                // update doctype for testator
               
                string query77 = "update TestatorDetails set WillType = '" + Session["WillType"].ToString() + "' , doctype='Will'  , documentstatus = 'Completed'   where  tId = " + NestId + "";
                SqlCommand cmd77 = new SqlCommand(query77, con);
                cmd77.ExecuteNonQuery();
               

                //end



                // for testator family

                string qchk001 = "update testatorFamily set documentstatus = 'Completed' where tId = " + NestId + " and WillType = 'Quick' ";
                SqlCommand cmd1 = new SqlCommand(qchk001, con);
                cmd1.ExecuteNonQuery();
                //end




                // for beneficiary institution

                string qchk0020 = "update BeneficiaryInstitutions set documentstatus = 'Completed' where tId = " + NestId + " and WillType = 'Quick'";
                SqlCommand cmd2 = new SqlCommand(qchk0020, con);
                cmd2.ExecuteNonQuery();

                //end





                // for beneficiary

                string qchk002 = "update BeneficiaryDetails set documentstatus = 'Completed' where tId = " + NestId + " and  WillType = 'Quick' ";
                SqlCommand cmd3 = new SqlCommand(qchk002, con);
                cmd3.ExecuteNonQuery();

                //end


                // for asset mapping 


                string qchk006 = "update BeneficiaryAssets set documentstatus = 'Completed' where tId = " + NestId + " and  WillType = 'Quick'";
                SqlCommand cmd5 = new SqlCommand(qchk006, con);
                cmd5.ExecuteNonQuery();


                //end


                // for appointees 


                string qchk008 = "update Appointees set documentstatus = 'Completed' where tId = " + NestId + " and WillType = 'Quick' and Type='Executor' ";
                SqlCommand cmd6 = new SqlCommand(qchk008, con);
                cmd6.ExecuteNonQuery();



                //end






                // for Addwitness 


                string qchk0082 = "update Appointees set documentstatus = 'Completed' where tId = " + NestId + " and WillType = 'Quick' and Type='Witness' ";
                SqlCommand cmd7 = new SqlCommand(qchk0082, con);
                cmd7.ExecuteNonQuery();

                //end


                // Reset users table with 0

                string qchk0083 = "update users set Will = 0 where uId=" + Convert.ToInt32(Session["uuid"]) + "   ";
                SqlCommand cmd8 = new SqlCommand(qchk0083, con);
                cmd8.ExecuteNonQuery();



                //end

            }







            if (Session["doctype"].ToString() == "POA")
            {



                // update doctype for testator

                string query77 = "update TestatorDetails set  doctype='" + Session["doctype"].ToString() + "'  , documentstatus = 'Completed'   where  tId = " + NestId + "";
                SqlCommand cmd77 = new SqlCommand(query77, con);
                cmd77.ExecuteNonQuery();


                //end







                // for beneficiary

                string qchk002 = "update BeneficiaryDetails set documentstatus = 'Completed' where tId = " + NestId + " and  doctype = 'POA' ";
                SqlCommand cmd3 = new SqlCommand(qchk002, con);
                cmd3.ExecuteNonQuery();

                //end











                // for assetinformation

                string qchk003 = "update AssetInformation set documentstatus = 'Completed' where tId = " + NestId + " and doctype = 'POA'  ";
                SqlCommand cmd4 = new SqlCommand(qchk003, con);
                cmd4.ExecuteNonQuery();


                //end









                // for asset mapping 


                string qchk006 = "update BeneficiaryAssets set documentstatus = 'Completed' where tId = " + NestId + " and doctype = 'POA'  ";
                SqlCommand cmd5 = new SqlCommand(qchk006, con);
                cmd5.ExecuteNonQuery();


                //end


               






                // for Addwitness 


                string qchk0082 = "update Appointees set documentstatus = 'Completed' where tId = " + NestId + " and WillType = 'Detailed' and Type='Witness' and doctype = 'POA' ";
                SqlCommand cmd7 = new SqlCommand(qchk0082, con);
                cmd7.ExecuteNonQuery();

                //end



                // Reset users table with 0

                string qchk0083 = "update users set POA = 0 where uId=" + Convert.ToInt32(Session["uuid"]) + "    ";
                SqlCommand cmd8 = new SqlCommand(qchk0083, con);
                cmd8.ExecuteNonQuery();



                //end

            }







            if (Session["doctype"].ToString() == "GiftDeeds")
            {





                // update doctype for testator

                string query77 = "update TestatorDetails set  doctype='" + Session["doctype"].ToString() + "'  , documentstatus = 'Completed'   where  tId = " + NestId + "";
                SqlCommand cmd77 = new SqlCommand(query77, con);
                cmd77.ExecuteNonQuery();


                //end




                // for beneficiary

                string qchk002 = "update BeneficiaryDetails set documentstatus = 'Completed' where tId = " + NestId + " and  doctype = 'Giftdeeds' ";
                SqlCommand cmd3 = new SqlCommand(qchk002, con);
                cmd3.ExecuteNonQuery();

                //end











                // for assetinformation

                string qchk003 = "update AssetInformation set documentstatus = 'Completed' where tId = " + NestId + " and doctype = 'Giftdeeds'  ";
                SqlCommand cmd4 = new SqlCommand(qchk003, con);
                cmd4.ExecuteNonQuery();


                //end









                // for asset mapping 


                string qchk006 = "update BeneficiaryAssets set documentstatus = 'Completed' where tId = " + NestId + " and doctype = 'Giftdeeds'  ";
                SqlCommand cmd5 = new SqlCommand(qchk006, con);
                cmd5.ExecuteNonQuery();


                //end









                // for Addwitness 


                string qchk0082 = "update Appointees set documentstatus = 'Completed' where tId = " + NestId + " and WillType = 'Detailed' and Type='Witness' and doctype = 'Giftdeeds' ";
                SqlCommand cmd7 = new SqlCommand(qchk0082, con);
                cmd7.ExecuteNonQuery();

                //end



                // Reset users table with 0

                string qchk0083 = "update users set Giftdeeds = 0 where uId=" + Convert.ToInt32(Session["uuid"]) + "  ";
                SqlCommand cmd8 = new SqlCommand(qchk0083, con);
                cmd8.ExecuteNonQuery();



                //end

            }



            con.Close();





            return RedirectToAction("Report.aspx", "page", new { NestId = NestId , WillType = Session["WillType"].ToString() , Typedoc = "Quickgenerate" });
        }




    }
}