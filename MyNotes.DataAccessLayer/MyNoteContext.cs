using MyNotes.EntityLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNotes.DataAccessLayer
{
    public class MyNoteContext : DbContext // İlk iş Db setleri ayarlıycaz db set database deki işleri ayarlaması için bu tabloları db ye göre ayarlıyor.
    {
        public DbSet<MyNotesUser> MyNotesUsers { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Liked> Likes { get; set; }

        public MyNoteContext() : base("SqlConDb") //Burası Constructor
        {
            Database.SetInitializer(new MyInitializer());
        }
    }
}
