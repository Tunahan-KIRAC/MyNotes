using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNotes.EntityLayer
{
    [Table("tblLikeds")]

    public class Liked
    {
        public int Id { get; set; } //Buraya daha fazla property yazmadık cunkü bunu nav property getirecek

      
        public virtual Note Note { get; set; }
        public virtual MyNotesUser LikedUser { get; set; } //Beğenen kişi


    }
}
