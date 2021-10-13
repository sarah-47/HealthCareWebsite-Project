using HealthCareWebsite.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthCareWebsite.Data
{
    public class DatabaseContext:DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        public DbSet<CategoriesModel> Categories { get; set; }
        public DbSet<MedicinesModel> Medicines { get; set; }
        public DbSet<OrderMedsModel> OrderMeds { get; set; }



    }
}

