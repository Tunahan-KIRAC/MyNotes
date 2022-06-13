using MyNotes.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNotes.BusinessLayer
{
    public class Test
    {
        public Test()
        {
            MyNoteContext db = new MyNoteContext();
            db.Categories.ToList();

        }
        
        
    }
}
