﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace biggschool.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
       

        public DbSet<Course> Courses { get; set; }
        public DbSet<Category> Categories { get; set; }

        public ApplicationDbContext()
            : base("biggschoolConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Attendance>()
            .HasRequired(a => a.Course)
             .WithMany()
            .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);


        }


    }

}