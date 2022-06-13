using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNotes.DataAccessLayer
{
    public class BaseRepository
    {
        private static MyNoteContext _db;
        private static object _lock = new object();

        public BaseRepository()
        {
                
        }

        public static MyNoteContext CreateContext()
        {
            if (_db==null)
            {
                lock (_lock)
                {
                    if (_db==null)
                    {
                        _db = new MyNoteContext();
                    }
                }
            }
            return _db;
        }
    }
}
