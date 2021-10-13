using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HealthCareWebsite.Models
{
    public class MedicinesModel
    {
        [Key]
        public int MId { get; set; }
        public string MName { get; set; }
        public decimal MPrice { get; set; }
        public bool IsAvailable { get; set; }

        public string MImage { get; set; }
        public int CategoryId { get; set; }

        [NotMapped]
        public string CName { get; set; }


    }
}
