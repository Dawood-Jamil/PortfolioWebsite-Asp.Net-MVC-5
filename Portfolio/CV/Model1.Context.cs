﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CV
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ResumeEntities : DbContext
    {
        public ResumeEntities()
            : base("name=ResumeEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Skill> Skills { get; set; }
        public virtual DbSet<Language> Languages { get; set; }
        public virtual DbSet<Admin_Users> Admin_Users { get; set; }
        public virtual DbSet<Personal_info> Personal_info { get; set; }
        public virtual DbSet<Education> Educations { get; set; }
        public virtual DbSet<Experience> Experiences { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<SocialLink> SocialLinks { get; set; }
        public virtual DbSet<Portfolio> Portfolios { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<WebRole> WebRoles { get; set; }
    }
}
