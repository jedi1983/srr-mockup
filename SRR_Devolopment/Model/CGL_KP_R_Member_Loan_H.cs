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
    using System.Collections.Generic;
    
    public partial class CGL_KP_R_Member_Loan_H
    {
        public int Member_Loan_Id { get; set; }
        public int Member_Id { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public decimal Interest_Rate { get; set; }
        public decimal Loan_Amount { get; set; }
        public decimal Interest_Amount { get; set; }
        public int Term_of_Loan { get; set; }
        public decimal Remaining_Loan_Amount { get; set; }
        public decimal Remaining_Interest_Amount { get; set; }
        public int Remaining_Term_of_Loan { get; set; }
        public bool Is_Deleted { get; set; }
        public string Created_By { get; set; }
        public System.DateTime Created_Date { get; set; }
        public string Modified_By { get; set; }
        public Nullable<System.DateTime> Modified_Date { get; set; }
        public string Deleted_By { get; set; }
        public Nullable<System.DateTime> Deleted_Date { get; set; }
    
        public virtual CGL_KP_M_Member_H CGL_KP_M_Member_H { get; set; }
    }
}
