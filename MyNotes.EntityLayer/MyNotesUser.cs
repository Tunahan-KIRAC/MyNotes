using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNotes.EntityLayer
{


    [Table("tblMyNotesUsers")]

    public class MyNotesUser : BaseEntity
    {
        [StringLength(30)]

        public string Name { get; set; }

        [StringLength(30)]
        public string LastName { get; set; }

        [StringLength(30), Required]
        public string UserName { get; set; }
        [StringLength(100), Required]
        public string Email { get; set; }
        [StringLength(100), Required]
        public string Password { get; set; }

        public bool IsActive { get; set; }
        [Required]
        public Guid ActivateGuid { get; set; } = Guid.NewGuid();
        public bool IsDelete { get; set; } = false;

        public bool IsAdmin { get; set; }
        public string ProfileImageFileName { get; set; }

        public virtual List<Note> Notes { get; set; } //List somut bir yapı Icollection soyut bir yapı List yerine Icollection,IList verebiliriz.
        public virtual List<Comment> Comments { get; set; } //List somut bir yapı Icollection soyut bir yapı List yerine Icollection,IList verebiliriz.


        public virtual List<Liked> Likes { get; set; } 







    }
}
