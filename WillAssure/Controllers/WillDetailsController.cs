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
        public ActionResult WillDetailsIndex(int NestId)
        {
            ViewBag.Collapse = "true";
            if (Session["Type"].ToString() != "DistributorAdmin")
            {
                if (Session["doctype"].ToString() == "Will")
                {
                    ViewBag.view = "Will";
                }


                if (Session["doctype"].ToString() == "POA" || Session["doctype"].ToString() == "GiftDeeds")
                {
                    ViewBag.view = "POA";
                    ViewBag.view = "GiftDeeds";
                }
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

        
                con.Open();
                string query = "select * from TestatorDetails where tId = "+NestId+" ";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();


                //WDM.VerifyId = NestId;
                if (dt.Rows.Count > 0)
                {

                    // testator details
                    //for (int i = 0; i < dt.Rows.Count; i++)
                    //{
                        WillDetailModel WDM1 = new WillDetailModel();

                        ViewBag.tid = Convert.ToInt32(dt.Rows[0]["tId"]);
                        WDM1.TestatorName = dt.Rows[0]["First_Name"].ToString();
                        WDM1.TestatorLastName = dt.Rows[0]["Last_Name"].ToString();
                        WDM1.TestatorMiddleName = dt.Rows[0]["Middle_Name"].ToString();
                        WDM1.TestatorDOB = dt.Rows[0]["DOB"].ToString();
                        WDM1.TestatorOccupation = dt.Rows[0]["Occupation"].ToString();
                        WDM1.TestatorMobile = dt.Rows[0]["Mobile"].ToString();
                        WDM1.TestatorEmail = dt.Rows[0]["Email"].ToString();
                        WDM1.TestatorMaritalStatus = dt.Rows[0]["maritalStatus"].ToString();
                        WDM1.TestatorRelationship = dt.Rows[0]["RelationShip"].ToString();
                        WDM1.TestatorReligion = dt.Rows[0]["Religion"].ToString();
                        WDM1.TestatorIdentityProof = dt.Rows[0]["Identity_Proof"].ToString();
                        WDM1.TestatorIdentityProofValue = dt.Rows[0]["Identity_proof_Value"].ToString();
                        WDM1.TestatorAltIdentityProof = dt.Rows[0]["Alt_Identity_Proof"].ToString();
                        WDM1.TestatorAltIdentityProofValue = dt.Rows[0]["Alt_Identity_proof_Value"].ToString();
                        WDM1.TestatorGender = dt.Rows[0]["Gender"].ToString();
                        WDM1.TestatorAddress1 = dt.Rows[0]["Address1"].ToString();
                        WDM1.TestatorAddress2 = dt.Rows[0]["Address2"].ToString();
                        WDM1.TestatorAddress3 = dt.Rows[0]["Address3"].ToString();
                        WDM1.TestatorCity = dt.Rows[0]["City"].ToString();
                        WDM1.TestatorState = dt.Rows[0]["State"].ToString();
                        WDM1.TestatorCountry = dt.Rows[0]["Country"].ToString();
                        WDM1.TestatorPin = dt.Rows[0]["Pin"].ToString();
                        //end

                        list1.Add(WDM1);

                    //}


                    ViewBag.TestatorDetails = list1;

                }
           



                // beneficiary 

          

                con.Open();
                string query2 = "select a.bpId , a.First_Name , a.Last_Name , a.Middle_Name , a.DOB , a.Mobile , a.Relationship , a.Marital_Status , a.Religion , a.Identity_proof , a.Identity_proof_value , a.Alt_Identity_proof , a.Alt_Identity_proof_value , a.Address1 , a.Address2 , a.Address3 , a.City , a.State , a.Pin , a.aiid , a.tId , a.dateCreated , a.createdBy , a.documentId , a.beneficiary_type from BeneficiaryDetails a inner join TestatorDetails b on a.tId=b.tId where b.tId = " + NestId + "";
                SqlDataAdapter da2 = new SqlDataAdapter(query2, con);
                DataTable dt2 = new DataTable();
                da2.Fill(dt2);
                con.Close();



                if (dt2.Rows.Count > 0)
                {

                    for (int i = 0; i < dt2.Rows.Count; i++)
                    {
                        WillDetailModel WDM2 = new WillDetailModel();

                        WDM2.BeneficiaryName = dt2.Rows[0]["First_Name"].ToString();

                        WDM2.BeneficiaryLastName = dt2.Rows[0]["Last_Name"].ToString();
                        WDM2.BeneficiaryMiddleName = dt2.Rows[0]["Middle_Name"].ToString();
                        WDM2.BeneficiaryDOB = dt2.Rows[0]["DOB"].ToString();
                        WDM2.BeneficiaryMobile = dt2.Rows[0]["Mobile"].ToString();
                        WDM2.BeneficiaryRelationship = dt2.Rows[0]["Relationship"].ToString();
                        WDM2.BeneficiaryMartialStatus = dt2.Rows[0]["Marital_Status"].ToString();
                        WDM2.BeneficiaryReligion = dt2.Rows[0]["Religion"].ToString();
                        WDM2.BeneficiaryIdentityProof = dt2.Rows[0]["Identity_proof"].ToString();
                        WDM2.BeneficiaryIdentityProofValue = dt2.Rows[0]["Identity_proof_value"].ToString();
                        WDM2.BeneficiaryaltidentityProof = dt2.Rows[0]["Alt_Identity_proof"].ToString();
                        WDM2.beneficiaryaltidentityproofvalue = dt2.Rows[0]["Alt_Identity_proof_value"].ToString();
                        WDM2.beneficiaryaddress1 = dt2.Rows[0]["Address1"].ToString();
                        WDM2.beneficiaryaddress2 = dt2.Rows[0]["Address2"].ToString();
                        WDM2.beneficiaryaddress3 = dt2.Rows[0]["Address3"].ToString();
                        WDM2.beneficiarycity = dt2.Rows[0]["City"].ToString();
                        WDM2.beneficiarystate = dt2.Rows[0]["State"].ToString();
                        WDM2.beneficiarypin = dt2.Rows[0]["Pin"].ToString();

                        list2.Add(WDM2);

                    }

                    ViewBag.beneficiary = list2;



                }
              



                //end






                // appointees 




                con.Open();
                string query3 = "select a.apId , a.documentId , a.Type , a.subType , a.Name , a.middleName  , a.Surname , a.Identity_Proof , a.Identity_Proof_Value , a.Alt_Identity_Proof , a.Alt_Identity_Proof_Value , a.DOB , a.Gender , a.Occupation , a.Relationship , a.Address1 , a.Address2 , a.Address3 , a.City , a.State , a.Pin , a.dateCreated, a.tid  from Appointees a inner join  TestatorDetails b on a.tid=b.tid where b.tId = " + NestId + "  ";
                SqlDataAdapter da4 = new SqlDataAdapter(query3, con);
                DataTable dt4 = new DataTable();
                da4.Fill(dt4);
                con.Close();



                if (dt4.Rows.Count > 0)
                {
                    
                        WillDetailModel WDM4 = new WillDetailModel();


                        WDM4.appointeesType = dt4.Rows[0]["Type"].ToString();

                        WDM4.appointeesSubtype = dt4.Rows[0]["subType"].ToString();
                        WDM4.appointeesName = dt4.Rows[0]["Name"].ToString();
                        WDM4.appointeesmiddle = dt4.Rows[0]["middleName"].ToString();
                        WDM4.appointeessurname = dt4.Rows[0]["Surname"].ToString();
                        WDM4.appointeesidentityproof = dt4.Rows[0]["Identity_Proof"].ToString();
                        WDM4.appointeesIdentityproofvalue = dt4.Rows[0]["Identity_Proof_Value"].ToString();
                        WDM4.appointeesaltidentityproof = dt4.Rows[0]["Alt_Identity_Proof"].ToString();
                        WDM4.appointeesaltidentityproofvalue = dt4.Rows[0]["Alt_Identity_Proof_Value"].ToString();
                        WDM4.appointeesDOB = dt4.Rows[0]["DOB"].ToString();
                        WDM4.appointeesGender = dt4.Rows[0]["Gender"].ToString();
                        WDM4.appointeesOccupation = dt4.Rows[0]["Occupation"].ToString();
                        WDM4.appointeesrelationship = dt4.Rows[0]["Relationship"].ToString();
                        WDM4.appointeesaddress1 = dt4.Rows[0]["Address1"].ToString();
                        WDM4.appointeesaddress2 = dt4.Rows[0]["Address2"].ToString();
                        WDM4.appointeesaddress3 = dt4.Rows[0]["Address3"].ToString();
                        WDM4.appointeescity = dt4.Rows[0]["City"].ToString();
                        WDM4.appointeesstate = dt4.Rows[0]["State"].ToString();
                        WDM4.appointeespin = dt4.Rows[0]["Pin"].ToString();

                        list5.Add(WDM4);
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

                   
                        WillDetailModel WDM3 = new WillDetailModel();
                        WDM3.tffirstname = dt5.Rows[0]["First_Name"].ToString();

                        WDM3.tflastname = dt5.Rows[0]["Last_Name"].ToString();
                        WDM3.tfmiddlename = dt5.Rows[0]["Middle_Name"].ToString();
                        WDM3.tfdob = dt5.Rows[0]["DOB"].ToString();
                        WDM3.tfmaritalstatus = dt5.Rows[0]["Marital_Status"].ToString();
                        WDM3.tfreligion = dt5.Rows[0]["Religion"].ToString();
                        WDM3.tfrelationship = dt5.Rows[0]["Relationship"].ToString();
                        WDM3.tfaddress1 = dt5.Rows[0]["Address1"].ToString();
                        WDM3.tfaddress2 = dt5.Rows[0]["Address2"].ToString();
                        WDM3.tfaddress3 = dt5.Rows[0]["Address3"].ToString();
                        WDM3.tfcity = dt5.Rows[0]["City"].ToString();
                        WDM3.tfstate = dt5.Rows[0]["State"].ToString();
                        WDM3.tfpin = dt5.Rows[0]["Pin"].ToString();
                        WDM3.tfidentityproof = dt5.Rows[0]["Identity_Proof"].ToString();
                        WDM3.tfidentityproofvalue = dt5.Rows[0]["Identity_Proof_Value"].ToString();
                        WDM3.tfaltidentityproof = dt5.Rows[0]["Alt_Identity_Proof"].ToString();
                        WDM3.tfaltidentityproofvalue = dt5.Rows[0]["Alt_Identity_Proof_Value"].ToString();
                        WDM3.tfisinformedperson = dt5.Rows[0]["Is_Informed_Person"].ToString();

                        list3.Add(WDM3);

                    
                    ViewBag.testatorFamily = list3;



                }
              

                // Beneficiary Mapping
                con.Open();
                string query6 = "select c.AssetsType , b.AssetsCategory , a.SchemeName , a.InstrumentName , a.Proportion from BeneficiaryAssets a inner join AssetsCategory b on a.AssetCategory_ID=b.amId inner join AssetsType c on b.atId = c.atId inner join TestatorDetails d on a.tid = d.tId where a.tid =  " + NestId + "";
                SqlDataAdapter da6 = new SqlDataAdapter(query6, con);
                DataTable dt6 = new DataTable();
                da6.Fill(dt6);
                con.Close();



                if (dt6.Rows.Count > 0)
                {

                
                        WillDetailModel WDM5 = new WillDetailModel();
                        WDM5.bmassettype = dt6.Rows[0]["AssetsType"].ToString();

                        WDM5.bmassetcat = dt6.Rows[0]["AssetsCategory"].ToString();
                        WDM5.bmschemename = dt6.Rows[0]["SchemeName"].ToString();
                        WDM5.bminstrumentname = dt6.Rows[0]["InstrumentName"].ToString();
                        WDM5.bmproportion = dt6.Rows[0]["Proportion"].ToString();


                        list6.Add(WDM5);

                    
                    ViewBag.BeneficiaryMapping = list6;



                }
                





                //end



                // altbene 



                con.Open();
                string query7 = "select * from alternate_Beneficiary where tid = " + NestId + "";
                SqlDataAdapter da7 = new SqlDataAdapter(query7, con);
                DataTable dt7 = new DataTable();
                da7.Fill(dt7);

                con.Close();

                if (dt7.Rows.Count > 0)
                {
                 
                        WillDetailModel ABM = new WillDetailModel();
                        ABM.altbenefirstname = dt7.Rows[0]["First_Name"].ToString();
                        ABM.altbenelastname = dt7.Rows[0]["Last_Name"].ToString();
                        ABM.altbenemiddlename = dt7.Rows[0]["Middle_Name"].ToString();

                        ABM.altbenedob = dt7.Rows[0]["DOB"].ToString();
                        ABM.altbenemobile = dt7.Rows[0]["Mobile"].ToString();
                        ABM.altbenerelationship = dt7.Rows[0]["Relationship"].ToString();
                        ABM.altbenemaritalstatus = dt7.Rows[0]["Marital_Status"].ToString();
                        ABM.altbenereligion = dt7.Rows[0]["Religion"].ToString();
                        ABM.altbeneidentityproof = dt7.Rows[0]["Identity_Proof"].ToString();
                        ABM.altbeneidentityproofvalue = dt7.Rows[0]["Identity_Proof_Value"].ToString();
                        ABM.altbenealtidentityproof = dt7.Rows[0]["Alt_Identity_Proof"].ToString();
                        ABM.altbenealtidentityproofvalue = dt7.Rows[0]["Alt_Identity_Proof_Value"].ToString();
                        ABM.altbeneaddress1 = dt7.Rows[0]["Address1"].ToString();
                        ABM.altbeneaddress2 = dt7.Rows[0]["Address2"].ToString();
                        ABM.altbeneaddress3 = dt7.Rows[0]["Address3"].ToString();
                        ABM.altbenecity = dt7.Rows[0]["City"].ToString();
                        ABM.altbenestate = dt7.Rows[0]["State"].ToString();
                        ABM.altbenepin = dt7.Rows[0]["Pin"].ToString();

                        list7.Add(ABM);

                    

                    ViewBag.altbene = list7;


                }
              




                con.Open();
                string query8 = "select a.nId , a.First_Name , a.Last_Name , a.Middle_Name , a.DOB , a.Mobile , a.Relationship , a.Marital_Status , a.Religion , a.Identity_Proof , a.Identity_Proof_Value , a.Alt_Identity_Proof , a.Alt_Identity_Proof_Value , a.Address1 , a.Address2 , a.Address3 , a.City , a.State , a.Pin , a.aiid , a.tId , a.dateCreated , a.createdBy , a.documentId , a.Description_of_Assets from Nominee a inner join TestatorDetails b on a.tId=b.tId where b.tId = " + NestId + "";
                SqlDataAdapter da8 = new SqlDataAdapter(query8, con);
                DataTable dt8 = new DataTable();
                da8.Fill(dt8);
                con.Close();


                if (dt8.Rows.Count > 0)
                {


                   
                        WillDetailModel NM = new WillDetailModel();

                        NM.nomfirstname = dt8.Rows[0]["First_Name"].ToString();
                        NM.nomlastname = dt8.Rows[0]["Last_Name"].ToString();
                        NM.nommiddlename = dt8.Rows[0]["Middle_Name"].ToString();
                        NM.nomdob = dt8.Rows[0]["DOB"].ToString();
                        NM.nommobile = dt8.Rows[0]["Mobile"].ToString();
                        NM.nomrelationship = dt8.Rows[0]["Relationship"].ToString();
                        NM.nommaritalstatus = dt8.Rows[0]["Marital_Status"].ToString();
                        NM.nomreligion = dt8.Rows[0]["Religion"].ToString();
                        NM.nomidentityproof = dt8.Rows[0]["Identity_Proof"].ToString();
                        NM.nomidentityproofvalue = dt8.Rows[0]["Identity_Proof_Value"].ToString();
                        NM.nomaltidentityproof = dt8.Rows[0]["Alt_Identity_Proof"].ToString();
                        NM.nomaltidentityproofvalue = dt8.Rows[0]["Alt_Identity_Proof_Value"].ToString();
                        NM.nomaddress1 = dt8.Rows[0]["Address1"].ToString();
                        NM.nomaddress2 = dt8.Rows[0]["Address2"].ToString();
                        NM.nomaddress3 = dt8.Rows[0]["Address3"].ToString();
                        NM.nomcity = dt8.Rows[0]["City"].ToString();
                        NM.nomstate = dt8.Rows[0]["State"].ToString();
                        NM.nompin = dt8.Rows[0]["Pin"].ToString();



                        list8.Add(NM);
                    


                    ViewBag.nominee = list8;
                }
                



                //end




                // alt appointment



                con.Open();
                string query9 = "select a.id , a.apId , a.Name , a.MiddleName , a.Surname , a.Identity_proof , a.Identity_proof_value , a.Alt_Identity_proof , a.Alt_Identity_proof_value , a.DOB , a.Gender , a.Occupation , a.Relationship , a.Address1 , a.Address2 , a.Address3 , a.City , a.State , a.Pin , a.altguardian , a.altexec from alternate_Appointees a inner join TestatorDetails b on a.tid=b.tId where a.tid = " + NestId + "";
                SqlDataAdapter da9 = new SqlDataAdapter(query9, con);
                DataTable dt9 = new DataTable();
                da9.Fill(dt9);
                con.Close();
                string data = "";

                if (dt9.Rows.Count > 0)
                {


                  
                        WillDetailModel Am = new WillDetailModel();


                        Am.altappname = dt9.Rows[0]["Name"].ToString();
                        Am.altappmiddlename = dt9.Rows[0]["middleName"].ToString();
                        Am.altappsurname = dt9.Rows[0]["Surname"].ToString();
                        Am.altappidentityproof = dt9.Rows[0]["Identity_Proof"].ToString();
                        Am.altappidentityproofvalue = dt9.Rows[0]["Identity_Proof_Value"].ToString();
                        Am.altappaltidentityproof = dt9.Rows[0]["Alt_Identity_Proof"].ToString();
                        Am.altappaltidentityproofvalue = dt9.Rows[0]["Alt_Identity_Proof_Value"].ToString();

                        Am.altappdob = dt9.Rows[0]["DOB"].ToString();

                        Am.altappgender = dt9.Rows[0]["Gender"].ToString();
                        Am.altappoccupation = dt9.Rows[0]["Occupation"].ToString();
                        Am.altapprelationship = dt9.Rows[0]["Relationship"].ToString();
                        Am.altappaddress1 = dt9.Rows[0]["Address1"].ToString();
                        Am.altappaddress2 = dt9.Rows[0]["Address2"].ToString();
                        Am.altappaddress3 = dt9.Rows[0]["Address3"].ToString();
                        Am.altappcity = dt9.Rows[0]["City"].ToString();
                        Am.altappstate = dt9.Rows[0]["State"].ToString();
                        Am.altapppin = dt9.Rows[0]["Pin"].ToString();
                        Am.altappaltguardian = dt9.Rows[0]["altguardian"].ToString();
                        Am.altappaltexec = dt9.Rows[0]["altexec"].ToString();


                        list9.Add(Am);

                    

                    ViewBag.altappointment = list9;
                }
             






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
    }
}