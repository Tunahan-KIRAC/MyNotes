using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNotes.EntityLayer
{
    public class BaseEntity
    {
        public int Id { get; set; }
        [Required] 
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set;}
        public string ModifiedUserName { get; set; }

    }
}
