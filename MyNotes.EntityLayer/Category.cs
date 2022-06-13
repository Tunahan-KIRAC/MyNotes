using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNotes.EntityLayer
{
    [Table("tblCategories")]

    public class Category : BaseEntity              //Required boş geçilemez haline getiriyor.Blok sitesi ypıyoruz notlar alıcaz onları paylaşacaz beğenip yorum yapacaklar ve bu işleri organize edicez.
    {


        [StringLength(50), Required]
        public string Tittle { get; set; }
        [StringLength(150)]
        public string Description { get; set; }

        public virtual List<Note> Notes { get; set; } = new List<Note>();

        public Category()
        {
            Notes=new List<Note>();
        }
        
    }
}
