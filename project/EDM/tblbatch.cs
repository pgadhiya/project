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
    
    public partial class tblbatch
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblbatch()
        {
            this.tblbatchdetails = new HashSet<tblbatchdetail>();
        }
    
        public int B_ID { get; set; }
        public string B_Name { get; set; }
        public Nullable<int> F_ID { get; set; }
        public Nullable<System.DateTime> B_ST_DATE { get; set; }
        public Nullable<System.DateTime> B_CR_DATE { get; set; }
        public string Status { get; set; }
    
        public virtual tblfaculty tblfaculty { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblbatchdetail> tblbatchdetails { get; set; }
    }
}