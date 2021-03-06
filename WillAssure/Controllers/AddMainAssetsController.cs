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
using System.Collections;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Globalization;

namespace WillAssure.Controllers
{
    public class AddMainAssetsController : Controller
    {
        public static string connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        SqlConnection con = new SqlConnection(connectionString);
        string json = "";
        // GET: AddMainAssets
        public ActionResult AddMainAssetsIndex(string NestId)
        {


            string queryc1 = "";
            string queryc2 = "";
            string queryc3 = "";
            string queryc4 = "";
            string queryc5 = "";
            string queryc22 = "";
            //// check data for next page link if available active links

            if (Session["distid"] != null && Session["willtype"] != null && Session["doctype"] != null)
            {
                con.Open();



                //////// check TesttaorFamily 


                if (Session["WillType"].ToString() == "Quick" && Session["doctype"].ToString() == "Will")
                {
                    queryc22 = "select * from Appointees where WillType = 'Quick'  and tId = " + Convert.ToInt32(Session["distid"]) + "  and Type = 'Guardian'   ";
                }
                if (Session["WillType"].ToString() == "Detailed" && Session["doctype"].ToString() == "Will")
                {
                    queryc22 = "select * from Appointees where WillType = 'Detailed'  and tId = " + Convert.ToInt32(Session["distid"]) + " and Type = 'Guardian'   ";
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





                ////////END











                con.Close();






                //end



            }
            else
            {
                return RedirectToAction("LoginPageIndex", "LoginPage");
            }









            if (NestId != null)
            {
                TempData["NestedId"] = NestId;
            }


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
            //if (Session["tid"] == null)
            //{
            //    ViewBag.Message = "link"
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







            // fill up already exist data for assetinformation

            int aaid = 0;

            string q44 = "select top 1 aiid  from AssetInformation order by aiid desc";
            SqlDataAdapter da44 = new SqlDataAdapter(q44, con);
            DataTable dt44 = new DataTable();
            da44.Fill(dt44);
            if (dt44.Rows.Count > 0)
            {

                if (dt44.Rows[0]["aiid"].ToString() != "")
                {
                    aaid = Convert.ToInt32(dt44.Rows[0]["aiid"]);
                }



            }





            string final = "";
            string structure = "";
            string data = "";
            string query = "";
            if (Session["distid"] != null)
            {

                if (Session["distid"].ToString() != "")
                {

                    con.Open();

                    if (NestId != null)
                    {
                        query = "select a.aiid , c.AssetsType , d.AssetsCategory , a.tid , a.docid , a.Json from AssetInformation a  inner join TestatorDetails b on a.tid=b.tId inner join AssetsType c on a.atId = c.atId inner join AssetsCategory d on a.amId=d.amId inner join users e on e.uId=b.uId  where a.aiid = " + NestId + "   ";
                    }
                    else
                    {

                        if (Session["doctype"] != null)
                        {

                            if (Session["doctype"].ToString() == "Will")
                            {
                                query = "select a.aiid , c.AssetsType , d.AssetsCategory , a.tid , a.docid , a.Json from AssetInformation a  inner join TestatorDetails b on a.tid=b.tId inner join AssetsType c on a.atId = c.atId inner join AssetsCategory d on a.amId=d.amId inner join users e on e.uId=b.uId  where a.aiid = " + aaid + "  and a.doctype = 'Will'  ";
                            }

                            if (Session["doctype"].ToString() == "POA")
                            {
                                query = "select a.aiid , c.AssetsType , d.AssetsCategory , a.tid , a.docid , a.Json from AssetInformation a  inner join TestatorDetails b on a.tid=b.tId inner join AssetsType c on a.atId = c.atId inner join AssetsCategory d on a.amId=d.amId inner join users e on e.uId=b.uId  where a.aiid = " + aaid + " and a.doctype = 'POA'   ";
                            }


                            if (Session["doctype"].ToString() == "GiftDeeds")
                            {
                                query = "select a.aiid , c.AssetsType , d.AssetsCategory , a.tid , a.docid , a.Json from AssetInformation a  inner join TestatorDetails b on a.tid=b.tId inner join AssetsType c on a.atId = c.atId inner join AssetsCategory d on a.amId=d.amId inner join users e on e.uId=b.uId  where a.aiid = " + aaid + " and a.doctype = 'Giftdeeds'   ";
                            }





                        }
                        else
                        {
                            return RedirectToAction("LoginPageIndex", "LoginPage");
                        }



                    }



                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();


                    if (dt.Rows.Count > 0)
                    {
                        ViewBag.disablefield = "true";

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string getjson = dt.Rows[i]["Json"].ToString();

                            ViewBag.assettype = dt.Rows[0]["AssetsType"].ToString();
                            ViewBag.assetcategory = dt.Rows[0]["AssetsCategory"].ToString();


                            var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(getjson);
                            int count = 0;
                            int index = 0;
                            foreach (var kv in dict)
                            {
                                string removecomma = kv.Key;
                                string first = removecomma.Split('~')[0];
                                string second = removecomma.Split('~')[1];

                                if (kv.Value == "Yes")
                                {
                                    ViewBag.disablefield = "true";
                                    ViewBag.nomineedata = "true";
                                }


                                final = final + kv.Key + ":" + kv.Value;




                                if (kv.Value != "")
                                {
                                    index = count++;

                                    structure = structure + "<form>" +
                                 "<div class='col-sm-3'>" +
                                  "<div class='form-group'>" +
                                  "<input type='hidden' id='col" + index + "' value='" + first + "'  />" +
                                  "<input type='hidden' id='c" + index + "' value='" + second + "'  />" +
                                  "<label for='input-1'>" + second + "</label>" +
                                  "<input type='text' id=" + index + " name='inputtxt' class='form-control' style='width:150px;' value='" + kv.Value + "'   />" +
                                  "</div>" +
                                  "</div>" +
                                  "</form>";

                                }









                            }


                        }

                        ViewBag.assetdata = structure;
                    }
                    else
                    {
                        ViewBag.check = "Blank";
                    }





                }

            }
            else
            {
                RedirectToAction("LoginPageIndex", "LoginPage");
            }


















            //end

            string query22 = "";

            MainAssetsModel NM = new MainAssetsModel();
            con.Open();

            if (NestId != null)
            {
                query22 = "select * from Nominee where nId = " + NestId + " ";
            }
            else
            {
                query22 = "select * from Nominee where tId = " + Convert.ToInt32(Session["distid"]) + " ";
            }


            SqlDataAdapter da22 = new SqlDataAdapter(query22, con);
            DataTable dt22 = new DataTable();
            da22.Fill(dt22);
            con.Close();


            if (dt22.Rows.Count > 0)
            {

                
                for (int i = 0; i < dt22.Rows.Count; i++)
                {
                    ViewBag.hasnominee = "true";
                    NM.nId = Convert.ToInt32(dt22.Rows[i]["nId"]);
                    NM.nomFirst_Name = dt22.Rows[i]["First_Name"].ToString();
                    NM.nomLast_Name = dt22.Rows[i]["Last_Name"].ToString();
                    NM.nomMiddle_Name = dt22.Rows[i]["Middle_Name"].ToString();
                    NM.setdob = Convert.ToDateTime(dt22.Rows[0]["DOB"]).ToString("dd-MM-yyyy");

                    if (dt22.Rows[i]["Mobile"].ToString() != "None")
                    {
                        NM.nomMobile = dt22.Rows[i]["Mobile"].ToString();
                    }
                   

                   


                    NM.setrelation = dt22.Rows[i]["Relationship"].ToString();
                    NM.nomMarital_Status = dt22.Rows[i]["Marital_Status"].ToString();
                    NM.nomReligion = dt22.Rows[i]["Religion"].ToString();
                    NM.nomIdentity_Proof = dt22.Rows[i]["Identity_Proof"].ToString();
                    NM.nomIdentity_Proof_Value = dt22.Rows[i]["Identity_Proof_Value"].ToString();
                    NM.nomAlt_Identity_Proof = dt22.Rows[i]["Alt_Identity_Proof"].ToString();
                    NM.nomAlt_Identity_Proof_Value = dt22.Rows[i]["Alt_Identity_Proof_Value"].ToString();
                    NM.nomAddress1 = dt22.Rows[i]["Address1"].ToString();
                    NM.nomAddress2 = dt22.Rows[i]["Address2"].ToString();
                    NM.nomAddress3 = dt22.Rows[i]["Address3"].ToString();
                    NM.nomcitytext = dt22.Rows[i]["City"].ToString();
                    NM.nomstatetext = dt22.Rows[i]["State"].ToString();
                    NM.nomPin = dt22.Rows[i]["Pin"].ToString();
                    NM.nomcreatedBy = dt22.Rows[i]["createdBy"].ToString();

                    NM.country_txt = dt22.Rows[i]["Country"].ToString();



                    NM.nomDescription_of_Assets = dt22.Rows[i]["Description_of_Assets"].ToString();


                    if (dt22.Rows[i]["Description_of_Assets"].ToString() != "None")
                    {
                        NM.nomMobile = dt22.Rows[i]["Mobile"].ToString();
                    }


                }
            }









            return View("~/Views/AddMainAssets/AddMainAssetsPageContent.cshtml", NM);
        }



