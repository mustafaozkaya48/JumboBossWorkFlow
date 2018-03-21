using _DataBaseLayer.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace _DataBaseLayer
{
    public class DatabaseContext : DbContext
    { 
        public DbSet<User> User { get; set; }

    }

    //Veritabanı yoksa oluştur//
    public class MyInitilizer : CreateDatabaseIfNotExists<DatabaseContext>
    {

        protected override void Seed(DatabaseContext context)
        {

        }
    }
}