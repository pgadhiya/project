﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class technotaskEntities : DbContext
    {
        public technotaskEntities()
            : base("name=technotaskEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<tbladmin> tbladmins { get; set; }
        public virtual DbSet<tblbatch> tblbatches { get; set; }
        public virtual DbSet<tblbatchdetail> tblbatchdetails { get; set; }
        public virtual DbSet<tblcity> tblcities { get; set; }
        public virtual DbSet<tblfaculty> tblfaculties { get; set; }
        public virtual DbSet<tblstate> tblstates { get; set; }
        public virtual DbSet<tblstudent> tblstudents { get; set; }
        public virtual DbSet<tbltask> tbltasks { get; set; }
        public virtual DbSet<tbltechnology> tbltechnologies { get; set; }
    }
}
