//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace project.EDM
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbltask
    {
        public int Task_ID { get; set; }
        public string Task_Desc { get; set; }
        public Nullable<int> F_ID { get; set; }
        public Nullable<int> AD_ID { get; set; }
        public Nullable<System.DateTime> Cr_Date { get; set; }
        public Nullable<System.DateTime> Act_Date { get; set; }
        public string Status { get; set; }
    
        public virtual tbladmin tbladmin { get; set; }
        public virtual tblfaculty tblfaculty { get; set; }
    }
}
