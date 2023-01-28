using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outloud.Domain.Entity
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Enter your login")]
        public string Login { get; set; }
        [Range(6,20),Required(ErrorMessage = "Your password must be between 6 and 20 characters long.")]
        public string Password { get; set; }
        public ICollection<Sub> Subs { get; set; }
    }
}
