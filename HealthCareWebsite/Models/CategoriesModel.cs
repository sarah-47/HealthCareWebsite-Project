using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HealthCareWebsite.Models
{
    [Table("Categories")]
    public class CategoriesModel
    {


        [Key]
        public int CId { get; set; }
        public string CName { get; set; }
        //public string CImage { get; set; }

    }
}
