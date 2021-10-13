using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HealthCareWebsite.Models
{
    public class UsersInfo
    {
    
            public string Id { get; set; }
            public string UserName { get; set; }
            public string Email { get; set; }
        
    }
}


