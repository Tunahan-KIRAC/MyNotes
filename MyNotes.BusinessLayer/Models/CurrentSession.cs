using MyNotes.EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MyNotes.BusinessLayer.Models
{
    public class CurrentSession
    {
        public static MyNotesUser User
        {
            get
            {
                return Get<MyNotesUser>("Login");
                //if (HttpContext.Current.Session["Login"]!=null)
                //{
                //    return HttpContext.Current.Session["Login"] as MyNotesUser;
                //}
                //return null;
            }
        }
        public static T Get<T>(string key)
        {
            if (HttpContext.Current.Session[key] != null)
            {
                return (T)HttpContext.Current.Session[key];
            }
            return default(T);
        }
        public static void Set<T>(string key, T obj)
        {
            HttpContext.Current.Session[key] = obj;
        }
        public static void Remove<T>(string key)
        {
            if (HttpContext.Current.Session[key] != null)
            {
                HttpContext.Current.Session.Remove(key);
            }
        }
        public static void Clear()
        {
            HttpContext.Current.Session.Clear();
        }

    }
}