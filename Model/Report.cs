//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EmplManagementSystem.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Report
    {
        public int reportId { get; set; }
        public System.DateTime ReportDate { get; set; }
        public string ReportName { get; set; }
        public Nullable<int> empId { get; set; }
        public Nullable<int> taskId { get; set; }
        public bool isActive { get; set; }
    
        public virtual Employee Employee { get; set; }
        public virtual Task Task { get; set; }
    }
}
