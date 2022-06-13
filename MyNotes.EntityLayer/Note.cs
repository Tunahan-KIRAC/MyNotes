using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNotes.EntityLayer
{
    [Table("tblNotes")]
    public class Note : BaseEntity
    {
        [StringLength(60), Required] //Required boş geçilmez demek.
        public string Tittle { get; set; }
        [StringLength(2000), Required]
        public string Text { get; set; }

        public Boolean isDraft { get; set; } // Yorum yazıcam ama publis edilmesini istemiyorum üstünde daha sonra güncelleme yapıcam bu yayınlansın mı yayınlanmasın mı diye soracak. Bu işi kullanıcı yapacak.

        public int LikeCount { get; set; } // Yazdığım yorumun almış oldugu beğeni sayısını takipedecek.

        public int? CategoryId { get; set; }

        public virtual MyNotesUser Owner { get; set; }  //Notelarım user la bağlantılı olacak, virtual ezme için kullanılır (Ekstra özellik katabilmek içn). MyNotesUser classına gidip Owner ekliycek.


        public virtual Category Category { get; set; }

        public virtual List<Comment> Comments { get; set; }
        public virtual List<Liked> Likes { get; set; }



        public Note()
        {
            Comments = new List<Comment>();

            Likes = new List<Liked>();
        }

        //iki tane yükleme eylemim var var(Bir nesneyi doldurma).

        //eager loading: olusturacagım nesnede alt baglamlar var ise hepsini getirir.
        //Ogrenciler ve siniflar tablom var bunlarin arasinda foreign key bağlantim var 
        //ogrenciler listesi aldıgımda eger eager loading kullanarak alıyorsam liste 
        /*
         *ad
         *soyad
         *sinif[
         *        {
         *           sinifadi
         *           kat
         *           egitmen
         *        }
         *     ]
         *
         */


        //lazy loading  : olusturulan nesne getirilir ilgili baglamlar yuklenmez.
        // virtual olarak işaretlenirse lazy loading yapilmis olur.


    }
}
