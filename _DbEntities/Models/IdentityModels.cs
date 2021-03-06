﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace _DbEntities.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public UserInfo userInfo { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {

        }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public virtual DbSet<Logs> Logs { get; set; }
        public virtual DbSet<WorkAddition> WorkAdditions { get; set; }
        public virtual DbSet<WorkCommented> WorkCommenteds { get; set; }
        public virtual DbSet<Workflow> Workflows { get; set; }
        public virtual DbSet<Work> Works { get; set; }
        public virtual DbSet<EmployeeInvite> EmployeeInvites { get; set; }
        public virtual DbSet<Likes> Likes { get; set; }
    }

    public class UserInfo
    {
        [Required(), StringLength(25), Column(TypeName = "VARCHAR"), DisplayName("Ad")]
        public string Name { get; set; }
        [Required(), StringLength(25), Column(TypeName = "VARCHAR"), DisplayName("Soyad")]
        public string SurName { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(350)]
        public string About { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(350)]
        public string CompanyInfo { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(350)]
        public string Address { get; set; }

        [Column(TypeName = "VARCHAR")]
        public string ProfilPicture { get; set; }
        [Column(TypeName = "VARCHAR"), StringLength(25)]
        public string Department { get; set; }
        public DateTime CreatedOn { get; set; }
        public string DependencyId { get; set; }
    }


}