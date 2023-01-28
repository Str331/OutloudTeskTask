using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outloud.Domain.ViewModel.Login
{
    public class Login
    {
        [Required(ErrorMessage = "Enter your \"Login\"")]
        public string userLogin { get; set; }
        [Range(6, 20), Required(ErrorMessage = "Password length from 6 to 20 characters.")]
        public int Password { get; set; }
    }
}
