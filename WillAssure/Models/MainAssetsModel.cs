﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WillAssure.Models
{
    public class MainAssetsModel
    {

        public string nomineecheck { get; set; }
        public string setdob { get; set; }
        public string setrelation { get; set; }
        public string inputtxt { get; set; }
        public int assettypeid { get; set; }
        public string assettypetext { get; set; }

        public string assetcattext { get; set; }

        public int aiid { get; set; }
        public string dueDate { get; set; }
        public string dueDateControls { get; set; }
        public string DueDateValues { get; set; }

        public string CurrentStatus { get; set; }
        public string CurrentStatusControls { get; set; }
        public string CurrentStatusValues { get; set; }

        public string IssuedBy { get; set; }
        public string IssuedByControls { get; set; }
        public string IssuedByValues { get; set; }

        public string Location { get; set; }
        public string LocationControls { get; set; }
        public string LocationValues { get; set; }

        public string assetsValue { get; set; }
        public string assetsValueControls { get; set; }
        public string assetsValueValues { get; set; }

        public string CertificateNumber { get; set; }
        public string CertificateNumberControls { get; set; }
        public string CertificateNumberValues { get; set; }

        public string PropertyDescription { get; set; }
        public string PropertyDescriptionControls { get; set; }
        public string PropertyDescriptionValues { get; set; }

        public string Qty { get; set; }
        public string QtyControls { get; set; }
        public string QtyValues { get; set; }

        public string Weight { get; set; }
        public string WeightControls { get; set; }
        public string WeightValues { get; set; }

        public string OwnerShip { get; set; }
        public string OwnerShipControls { get; set; }
        public string OwnerShipValues { get; set; }

        public int? nId { get; set; }
        public int? nIdControls { get; set; }
        public int? nIdValues { get; set; }

        public string Remark { get; set; }
        public string RemarkControls { get; set; }
        public string RemarkValues { get; set; }

        public string Nomination { get; set; }
        public string NominationControls { get; set; }
        public string NominationValues { get; set; }

        public string NomineeDetails { get; set; }
        public string NomineeDetailsControls { get; set; }
        public string NomineeDetailsValues { get; set; }

        public string Name { get; set; }
        public string NameControls { get; set; }
        public string NameValues { get; set; }

        public string RegisteredAddress { get; set; }
        public string RegisteredAddressControls { get; set; }
        public string RegisteredAddressValues { get; set; }

        public string PermanentAddress { get; set; }
        public string PermanentAddressControls { get; set; }
        public string PermanentAddressValues { get; set; }

        public string Identity_proof { get; set; }
        public string Identity_proofControls { get; set; }
        public string Identity_proofValues { get; set; }

        public string Identity_proof_value { get; set; }
        public string Identity_proof_valueControls { get; set; }
        public string Identity_proof_valueValues { get; set; }


        public string Alt_Identity_proof { get; set; }
        public string Alt_Identity_proofControls { get; set; }
        public string Alt_Identity_proofValues { get; set; }

        public string Alt_Identity_proof_value { get; set; }
        public string Alt_Identity_proof_valueControls { get; set; }
        public string Alt_Identity_proof_valueValues { get; set; }

        public string Phone { get; set; }
        public string PhoneControls { get; set; }
        public string PhoneValues { get; set; }

        public string Mobile { get; set; }
        public string MobileControls { get; set; }
        public string MobileValues { get; set; }

        public string Amount { get; set; }
        public string AmountControls { get; set; }
        public string AmountValues { get; set; }


        public string Identifier { get; set; }
        public string IdentifierControls { get; set; }
        public string IdentifierValues { get; set; }


        public string assettxt { get; set; }
        public string assetid { get; set; }

        public string column { get; set; }
        public string text { get; set; }


        public string assetcattxt { get; set; }
        public int? assetcatid { get; set; }


        public string lbl1 { get; set; }
        public string lbl2 { get; set; }
        public string lbl3 { get; set; }
        public string lbl4 { get; set; }
        public string lbl5 { get; set; }


        public string COLUMN { get; set; }
        public string VALUE { get; set; }


        public string DescriptionTypeofItem {get; set;}
        public string NumberofItems { get; set; }


        public string Address { get; set; }
        public string CTSNo { get; set; }


        public int? inputnumberofitems  { get; set; }
        public int? inputweight { get; set; }



        public string ddltestatorname { get; set; }
        public int ddltid { get; set; }



        public int Beneficiary_Asset_ID { get; set; }
        public string AssetsType { get; set; }
        public string AssetsCategory { get; set; }
        public string SchemeName { get; set; }
        public string InstrumentName { get; set; }
        public string Proportion { get; set; }



        public int Beneficiaryid { get; set; }
        public string Beneficiarytxt { get; set; }


        public int InstrumentId { get; set; }
        public string Instrumenttxt { get; set; }

        public string data0 { get; set; }
        public string data1 { get; set; }
        public string data2 { get; set; }
        public string data3 { get; set; }
        public string data4 { get; set; }
        public string data5 { get; set; }
        public string data6 { get; set; }
        public string data7 { get; set; }
        public string data8 { get; set; }
        public string data9 { get; set; }
        public string data10 { get; set; }




        /// nominee details
        /// 


        public string nomFirst_Name { get; set; }
        public string nomLast_Name { get; set; }
        public string nomMiddle_Name { get; set; }
        public string nomDob { get; set; }
        public string nomMobile { get; set; }
        public string nomRelationship { get; set; }
        public int nomRelationshipId { get; set; }
        public string nomRelationshipTxt { get; set; }
        public string nomMarital_Status { get; set; }
        public string nomReligion { get; set; }
        public string nomIdentity_Proof { get; set; }
        public string nomIdentity_Proof_Value { get; set; }
        public string nomAlt_Identity_Proof { get; set; }
        public string nomAlt_Identity_Proof_Value { get; set; }
        public string nomAddress1 { get; set; }
        public string nomAddress2 { get; set; }
        public string nomAddress3 { get; set; }
        public string nomCity { get; set; }
        public int nomcityid { get; set; }
        public string nomcitytext { get; set; }
        public string nomState { get; set; }
        public int nomstateid { get; set; }
        public string nomstatetext { get; set; }

        public string nomPin { get; set; }

        public string nomcreatedBy { get; set; }
        public int nomdocumentId { get; set; }
        public string nomDescription_of_Assets { get; set; }

        public string country_txt { get; set; }
        public int country_id { get; set; }




    }
}