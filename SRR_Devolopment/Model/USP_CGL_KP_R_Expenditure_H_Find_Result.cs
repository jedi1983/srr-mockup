//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SRR_Devolopment.Model
{
    using System;
    
    public partial class USP_CGL_KP_R_Expenditure_H_Find_Result
    {
        public int Expenditure_Id { get; set; }
        public string Expenditure_No { get; set; }
        public System.DateTime Expenditure_Date { get; set; }
        public int Expenditure_Type_Id { get; set; }
        public Nullable<int> Member_Id { get; set; }
        public decimal Expenditure_Amount { get; set; }
        public string Description { get; set; }
        public bool Is_Approved { get; set; }
        public string Approved_By { get; set; }
        public Nullable<System.DateTime> Approved_Date { get; set; }
        public bool Is_Deleted { get; set; }
        public string Legal_Entity_Name { get; set; }
        public string Employee_No { get; set; }
        public string Name { get; set; }
        public Nullable<int> Member_Loan_Id { get; set; }
    }
}
