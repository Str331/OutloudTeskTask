using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outloud.Domain.Entity
{
    public class News
    {
        public int Id { get; set; }
        public int FeedId { get; set; }

        [Required(ErrorMessage ="Please, enter URL-address")]
        public string URLadress { get; set; }

        [Required(ErrorMessage = "Enter your \"Title\"")]
        public string Title { get; set; }
        [Required]
        public bool IsRead { get; set; }

        public string Description { get; set; }
        [Required(ErrorMessage ="Enter \"Date Field\"")]
        public DateTime DateofAdd { get; set; }
        public Feed Feed { get; set; }
    }
}
