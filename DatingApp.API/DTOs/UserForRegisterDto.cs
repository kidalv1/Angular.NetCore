using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.API.DTOs
{
    public class UserForRegisterDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [StringLength(8, MinimumLength = 4, ErrorMessage ="Wachtwoord moet tussen 4 en 8 tekens zijn")]
        public string Password { get; set; }
    }
}
