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
    
    public partial class tblcity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblcity()
        {
            this.tblstudents = new HashSet<tblstudent>();
        }
    
        public int CT_ID { get; set; }
        public string Cityname { get; set; }
        public Nullable<int> ST_ID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblstudent> tblstudents { get; set; }
    }
}
