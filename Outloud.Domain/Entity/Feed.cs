using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outloud.Domain.Entity
{
    public class Feed
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="You need to enter \"Designation\"")]
        public string Designation { get; set; }

        [Required(ErrorMessage ="Please, enter URL-address")]
        public string URLadress { get; set; }

        public virtual ICollection<News> News { get; set; }

        public bool Active { get; set; }

        public ICollection<Sub> Subs { get; set; }


    }
}
