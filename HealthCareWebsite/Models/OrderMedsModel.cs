using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HealthCareWebsite.Models
{
    public class OrderMedsModel
    {
        [Key]
        public int OrderID { get; set; }
        public int Quantity { get; set; }
        public decimal UniPrice { get; set; }
        public int MId { get; set; }
        public string UserID { get; set; }
    }
    public class Corder {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal UniPrice { get; set; }
        public string Image { get; set; }
    }

}

