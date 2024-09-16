using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace LearnEF.Model
{
    public class User: IdentityUser
    {
        [MaxLength(5, ErrorMessage = "Only 5 character are alowwed")]
        public string? Initials { get; set; }
    }
}