using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outloud.Domain.Entity
{
    public class Sub
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Enter this Field")]
        public int FeedId { get; set; }
        [Required(ErrorMessage ="You must enter \"User Login\"")]
        public string UserLogin { get; set; }
    }
}
