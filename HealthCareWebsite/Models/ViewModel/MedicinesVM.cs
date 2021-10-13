using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HealthCareWebsite.Models.ViewModel
{

    public class MedicinesVM
    {
        [Key]
        [Required]
        public int MId { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "The Name Should be less than 50 characters")]
        public string MName { get; set; }
        [Required]
        public decimal MPrice { get; set; }
        public bool IsAvailable { get; set; }
        [Required]
        public IFormFile MImage { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }

        public int CId { get; set; }
    }
}
