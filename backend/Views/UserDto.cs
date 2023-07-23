using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Views
{
    public class UserDto
    {
        [Required]
        public required string Username { get; set; }
        [Required]
        public required string Token { get; set; }
    }
}