        public ActionResult jstruct()
        {



            return View("~/Views/AddMainAssets/AddMainAssetsPageContent.cshtml");
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



        public String BindAssetTypeDDL()
        {

            con.Open();
            string query = "select * from AssetsType";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            string data = "";

            if (dt.Rows.Count > 0)
            {


                for (int i = 0; i < dt.Rows.Count; i++)
                {




                    data = data + "<option value=" + dt.Rows[i]["atId"].ToString() + " >" + dt.Rows[i]["AssetsType"].ToString() + "</option> ";



                }



            }

            return data;

        }




        //public string GenerateColumns()
        //{
        //    string final = "";

        //    con.Open();
        //    SqlCommand cmd = new SqlCommand("SP_AssetColumns", con);
        //    SqlDataAdapter da = new SqlDataAdapter(cmd);
        //    DataTable dt = new DataTable();
        //    da.Fill(dt);
        //    con.Close();
        //    string data = "";

        //    if (dt.Rows.Count > 0)
        //    {







        //        data = data + "<option value=''>--Select--</option><option value='1' >" + dt.Rows[0]["DueDate"].ToString() + "</option>" +
        //        data + "<option value='2' >" + dt.Rows[0]["CurrentStatus"].ToString() + "</option>" +
        //        data + "<option value='3' >" + dt.Rows[0]["IssuedBy"].ToString() + "</option>" +
        //        data + "<option value='4' >" + dt.Rows[0]["Location"].ToString() + "</option>" +
        //        data + "<option value='5' >" + dt.Rows[0]["Identifier"].ToString() + "</option>" +
        //        data + "<option value='6' >" + dt.Rows[0]["assetsValue"].ToString() + "</option>" +
        //        data + "<option value='7' >" + dt.Rows[0]["CertificateNumber"].ToString() + "</option>" +
        //        data + "<option value='8' >" + dt.Rows[0]["PropertyDescription"].ToString() + "</option>" +
        //        data + "<option value='9' >" + dt.Rows[0]["Qty"].ToString() + "</option>" +
        //        data + "<option value='10' >" + dt.Rows[0]["Weight"].ToString() + "</option>" +
        //        data + "<option value='11' >" + dt.Rows[0]["OwnerShip"].ToString() + "</option>" +
        //        data + "<option value='12' >" + dt.Rows[0]["Remark"].ToString() + "</option>" +
        //        data + "<option value='13' >" + dt.Rows[0]["Nomination"].ToString() + "</option>" +
        //        data + "<option value='14' >" + dt.Rows[0]["NomineeDetails"].ToString() + "</option>" +
        //        data + "<option value='15' >" + dt.Rows[0]["Name"].ToString() + "</option>" +
        //        data + "<option value='16' >" + dt.Rows[0]["RegisteredAddress"].ToString() + "</option>" +
        //        data + "<option value='17' >" + dt.Rows[0]["PermanentAddress"].ToString() + "</option>" +
        //        data + "<option value='18' >" + dt.Rows[0]["Identity_proof"].ToString() + "</option>" +
        //        data + "<option value='19' >" + dt.Rows[0]["Identity_proof_value"].ToString() + "</option>" +
        //        data + "<option value='20' >" + dt.Rows[0]["Alt_Identity_proof"].ToString() + "</option>" +
        //        data + "<option value='21' >" + dt.Rows[0]["Alt_Identity_proof_value"].ToString() + "</option>" +
        //        data + "<option value='22' >" + dt.Rows[0]["Phone"].ToString() + "</option>" +
        //        data + "<option value='23' >" + dt.Rows[0]["Mobile"].ToString() + "</option>" +
        //        data + "<option value='24' >" + dt.Rows[0]["Amount"].ToString() + "</option>";







        //    }





        //    con.Open();
        //    string query = "select * from AssetsCategory";
        //    SqlDataAdapter da2 = new SqlDataAdapter(query, con);
        //    DataTable dt2 = new DataTable();
        //    da2.Fill(dt2);
        //    con.Close();
        //    string data2 = "";

        //    if (dt2.Rows.Count > 0)
        //    {







        //        for (int i = 0; i < dt2.Rows.Count; i++)
        //        {




        //            data2 = data2 + "<option value=" + dt2.Rows[i]["amId"].ToString() + " >" + dt2.Rows[i]["AssetsCategory"].ToString() + "</option>";



        //        }







        //    }







        //    final = "<div class='col-sm-4'><div class='form-group'><label for='input-1'>Select Asset</label><select id='ddlasset' class='form-control input-shadow'  onChange='getassetcat(this.value)'><option value='0' >--Select--</option>" + data2 + "</select></div></div>            <div class='col-sm-4'><div class='form-group'><label for='input-1'>Select Asset</label><select id='ddlasset' class='form-control input-shadow'  onChange='getassetcolumntext(this.options[this.selectedIndex].innerHTML)'><option value='0' >--Select--</option>" + data + "</select></div></div>   <div class='col-sm-4'><div class='form-group'><label for='input-1'>Values</label><input type='text' class='form-control input-shadow'  onchange=bar2(this.value)  placeholder='Enter Value For Your Asset'/></div></div>";




        //    return final;
        //}

        public String BindAssetCategoryDDL()
        {
            int response = Convert.ToInt32(Request["send"]);
            TempData["atid"] = response;
            con.Open();
            string query = "select * from AssetsCategory where atId = " + response + "";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            string data = "<option value='' >--Select--</option>";

            if (dt.Rows.Count > 0)
            {


                for (int i = 0; i < dt.Rows.Count; i++)
                {




                    data = data + "<option value=" + dt.Rows[i]["amId"].ToString() + " >" + dt.Rows[i]["AssetsCategory"].ToString() + "</option>";



                }




            }

            return data;

        }


        public string OnChangeDDLCat()
        {
            int response = Convert.ToInt32(Request["send"]);
            TempData["amid"] = response;
            con.Open();
            string query = "select * from AssetsInfo where amId = " + response + " ";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataTable dt3 = new DataTable();
            da.Fill(dt3);
            con.Close();
            string data = "";
            string column = "";
            string finalstruct = "";
            MainAssetsModel MAM = new MainAssetsModel();
            if (dt3.Rows.Count > 0)
            {
                for (int i = 0; i < dt3.Rows.Count; i++)
                {
                    if (dt3.Rows[i]["DueDate"].ToString() != "")
                    {
                        string testString = dt3.Rows[i]["DueDate"].ToString();
                        ArrayList result = new ArrayList(testString.Split('~'));

                        column = column + "<div class='col-sm-3'> <div class='form-group'><label for='input-1' >" + result[1].ToString() + "</label>";
                        MAM.dueDate = dt3.Rows[i]["DueDate"].ToString();

                    }

                    if (dt3.Rows[i]["DueDateControls"].ToString() != "")
                    {

                        column = column + "<input type=" + dt3.Rows[i]["DueDateControls"].ToString() + "   class='form-control input-shadow validate[required]' name='inputName' /></div></div>";
                        MAM.dueDateControls = dt3.Rows[i]["DueDateControls"].ToString();
                    }


                    //if (dt3.Rows[i]["CurrentStatus"].ToString() != "")
                    //{
                    //    string testString = dt3.Rows[i]["CurrentStatus"].ToString();
                    //    ArrayList result = new ArrayList(testString.Split('~'));

                    //    column = column + "<div class='col-sm-3'> <div class='form-group'><label for='input-1' >" + result[1].ToString() + "</label>";
                    //    MAM.CurrentStatus = dt3.Rows[i]["CurrentStatus"].ToString();
                    //}

                    //if (dt3.Rows[i]["CurrentStatusControls"].ToString() != "")
                    //{

                    //    if (dt3.Rows[i]["CurrentStatusControls"].ToString() == "RadioButton" && dt3.Rows[i]["CurrentStatusValues"].ToString() != "")
                    //    {

                    //        string testString = dt3.Rows[i]["CurrentStatusValues"].ToString();
                    //        ArrayList result = new ArrayList(testString.Split('/'));

                    //        column = column + " <br> <label class='radio-inline' >  <input type='radio'  id='ddlrole' name='Currentradio' value=" + result[0] + " checked> " + result[0] + "</label>  <label class='radio - inline'> <input type='radio' id='ddlrole' name='Currentradio'  value=" + result[1] + ">" + result[1] + "</label></div></div>";


                    //    }

                    //    MAM.CurrentStatusValues = dt3.Rows[i]["CurrentStatusValues"].ToString();

                    //}




                    if (dt3.Rows[i]["IssuedBy"].ToString() != "")
                    {
                        string testString = dt3.Rows[i]["IssuedBy"].ToString();
                        ArrayList result = new ArrayList(testString.Split('~'));

                        column = column + "<div class='col-sm-3'> <div class='form-group'><label for='input-1' >" + result[1].ToString() + "</label>";
                        MAM.IssuedBy = dt3.Rows[i]["IssuedBy"].ToString();
                    }

                    if (dt3.Rows[i]["IssuedByControls"].ToString() != "")
                    {

                        column = column + "<input type=" + dt3.Rows[i]["IssuedByControls"].ToString() + " class='form-control input-shadow validate[required]' name='inputName' /></div></div>";
                        MAM.column = dt3.Rows[i]["IssuedByControls"].ToString();

                    }



                    if (dt3.Rows[i]["Location"].ToString() != "")
                    {
                        string testString = dt3.Rows[i]["Location"].ToString();
                        ArrayList result = new ArrayList(testString.Split('~'));

                        column = column + "<div class='col-sm-3'> <div class='form-group'><label for='input-1' >" + result[1].ToString() + "</label>";
                        MAM.Location = dt3.Rows[i]["Location"].ToString();
                    }

                    if (dt3.Rows[i]["LocationControls"].ToString() != "")
                    {
                        if (dt3.Rows[i]["LocationControls"].ToString() == "TextArea")
                        {



                            column = column + "<textarea class='form-control input-shadow validate[required]' name='inputName'></textarea></div></div>";


                        }
                        else
                        {
                            column = column + "<input type=" + dt3.Rows[i]["LocationControls"].ToString() + " class='form-control input-shadow' /></div></div>";

                        }
                        MAM.LocationControls = dt3.Rows[i]["LocationControls"].ToString();
                    }



                    if (dt3.Rows[i]["Identifier"].ToString() != "")
                    {
                        string testString = dt3.Rows[i]["Identifier"].ToString();
                        ArrayList result = new ArrayList(testString.Split('~'));

                        column = column + "<div class='col-sm-3'> <div class='form-group'><label for='input-1' >" + result[1].ToString() + "</label>";
                        MAM.Identifier = dt3.Rows[i]["Identifier"].ToString();
                    }

                    if (dt3.Rows[i]["IdentifierControls"].ToString() != "")
                    {

                        column = column + "<input type=" + dt3.Rows[i]["IdentifierControls"].ToString() + " class='form-control input-shadow validate[required]' name='inputName'/></div></div>";
                        MAM.IdentifierControls = dt3.Rows[i]["IdentifierControls"].ToString();
                    }



                    if (dt3.Rows[i]["assetsValue"].ToString() != "")
                    {
                        string testString = dt3.Rows[i]["assetsValue"].ToString();
                        ArrayList result = new ArrayList(testString.Split('~'));
                        column = column + "<div class='col-sm-3'> <div class='form-group'><label for='input-1' >" + result[1].ToString() + "</label>";
                        MAM.assetsValue = dt3.Rows[i]["assetsValue"].ToString();
                    }
                    if (dt3.Rows[i]["assetsValueControls"].ToString() != "")
                    {

                        column = column + "<input type=" + dt3.Rows[i]["assetsValueControls"].ToString() + " class='form-control input-shadow validate[required]' name='inputName' /></div></div>";
                        MAM.assetsValueControls = dt3.Rows[i]["assetsValueControls"].ToString();
                    }

                    if (dt3.Rows[i]["CertificateNumber"].ToString() != "")
                    {
                        string testString = dt3.Rows[i]["CertificateNumber"].ToString();
                        ArrayList result = new ArrayList(testString.Split('~'));
                        column = column + "<div class='col-sm-3'> <div class='form-group'><label for='input-1' >" + result[1].ToString() + "</label>";
                        MAM.CertificateNumber = dt3.Rows[i]["CertificateNumber"].ToString();
                    }
                    if (dt3.Rows[i]["CertificateNumberControls"].ToString() != "")
                    {

                        column = column + "<input type=" + dt3.Rows[i]["CertificateNumberControls"].ToString() + " class='form-control input-shadow validate[required]' name='inputName' /></div></div>";
                        MAM.CertificateNumberControls = dt3.Rows[i]["CertificateNumberControls"].ToString();
                    }

                    if (dt3.Rows[i]["PropertyDescription"].ToString() != "")
                    {
                        string testString = dt3.Rows[i]["PropertyDescription"].ToString();
                        ArrayList result = new ArrayList(testString.Split('~'));
                        column = column + "<div class='col-sm-3'> <div class='form-group'><label for='input-1' >" + result[1].ToString() + "</label>";
                        MAM.PropertyDescription = dt3.Rows[i]["PropertyDescription"].ToString();
                    }

                    if (dt3.Rows[i]["PropertyDescriptionControls"].ToString() != "")
                    {

                        column = column + "<input type=" + dt3.Rows[i]["PropertyDescriptionControls"].ToString() + " class='form-control input-shadow validate[required]' name='inputName'/></div></div>";
                        MAM.PropertyDescriptionControls = dt3.Rows[i]["PropertyDescriptionControls"].ToString();

                    }

                    if (dt3.Rows[i]["Qty"].ToString() != "")
                    {
                        string testString = dt3.Rows[i]["Qty"].ToString();
                        ArrayList result = new ArrayList(testString.Split('~'));
                        column = column + "<div class='col-sm-3'> <div class='form-group'><label for='input-1' >" + result[1].ToString() + "</label>";
                        MAM.Qty = dt3.Rows[i]["Qty"].ToString();
                    }
                    if (dt3.Rows[i]["QtyControls"].ToString() != "")
                    {

                        column = column + "<input type=" + dt3.Rows[i]["QtyControls"].ToString() + " class='form-control input-shadow validate[required]' name='inputName' /></div></div>";
                        MAM.QtyControls = dt3.Rows[i]["QtyControls"].ToString();
                    }

                    if (dt3.Rows[i]["Weight"].ToString() != "")
                    {
                        string testString = dt3.Rows[i]["Weight"].ToString();
                        ArrayList result = new ArrayList(testString.Split('~'));
                        column = column + "<div class='col-sm-3'> <div class='form-group'><label for='input-1' >" + result[1].ToString() + "</label>";
                        MAM.Weight = dt3.Rows[i]["Weight"].ToString();
                    }
                    if (dt3.Rows[i]["WeightControls"].ToString() != "")
                    {

                        column = column + "<input type=" + dt3.Rows[i]["WeightControls"].ToString() + " class='form-control input-shadow validate[required]' name='inputName' /></div></div>";
                        MAM.WeightControls = dt3.Rows[i]["WeightControls"].ToString();
                    }

                    //if (dt3.Rows[i]["OwnerShip"].ToString() != "")
                    //{
                    //    string testString = dt3.Rows[i]["OwnerShip"].ToString();
                    //    ArrayList result = new ArrayList(testString.Split('~'));
                    //    column = column + "<div class='col-sm-3'> <div class='form-group'><label for='input-1' >" + result[1].ToString() + "</label>";
                    //    MAM.OwnerShip = dt3.Rows[i]["OwnerShip"].ToString();
                    //}
                    //if (dt3.Rows[i]["OwnerShipControls"].ToString() != "")
                    //{
                    //    if (dt3.Rows[i]["OwnerShipControls"].ToString() == "RadioButton" && dt3.Rows[i]["OwnerShipValues"].ToString() != "")
                    //    {

                    //        string testString = dt3.Rows[i]["OwnerShipValues"].ToString();
                    //        ArrayList result = new ArrayList(testString.Split('/'));

                    //        column = column + " <br> <label class='radio-inline' >  <input type='radio' id='ddlrole' name='ownershipRadio' value=" + result[0] + " checked> " + result[0] + "</label>  <label class='radio - inline'> <input type='radio' id='ddlrole' name='ownershipRadio' value=" + result[1] + ">" + result[1] + "</label></div></div>";


                    //    }

                    //    MAM.OwnerShipValues = dt3.Rows[i]["OwnerShipValues"].ToString();


                    //}

                    if (dt3.Rows[i]["Remark"].ToString() != "")
                    {
                        string testString = dt3.Rows[i]["Remark"].ToString();
                        ArrayList result = new ArrayList(testString.Split('~'));
                        column = column + "<div class='col-sm-3'> <div class='form-group'><label for='input-1' >" + result[1].ToString() + "</label>";
                        MAM.Remark = dt3.Rows[i]["Remark"].ToString();
                    }
                    if (dt3.Rows[i]["RemarkControls"].ToString() != "")
                    {
                        column = column + "<input type=" + dt3.Rows[i]["RemarkControls"].ToString() + " class='form-control input-shadow validate[required]' name='inputName' /></div></div>";
                        MAM.RemarkControls = dt3.Rows[i]["RemarkControls"].ToString();
                    }

                    if (dt3.Rows[i]["Nomination"].ToString() != "")
                    {
                        string testString = dt3.Rows[i]["Nomination"].ToString();
                        ArrayList result = new ArrayList(testString.Split('~'));
                        column = column + "<div class='col-sm-3'> <div class='form-group'><label for='input-1' >" + result[1].ToString() + "</label>";
                        MAM.Nomination = dt3.Rows[i]["Nomination"].ToString();
                    }


                    if (dt3.Rows[i]["NominationControls"].ToString() != "")
                    {
                        if (dt3.Rows[i]["NominationControls"].ToString() == "RadioButton" && dt3.Rows[i]["NominationValues"].ToString() != "")
                        {

                            string testString = dt3.Rows[i]["NominationValues"].ToString();
                            ArrayList result = new ArrayList(testString.Split('/'));

                            column = column + " <br> <label class='radio-inline' >  <input type='radio' id='ddlrole' name='nominationradio'  onchange='getstatusone(this.value)' value=" + result[0] + " > " + result[0] + "</label>  <label class='radio - inline'> <input type='radio' id='ddlrole'  onchange='getstatustwo(this.value)' name='nominationradio' value=" + result[1] + ">" + result[1] + "</label></div></div>";



                        }
                        else
                        {
                            column = column + "<input type=" + dt3.Rows[i]["NominationControls"].ToString() + " name='inputName' style='display:none' id='txtnominee' class='form-control input-shadow' /> </div></div>";
                        }

                        MAM.NominationControls = dt3.Rows[i]["NominationControls"].ToString();

                    }

                    if (dt3.Rows[i]["NomineeDetails"].ToString() != "")
                    {
                        string testString = dt3.Rows[i]["NomineeDetails"].ToString();
                        ArrayList result = new ArrayList(testString.Split('~'));
                        column = column + "<div class='col-sm-3'> <div class='form-group'><label style='display:none;' for='input-1' >" + result[1].ToString() + "</label>";
                        MAM.NomineeDetails = dt3.Rows[i]["NomineeDetails"].ToString();
                    }


                    if (dt3.Rows[i]["NomineeDetailsControls"].ToString() != "")
                    {
                        column = column + "<input type=" + dt3.Rows[i]["NomineeDetailsControls"].ToString() + " class='form-control input-shadow validate[required]' name='inputName' style='display:none' id='txtnominee' /> </div></div>";
                        MAM.NomineeDetailsControls = dt3.Rows[i]["NomineeDetailsControls"].ToString();
                    }

                    if (dt3.Rows[i]["Name"].ToString() != "")
                    {
                        string testString = dt3.Rows[i]["Name"].ToString();
                        ArrayList result = new ArrayList(testString.Split('~'));
                        column = column + "<div class='col-sm-3'> <div class='form-group'><label for='input-1' >" + result[1].ToString() + "</label>";
                        MAM.Name = dt3.Rows[i]["Name"].ToString();

                    }
                    if (dt3.Rows[i]["NameControls"].ToString() != "")
                    {
                        column = column + "<input type=" + dt3.Rows[i]["NameControls"].ToString() + " class='form-control input-shadow validate[required]' name='inputName' /></div></div>";
                        MAM.NameControls = dt3.Rows[i]["NameControls"].ToString();
                    }

                    if (dt3.Rows[i]["RegisteredAddress"].ToString() != "")
                    {
                        string testString = dt3.Rows[i]["RegisteredAddress"].ToString();
                        ArrayList result = new ArrayList(testString.Split('~'));
                        column = column + "<div class='col-sm-3'> <div class='form-group'><label for='input-1' >" + result[1].ToString() + "</label>";
                        MAM.RegisteredAddress = dt3.Rows[i]["RegisteredAddress"].ToString();
                    }
                    if (dt3.Rows[i]["RegisteredAddressControls"].ToString() != "")
                    {
                        column = column + "<input type=" + dt3.Rows[i]["RegisteredAddressControls"].ToString() + " class='form-control input-shadow validate[required]' name='inputName' /></div></div>";
                        MAM.RegisteredAddressControls = dt3.Rows[i]["RegisteredAddressControls"].ToString();
                    }

                    if (dt3.Rows[i]["PermanentAddress"].ToString() != "")
                    {
                        string testString = dt3.Rows[i]["PermanentAddress"].ToString();
                        ArrayList result = new ArrayList(testString.Split('~'));
                        column = column + "<div class='col-sm-3'> <div class='form-group'><label for='input-1' >" + result[1].ToString() + "</label>";
                        MAM.PermanentAddress = dt3.Rows[i]["PermanentAddress"].ToString();
                    }
                    if (dt3.Rows[i]["PermanentAddressControls"].ToString() != "")
                    {
                        column = column + "<input type=" + dt3.Rows[i]["PermanentAddressControls"].ToString() + " class='form-control input-shadow validate[required]' name='inputName' /></div></div>";
                        MAM.PermanentAddressControls = dt3.Rows[i]["PermanentAddressControls"].ToString();
                    }

                    if (dt3.Rows[i]["Identity_proof"].ToString() != "")
                    {
                        string testString = dt3.Rows[i]["Identity_proof"].ToString();
                        ArrayList result = new ArrayList(testString.Split('~'));
                        column = column + "<div class='col-sm-3'> <div class='form-group'><label for='input-1' >" + result[1].ToString() + "</label>";
                        MAM.Identity_proof = dt3.Rows[i]["Identity_proof"].ToString();
                    }
                    if (dt3.Rows[i]["Identity_proofControls"].ToString() != "")
                    {
                        column = column + "<input type=" + dt3.Rows[i]["Identity_proofControls"].ToString() + " class='form-control input-shadow validate[required]' name='inputName' /></div></div>";
                        MAM.Identity_proofControls = dt3.Rows[i]["Identity_proofControls"].ToString();
                    }

                    if (dt3.Rows[i]["Identity_proof_value"].ToString() != "")
                    {
                        string testString = dt3.Rows[i]["Identity_proof_value"].ToString();
                        ArrayList result = new ArrayList(testString.Split('~'));
                        column = column + "<div class='col-sm-3'> <div class='form-group'><label for='input-1' >" + result[1].ToString() + "</label>";
                        MAM.Identity_proof_value = dt3.Rows[i]["Identity_proof_value"].ToString();
                    }
                    if (dt3.Rows[i]["Identity_proof_valueControls"].ToString() != "")
                    {
                        column = column + "<input type=" + dt3.Rows[i]["Identity_proof_valueControls"].ToString() + " class='form-control input-shadow validate[required]' name='inputName' /></div></div>";
                        MAM.Identity_proof_valueControls = dt3.Rows[i]["Identity_proof_valueControls"].ToString();
                    }

                    if (dt3.Rows[i]["Alt_Identity_proof"].ToString() != "")
                    {
                        string testString = dt3.Rows[i]["Alt_Identity_proof"].ToString();
                        ArrayList result = new ArrayList(testString.Split('~'));
                        column = column + "<div class='col-sm-3'> <div class='form-group'><label for='input-1' >" + result[1].ToString() + "</label>";
                        MAM.Alt_Identity_proof = dt3.Rows[i]["Alt_Identity_proof"].ToString();
                    }
                    if (dt3.Rows[i]["Alt_Identity_proofControls"].ToString() != "")
                    {
                        column = column + "<input type=" + dt3.Rows[i]["Alt_Identity_proofControls"].ToString() + " class='form-control input-shadow validate[required]' name='inputName' /></div></div>";
                        MAM.Alt_Identity_proofControls = dt3.Rows[i]["Alt_Identity_proofControls"].ToString();
                    }

                    if (dt3.Rows[i]["Alt_Identity_proof_value"].ToString() != "")
                    {
                        string testString = dt3.Rows[i]["Alt_Identity_proof_value"].ToString();
                        ArrayList result = new ArrayList(testString.Split('~'));
                        column = column + "<div class='col-sm-3'> <div class='form-group'><label for='input-1' >" + result[1].ToString() + "</label>";
                        MAM.Alt_Identity_proof_value = dt3.Rows[i]["Alt_Identity_proof_value"].ToString();
                    }
                    if (dt3.Rows[i]["Alt_Identity_proof_valueControls"].ToString() != "")
                    {
                        column = column + "<input type=" + dt3.Rows[i]["Alt_Identity_proof_valueControls"].ToString() + " class='form-control input-shadow validate[required]' name='inputName' /></div></div>";
                        MAM.Alt_Identity_proof_valueControls = dt3.Rows[i]["Alt_Identity_proof_valueControls"].ToString();
                    }

                    if (dt3.Rows[i]["Phone"].ToString() != "")
                    {
                        string testString = dt3.Rows[i]["Phone"].ToString();
                        ArrayList result = new ArrayList(testString.Split('~'));
                        column = column + "<div class='col-sm-3'> <div class='form-group'><label for='input-1' >" + result[1].ToString() + "</label>";
                        MAM.Phone = dt3.Rows[i]["Phone"].ToString();
                    }
                    if (dt3.Rows[i]["PhoneControls"].ToString() != "")
                    {
                        column = column + "<input type=" + dt3.Rows[i]["PhoneControls"].ToString() + " class='form-control input-shadow validate[required]' name='inputName' /></div></div>";
                        MAM.PhoneControls = dt3.Rows[i]["PhoneControls"].ToString();
                    }

                    if (dt3.Rows[i]["Mobile"].ToString() != "")
                    {
                        string testString = dt3.Rows[i]["Mobile"].ToString();
                        ArrayList result = new ArrayList(testString.Split('~'));
                        column = column + "<div class='col-sm-3'> <div class='form-group'><label for='input-1' >" + result[1].ToString() + "</label>";
                        MAM.Mobile = dt3.Rows[i]["Mobile"].ToString();


                    }
                    if (dt3.Rows[i]["MobileControls"].ToString() != "")
                    {
                        column = column + "<input type=" + dt3.Rows[i]["MobileControls"].ToString() + " class='form-control input-shadow validate[required]' name='inputName' /></div></div>";
                        MAM.MobileControls = dt3.Rows[i]["MobileControls"].ToString();

                    }

                    if (dt3.Rows[i]["Amount"].ToString() != "")
                    {
                        string testString = dt3.Rows[i]["Amount"].ToString();
                        ArrayList result = new ArrayList(testString.Split('~'));
                        column = column + "<div class='col-sm-3'> <div class='form-group'><label for='input-1' >" + result[1].ToString() + "</label>";
                        MAM.Amount = dt3.Rows[i]["Amount"].ToString();

                    }
                    if (dt3.Rows[i]["AmountControls"].ToString() != "")
                    {
                        column = column + "<input type=" + dt3.Rows[i]["AmountControls"].ToString() + " class='form-control input-shadow validate[required]' name='inputName' /></div></div>";
                        MAM.AmountControls = dt3.Rows[i]["AmountControls"].ToString();
                    }

                    json = JsonConvert.SerializeObject(MAM);
                    TempData["mydata"] = json;
                    finalstruct = column;


                }


            }

            return finalstruct;

        }



        [HttpPost]
        public ActionResult InsertMainAsset(FormCollection collection)
        {
            ViewBag.collapse = "true";
            int nomineeid = 0;
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


            // string response = Convert.ToString(Request["send"]);
            MainAssetsModel MAM = new MainAssetsModel();
            MainAssetsModel JSON = new MainAssetsModel();
            List<MainAssetsModel> assetlist = new List<MainAssetsModel>();
            string data = TempData["mydata"].ToString();
            MainAssetsModel obj = JsonConvert.DeserializeObject<MainAssetsModel>(data);
            string value = Convert.ToString(collection["inputName"]);
            var radio1 = Convert.ToString(Request.Form["Currentradio"]);
            var radio2 = Convert.ToString(Request.Form["ownershipRadio"]);
            var radio3 = Convert.ToString(Request.Form["nominationradio"]);

            string nomineestate = "";

            if (radio3 == "Yes")
            {
                nomineestate = "true";
            }
            else
            {
                nomineestate = "false";
            }



            var tid = Convert.ToString(collection["ddlTid"]);
            //string ttid = tid.Replace("true", "");
            string ttid = Session["distid"].ToString();
            string c = "";

            if (obj.dueDate != null)
            {
                string testString = obj.dueDate;
                ArrayList result = new ArrayList(testString.Split('~'));

                c = c + obj.dueDate + ",";
            }


            if (obj.IssuedBy != null)
            {
                string testString = obj.IssuedBy;
                ArrayList result = new ArrayList(testString.Split('~'));
                c = c + obj.IssuedBy + ",";
            }
            if (obj.Location != null)
            {
                string testString = obj.Location;
                ArrayList result = new ArrayList(testString.Split('~'));
                c = c + obj.Location + ",";
            }
            if (obj.Identifier != null)
            {
                string testString = obj.Identifier;
                ArrayList result = new ArrayList(testString.Split('~'));
                c = c + obj.Identifier + ",";
            }
            if (obj.assetsValue != null)
            {
                string testString = obj.assetsValue;
                ArrayList result = new ArrayList(testString.Split('~'));
                c = c + obj.assetsValue + ",";
            }
            if (obj.CertificateNumber != null)
            {
                string testString = obj.CertificateNumber;
                ArrayList result = new ArrayList(testString.Split('~'));
                c = c + obj.CertificateNumber + ",";
            }
            if (obj.PropertyDescription != null)
            {
                string testString = obj.PropertyDescription;
                ArrayList result = new ArrayList(testString.Split('~'));
                c = c + obj.PropertyDescription + ",";
            }

            if (obj.Qty != null)
            {
                string testString = obj.Qty;
                ArrayList result = new ArrayList(testString.Split('~'));
                c = c + obj.Qty + ",";
            }

            if (obj.Weight != null)
            {
                string testString = obj.Weight;
                ArrayList result = new ArrayList(testString.Split('~'));
                c = c + obj.Weight + ",";
            }

            if (obj.Remark != null)
            {
                string testString = obj.Remark;
                ArrayList result = new ArrayList(testString.Split('~'));
                c = c + obj.Remark + ",";
            }

            if (obj.NomineeDetails != null)
            {
                string testString = obj.NomineeDetails;
                ArrayList result = new ArrayList(testString.Split('~'));
                c = c + obj.NomineeDetails + ",";
            }
            if (obj.Name != null)
            {
                string testString = obj.Name;
                ArrayList result = new ArrayList(testString.Split('~'));
                c = c + obj.Name + ",";
            }
            if (obj.RegisteredAddress != null)
            {
                string testString = obj.RegisteredAddress;
                ArrayList result = new ArrayList(testString.Split('~'));
                c = c + obj.RegisteredAddress + ",";
            }
            if (obj.PermanentAddress != null)
            {
                string testString = obj.PermanentAddress;
                ArrayList result = new ArrayList(testString.Split('~'));
                c = c + obj.PermanentAddress + ",";
            }
            if (obj.Identity_proof != null)
            {
                string testString = obj.Identity_proof;
                ArrayList result = new ArrayList(testString.Split('~'));
                c = c + obj.Identity_proof + ",";
            }
            if (obj.Identity_proof_value != null)
            {
                string testString = obj.Identity_proof_value;
                ArrayList result = new ArrayList(testString.Split('~'));
                c = c + obj.Identity_proof_value + ",";
            }
            if (obj.Alt_Identity_proof != null)
            {
                string testString = obj.Alt_Identity_proof;
                ArrayList result = new ArrayList(testString.Split('~'));
                c = c + obj.Alt_Identity_proof + ",";
            }
            if (obj.Alt_Identity_proof_value != null)
            {
                string testString = obj.Alt_Identity_proof_value;
                ArrayList result = new ArrayList(testString.Split('~'));
                c = c + obj.Alt_Identity_proof_value + ",";
            }
            if (obj.Phone != null)
            {
                string testString = obj.Phone;
                ArrayList result = new ArrayList(testString.Split('~'));
                c = c + obj.Phone + ",";
            }
            if (obj.Mobile != null)
            {
                string testString = obj.Mobile;
                ArrayList result = new ArrayList(testString.Split('~'));
                c = c + obj.Mobile + ",";
            }
            if (obj.Amount != null)
            {
                string testString = obj.Amount;
                ArrayList result = new ArrayList(testString.Split('~'));
                c = c + obj.Amount + ",";
            }







            var itemToAdd = new JObject();
            ArrayList Column = new ArrayList(c.Split(','));
            ArrayList Values = new ArrayList(value.Split(','));

            JArray jsonArray6 = new JArray();


            ArrayList NewList = new ArrayList();
            NewList.AddRange(Column);
            NewList.AddRange(Values);

            Dictionary<string, string> dd = new Dictionary<string, string>();
            for (int i = 0; i <= Column.Count; i++)
            {
                if (Column[i].ToString() != "")
                {
                    dd.Add(Column[i].ToString(), Values[i].ToString());
                }
                else
                {
                    break;
                }

            }

            if (obj.CurrentStatus != null)
            {


                dd.Add(obj.CurrentStatus, radio1.ToString());
            }


            try
            {
                if (obj.OwnerShip != null)
                {

                    dd.Add(obj.OwnerShip, radio2.ToString());


                }
            }
            catch (Exception)
            {

                
            }
            

          

            if (obj.Nomination != null)
            {

                dd.Add(obj.Nomination, radio3.ToString());
            }
            if (Session["aiid"] == null)
            {
                RedirectToAction("LoginPageIndex", "LoginPage");
            }

            //if (Session["tid"] != null)
            //{

            if (Session["doctype"].ToString() == "Will")
            {

                string json = JsonConvert.SerializeObject(dd);
                int amid = Convert.ToInt32(TempData["amid"]);



                //con.Open();
                //string qcheck = "SELECT count(*) FROM AssetInformation where amId = " + amid + "  ";
                //SqlCommand cmdchk = new SqlCommand(qcheck, con);
                //int count = (int)cmdchk.ExecuteScalar();
                //con.Close();

                //if (count > 0)
                //{
                //    Response.Write("<script>alert('Asset Category Already Mapped Please Select Another Asset')</script>");
                //}
                //else
                //{

                con.Open();
                string query = "insert into AssetInformation (atId,amId,Json,tid,uId,documentstatus,Remark,WillType) values (" + TempData["atid"] + " , " + amid + " ,'" + json + "' , " + ttid + " , " + Convert.ToInt32(Session["uuid"]) + " , 'incompleted' , 'Incompleted' , '" + Session["WillType"].ToString() + "' )";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                con.Close();

                con.Open();
                string query2 = "select top 1 * from AssetInformation order by aiid desc";
                SqlDataAdapter da = new SqlDataAdapter(query2, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {

                    Session["aiid"] = Convert.ToInt32(dt.Rows[0]["aiid"]);

                }
                con.Close();

                con.Open();
                string query22 = "update AssetInformation set doctype = 'Will' where aiid = " + Session["aiid"] + "  ";
                SqlCommand cmd22 = new SqlCommand(query22, con);
                cmd22.ExecuteNonQuery();
                con.Close();
                ViewBag.Message = "Verified";
                ViewBag.disablefield = "true";
                //}


                if (collection["nomineecheck"] == "true")
                {

                    con.Open();
                    SqlCommand cmd2 = new SqlCommand("SP_CRUDNominee", con);
                    cmd2.CommandType = CommandType.StoredProcedure;
                    cmd2.Parameters.AddWithValue("@action", "insert");
                    cmd2.Parameters.AddWithValue("@First_Name", collection["nomFirst_Name"]);
                    cmd2.Parameters.AddWithValue("@Last_Name", collection["nomLast_Name"]);

                    if (collection["nomMiddle_Name"] != "" || collection["nomMiddle_Name"] != null)
                    {
                        cmd2.Parameters.AddWithValue("@Middle_Name", collection["nomMiddle_Name"]);
                    }
                    else
                    {
                        cmd2.Parameters.AddWithValue("@Middle_Name", "None");
                    }

                 


                    string dateString = collection["nomDob"];
                    DateTime dd2 = Convert.ToDateTime(dateString,
                        System.Globalization.CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat);


                    cmd2.Parameters.AddWithValue("@DOB", dd2);



                    if (collection["nomMobile"] != null)
                    {

                        cmd2.Parameters.AddWithValue("@Mobile", collection["nomMobile"]);
                    }
                    else
                    {
                        cmd2.Parameters.AddWithValue("@Mobile", "None");
                    }

                    



                    cmd2.Parameters.AddWithValue("@Relationship", collection["nomRelationshipTxt"]);
                    cmd2.Parameters.AddWithValue("@Marital_Status", collection["nomMarital_Status"]);
                    cmd2.Parameters.AddWithValue("@Religion", collection["nomReligion"]);
                    cmd2.Parameters.AddWithValue("@Identity_Proof", collection["nomIdentity_Proof"]);
                    cmd2.Parameters.AddWithValue("@Identity_Proof_Value", collection["Identity_Proof_Value"]);
                    cmd2.Parameters.AddWithValue("@Country", collection["country_txt"]);

                    if (collection["nomAlt_Identity_Proof"] != "")
                    {
                        cmd2.Parameters.AddWithValue("@Alt_Identity_Proof", collection["nomAlt_Identity_Proof"]);
                    }
                    else
                    {
                        collection["nomAlt_Identity_Proof"] = "None";
                        cmd2.Parameters.AddWithValue("@Alt_Identity_Proof", collection["nomAlt_Identity_Proof"]);
                    }


                    if (collection["nomAlt_Identity_Proof_Value"] != "undefined")
                    {
                        cmd2.Parameters.AddWithValue("@Alt_Identity_Proof_Value", collection["nomAlt_Identity_Proof_Value"]);
                    }
                    else
                    {
                        collection["nomAlt_Identity_Proof_Value"] = "None";
                        cmd2.Parameters.AddWithValue("@Alt_Identity_Proof_Value", collection["nomAlt_Identity_Proof_Value"]);
                    }


                    cmd2.Parameters.AddWithValue("@Address1", collection["nomAddress1"]);
                    if (collection["nomAddress2"] != "")
                    {
                        cmd2.Parameters.AddWithValue("@Address2", collection["nomAddress2"]);
                    }
                    else
                    {
                        collection["nomAddress2"] = "None";
                        cmd2.Parameters.AddWithValue("@Address2", collection["nomAddress2"]);
                    }

                    if (collection["nomAddress3"] != "")
                    {
                        cmd2.Parameters.AddWithValue("@Address3", collection["nomAddress3"]);
                    }
                    else
                    {
                        collection["nomAddress3"] = "None";
                        cmd2.Parameters.AddWithValue("@Address3", collection["nomAddress3"]);
                    }


                    cmd2.Parameters.AddWithValue("@City", collection["nomcitytext"]);
                    cmd2.Parameters.AddWithValue("@State", collection["nomstatetext"]);
                    cmd2.Parameters.AddWithValue("@Pin", collection["nomPin"]);
                    //cmd.Parameters.AddWithValue("@aid", NM.nomaid);
                    //cmd.Parameters.AddWithValue("@tId", NM.nomddltid);
                    cmd2.Parameters.AddWithValue("@createdBy", Convert.ToInt32(Session["uuid"]));
                    cmd2.Parameters.AddWithValue("@documentId", "0");
                    cmd2.Parameters.AddWithValue("@Description_of_Assets", "None");
                    cmd2.Parameters.AddWithValue("@aid", Convert.ToInt32(dt.Rows[0]["aiid"]));
                    cmd2.Parameters.AddWithValue("@tId", Convert.ToInt32(Session["distid"]));

                    cmd2.ExecuteNonQuery();
                    con.Close();
                    ModelState.Clear();
                    TempData["Message"] = "true";
                    //}
                    //else
                    //{


                    //    ViewBag.Message = "link";

                    //}

                    ViewBag.disablefield = "true";


                }








            }





            if (Session["doctype"].ToString() == "POA")
            {
                string json = JsonConvert.SerializeObject(dd);
                int amid = Convert.ToInt32(TempData["amid"]);


                con.Open();
                string qcheck = "SELECT count(*) FROM AssetInformation where amId = " + amid + "  ";
                SqlCommand cmdchk = new SqlCommand(qcheck, con);
                int count = (int)cmdchk.ExecuteScalar();
                con.Close();

              
                    con.Open();
                    string query = "insert into AssetInformation (atId,amId,Json,tid,uId,Remark,WillType) values (" + TempData["atid"] + " , " + amid + " ,'" + json + "' , " + ttid + " , " + Convert.ToInt32(Session["uuid"]) + " , 'Incompleted' , '" + Session["WillType"].ToString() + "')";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    con.Close();

                    con.Open();
                    string query2 = "select top 1 * from AssetInformation order by aiid desc";
                    SqlDataAdapter da = new SqlDataAdapter(query2, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {

                        Session["aiid"] = Convert.ToInt32(dt.Rows[0]["aiid"]);

                    }
                    con.Close();

                    con.Open();
                    string query22 = "update AssetInformation set doctype = 'POA' where aiid = " + Session["aiid"] + "  ";
                    SqlCommand cmd22 = new SqlCommand(query22, con);
                    cmd22.ExecuteNonQuery();
                    con.Close();
                    ViewBag.Message = "Verified";



                    if (collection["nomineecheck"] == "true")
                    {

                        con.Open();
                        SqlCommand cmd2 = new SqlCommand("SP_CRUDNominee", con);
                        cmd2.CommandType = CommandType.StoredProcedure;
                        cmd2.Parameters.AddWithValue("@action", "insert");
                        cmd2.Parameters.AddWithValue("@First_Name", collection["nomFirst_Name"]);
                        cmd2.Parameters.AddWithValue("@Last_Name", collection["nomLast_Name"]);
                        cmd2.Parameters.AddWithValue("@Middle_Name", collection["nomMiddle_Name"]);
           
                        cmd2.Parameters.AddWithValue("@DOB", Convert.ToDateTime(collection["nomDob"]).ToString("yyyy-MM-dd"));
                        cmd2.Parameters.AddWithValue("@Mobile", collection["nomMobile"]);
                        cmd2.Parameters.AddWithValue("@Relationship", collection["nomRelationshipTxt"]);
                        cmd2.Parameters.AddWithValue("@Marital_Status", collection["nomMarital_Status"]);
                        cmd2.Parameters.AddWithValue("@Religion", collection["nomReligion"]);
                        cmd2.Parameters.AddWithValue("@Identity_Proof", collection["nomIdentity_Proof"]);
                        cmd2.Parameters.AddWithValue("@Identity_Proof_Value", collection["nomIdentity_Proof_Value"]);

                        if (collection["nomAlt_Identity_Proof"] != "")
                        {
                            cmd2.Parameters.AddWithValue("@Alt_Identity_Proof", collection["nomAlt_Identity_Proof"]);
                        }
                        else
                        {
                            collection["nomAlt_Identity_Proof"] = "None";
                            cmd2.Parameters.AddWithValue("@Alt_Identity_Proof", collection["nomAlt_Identity_Proof"]);
                        }


                        if (collection["nomAlt_Identity_Proof_Value"] != "undefined")
                        {
                            cmd2.Parameters.AddWithValue("@Alt_Identity_Proof_Value", collection["nomAlt_Identity_Proof_Value"]);
                        }
                        else
                        {
                            collection["nomAlt_Identity_Proof_Value"] = "None";
                            cmd2.Parameters.AddWithValue("@Alt_Identity_Proof_Value", collection["nomAlt_Identity_Proof_Value"]);
                        }


                        cmd2.Parameters.AddWithValue("@Address1", collection["nomAddress1"]);
                        if (collection["nomAddress2"] != "")
                        {
                            cmd2.Parameters.AddWithValue("@Address2", collection["nomAddress2"]);
                        }
                        else
                        {
                            collection["nomAddress2"] = "None";
                            cmd2.Parameters.AddWithValue("@Address2", collection["nomAddress2"]);
                        }

                        if (collection["nomAddress3"] != "")
                        {
                            cmd2.Parameters.AddWithValue("@Address3", collection["nomAddress3"]);
                        }
                        else
                        {
                            collection["nomAddress3"] = "None";
                            cmd2.Parameters.AddWithValue("@Address3", collection["nomAddress3"]);
                        }


                        cmd2.Parameters.AddWithValue("@City", collection["nomcitytext"]);
                        cmd2.Parameters.AddWithValue("@State", collection["nomstatetext"]);
                        cmd2.Parameters.AddWithValue("@Pin", collection["nomPin"]);
                        //cmd.Parameters.AddWithValue("@aid", NM.nomaid);
                        //cmd.Parameters.AddWithValue("@tId", NM.nomddltid);
                        cmd2.Parameters.AddWithValue("@createdBy", Convert.ToInt32(Session["uuid"]));
                        cmd2.Parameters.AddWithValue("@documentId", "0");
                        cmd2.Parameters.AddWithValue("@Description_of_Assets", collection["nomDescription_of_Assets"]);
                        cmd2.Parameters.AddWithValue("@aid", Convert.ToInt32(dt.Rows[0]["aiid"]));
                        cmd2.Parameters.AddWithValue("@tId", Convert.ToInt32(Session["distid"]));

                        cmd2.ExecuteNonQuery();
                        con.Close();
                        ModelState.Clear();
                        TempData["Message"] = "true";
                        //}
                        //else
                        //{


                        //    ViewBag.Message = "link";

                        //}

                        ViewBag.disablefield = "true";


                    }





                



            }





            if (Session["doctype"].ToString() == "GiftDeeds")
            {
                string json = JsonConvert.SerializeObject(dd);
                int amid = Convert.ToInt32(TempData["amid"]);


            
                    con.Open();
                    string query = "insert into AssetInformation (atId,amId,Json,tid,uId,Remark,WillType) values (" + TempData["atid"] + " , " + amid + " ,'" + json + "' , " + ttid + " , " + Convert.ToInt32(Session["uuid"]) + " , 'Incompleted' , '" + Session["WillType"].ToString() + "')";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    con.Close();

                    con.Open();
                    string query2 = "select top 1 * from AssetInformation order by aiid desc";
                    SqlDataAdapter da = new SqlDataAdapter(query2, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {

                        Session["aiid"] = Convert.ToInt32(dt.Rows[0]["aiid"]);

                    }
                    con.Close();



                    con.Open();
                    string query22 = "update AssetInformation set doctype = 'Giftdeeds' where aiid = " + Session["aiid"] + "  ";
                    SqlCommand cmd22 = new SqlCommand(query22, con);
                    cmd22.ExecuteNonQuery();
                    con.Close();



                    if (collection["nomineecheck"] == "true")
                    {

                        con.Open();
                        SqlCommand cmd2 = new SqlCommand("SP_CRUDNominee", con);
                        cmd2.CommandType = CommandType.StoredProcedure;
                        cmd2.Parameters.AddWithValue("@action", "insert");
                        cmd2.Parameters.AddWithValue("@First_Name", collection["nomFirst_Name"]);
                        cmd2.Parameters.AddWithValue("@Last_Name", collection["nomLast_Name"]);
                        cmd2.Parameters.AddWithValue("@Middle_Name", collection["nomMiddle_Name"]);
                        DateTime dat = DateTime.ParseExact(collection["nomDob"], "dd-MM-yyyy", CultureInfo.InvariantCulture);
                        cmd2.Parameters.AddWithValue("@DOB", dat);
                        cmd2.Parameters.AddWithValue("@Mobile", collection["nomMobile"]);
                        cmd2.Parameters.AddWithValue("@Relationship", collection["nomRelationshipTxt"]);
                        cmd2.Parameters.AddWithValue("@Marital_Status", collection["nomMarital_Status"]);
                        cmd2.Parameters.AddWithValue("@Religion", collection["nomReligion"]);
                        cmd2.Parameters.AddWithValue("@Identity_Proof", collection["nomIdentity_Proof"]);
                        cmd2.Parameters.AddWithValue("@Identity_Proof_Value", collection["nomIdentity_Proof_Value"]);

                        if (collection["nomAlt_Identity_Proof"] != "")
                        {
                            cmd2.Parameters.AddWithValue("@Alt_Identity_Proof", collection["nomAlt_Identity_Proof"]);
                        }
                        else
                        {
                            collection["nomAlt_Identity_Proof"] = "None";
                            cmd2.Parameters.AddWithValue("@Alt_Identity_Proof", collection["nomAlt_Identity_Proof"]);
                        }


                        if (collection["nomAlt_Identity_Proof_Value"] != "undefined")
                        {
                            cmd2.Parameters.AddWithValue("@Alt_Identity_Proof_Value", collection["nomAlt_Identity_Proof_Value"]);
                        }
                        else
                        {
                            collection["nomAlt_Identity_Proof_Value"] = "None";
                            cmd2.Parameters.AddWithValue("@Alt_Identity_Proof_Value", collection["nomAlt_Identity_Proof_Value"]);
                        }


                        cmd2.Parameters.AddWithValue("@Address1", collection["nomAddress1"]);
                        if (collection["nomAddress2"] != "")
                        {
                            cmd2.Parameters.AddWithValue("@Address2", collection["nomAddress2"]);
                        }
                        else
                        {
                            collection["nomAddress2"] = "None";
                            cmd2.Parameters.AddWithValue("@Address2", collection["nomAddress2"]);
                        }

                        if (collection["nomAddress3"] != "")
                        {
                            cmd2.Parameters.AddWithValue("@Address3", collection["nomAddress3"]);
                        }
                        else
                        {
                            collection["nomAddress3"] = "None";
                            cmd2.Parameters.AddWithValue("@Address3", collection["nomAddress3"]);
                        }


                        cmd2.Parameters.AddWithValue("@City", collection["nomcitytext"]);
                        cmd2.Parameters.AddWithValue("@State", collection["nomstatetext"]);
                        cmd2.Parameters.AddWithValue("@Pin", collection["nomPin"]);
                        //cmd.Parameters.AddWithValue("@aid", NM.nomaid);
                        //cmd.Parameters.AddWithValue("@tId", NM.nomddltid);
                        cmd2.Parameters.AddWithValue("@createdBy", Convert.ToInt32(Session["uuid"]));
                        cmd2.Parameters.AddWithValue("@documentId", "0");
                        cmd2.Parameters.AddWithValue("@Description_of_Assets", collection["nomDescription_of_Assets"]);
                        cmd2.Parameters.AddWithValue("@aid", Convert.ToInt32(dt.Rows[0]["aiid"]));
                        cmd2.Parameters.AddWithValue("@tId", Convert.ToInt32(Session["distid"]));

                        cmd2.ExecuteNonQuery();
                        con.Close();
                        ModelState.Clear();
                        TempData["Message"] = "true";
                        //}
                        //else
                        //{


                        //    ViewBag.Message = "link";

                        //}

                        ViewBag.disablefield = "true";



                        con.Open();
                        string querycheck = "select top 1 nId from Nominee order by nId desc";
                        SqlDataAdapter dachk = new SqlDataAdapter(querycheck, con);
                        DataTable dtchkr = new DataTable();
                        dachk.Fill(dtchkr);
                        if (dtchkr.Rows.Count > 0)
                        {
                            nomineeid = Convert.ToInt32(dtchkr.Rows[0]["nId"]);

                        }
                        con.Close();




                    }



            



            }






            con.Close();
            ModelState.Clear();

            TempData["Message"] = "true";
            //}
            //else
            //{
            //    ViewBag.Message = "link";

            //}






            return RedirectToAction("AddMainAssetsIndex", "AddMainAssets");
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
                    string query2 = "select * from AssetInformation where tId =  " + Convert.ToInt32(dt.Rows[0]["tId"]) + " ";
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
                if (Request["send"] != "")
                {
                    // check for data exists or not for testato family
                    if (value != null)
                    {
                        int Response = Convert.ToInt32(Request["send"]);
                        con.Open();
                        string query1 = "select a.aiid , a.atId , a.amId , a.tid , a.docid , a.Json from AssetInformation a inner join TestatorDetails b on a.tid=b.tId where b.tId = " + value + "   ";
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
            }







            return check;

        }









        public string UpdateAssetInformation(string column1, string value2)
        {
            //MainAssetsModel MAM = new MainAssetsModel();
            //string response = Request["send"];


            //string jsondata = "";



            ArrayList Column = new ArrayList(column1.Split(','));
            ArrayList Values = new ArrayList(value2.Split(','));




            ArrayList NewList = new ArrayList();
            NewList.AddRange(Column);
            NewList.AddRange(Values);

            Dictionary<string, string> dd = new Dictionary<string, string>();
            for (int i = 0; i <= Column.Count - 1; i++)
            {
                string abc = Column[i].ToString();
                if (abc.Contains("undefined~undefined"))
                {
                    continue;
                }


                if (Column[i].ToString() != "" || Column[i].ToString() != "undefined~undefined" || Column[i].ToString() != "undefined" || Column[i].ToString() != null)
                {
                    dd.Add(Column[i].ToString(), Values[i].ToString());
                }
                else
                {
                    break;
                }

            }



            string jdata = JsonConvert.SerializeObject(dd);


            string assetid = "";

            if (TempData["NestedId"] != null)
            {
                assetid = TempData["NestedId"].ToString();
            }

            //string j = "{" + filterdata + "}";


            con.Open();
            string queryup = "update AssetInformation set Json = '" + jdata + "' where  aiid = " + assetid + "";
            SqlCommand cmd = new SqlCommand(queryup, con);
            cmd.ExecuteNonQuery();




            //updatenominee();









            con.Close();















            return "";
        }






        //public static void updatenominee()
        //{
        //    /// update nominee data 
        //    FormCollection form;

        //    SqlCommand cmd = new SqlCommand("SP_CRUDNominee", con);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.AddWithValue("@action", "update");
        //    cmd.Parameters.AddWithValue("@nId", NM.nId);
        //    cmd.Parameters.AddWithValue("@First_Name", NM.First_Name);
        //    cmd.Parameters.AddWithValue("@Last_Name", NM.Last_Name);
        //    cmd.Parameters.AddWithValue("@Middle_Name", NM.Middle_Name);
        //    string dateString = NM.Dob;
        //    DateTime dd = Convert.ToDateTime(dateString,
        //        System.Globalization.CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat);
        //    //DateTime dat = DateTime.ParseExact(Convert.ToDateTime(NM.Dob), "dd-MM-yyyy", CultureInfo.InvariantCulture);
        //    cmd.Parameters.AddWithValue("@DOB", dd);
        //    cmd.Parameters.AddWithValue("@Mobile", NM.Mobile);
        //    cmd.Parameters.AddWithValue("@Relationship", NM.RelationshipTxt);
        //    cmd.Parameters.AddWithValue("@Marital_Status", NM.Marital_Status);
        //    cmd.Parameters.AddWithValue("@Religion", NM.Religion);
        //    cmd.Parameters.AddWithValue("@Identity_Proof", NM.Identity_Proof);
        //    cmd.Parameters.AddWithValue("@Identity_Proof_Value", NM.Identity_Proof_Value);
        //    cmd.Parameters.AddWithValue("@Alt_Identity_Proof", NM.Alt_Identity_Proof);
        //    cmd.Parameters.AddWithValue("@Alt_Identity_Proof_Value", NM.Alt_Identity_Proof_Value);
        //    cmd.Parameters.AddWithValue("@Address1", NM.Address1);
        //    cmd.Parameters.AddWithValue("@Address2", NM.Address2);
        //    cmd.Parameters.AddWithValue("@Address3", NM.Address3);
        //    cmd.Parameters.AddWithValue("@City", NM.citytext);
        //    cmd.Parameters.AddWithValue("@State", NM.statetext);
        //    cmd.Parameters.AddWithValue("@Pin", NM.Pin);
        //    cmd.Parameters.AddWithValue("@aid", NM.aid);
        //    cmd.Parameters.AddWithValue("@tId", NM.ddltid);
        //    cmd.Parameters.AddWithValue("@createdBy", "None");
        //    cmd.Parameters.AddWithValue("@documentId", NM.documentId);
        //    cmd.Parameters.AddWithValue("@Description_of_Assets", NM.Description_of_Assets);
        //    cmd.ExecuteNonQuery();






        //    /// end
        //}









        public String BindStateDDL()
        {

            string response = Request["send"].ToString();
            con.Open();
            string query = "select distinct * from tbl_state where country_id = " + response + " order by statename asc  ";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            string data = "<option value=''>--Select State--</option>";

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
            string data = "<option value=''>--Select City--</option>";

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







        public ActionResult InsertNomineeData()
        {

            string getdata = Request["send"];
            var txtfirstname = getdata.Split('~')[0];
            var txtmiddlename = getdata.Split('~')[1];
            var txtlastname = getdata.Split('~')[2];
            var DOB = getdata.Split('~')[3];
            var txtmobile = getdata.Split('~')[4];
            var RelationshipTxt = getdata.Split('~')[5];
            var nommaritalstatus = getdata.Split('~')[6];
            var nomreligion = getdata.Split('~')[7];
            var statetext = getdata.Split('~')[8];
            var citytext = getdata.Split('~')[9];
            var txtpin = getdata.Split('~')[10];
            var txtaddress1 = getdata.Split('~')[11];
            var txtaddress2 = getdata.Split('~')[12];
            var txtaddress3 = getdata.Split('~')[13];
            var identityproof = getdata.Split('~')[14];
            var txtidentityproof = getdata.Split('~')[15];
            var Alt_Identity_Proof = getdata.Split('~')[16];
            var txtIdentityProofValue = getdata.Split('~')[17];
            var txtdescriptionofasset = getdata.Split('~')[18];




            return RedirectToAction("AddNomineeIndex", "AddNominee");
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