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
    
    public partial class CGL_KP_R_Cooperative_Balance_H
    {
        public int Legal_Entity_Id { get; set; }
        public decimal Cooperative_Balance { get; set; }
        public bool Is_Deleted { get; set; }
        public string Created_By { get; set; }
        public System.DateTime Created_Date { get; set; }
        public string Modified_By { get; set; }
        public Nullable<System.DateTime> Modified_Date { get; set; }
        public string Deleted_By { get; set; }
        public Nullable<System.DateTime> Deleted_Date { get; set; }
    
        public virtual CGL_KP_M_Legal_Entity_H CGL_KP_M_Legal_Entity_H { get; set; }
    }
}
